using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using System.Data.Entity;

namespace PPI.Core
{
    using PPI.Core.Web.Controllers;
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Entities;
    using PPI.Core.Web.Models;
    using CsvHelper;
    using PPI.Core.Web.Infrastructure;

    public class PeopleController : BaseController
    {
        [Log]
        public PeopleController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }




        [Log]
        [HttpGet]
        public ActionResult GetHoganCount(int SelectedProgramSiteEventId)
        {

            if (SelectedProgramSiteEventId != -1)
            {

                Event events = UnitOfWork.IEventRepository.AsQueryable().FirstOrDefault(m => m.Id == SelectedProgramSiteEventId);
                var programId = events.ProgramSite.ProgramId;
                var SurveyType = events.SurveyType;

                //var programId = UnitOfWork.IEventRepository.AsQueryable().FirstOrDefault(m => m.Id == SelectedProgramSiteEventId).ProgramSite.ProgramId;
                //var SurveyType = UnitOfWork.IEventRepository.AsQueryable().FirstOrDefault(m => m.Id == SelectedProgramSiteEventId).SurveyType;
                var HoganCount = UnitOfWork.IHoganUserInfoRepository.AsQueryable().Count(m => m.ProgramId == programId && m.Used == false);
                return Json(new { surveyType = SurveyType.Name.ToString(), hoganCount = HoganCount.ToString() }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { surveyType = "", hoganCount = "" }, JsonRequestBehavior.AllowGet);
        }




        [Log]
        protected PersonViewModel GetPeopleList(int? page, string search, int? eventFilter)
        {
            var model = new PersonViewModel();
            int totalRecords = 0;
            if (User.IsInRole("SiteCordinator"))
            {
                int SiteId = CurrentSite;


                var preFilter = UnitOfWork.IvPersonBySiteRepository.AsQueryable().Where(m => m.SiteId == SiteId)
                    .Select(t => t.Person);
                if (eventFilter.HasValue)
                {
                    if (eventFilter != -1)
                    {
                        preFilter = preFilter
                        .SelectMany(s => s.PersonEvents)
                        .Where(x => x.EventId == eventFilter).Select(t => t.Person);
                    }
                }
                totalRecords = preFilter.Count();
                PagingInfo pagingInfo = new PagingInfo { CurrentPage = page.GetValueOrDefault(1), PageCount = 5, TotalRecords = totalRecords };
                pagingInfo.PageSize = int.Parse(PPI.Core.Web.Infrastructure.Utility.GetCookie("pageSize"));
                pagingInfo.LastPage = totalRecords / pagingInfo.PageSize;

                if (search != null && search != "")
                    model.People = preFilter
                        .Where(p => p.Hogan_Id == search || p.LastName.ToUpper().Contains(search) || p.PrimaryEmail.ToUpper().Contains(search) || p.AAMCNumber.Contains(search) || p.FirstName.Contains(search))
                        .ToList().OrderBy(m => m.LastName)
                        .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                        .Take(pagingInfo.PageSize);
                else
                    model.People = preFilter
                        .ToList().OrderBy(m => m.LastName)
                        .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                        .Take(pagingInfo.PageSize);

                model.PagingInfo = pagingInfo;
            }
            else if (User.IsInRole("Admin") || User.IsInRole("J3PAdmin"))
            {
                var preFilter = UnitOfWork.IPersonRepository.AsQueryable();
                if (eventFilter.HasValue)
                {
                    if (eventFilter != -1)
                    {
                        preFilter = preFilter
                        .SelectMany(s => s.PersonEvents)
                        .Where(x => x.EventId == eventFilter).Select(t => t.Person);
                    }
                }

                totalRecords = preFilter.Count();
                PagingInfo pagingInfo = new PagingInfo { CurrentPage = page.GetValueOrDefault(1), PageCount = 5, TotalRecords = totalRecords };
                pagingInfo.PageSize = int.Parse(PPI.Core.Web.Infrastructure.Utility.GetCookie("pageSize"));
                pagingInfo.LastPage = totalRecords / pagingInfo.PageSize;


                if (search != null && search != "")
                    model.People = preFilter
                        .Where(p => p.Hogan_Id == search || p.LastName.ToUpper().Contains(search) || p.PrimaryEmail.ToUpper().Contains(search) || p.AAMCNumber.Contains(search) || p.FirstName.Contains(search))
                        .ToList().OrderBy(m => m.LastName)
                        .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                        .Take(pagingInfo.PageSize);
                else
                    model.People = preFilter
                        .ToList()
                        .OrderBy(m => m.LastName)
                        .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                        .Take(pagingInfo.PageSize);

                model.PagingInfo = pagingInfo;
            }

            //populate the assessment status flags
            var hoganRepo = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable();
            foreach (Person person in model.People)
            {
                Manual_Hogan_Import hoganRec = hoganRepo.FirstOrDefault(p => p.Hogan_User_ID == person.Hogan_Id && p.HPIDate != null);

                if (hoganRec != null)
                {
                    person.HPICompletedDate = (string.IsNullOrWhiteSpace(hoganRec.HPIDate) ? null : hoganRec.HPIDate);
                    person.HDSCompletedDate = (string.IsNullOrWhiteSpace(hoganRec.HDSDate) ? null : hoganRec.HDSDate);
                    person.MVPICompletedDate = (string.IsNullOrWhiteSpace(hoganRec.MVPIDate) ? null : hoganRec.MVPIDate);
                }
            }

            return model;
        }
        //
        // GET: /Persons/
        protected SelectList GenderList(string selected)
        {
            var GenderList = new List<SelectListItem>();
            GenderList.Add(new SelectListItem { Value = "Male", Text = "Male" });
            GenderList.Add(new SelectListItem { Value = "Female", Text = "Female" });
            return new SelectList(GenderList, "Value", "Text", (selected == "Male") ? "Male" : "Female");
        }

        #region Persons
        [Log]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        public ActionResult Index(int? page, string search, int? eventFilter)
        {
            if (eventFilter.HasValue)
            {
                //Set the EventSelected
                this.CurrentEvent = eventFilter.Value;
            }
            ViewBag.Events = new SelectList(this.Events, "Value", "Text", this.CurrentEvent);

            var model = GetPeopleList(page, search, this.CurrentEvent);
            if (TempData["uploadStats"] != null)
            {
                model.Stats = (List<PersonUploadStats>)TempData["uploadStats"];
            }
            model.SearchString = search;
            model.eventFilter = this.CurrentEvent;
            return View(model);
        }
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        [ActionName("Edit")]
        public ActionResult EditPersons(int id)
        {

            Person model = null;
            if (User.IsInRole("SiteCordinator"))
            {
                //make sure this user is part of this site users site
                int SiteId = CurrentSite;
                model = UnitOfWork.IvPersonBySiteRepository.AsQueryable().Where(m => m.SiteId == SiteId).Select(m => m.Person).Single(m => m.Id == id);
            }
            else if (User.IsInRole("Admin") || User.IsInRole("J3PAdmin"))
            {
                model = UnitOfWork.IPersonRepository.First(m => m.Id == id);
            }

            ViewData["Gender"] = GenderList(model.Gender);
            return View(model);
        }
        [Log]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult SavePersons(Person Person)
        {
            //Zco
            if (string.IsNullOrEmpty(Person.AAMCNumber))
            {
                Person.AAMCNumber = "U" + Person.FirstName.Substring(0, 2).ToUpper() + Person.LastName.Substring(0, 2).ToUpper() + DateTime.UtcNow.ToString("yyMMddHHmmssfff");
            }
            UnitOfWork.IPersonRepository.Update(Person);
            UnitOfWork.Commit();
            ViewData["Gender"] = GenderList(Person.Gender);
            //return View(Person);
            return RedirectToAction("Edit");
        }
        [Log]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("New")]
        public ActionResult AddPersons(PersonNewViewModel newPerson)
        {

            //Zco
            if (string.IsNullOrEmpty(newPerson.Person.AAMCNumber))
            {
                newPerson.Person.AAMCNumber = "U" + newPerson.Person.FirstName.Substring(0, 2).ToUpper() + newPerson.Person.LastName.Substring(0, 2).ToUpper() + DateTime.UtcNow.ToString("yyMMddHHmmssfff");
            }

            //Before we add check the People table first for AAMC and / or Email address match and do an update
            var personcheck = UnitOfWork.IPersonRepository.AsQueryable().FirstOrDefault(m => m.AAMCNumber == newPerson.Person.AAMCNumber || m.PrimaryEmail == newPerson.Person.PrimaryEmail);
            if (personcheck != null)
            {

                //alread exists
                personcheck.PrimaryEmail = newPerson.Person.PrimaryEmail;
                personcheck.FirstName = newPerson.Person.FirstName;
                personcheck.LastName = newPerson.Person.LastName;
                personcheck.Title = newPerson.Person.Title;
                UnitOfWork.IPersonRepository.Update(personcheck);
                newPerson.Person = personcheck;
            }
            else
            {
                //var programId = UnitOfWork.IEventRepository.Find(m => m.Id == newPerson.SelectedEventId).FirstOrDefault().ProgramSite.ProgramId;
                var SelectedEvent = UnitOfWork.IEventRepository.Find(m => m.Id == newPerson.SelectedEventId).FirstOrDefault();
                var programId = SelectedEvent.ProgramSite.ProgramId;
                var EventType = SelectedEvent.SurveyType.Name;
                if (EventType.ToUpper() == "J3P")
                {
                    Random random = new Random();
                    var j3p_ID = string.Format("J" + newPerson.Person.FirstName.Substring(0, 2).ToUpper() + newPerson.Person.LastName.Substring(0, 2).ToUpper() + "{0}", random.Next(900000));
                    newPerson.Person.J3P_Id = j3p_ID;
                }
                else
                {
                    var HoganID = UnitOfWork.IHoganUserInfoRepository.AsQueryable().Where(m => m.Used == false && m.ProgramId == programId);
                    if (newPerson.Person.Hogan_Id == null && HoganID.Count() > 0)
                    {
                        var HoganRecord = HoganID.FirstOrDefault();
                        newPerson.Person.Hogan_Id = HoganRecord.UserId;
                        newPerson.Person.Hogan_Password = HoganRecord.Password;
                        HoganRecord.Used = true;
                        UnitOfWork.IHoganUserInfoRepository.Update(HoganRecord);
                    }
                }
                UnitOfWork.IPersonRepository.Add(newPerson.Person);
            }
            if (newPerson.Person.PersonEvents.FirstOrDefault(m => m.EventId == newPerson.SelectedEventId) == null)
            {
                var PersonEvent = new PersonEvent();
                PersonEvent.PersonId = newPerson.Person.Id;
                PersonEvent.EventId = newPerson.SelectedEventId;
                UnitOfWork.IPersonEventRepository.Add(PersonEvent);
            }

            UnitOfWork.Commit();
            return RedirectToAction("New");
        }

        [Log]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        [ActionName("New")]
        public ActionResult AddPersons()
        {
            var model = new PersonNewViewModel();
            int DefaultSite = 0;
            //IEnumerable<ProgramSiteEvent> ProgramsSitesEvents = null;
            IEnumerable<Event> Events = null;
            if (User.IsInRole("SiteCordinator"))
            {
                DefaultSite = CurrentSite;
                Events = UnitOfWork.IEventRepository.AsQueryable().Where(m => m.ProgramSite.SiteId == DefaultSite && m.EventStatusId != 1).OrderBy(s => s.Name.Trim());
            }
            else if (User.IsInRole("Admin") || User.IsInRole("J3PAdmin"))
            {
                Events = UnitOfWork.IEventRepository.AsQueryable().Where(m => m.EventStatusId != 1).OrderBy(s => s.Name.Trim());
            }
            var UploadEvents = new List<SelectListItem>();

            var snewItem = new SelectListItem();
            snewItem.Text = "No Event Selected";
            //snewItem.Value = "-1";
            UploadEvents.Add(snewItem);

            foreach (var item in Events)
            {
                var newItem = new SelectListItem();
                newItem.Text = item.Name;
                newItem.Value = item.Id.ToString();
                UploadEvents.Add(newItem);
            }

            ViewData["Gender"] = GenderList("Male");
            ViewData["EventCount"] = UploadEvents.Count;

            ViewData["EventList"] = new SelectList(UploadEvents, "Value", "Text", CurrentEvent);
            if (UploadEvents.Find(m => m.Value == CurrentEvent.ToString()) != null)
                ViewData["hogancount"] = this.GetHoganCount(CurrentEvent);

            ModelState.Remove("SelectedEventId");
            return View(model);
        }
        [Log]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        [ActionName("Load")]
        public ActionResult LoadPersons()
        {
            var model = new PersonLoadViewModel();
            //IEnumerable<ProgramSiteEvent> ProgramsSitesEvents = null;
            IEnumerable<Event> Events = null;
            if (User.IsInRole("SiteCordinator"))
            {
                var DefaultSite = CurrentSite;
                Events = UnitOfWork.IEventRepository.AsQueryable().Where(m => m.ProgramSite.SiteId == DefaultSite && m.EventStatusId != 1).OrderBy(s => s.Name.Trim());
            }
            else if (User.IsInRole("Admin") || User.IsInRole("J3PAdmin"))
            {
                Events = UnitOfWork.IEventRepository.AsQueryable().Where(m => m.EventStatusId != 1).OrderBy(s => s.Name.Trim());
            }

            var UploadEvents = new List<SelectListItem>();
            var snewItem = new SelectListItem();
            snewItem.Text = "No Event Selected";
            //snewItem.Value = "-1";
            UploadEvents.Add(snewItem);
            foreach (var item in Events)
            {
                var newItem = new SelectListItem();
                newItem.Text = item.Name;
                newItem.Value = item.Id.ToString();
                UploadEvents.Add(newItem);
            }

            ViewData["EventList"] = new SelectList(UploadEvents, "Value", "Text", CurrentEvent);
            if (UploadEvents.Find(m => m.Value == CurrentEvent.ToString()) != null)
                ViewData["hogancount"] = this.GetHoganCount(CurrentEvent);

            return View(model);
        }

        [Log]
        private void CsvFatalExceptionHelper(PersonUploadStats stats, string message)
        {
            if (stats.FailedPersons != null)
            {
                stats.FailedPersons.Clear();
            }
            else
            {
                stats.FailedPersons = new List<PersonUploadRecord>();
            }
            stats.RowsFailedText = "All";
            PersonUploadRecord statRecord = new PersonUploadRecord();
            statRecord.ErrorMessage = message;
            statRecord.ImportLevel = true;
            stats.FailedPersons.Add(statRecord);
        }

        [Log]
        private void LoadHoganPersons(int SelectedEventId)
        {
            int i = 0;
            PersonViewModel model = new PersonViewModel();
            model.Stats = new List<PersonUploadStats>();
            PersonUploadStats stats;
            bool isNewPerson;
            bool stopProcessing = false;

            var r = new List<UploadFileModel>();
            string savedFileName = "";
            foreach (string key in Request.Files)
            {
                if (stopProcessing)
                {
                    //if fatal error occurs while processing previous csv file, don't process next one
                    break;
                }

                stats = new PersonUploadStats();

                HttpPostedFileBase hpf = Request.Files[i] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                string name_without_extention = Path.GetFileNameWithoutExtension(hpf.FileName);
                string get_extetion = Path.GetExtension(hpf.FileName);
                string file_name = name_without_extention + DateTime.Now.ToString("yyyyMMddHHmmssfff") + get_extetion;
                savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PPI.Core.Web.Properties.Settings.Default.UploadFilesFolderName, file_name);
                hpf.SaveAs(savedFileName);
                stats.FileName = hpf.FileName;

                FileInfo fileInfo = new FileInfo(savedFileName);
                if (fileInfo.Exists)
                {
                    //added additional pass to first validate the # cols in header row.
                    //this is important because if the header has incorrect # cols, it
                    //throws off some of the subsequence exception handling that checks
                    //for variations in data row col counts.
                    using (var parser = new CsvHelper.CsvParser(new StreamReader(savedFileName)))
                    {
                        string[] header = parser.Read();

                        if (header.GetLength(0) != PPI.Core.Web.Properties.Settings.Default.PersonUploadCols)
                        {
                            stats.RowsFailedText = "All";
                            PersonUploadRecord statRecord = new PersonUploadRecord();
                            statRecord.ErrorMessage = string.Format("{0}: Incorrect number of columns in CSV header row.  Should be {1} but is {2}", stats.FileName, PPI.Core.Web.Properties.Settings.Default.PersonUploadCols, header.GetLength(0));
                            statRecord.ImportLevel = true;
                            if (stats.FailedPersons == null)
                            {
                                stats.FailedPersons = new List<PersonUploadRecord>();
                            }
                            stats.FailedPersons.Add(statRecord);
                            stopProcessing = true;
                        }
                        else
                        {
                            //might as well do a quick check for empty header col e.g. Col1,,Col2
                            //and also check for " Col1" vs "Col1" in header since this is not handled gracefully.

                            foreach (string colName in header)
                            {
                                if (string.IsNullOrWhiteSpace(colName) || colName != colName.Trim())
                                {
                                    stats.RowsFailedText = "All";
                                    PersonUploadRecord statRecord = new PersonUploadRecord();
                                    statRecord.ErrorMessage = string.Format("{0}: One or more column names is either empty or contains leading/trailing spaces.", stats.FileName);
                                    statRecord.ImportLevel = true;
                                    if (stats.FailedPersons == null)
                                    {
                                        stats.FailedPersons = new List<PersonUploadRecord>();
                                    }
                                    stats.FailedPersons.Add(statRecord);
                                    stopProcessing = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (!stopProcessing)
                    {
                        using (var reader = new CsvHelper.CsvReader(new StreamReader(savedFileName)))
                        {
                            reader.Configuration.RegisterClassMap<PPI.Core.Web.Infrastructure.ImportMaps.People_Map>();
                            reader.Configuration.IgnoreReadingExceptions = true;    //so callbacks work
                            //reader.Configuration.ReadingExceptionCallback = CsvHelperCallback;

                            reader.Configuration.ReadingExceptionCallback = (ex, rdr) =>
                            {
                                ExceptionSvcHelper.HandleNonFatalException(ex);

                                if (ex is CsvTextTooLongException
                                    || ex is CsvInvalidEmailException
                                    || ex is CsvRequiredColEmptyException)
                                {
                                    //called per row
                                    if (!stopProcessing)
                                    {
                                        stats.RowsFailed++;
                                        PersonUploadRecord statRecord = new PersonUploadRecord();
                                        statRecord.RawRow = rdr.Parser.RawRecord;
                                        statRecord.ErrorMessage = string.Format("{0}: {1}", stats.FileName, ex.Message);
                                        statRecord.UploadRowNumber = ((CsvValidationException)ex).RowNumber;
                                        statRecord.ImportLevel = false;
                                        if (stats.FailedPersons == null)
                                        {
                                            stats.FailedPersons = new List<PersonUploadRecord>();
                                        }
                                        stats.FailedPersons.Add(statRecord);
                                    }
                                }
                                else if (ex is CsvMissingFieldException || ex is CsvReaderException)
                                {
                                    //called per import, thrown by csv helper                                    
                                    //I'm using the personuploadrecord model member
                                    //to display an import-level exception
                                    if (!stopProcessing)
                                    {
                                        CsvFatalExceptionHelper(stats, string.Format("{0}: {1}", stats.FileName, ex.Message));
                                        stopProcessing = true;
                                    }
                                }
                                else    //unknown csvhelper or other error
                                {
                                    if (!stopProcessing)
                                    {
                                        CsvFatalExceptionHelper(stats, string.Format("{0}: {1}", stats.FileName, ex.Message));
                                        stopProcessing = true;
                                    }
                                }

                                Trace.TraceInformation("Row Exception {0}: Row Data {1}", ex.Message, rdr.Parser.RawRecord);
                            };

                            reader.Configuration.DetectColumnCountChanges = true;
                            reader.Configuration.TrimFields = true;

                            //currently, these are correct as default values, but set explicitly anyway
                            reader.Configuration.HasHeaderRecord = true;            //default is true but set anyway
                            reader.Configuration.IgnoreBlankLines = true;           //default is true but set anyway
                            reader.Configuration.WillThrowOnMissingField = true;    //default is true but set anyway                                                       

                            IEnumerable<Person> records = new List<Person>();

                            try
                            {
                                records = reader.GetRecords<PPI.Core.Domain.Entities.Person>().ToList();
                            }
                            catch (CsvBadDataException ex)
                            {
                                //called during call to GetRecords if DetectColumnChanges is true 
                                //and a row has different number of cols than header row
                                CsvFatalExceptionHelper(stats, string.Format("{0}: {1}, {2}", stats.FileName, ex.Message, ex.Data["CsvHelper"]));
                                ExceptionSvcHelper.HandleNonFatalException(ex);
                            }
                            catch (CsvReaderException ex)
                            {
                                //more general csvreader failure
                                CsvFatalExceptionHelper(stats, string.Format("{0}: {1}", stats.FileName, ex.Message));
                                ExceptionSvcHelper.HandleNonFatalException(ex);
                            }
                            catch (Exception ex)
                            {
                                //unknowwn failure
                                CsvFatalExceptionHelper(stats, string.Format("{0}: {1}", stats.FileName, ex.Message));
                                ExceptionSvcHelper.HandleNonFatalException(ex);
                            }

                            foreach (var item in records)
                            {
                                //Zco
                                if (string.IsNullOrEmpty(item.AAMCNumber))
                                {
                                    item.AAMCNumber = "U" + item.FirstName.Substring(0, 2).ToUpper() + item.LastName.Substring(0, 2).ToUpper() + DateTime.UtcNow.ToString("yyMMddHHmmssfff");
                                }

                                var personcheck = UnitOfWork.IPersonRepository.AsQueryable().FirstOrDefault(m => m.AAMCNumber == item.AAMCNumber || m.PrimaryEmail == item.PrimaryEmail);
                                var PersonEvent = new PersonEvent();

                                if (personcheck != null)
                                {
                                    //already exists - match on AAMCNumber or email address
                                    personcheck.PrimaryEmail = item.PrimaryEmail;
                                    personcheck.FirstName = item.FirstName;
                                    personcheck.LastName = item.LastName;
                                    personcheck.Title = item.Title;
                                    UnitOfWork.IPersonRepository.Update(personcheck);
                                    if (personcheck.PersonEvents.FirstOrDefault(m => m.EventId == SelectedEventId) == null)
                                    {
                                        PersonEvent.PersonId = personcheck.Id;
                                        PersonEvent.EventId = SelectedEventId;
                                        UnitOfWork.IPersonEventRepository.Add(PersonEvent);
                                    }

                                    //stats processing
                                    isNewPerson = false;
                                }
                                else    //no match on AAMCNumber or email address 
                                {
                                    UnitOfWork.IPersonRepository.Add(item);
                                    var ProgramSiteEvent = UnitOfWork.IEventRepository.AsQueryable().FirstOrDefault(m => m.Id == SelectedEventId);

                                    PersonEvent = new PersonEvent();
                                    PersonEvent.PersonId = item.Id;
                                    // go get one from the Availible Pool
                                    var HoganID = UnitOfWork.IHoganUserInfoRepository.AsQueryable().Where(m => m.Used == false && m.ProgramId == ProgramSiteEvent.ProgramSite.ProgramId);
                                    if (item.Hogan_Id == "" && HoganID.Count() > 0)
                                    {
                                        var HoganRecord = HoganID.FirstOrDefault();
                                        item.Hogan_Id = HoganRecord.UserId;
                                        item.Hogan_Password = HoganRecord.Password;
                                        HoganRecord.Used = true;
                                        UnitOfWork.IHoganUserInfoRepository.Update(HoganRecord);
                                    }
                                    PersonEvent.EventId = SelectedEventId;
                                    UnitOfWork.IPersonEventRepository.Add(PersonEvent);

                                    //stats processing 
                                    isNewPerson = true;
                                }

                                UnitOfWork.Commit();

                                //commit was successful, add person to stats
                                if (!isNewPerson)
                                {
                                    stats.RowsUpdated++;
                                    if (stats.UpdatedPersons == null)
                                    {
                                        stats.UpdatedPersons = new List<PersonUploadRecord>();
                                    }
                                    PersonUploadRecord statRecord = new PersonUploadRecord();
                                    statRecord.ThePerson = item;     //would like to save RawRow but not sure how to get it here
                                    stats.UpdatedPersons.Add(statRecord);
                                }
                                else
                                {
                                    stats.RowsAdded++;
                                    if (stats.AddedPersons == null)
                                    {
                                        stats.AddedPersons = new List<PersonUploadRecord>();
                                    }
                                    PersonUploadRecord statRecord = new PersonUploadRecord();
                                    statRecord.ThePerson = item;     //would like to save RawRow but not sure how to get it here
                                    stats.AddedPersons.Add(statRecord);
                                }
                            }
                        }
                    }
                }

                i++;

                model.Stats.Add(stats);
            }

            TempData["uploadStats"] = model.Stats;
        }
        [Log]
        private void LoadJ3PPersons(int SelectedEventId)
        {
            int i = 0;
            PersonViewModel model = new PersonViewModel();
            model.Stats = new List<PersonUploadStats>();
            PersonUploadStats stats;
            bool isNewPerson;
            bool stopProcessing = false;

            var r = new List<UploadFileModel>();
            string savedFileName = "";
            foreach (string key in Request.Files)
            {
                if (stopProcessing)
                {
                    //if fatal error occurs while processing previous csv file, don't process next one
                    break;
                }

                stats = new PersonUploadStats();

                HttpPostedFileBase hpf = Request.Files[i] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                string name_without_extention = Path.GetFileNameWithoutExtension(hpf.FileName);
                string get_extetion = Path.GetExtension(hpf.FileName);
                string file_name = name_without_extention + DateTime.Now.ToString("yyyyMMddHHmmssfff") + get_extetion;
                savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PPI.Core.Web.Properties.Settings.Default.UploadFilesFolderName, file_name);
                hpf.SaveAs(savedFileName);
                stats.FileName = hpf.FileName;

                FileInfo fileInfo = new FileInfo(savedFileName);
                if (fileInfo.Exists)
                {
                    //added additional pass to first validate the # cols in header row.
                    //this is important because if the header has incorrect # cols, it
                    //throws off some of the subsequence exception handling that checks
                    //for variations in data row col counts.
                    using (var parser = new CsvHelper.CsvParser(new StreamReader(savedFileName)))
                    {
                        string[] header = parser.Read();

                        if (header.GetLength(0) != PPI.Core.Web.Properties.Settings.Default.PersonUploadCols)
                        {
                            stats.RowsFailedText = "All";
                            PersonUploadRecord statRecord = new PersonUploadRecord();
                            statRecord.ErrorMessage = string.Format("{0}: Incorrect number of columns in CSV header row.  Should be {1} but is {2}", stats.FileName, PPI.Core.Web.Properties.Settings.Default.PersonUploadCols, header.GetLength(0));
                            statRecord.ImportLevel = true;
                            if (stats.FailedPersons == null)
                            {
                                stats.FailedPersons = new List<PersonUploadRecord>();
                            }
                            stats.FailedPersons.Add(statRecord);
                            stopProcessing = true;
                        }
                        else
                        {
                            //might as well do a quick check for empty header col e.g. Col1,,Col2
                            //and also check for " Col1" vs "Col1" in header since this is not handled gracefully.

                            foreach (string colName in header)
                            {
                                if (string.IsNullOrWhiteSpace(colName) || colName != colName.Trim())
                                {
                                    stats.RowsFailedText = "All";
                                    PersonUploadRecord statRecord = new PersonUploadRecord();
                                    statRecord.ErrorMessage = string.Format("{0}: One or more column names is either empty or contains leading/trailing spaces.", stats.FileName);
                                    statRecord.ImportLevel = true;
                                    if (stats.FailedPersons == null)
                                    {
                                        stats.FailedPersons = new List<PersonUploadRecord>();
                                    }
                                    stats.FailedPersons.Add(statRecord);
                                    stopProcessing = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (!stopProcessing)
                    {
                        using (var reader = new CsvHelper.CsvReader(new StreamReader(savedFileName)))
                        {
                            reader.Configuration.RegisterClassMap<PPI.Core.Web.Infrastructure.ImportMaps.J3PPeople_Map>();
                            reader.Configuration.IgnoreReadingExceptions = true;    //so callbacks work
                            //reader.Configuration.ReadingExceptionCallback = CsvHelperCallback;

                            reader.Configuration.ReadingExceptionCallback = (ex, rdr) =>
                            {
                                ExceptionSvcHelper.HandleNonFatalException(ex);

                                if (ex is CsvTextTooLongException
                                    || ex is CsvInvalidEmailException
                                    || ex is CsvRequiredColEmptyException)
                                {
                                    //called per row
                                    if (!stopProcessing)
                                    {
                                        stats.RowsFailed++;
                                        PersonUploadRecord statRecord = new PersonUploadRecord();
                                        statRecord.RawRow = rdr.Parser.RawRecord;
                                        statRecord.ErrorMessage = string.Format("{0}: {1}", stats.FileName, ex.Message);
                                        statRecord.UploadRowNumber = ((CsvValidationException)ex).RowNumber;
                                        statRecord.ImportLevel = false;
                                        if (stats.FailedPersons == null)
                                        {
                                            stats.FailedPersons = new List<PersonUploadRecord>();
                                        }
                                        stats.FailedPersons.Add(statRecord);
                                    }
                                }
                                else if (ex is CsvMissingFieldException || ex is CsvReaderException)
                                {
                                    //called per import, thrown by csv helper                                    
                                    //I'm using the personuploadrecord model member
                                    //to display an import-level exception
                                    if (!stopProcessing)
                                    {
                                        CsvFatalExceptionHelper(stats, string.Format("{0}: {1}", stats.FileName, ex.Message));
                                        stopProcessing = true;
                                    }
                                }
                                else    //unknown csvhelper or other error
                                {
                                    if (!stopProcessing)
                                    {
                                        CsvFatalExceptionHelper(stats, string.Format("{0}: {1}", stats.FileName, ex.Message));
                                        stopProcessing = true;
                                    }
                                }

                                Trace.TraceInformation("Row Exception {0}: Row Data {1}", ex.Message, rdr.Parser.RawRecord);
                            };

                            reader.Configuration.DetectColumnCountChanges = true;
                            reader.Configuration.TrimFields = true;

                            //currently, these are correct as default values, but set explicitly anyway
                            reader.Configuration.HasHeaderRecord = true;            //default is true but set anyway
                            reader.Configuration.IgnoreBlankLines = true;           //default is true but set anyway
                            reader.Configuration.WillThrowOnMissingField = true;    //default is true but set anyway                                                       

                            IEnumerable<Person> records = new List<Person>();

                            try
                            {
                                records = reader.GetRecords<PPI.Core.Domain.Entities.Person>().ToList();
                            }
                            catch (CsvBadDataException ex)
                            {
                                //called during call to GetRecords if DetectColumnChanges is true 
                                //and a row has different number of cols than header row
                                CsvFatalExceptionHelper(stats, string.Format("{0}: {1}, {2}", stats.FileName, ex.Message, ex.Data["CsvHelper"]));
                                ExceptionSvcHelper.HandleNonFatalException(ex);
                            }
                            catch (CsvReaderException ex)
                            {
                                //more general csvreader failure
                                CsvFatalExceptionHelper(stats, string.Format("{0}: {1}", stats.FileName, ex.Message));
                                ExceptionSvcHelper.HandleNonFatalException(ex);
                            }
                            catch (Exception ex)
                            {
                                //unknowwn failure
                                CsvFatalExceptionHelper(stats, string.Format("{0}: {1}", stats.FileName, ex.Message));
                                ExceptionSvcHelper.HandleNonFatalException(ex);
                            }

                            foreach (var item in records)
                            {
                                //Zco
                                if (string.IsNullOrEmpty(item.AAMCNumber))
                                {
                                    item.AAMCNumber = "U" + item.FirstName.Substring(0, 2).ToUpper() + item.LastName.Substring(0, 2).ToUpper() + DateTime.UtcNow.ToString("yyMMddHHmmssfff");
                                }

                                var personcheck = UnitOfWork.IPersonRepository.AsQueryable().FirstOrDefault(m => m.AAMCNumber == item.AAMCNumber || m.PrimaryEmail == item.PrimaryEmail);
                                var PersonEvent = new PersonEvent();

                                if (personcheck != null)
                                {
                                    //already exists - match on AAMCNumber or email address
                                    personcheck.PrimaryEmail = item.PrimaryEmail;
                                    personcheck.FirstName = item.FirstName;
                                    personcheck.LastName = item.LastName;
                                    personcheck.Title = item.Title;
                                    UnitOfWork.IPersonRepository.Update(personcheck);
                                    if (personcheck.PersonEvents.FirstOrDefault(m => m.EventId == SelectedEventId) == null)
                                    {
                                        PersonEvent.PersonId = personcheck.Id;
                                        PersonEvent.EventId = SelectedEventId;
                                        UnitOfWork.IPersonEventRepository.Add(PersonEvent);
                                    }

                                    //stats processing
                                    isNewPerson = false;
                                }
                                else    //no match on AAMCNumber or email address 
                                {
                                    UnitOfWork.IPersonRepository.Add(item);
                                    var ProgramSiteEvent = UnitOfWork.IEventRepository.AsQueryable().FirstOrDefault(m => m.Id == SelectedEventId);

                                    PersonEvent = new PersonEvent();
                                    PersonEvent.PersonId = item.Id;
                                    // go get one from the Availible Pool
                                    var HoganID = UnitOfWork.IHoganUserInfoRepository.AsQueryable().Where(m => m.Used == false && m.ProgramId == ProgramSiteEvent.ProgramSite.ProgramId);
                                    if (item.Hogan_Id == "" && HoganID.Count() > 0)
                                    {
                                        var HoganRecord = HoganID.FirstOrDefault();
                                        item.Hogan_Id = HoganRecord.UserId;
                                        item.Hogan_Password = HoganRecord.Password;
                                        HoganRecord.Used = true;
                                        UnitOfWork.IHoganUserInfoRepository.Update(HoganRecord);
                                    }
                                    PersonEvent.EventId = SelectedEventId;
                                    UnitOfWork.IPersonEventRepository.Add(PersonEvent);

                                    //stats processing 
                                    isNewPerson = true;
                                }

                                UnitOfWork.Commit();

                                //commit was successful, add person to stats
                                if (!isNewPerson)
                                {
                                    stats.RowsUpdated++;
                                    if (stats.UpdatedPersons == null)
                                    {
                                        stats.UpdatedPersons = new List<PersonUploadRecord>();
                                    }
                                    PersonUploadRecord statRecord = new PersonUploadRecord();
                                    statRecord.ThePerson = item;     //would like to save RawRow but not sure how to get it here
                                    stats.UpdatedPersons.Add(statRecord);
                                }
                                else
                                {
                                    stats.RowsAdded++;
                                    if (stats.AddedPersons == null)
                                    {
                                        stats.AddedPersons = new List<PersonUploadRecord>();
                                    }
                                    PersonUploadRecord statRecord = new PersonUploadRecord();
                                    statRecord.ThePerson = item;     //would like to save RawRow but not sure how to get it here
                                    stats.AddedPersons.Add(statRecord);
                                }
                            }
                        }
                    }
                }

                i++;

                model.Stats.Add(stats);
            }

            TempData["uploadStats"] = model.Stats;
        }

        [Log]
        [ActionName("ExecuteLoad")]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        public ActionResult ExecuteLoadPersons(int SelectedEventId)
        {

            Event events = UnitOfWork.IEventRepository.AsQueryable().FirstOrDefault(m => m.Id == SelectedEventId);

            if (events.SurveyType.Name.ToString() == "Hogan")
            {
                LoadHoganPersons(SelectedEventId);
            }
            else
            {

                //J3PPeople_Map
                LoadJ3PPersons(SelectedEventId);
            }

            return RedirectToAction("Index");
        }



        [Log]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        [ActionName("Delete")]
        public ActionResult DeletePerson(int id, int? page)
        {
            var model = new PersonViewModel();
            if (User.IsInRole("Admin") || User.IsInRole("J3PAdmin"))
            {
                UnitOfWork.IPersonRepository.Delete(UnitOfWork.IPersonRepository.AsQueryable().Single(m => m.Id == id));
                UnitOfWork.Commit();
            }
            else if (User.IsInRole("SiteCordinator"))
            {
                int SiteId = CurrentSite;
                var EventsToRemove = UnitOfWork.IPersonEventRepository.AsQueryable()
                    .Where(m => m.PersonId == id)
                    .Where(m => m.Event.ProgramSite.SiteId == SiteId);
                UnitOfWork.IPersonEventRepository.Delete(EventsToRemove);
                UnitOfWork.Commit();
            }
            return RedirectToAction("Index", new { page = page });
        }
        [Log]
        [Authorize(Roles = "Admin,J3PAdmin,SiteCordinator")]
        public ActionResult Profile(int id)
        {

            var model = new PersonProfileViewModel();

            model.Person = UnitOfWork.IPersonRepository.AsQueryable().Single(m => m.Id == id);
            //model.ReportDataAvailible = PPI.Core.Web.Infrastructure.Utility.ReportDataAvailible(model.Person.Hogan_Id, UnitOfWork);
            model.FullName = model.Person.FirstName + " " + model.Person.LastName;
            //var HoganData = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable().FirstOrDefault(m => m.Hogan_User_ID == model.Person.Hogan_Id);
           // model.Hogan_Id = model.Person.Hogan_Id;

            Manual_Hogan_Import HoganData = null;
            AssessmentResponse J3pData = null;
            if (model.Person.J3P_Id != null) {
                J3pData = UnitOfWork.IAssessmentResponse.AsQueryable().FirstOrDefault(m => m.PersonId == id);
            }
            else
            {
                model.ReportDataAvailible = PPI.Core.Web.Infrastructure.Utility.ReportDataAvailible(model.Person.Hogan_Id, UnitOfWork);
                HoganData = UnitOfWork.IManual_Hogan_ImportRepository.AsQueryable().FirstOrDefault(m => m.Hogan_User_ID == model.Person.Hogan_Id);
                model.Hogan_Id = model.Person.Hogan_Id;
            }
            //NEED RESX for all these

            if (model.Person.J3P_Id != null)
            {
                if (J3pData != null)
                {
                    model.J3PProcess = "Completed";
                    model.AsOfDate = J3pData.CreateDate.ToShortDateString();
                }
                else
                {
                    model.J3PProcess = "Not Started";
                    model.AsOfDate = DateTime.Now.ToShortDateString();
                }
            }
            else
            {
                if (HoganData == null)
                {
                    model.HDS = "Not Started";
                    model.HPI = "Not Started";
                    model.MVPI = "Not Started";
                    model.HoganProcess = "Not Started";
                    model.AsOfDate = DateTime.Now.ToShortDateString();
                }
                else
                {
                    model.HPI = (HoganData.HPIDate != null && HoganData.HPIDate != "") ? "Completed" : "InProgress";
                    model.HDS = (HoganData.HDSDate != null && HoganData.HDSDate != "") ? "Completed" : "InProgress";
                    model.MVPI = (HoganData.MVPIDate != null && HoganData.MVPIDate != "") ? "Completed" : "InProgress";
                    model.HoganProcess = (model.HPI == "Completed" && model.HDS == "Completed" && model.MVPI == "Completed") ? "Completed" : "In Process";
                    model.AsOfDate = HoganData.LastUpdated.ToShortDateString();
                }
            }
            var AssociatedEvents = UnitOfWork.IPersonEventRepository.AsQueryable().Where(m => m.PersonId == id).Select(m => m.Event);

            model.ProgramEvents = new List<ProgramEventModel>();
            IQueryable<PersonEmail> PersonEmails = null;
            if (User.IsInRole("Admin") || User.IsInRole("J3PAdmin"))
            {
                PersonEmails = UnitOfWork.IPersonEmailRepository.AsQueryable().Where(m => m.PersonId == id).OrderByDescending(m => m.SentDate);
                var programs = AssociatedEvents.Select(m => m.ProgramSite.Program).Distinct();
                foreach (var item in programs)
                {
                    var ProgramEvent = new ProgramEventModel();
                    ProgramEvent.ProgramName = item.ProgramName;
                    ProgramEvent.ProgramId = item.Id;
                    ProgramEvent.Events = AssociatedEvents.Where(m => m.ProgramSite.ProgramId == item.Id);
                    model.ProgramEvents.Add(ProgramEvent);
                }
            }
            else if (User.IsInRole("SiteCordinator"))
            {
                PersonEmails = UnitOfWork.IPersonEmailRepository.AsQueryable().Where(m => m.PersonId == id && m.Email.Event.ProgramSite.SiteId == CurrentSite).OrderByDescending(m => m.SentDate);
                int SiteId = CurrentSite;
                var programs = AssociatedEvents.Where(m => m.ProgramSite.SiteId == SiteId).Select(m => m.ProgramSite.Program).Distinct();

                foreach (var item in programs)
                {
                    var ProgramEvent = new ProgramEventModel();
                    ProgramEvent.ProgramName = item.ProgramName;
                    ProgramEvent.Events = AssociatedEvents.Where(m => m.ProgramSite.ProgramId == item.Id && m.ProgramSite.SiteId == SiteId);
                    model.ProgramEvents.Add(ProgramEvent);
                }
            }

            //email history

            List<PersonEmail> lstPersonEmails = new List<PersonEmail>();

            foreach (var item in PersonEmails)
            {
                lstPersonEmails.Add(item);
            }

            model.PersonEmails = lstPersonEmails;

            return View(model);
        }
        #endregion
        [Log]
        [Authorize(Roles = "Admin,SiteCordinator,J3PAdmin")]
        public ActionResult Export(int? page, string search, int? eventFilter)
        {
            string exportName = "peopleExport.csv";
            CsvWriter writer;
            var streamoutput = new MemoryStream();
            var sw = new StreamWriter(streamoutput);
            var PeopleList = GetPeopleList(page, search, eventFilter);
            if (PeopleList.People.Count() > 0)
            {
                var CsvConfig = new CsvHelper.Configuration.CsvConfiguration();
                CsvConfig.RegisterClassMap<PPI.Core.Web.Infrastructure.ImportMaps.PersonExport_Map>();
                writer = new CsvWriter(sw, CsvConfig);
                IEnumerable<Person> records = PeopleList.People.ToList();
                writer.WriteRecords(records);
                sw.Flush();
            }
            streamoutput.Seek(0, SeekOrigin.Begin);
            return File(streamoutput, "text/csv", exportName);
        }
    }
}
