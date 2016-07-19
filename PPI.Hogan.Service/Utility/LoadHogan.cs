using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;
using PPI.Core.Domain.Entities;
using PPI.Core;

namespace PPI.Hogan.Service.Utility
{
    public class Util
    {
        protected System.Collections.Generic.Queue<string> FilesToProcess = new Queue<string>();
        
        public bool LoadData(string filePath)
        {
            try
            {
                Console.WriteLine("Processing File: {0}", filePath);

                var UnitOfWork = new PPI.Core.Domain.Concrete.EfUnitOfWork();
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {                    
                    using (var reader = new CsvHelper.CsvReader(fileInfo.OpenText()))
                    {
                        reader.Configuration.RegisterClassMap<PPI.Hogan.Service.Utility.Hogan_File_Map>();
                        reader.Configuration.IgnoreReadingExceptions = true;
                        reader.Configuration.ReadingExceptionCallback = (exception, row) =>
                        {
                            Trace.TraceInformation("Row Exception {0}: Row Data {1}", exception.Message, row.Parser.RawRecord);
                        };
                        var records = reader.GetRecords<PPI.Core.Domain.Entities.Manual_Hogan_Import>();

                        //Should be an Add / Update base on Hogin ID
                        foreach (var item in records)
                        {
                            var Hogan = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable().FirstOrDefault(m => m.Hogan_User_ID == item.Hogan_User_ID);
                            if (Hogan == null)
                            {
                                item.LastUpdated = DateTime.Now;
                                UnitOfWork.IManual_Hogan_ImportRepository.Add(item);
                            }
                            else
                            {
                                item.LastUpdated = DateTime.Now;
                                UnitOfWork.IManual_Hogan_ImportRepository.Update(Update(Hogan, item));
                            }
                            UnitOfWork.Commit();

                            //leverage scheduled process, check if person has completed all three surveys,
                            //if yes, send notification email
                            if (!string.IsNullOrWhiteSpace(item.HPIDate) && !string.IsNullOrWhiteSpace(item.MVPIDate) && !string.IsNullOrWhiteSpace(item.HDSDate))
                            {
                                //only send notification if one has not been sent yet
                                var recipPersons = UnitOfWork.IPersonRepository.AsQueryable()
                                    .Where(p => p.Hogan_Id == item.Hogan_User_ID)                                    
                                    .Where(p => p.PersonEmails.Count(pe => pe.Email.EmailTypeId == 3) > 0);  //just in case                                

                                if (recipPersons.Count() > 0)
                                {
                                    var recipPerson = recipPersons
                                        .Where(p => p.PersonEmails.Count(pe => pe.Email.EmailTypeId == 7) < 1)
                                        .FirstOrDefault();

                                    if (recipPerson != null)
                                    {
                                        using (var webClient = new WebClient())
                                        {
                                            NameValueCollection scheduleParams = new NameValueCollection();
                                            scheduleParams.Add("hoganId", recipPerson.Hogan_Id);
                                            scheduleParams.Add("userName", Properties.Settings.Default.EmailsControllerUser);
                                            scheduleParams.Add("password", Properties.Settings.Default.EmailsControllerPwd);

                                            try
                                            {
                                                byte[] responseBytes = webClient.UploadValues(Properties.Settings.Default.AssessmentsCompletedUri, "POST", scheduleParams);
                                                string responseText = Encoding.UTF8.GetString(responseBytes);
                                                Console.WriteLine("Response: {0}", responseText);

                                                if (responseText != string.Empty)
                                                {
                                                    ExceptionSvcHelper.HandleNonFatalException(new ApplicationException(responseText));
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ExceptionSvcHelper.HandleNonFatalException(ex);
                                            }
                                        }
                                    }
                                } 
                                else
                                {
                                    ExceptionSvcHelper.HandleNonFatalException(new ApplicationException(string.Format("Could not locate person record or invitation email record for Hogan Id {0}", item.Hogan_User_ID)));
                                }
                            }                            
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionSvcHelper.HandleNonFatalException(ex);
                return false;
            }
            
        }
        [Log]
        public void fileWatcher_Error(object sender, ErrorEventArgs e)
        {
           
        }
        [Log]
        public void fileWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            Trace.TraceInformation(e.Name);
            Trace.TraceInformation(e.ChangeType.ToString());
            Trace.TraceInformation(e.FullPath);
            FilesToProcess.Enqueue(e.FullPath);
            if (LoadData(FilesToProcess.Peek()))
                FilesToProcess.Dequeue();
            else
            { 
                //pause for 10 seconds and try again
                System.Threading.Thread.Sleep(10000);
                if (LoadData(FilesToProcess.Peek()))
                    FilesToProcess.Dequeue();
                else
                {
                    Trace.TraceError("file will not process - " + FilesToProcess.Dequeue());
                }
            }
        }
        [Log]
        public void StartWatching(FileSystemWatcher fileWatcher)
        {
            fileWatcher = new System.IO.FileSystemWatcher();
            fileWatcher.Path = Properties.Settings.Default.FileMonitorPath;
            fileWatcher.Filter = Properties.Settings.Default.Filter;
            fileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                         | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fileWatcher.EnableRaisingEvents = true;
            fileWatcher.Created += fileWatcher_Created;
            //fileWatcher.Changed += fileWatcher_Created;
            //fileWatcher.Deleted += fileWatcher_Created;
            //fileWatcher.Renamed += fileWatcher_Created;
            fileWatcher.Error += fileWatcher_Error;
            Trace.TraceInformation(Properties.Settings.Default.FileMonitorPath);
            Trace.TraceInformation(Properties.Settings.Default.Filter);
        
        }
        public static Manual_Hogan_Import Update(Manual_Hogan_Import orginal, Manual_Hogan_Import newData)
        {
            orginal.LastUpdated = DateTime.Now;
            orginal.ClientName = newData.ClientName;
            orginal.GroupName = newData.GroupName;
            orginal.First_Name = newData.First_Name;
            orginal.Last_Name = newData.Last_Name;
            orginal.Gender = newData.Gender;
            orginal.HPIDate = newData.HPIDate;
            orginal.Valid = newData.Valid;
            orginal.Empathy = newData.Empathy;
            orginal.NotAnxious = newData.NotAnxious;
            orginal.NoGuilt = newData.NoGuilt;
            orginal.Calmness = newData.Calmness;
            orginal.EvenTempered = newData.EvenTempered;
            orginal.NoSomaticComplaint = newData.NoSomaticComplaint;
            orginal.Trusting = newData.Trusting;
            orginal.GoodAttachment = newData.GoodAttachment;
            orginal.Competitive = newData.Competitive;
            orginal.SelfConfidence = newData.SelfConfidence;
            orginal.NoDepression = newData.NoDepression;
            orginal.Leadership = newData.Leadership;
            orginal.Identity = newData.Identity;
            orginal.NoSocialAnxiety = newData.NoSocialAnxiety;
            orginal.LikesParties = newData.LikesParties;
            orginal.LikesCrowds = newData.LikesCrowds;
            orginal.ExperienceSeeking = newData.ExperienceSeeking;
            orginal.Exhibitionistic = newData.Exhibitionistic;
            orginal.Entertaining = newData.Entertaining;
            orginal.EasyToLiveWith = newData.EasyToLiveWith;
            orginal.Sensitive = newData.Sensitive;
            orginal.Caring = newData.Caring;
            orginal.LikesPeople = newData.LikesPeople;
            orginal.NoHostility = newData.NoHostility;
            orginal.Moralistic = newData.Moralistic;
            orginal.Mastery = newData.Mastery;
            orginal.Virtuous = newData.Virtuous;
            orginal.NotAutonomous = newData.NotAutonomous;
            orginal.NotSpontaneous = newData.NotSpontaneous;
            orginal.ImpulseControl = newData.ImpulseControl;
            orginal.AvoidsTrouble = newData.AvoidsTrouble;
            orginal.ScienceAbility = newData.ScienceAbility;
            orginal.Curiosity = newData.Curiosity;
            orginal.ThrillSeeking = newData.ThrillSeeking;
            orginal.IntellectualGames = newData.IntellectualGames;
            orginal.GeneratesIdeas = newData.GeneratesIdeas;
            orginal.Culture = newData.Culture;
            orginal.Education = newData.Education;
            orginal.MathAbility = newData.MathAbility;
            orginal.GoodMemory = newData.GoodMemory;
            orginal.Reading = newData.Reading;
            orginal.RValidity = newData.RValidity;
            orginal.RAdjustment = newData.RAdjustment;
            orginal.RAmbition = newData.RAmbition;
            orginal.RSociability = newData.RSociability;
            orginal.RInterpersonalSensitivity = newData.RInterpersonalSensitivity;
            orginal.RPrudence = newData.RPrudence;
            orginal.RInquisitive = newData.RInquisitive;
            orginal.RLearningApproach = newData.RLearningApproach;
            orginal.RServiceOrientation = newData.RServiceOrientation;
            orginal.RStressTolerance = newData.RStressTolerance;
            orginal.RReliability = newData.RReliability;
            orginal.RClericalPotential = newData.RClericalPotential;
            orginal.RSalesPotential = newData.RSalesPotential;
            orginal.RManagerialPotential = newData.RManagerialPotential;
            orginal.PValidity = newData.PValidity;
            orginal.PAdjustment = newData.PAdjustment;
            orginal.PAmbition = newData.PAmbition;
            orginal.PSociability = newData.PSociability;
            orginal.PInterpersonalSensitivity = newData.PInterpersonalSensitivity;
            orginal.PPrudence = newData.PPrudence;
            orginal.PInquisitive = newData.PInquisitive;
            orginal.PLearningApproach = newData.PLearningApproach;
            orginal.PServiceOrientation = newData.PServiceOrientation;
            orginal.PStressTolerance = newData.PStressTolerance;
            orginal.PReliability = newData.PReliability;
            orginal.PClericalPotential = newData.PClericalPotential;
            orginal.PSalesPotential = newData.PSalesPotential;
            orginal.PManagerialPotential = newData.PManagerialPotential;
            orginal.HDSDate = newData.HDSDate;
            orginal.RExcitable = newData.RExcitable;
            orginal.RSkeptical = newData.RSkeptical;
            orginal.RCautious = newData.RCautious;
            orginal.RReserved = newData.RReserved;
            orginal.RLeisurely = newData.RLeisurely;
            orginal.RBold = newData.RBold;
            orginal.RMischievous = newData.RMischievous;
            orginal.RColorful = newData.RColorful;
            orginal.RImaginative = newData.RImaginative;
            orginal.RDiligent = newData.RDiligent;
            orginal.RDutiful = newData.RDutiful;
            orginal.RUnlikeness = newData.RUnlikeness;
            orginal.PExcitable = newData.PExcitable;
            orginal.PSkeptical = newData.PSkeptical;
            orginal.PCautious = newData.PCautious;
            orginal.PReserved = newData.PReserved;
            orginal.PLeisurely = newData.PLeisurely;
            orginal.PBold = newData.PBold;
            orginal.PMischievous = newData.PMischievous;
            orginal.PColorful = newData.PColorful;
            orginal.PImaginative = newData.PImaginative;
            orginal.PDiligent = newData.PDiligent;
            orginal.PDutiful = newData.PDutiful;
            orginal.PUnlikeness = newData.PUnlikeness;
            orginal.MVPIDate = newData.MVPIDate;
            orginal.RAesthetic_Lifestyles = newData.RAesthetic_Lifestyles;
            orginal.RAesthetic_Beliefs = newData.RAesthetic_Beliefs;
            orginal.RAesthetic_Occupational_Prferences = newData.RAesthetic_Occupational_Prferences;
            orginal.RAesthetic_Aversions = newData.RAesthetic_Aversions;
            orginal.RAesthetic_Preferred_Associates = newData.RAesthetic_Preferred_Associates;
            orginal.RAffiliation_Lifestyles = newData.RAffiliation_Lifestyles;
            orginal.RAffiliation_Beliefs = newData.RAffiliation_Beliefs;
            orginal.RAffiliation_Occupational_Prferences = newData.RAffiliation_Occupational_Prferences;
            orginal.RAffiliation_Aversions = newData.RAffiliation_Aversions;
            orginal.RAffiliation_Preferred_Associates = newData.RAffiliation_Preferred_Associates;
            orginal.RAltruistic_Lifestyles = newData.RAltruistic_Lifestyles;
            orginal.RAltruistic_Beliefs = newData.RAltruistic_Beliefs;
            orginal.RAltruistic_Occupational_Prferences = newData.RAltruistic_Occupational_Prferences;
            orginal.RAltruistic_Aversions = newData.RAltruistic_Aversions;
            orginal.RAltruistic_Preferred_Associates = newData.RAltruistic_Preferred_Associates;
            orginal.RCommercial_Lifestyles = newData.RCommercial_Lifestyles;
            orginal.RCommercial_Beliefs = newData.RCommercial_Beliefs;
            orginal.RCommercial_Occupational_Prferences = newData.RCommercial_Occupational_Prferences;
            orginal.RCommercial_Aversions = newData.RCommercial_Aversions;
            orginal.RCommercial_Preferred_Associates = newData.RCommercial_Preferred_Associates;
            orginal.RHedonistic_Lifestyles = newData.RHedonistic_Lifestyles;
            orginal.RHedonistic_Beliefs = newData.RHedonistic_Beliefs;
            orginal.RHedonistic_Occupational_Prferences = newData.RHedonistic_Occupational_Prferences;
            orginal.RHedonistic_Aversions = newData.RHedonistic_Aversions;
            orginal.RHedonistic_Preferred_Associates = newData.RHedonistic_Preferred_Associates;
            orginal.RPower_Lifestyles = newData.RPower_Lifestyles;
            orginal.RPower_Beliefs = newData.RPower_Beliefs;
            orginal.RPower_Occupational_Prferences = newData.RPower_Occupational_Prferences;
            orginal.RPower_Aversions = newData.RPower_Aversions;
            orginal.RPower_Preferred_Associates = newData.RPower_Preferred_Associates;
            orginal.RRecognition_Lifestyles = newData.RRecognition_Lifestyles;
            orginal.RRecognition_Beliefs = newData.RRecognition_Beliefs;
            orginal.RRecognition_Occupational_Prferences = newData.RRecognition_Occupational_Prferences;
            orginal.RRecognition_Aversions = newData.RRecognition_Aversions;
            orginal.RRecognition_Preferred_Associates = newData.RRecognition_Preferred_Associates;
            orginal.RScientific_Lifestyles = newData.RScientific_Lifestyles;
            orginal.RScientific_Beliefs = newData.RScientific_Beliefs;
            orginal.RScientific_Occupational_Prferences = newData.RScientific_Occupational_Prferences;
            orginal.RScientific_Aversions = newData.RScientific_Aversions;
            orginal.RScientific_Preferred_Associates = newData.RScientific_Preferred_Associates;
            orginal.RSecurity_Lifestyles = newData.RSecurity_Lifestyles;
            orginal.RSecurity_Beliefs = newData.RSecurity_Beliefs;
            orginal.RSecurity_Occupational_Prferences = newData.RSecurity_Occupational_Prferences;
            orginal.RSecurity_Aversions = newData.RSecurity_Aversions;
            orginal.RSecurity_Preferred_Associates = newData.RSecurity_Preferred_Associates;
            orginal.RTradition_Lifestyles = newData.RTradition_Lifestyles;
            orginal.RTradition_Beliefs = newData.RTradition_Beliefs;
            orginal.RTradition_Occupational_Prferences = newData.RTradition_Occupational_Prferences;
            orginal.RTradition_Aversions = newData.RTradition_Aversions;
            orginal.RTradition_Preferred_Associates = newData.RTradition_Preferred_Associates;
            orginal.RAesthetic = newData.RAesthetic;
            orginal.RAffiliation = newData.RAffiliation;
            orginal.RAltruistic = newData.RAltruistic;
            orginal.RCommercial = newData.RCommercial;
            orginal.RHedonistic = newData.RHedonistic;
            orginal.RPower = newData.RPower;
            orginal.RRecognition = newData.RRecognition;
            orginal.RScientific = newData.RScientific;
            orginal.RSecurity = newData.RSecurity;
            orginal.RTradition = newData.RTradition;
            orginal.PAesthetic = newData.PAesthetic;
            orginal.PAffiliation = newData.PAffiliation;
            orginal.PAltruistic = newData.PAltruistic;
            orginal.PCommercial = newData.PCommercial;
            orginal.PHedonistic = newData.PHedonistic;
            orginal.PPower = newData.PPower;
            orginal.PRecognition = newData.PRecognition;
            orginal.PScientific = newData.PScientific;
            orginal.PSecurity = newData.PSecurity;
            orginal.PTradition = newData.PTradition;            
            return orginal;
        }       
    }
}
