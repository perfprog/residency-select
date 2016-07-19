using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using PPI.Core.Domain.Abstract;
using PPI.Core.Domain.Concrete;
using PPI.Core.Domain.Entities;
using EntityFramework.MappingAPI.Extensions;


namespace PPI.Core.Web.Infrastructure
{
    public static class ReflectionUtility
    {
        public static string PropertyName<T>(Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }
            return body.Member.Name;
        }
    }

    public class ImportMaps
    {
        #region CSV Maps

        [Log]
        public sealed class Zco_Hogan_Export_Map : CsvClassMap<PPI.Core.Domain.Entities.Manual_Hogan_Import>
        {
            public override void CreateMap()
            {
                string colName;

                //REVISIT
                //Any need to globalization / conversion on export?  seems not
                //e.g. Map(m => m.RPower_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);

                //pull sessioned data - selected hogan cols and universe of all hogan cols
                Dictionary<string, ZCOExportMap> selectedCols =
                    (Dictionary<string, ZCOExportMap>)HttpContext.Current.Session["zcoHoganExportSelectedCols"];
                Dictionary<string, ZCOExportMap> allCols =
                    (Dictionary<string, ZCOExportMap>)HttpContext.Current.Session["zcoHoganExportAllCols"];


                //REVISIT: Can the "per hogan field" code below be automated?  
                //had trouble with iterating over PropertyInfo collection.
                //System.Reflection.PropertyInfo[] myPropertyInfo;                
                //myPropertyInfo = typeof(PPI.Core.Domain.Entities.Manual_Hogan_Import).GetProperties();
                //for (int i = 0; i < myPropertyInfo.Length; i++)
                //{
                //    string sPropName = myPropertyInfo[i].Name;
                //    Map(m => m.GetType().GetProperty(sPropName).SetMethod).Ignore(!selectedCols.ContainsKey(sPropName));                                       
                //    //string s = myPropertyInfo[i].Name;
                //}                

                //proceed with csv mapping

                //.Ignore: decides whether to include the column or not
                //.Index: sets position in csv.  if position in db is null then specify Int.maxvalue to push it to the right
                //.Name: set the column name in the destination csv

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.ClientID);
                Map(m => m.ClientID)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.ClientName);
                Map(m => m.ClientName)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.GroupName);
                Map(m => m.GroupName)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Hogan_User_ID);
                Map(m => m.Hogan_User_ID)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.First_Name);
                Map(m => m.First_Name)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Last_Name);
                Map(m => m.Last_Name)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Gender);
                Map(m => m.Gender)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.HPIDate);
                Map(m => m.HPIDate)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Valid);
                Map(m => m.Valid)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Empathy);
                Map(m => m.Empathy)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.NotAnxious);
                Map(m => m.NotAnxious)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.NoGuilt);
                Map(m => m.NoGuilt)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Calmness);
                Map(m => m.Calmness)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.EvenTempered);
                Map(m => m.EvenTempered)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.NoSomaticComplaint);
                Map(m => m.NoSomaticComplaint)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Trusting);
                Map(m => m.Trusting)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.GoodAttachment);
                Map(m => m.GoodAttachment)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Competitive);
                Map(m => m.Competitive)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.SelfConfidence);
                Map(m => m.SelfConfidence)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.NoDepression);
                Map(m => m.NoDepression)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Leadership);
                Map(m => m.Leadership)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Identity);
                Map(m => m.Identity)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.NoSocialAnxiety);
                Map(m => m.NoSocialAnxiety)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.LikesParties);
                Map(m => m.LikesParties)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.LikesCrowds);
                Map(m => m.LikesCrowds)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.ExperienceSeeking);
                Map(m => m.ExperienceSeeking)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Exhibitionistic);
                Map(m => m.Exhibitionistic)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Entertaining);
                Map(m => m.Entertaining)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.EasyToLiveWith);
                Map(m => m.EasyToLiveWith)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Sensitive);
                Map(m => m.Sensitive)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Caring);
                Map(m => m.Caring)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.LikesPeople);
                Map(m => m.LikesPeople)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.NoHostility);
                Map(m => m.NoHostility)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Moralistic);
                Map(m => m.Moralistic)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Mastery);
                Map(m => m.Mastery)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Virtuous);
                Map(m => m.Virtuous)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.NotAutonomous);
                Map(m => m.NotAutonomous)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.NotSpontaneous);
                Map(m => m.NotSpontaneous)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.ImpulseControl);
                Map(m => m.ImpulseControl)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.AvoidsTrouble);
                Map(m => m.AvoidsTrouble)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.ScienceAbility);
                Map(m => m.ScienceAbility)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Curiosity);
                Map(m => m.Curiosity)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.ThrillSeeking);
                Map(m => m.ThrillSeeking)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.IntellectualGames);
                Map(m => m.IntellectualGames)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.GeneratesIdeas);
                Map(m => m.GeneratesIdeas)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Culture);
                Map(m => m.Culture)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Education);
                Map(m => m.Education)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.MathAbility);
                Map(m => m.MathAbility)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.GoodMemory);
                Map(m => m.GoodMemory)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.Reading);
                Map(m => m.Reading)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RValidity);
                Map(m => m.RValidity)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAdjustment);
                Map(m => m.RAdjustment)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAmbition);
                Map(m => m.RAmbition)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RSociability);
                Map(m => m.RSociability)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RInterpersonalSensitivity);
                Map(m => m.RInterpersonalSensitivity)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RPrudence);
                Map(m => m.RPrudence)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RInquisitive);
                Map(m => m.RInquisitive)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RLearningApproach);
                Map(m => m.RLearningApproach)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RServiceOrientation);
                Map(m => m.RServiceOrientation)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RStressTolerance);
                Map(m => m.RStressTolerance)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RReliability);
                Map(m => m.RReliability)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RClericalPotential);
                Map(m => m.RClericalPotential)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RSalesPotential);
                Map(m => m.RSalesPotential)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RManagerialPotential);
                Map(m => m.RManagerialPotential)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PValidity);
                Map(m => m.PValidity)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PAdjustment);
                Map(m => m.PAdjustment)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PAmbition);
                Map(m => m.PAmbition)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PSociability);
                Map(m => m.PSociability)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PInterpersonalSensitivity);
                Map(m => m.PInterpersonalSensitivity)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PPrudence);
                Map(m => m.PPrudence)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PInquisitive);
                Map(m => m.PInquisitive)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PLearningApproach);
                Map(m => m.PLearningApproach)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PServiceOrientation);
                Map(m => m.PServiceOrientation)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PStressTolerance);
                Map(m => m.PStressTolerance)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PReliability);
                Map(m => m.PReliability)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PClericalPotential);
                Map(m => m.PClericalPotential)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PSalesPotential);
                Map(m => m.PSalesPotential)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PManagerialPotential);
                Map(m => m.PManagerialPotential)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.HDSDate);
                Map(m => m.HDSDate)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RExcitable);
                Map(m => m.RExcitable)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RSkeptical);
                Map(m => m.RSkeptical)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RCautious);
                Map(m => m.RCautious)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RReserved);
                Map(m => m.RReserved)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RLeisurely);
                Map(m => m.RLeisurely)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RBold);
                Map(m => m.RBold)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RMischievous);
                Map(m => m.RMischievous)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RColorful);
                Map(m => m.RColorful)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RImaginative);
                Map(m => m.RImaginative)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RDiligent);
                Map(m => m.RDiligent)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RDutiful);
                Map(m => m.RDutiful)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RUnlikeness);
                Map(m => m.RUnlikeness)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PExcitable);
                Map(m => m.PExcitable)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PSkeptical);
                Map(m => m.PSkeptical)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PCautious);
                Map(m => m.PCautious)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PReserved);
                Map(m => m.PReserved)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PLeisurely);
                Map(m => m.PLeisurely)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PBold);
                Map(m => m.PBold)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PMischievous);
                Map(m => m.PMischievous)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PColorful);
                Map(m => m.PColorful)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PImaginative);
                Map(m => m.PImaginative)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PDiligent);
                Map(m => m.PDiligent)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PDutiful);
                Map(m => m.PDutiful)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PUnlikeness);
                Map(m => m.PUnlikeness)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.MVPIDate);
                Map(m => m.MVPIDate)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAesthetic_Lifestyles);
                Map(m => m.RAesthetic_Lifestyles)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAesthetic_Beliefs);
                Map(m => m.RAesthetic_Beliefs)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAesthetic_Occupational_Prferences);
                Map(m => m.RAesthetic_Occupational_Prferences)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAesthetic_Aversions);
                Map(m => m.RAesthetic_Aversions)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAesthetic_Preferred_Associates);
                Map(m => m.RAesthetic_Preferred_Associates)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAffiliation_Lifestyles);
                Map(m => m.RAffiliation_Lifestyles)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAffiliation_Beliefs);
                Map(m => m.RAffiliation_Beliefs)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAffiliation_Occupational_Prferences);
                Map(m => m.RAffiliation_Occupational_Prferences)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAffiliation_Aversions);
                Map(m => m.RAffiliation_Aversions)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAffiliation_Preferred_Associates);
                Map(m => m.RAffiliation_Preferred_Associates)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAltruistic_Lifestyles);
                Map(m => m.RAltruistic_Lifestyles)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAltruistic_Beliefs);
                Map(m => m.RAltruistic_Beliefs)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAltruistic_Occupational_Prferences);
                Map(m => m.RAltruistic_Occupational_Prferences)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAltruistic_Aversions);
                Map(m => m.RAltruistic_Aversions)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAltruistic_Preferred_Associates);
                Map(m => m.RAltruistic_Preferred_Associates)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RCommercial_Lifestyles);
                Map(m => m.RCommercial_Lifestyles)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RCommercial_Beliefs);
                Map(m => m.RCommercial_Beliefs)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RCommercial_Occupational_Prferences);
                Map(m => m.RCommercial_Occupational_Prferences)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RCommercial_Aversions);
                Map(m => m.RCommercial_Aversions)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RCommercial_Preferred_Associates);
                Map(m => m.RCommercial_Preferred_Associates)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RHedonistic_Lifestyles);
                Map(m => m.RHedonistic_Lifestyles)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RHedonistic_Beliefs);
                Map(m => m.RHedonistic_Beliefs)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RHedonistic_Occupational_Prferences);
                Map(m => m.RHedonistic_Occupational_Prferences)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RHedonistic_Aversions);
                Map(m => m.RHedonistic_Aversions)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RHedonistic_Preferred_Associates);
                Map(m => m.RHedonistic_Preferred_Associates)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RPower_Lifestyles);
                Map(m => m.RPower_Lifestyles)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RPower_Beliefs);
                Map(m => m.RPower_Beliefs)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RPower_Occupational_Prferences);
                Map(m => m.RPower_Occupational_Prferences)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RPower_Aversions);
                Map(m => m.RPower_Aversions)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RPower_Preferred_Associates);
                Map(m => m.RPower_Preferred_Associates)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RRecognition_Lifestyles);
                Map(m => m.RRecognition_Lifestyles)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RRecognition_Beliefs);
                Map(m => m.RRecognition_Beliefs)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RRecognition_Occupational_Prferences);
                Map(m => m.RRecognition_Occupational_Prferences)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RRecognition_Aversions);
                Map(m => m.RRecognition_Aversions)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RRecognition_Preferred_Associates);
                Map(m => m.RRecognition_Preferred_Associates)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RScientific_Lifestyles);
                Map(m => m.RScientific_Lifestyles)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RScientific_Beliefs);
                Map(m => m.RScientific_Beliefs)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RScientific_Occupational_Prferences);
                Map(m => m.RScientific_Occupational_Prferences)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RScientific_Aversions);
                Map(m => m.RScientific_Aversions)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RScientific_Preferred_Associates);
                Map(m => m.RScientific_Preferred_Associates)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RSecurity_Lifestyles);
                Map(m => m.RSecurity_Lifestyles)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RSecurity_Beliefs);
                Map(m => m.RSecurity_Beliefs)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RSecurity_Occupational_Prferences);
                Map(m => m.RSecurity_Occupational_Prferences)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RSecurity_Aversions);
                Map(m => m.RSecurity_Aversions)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RSecurity_Preferred_Associates);
                Map(m => m.RSecurity_Preferred_Associates)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RTradition_Lifestyles);
                Map(m => m.RTradition_Lifestyles)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RTradition_Beliefs);
                Map(m => m.RTradition_Beliefs)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RTradition_Occupational_Prferences);
                Map(m => m.RTradition_Occupational_Prferences)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RTradition_Aversions);
                Map(m => m.RTradition_Aversions)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RTradition_Preferred_Associates);
                Map(m => m.RTradition_Preferred_Associates)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAesthetic);
                Map(m => m.RAesthetic)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAffiliation);
                Map(m => m.RAffiliation)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RAltruistic);
                Map(m => m.RAltruistic)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RCommercial);
                Map(m => m.RCommercial)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RHedonistic);
                Map(m => m.RHedonistic)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RPower);
                Map(m => m.RPower)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RRecognition);
                Map(m => m.RRecognition)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RScientific);
                Map(m => m.RScientific)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RSecurity);
                Map(m => m.RSecurity)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.RTradition);
                Map(m => m.RTradition)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PAesthetic);
                Map(m => m.PAesthetic)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PAffiliation);
                Map(m => m.PAffiliation)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PAltruistic);
                Map(m => m.PAltruistic)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PCommercial);
                Map(m => m.PCommercial)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PHedonistic);
                Map(m => m.PHedonistic)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PPower);
                Map(m => m.PPower)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PRecognition);
                Map(m => m.PRecognition)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PScientific);
                Map(m => m.PScientific)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PSecurity);
                Map(m => m.PSecurity)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.PTradition);
                Map(m => m.PTradition)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);

                colName = ReflectionUtility.PropertyName<Manual_Hogan_Import>(t => t.LastUpdated);
                Map(m => m.LastUpdated)
                .Ignore(!selectedCols.ContainsKey(colName))
                .Index(allCols[colName].Position ?? Int16.MaxValue)
                .Name(allCols[colName].ZCOColName);
                //}
            }
        }

        [Log]
        public sealed class HoganUser_Map : CsvClassMap<HoganUserInfo>
        {
            [Obsolete]
            public override void CreateMap()
            {
                Map(m => m.UserId).Index(0);
                Map(m => m.Password).Index(1);
                Map(m => m.GroupName).Index(2);
            }
        }

        public sealed class People_Map : CsvClassMap<Person>
        {
            [Log]
            private bool GetDbColProps(string dbColName, ref bool isRequired, ref int maxLength)
            {
                try
                {
                    using (ResidencySelect_Entities ctx = new ResidencySelect_Entities())
                    {
                        var map = ctx.Db<PPI.Core.Domain.Entities.Person>();
                        //var tableName = map.TableName;
                        if (dbColName != "AAMCNumber")
                        {
                            isRequired = map.Properties
                                .Where(x => x.ColumnName == dbColName)
                                .Single().IsRequired;
                        }
                        else
                        {
                            isRequired = false;
                        }
                        //.ColumnName;
                        //.Where(x => x.PropertyName == "Title")

                        maxLength = map.Properties
                            .Where(x => x.ColumnName == dbColName)
                            .Single().MaxLength;
                    }

                    return (true);
                }
                catch
                {
                    return (false);     //handle in the caller                    
                }
            }

            [Log]
            public override void CreateMap()
            {
                bool isRequired = true;

                int maxLength = 0;
                string dbColName;
                string csvColName;
                string colVal;

                //Csv Row Number
                Map(m => m.UploadRowNumber).ConvertUsing(row =>
                {
                    return (row.Row);
                });

                //First Name
                Map(m => m.FirstName).Index(0).ConvertUsing(row =>
                {
                    dbColName = csvColName = "FirstName";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Last Name
                Map(m => m.LastName).Index(1).ConvertUsing(row =>
                {
                    dbColName = csvColName = "LastName";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Email
                Map(m => m.PrimaryEmail).Index(2).ConvertUsing(row =>
                {
                    dbColName = csvColName = "PrimaryEmail";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }
                    else if (!EmailService.isValidEmailAlt(colVal))
                    {
                        throw new CsvInvalidEmailException(csvColName, row.Row);
                    }

                    return (colVal);
                });

                //Hogan ID
                Map(m => m.Hogan_Id).Index(3).ConvertUsing(row =>
                {
                    dbColName = "Hogan_Id";
                    csvColName = "HoganId";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //AAMC Number
                Map(m => m.AAMCNumber).Index(4).ConvertUsing(row =>
                {
                    dbColName = csvColName = "AAMCNumber";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Gender
                Map(m => m.Gender).Index(5).ConvertUsing(row =>
                {
                    dbColName = csvColName = "Gender";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Title
                Map(m => m.Title).Index(6).ConvertUsing(row =>
                {
                    dbColName = csvColName = "Title";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(row.GetField<string>(csvColName)))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                ////J3P_Id
                //Map(m => m.J3P_Id).Index(7).ConvertUsing(row =>
                //{
                //    dbColName = csvColName = "J3P_Id";
                //    colVal = row.GetField<string>(csvColName);

                //    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                //    {
                //        throw new CsvMetadataException(csvColName, row.Row);
                //    }

                //    if (string.IsNullOrEmpty(row.GetField<string>(csvColName)))
                //    {
                //        if (isRequired)
                //        {
                //            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                //        }
                //    }
                //    else if (colVal.Length > maxLength)
                //    {
                //        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                //    }

                //    return (colVal);
                //});
            }
        }

        public sealed class J3PPeople_Map : CsvClassMap<Person>
        {
            [Log]
            private bool GetDbColProps(string dbColName, ref bool isRequired, ref int maxLength)
            {
                try
                {
                    using (ResidencySelect_Entities ctx = new ResidencySelect_Entities())
                    {
                        var map = ctx.Db<PPI.Core.Domain.Entities.Person>();
                        //var tableName = map.TableName;
                        if (dbColName != "AAMCNumber")
                        {
                            isRequired = map.Properties
                                .Where(x => x.ColumnName == dbColName)
                                .Single().IsRequired;
                        }
                        else
                        {
                            isRequired = false;
                        }
                        //.ColumnName;
                        //.Where(x => x.PropertyName == "Title")

                        maxLength = map.Properties
                            .Where(x => x.ColumnName == dbColName)
                            .Single().MaxLength;
                    }

                    return (true);
                }
                catch
                {
                    return (false);     //handle in the caller                    
                }
            }

            [Log]
            public override void CreateMap()
            {
                bool isRequired = true;

                int maxLength = 0;
                string dbColName;
                string csvColName;
                string colVal;

                //Csv Row Number
                Map(m => m.UploadRowNumber).ConvertUsing(row =>
                {
                    return (row.Row);
                });

                //First Name
                Map(m => m.FirstName).Index(0).ConvertUsing(row =>
                {
                    dbColName = csvColName = "FirstName";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Last Name
                Map(m => m.LastName).Index(1).ConvertUsing(row =>
                {
                    dbColName = csvColName = "LastName";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Email
                Map(m => m.PrimaryEmail).Index(2).ConvertUsing(row =>
                {
                    dbColName = csvColName = "PrimaryEmail";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }
                    else if (!EmailService.isValidEmailAlt(colVal))
                    {
                        throw new CsvInvalidEmailException(csvColName, row.Row);
                    }

                    return (colVal);
                });

                //J3P_Id
                Map(m => m.J3P_Id).Index(3).ConvertUsing(row =>
                {
                    dbColName = csvColName = "J3P_Id";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(row.GetField<string>(csvColName)))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //AAMC Number
                Map(m => m.AAMCNumber).Index(4).ConvertUsing(row =>
                {
                    dbColName = csvColName = "AAMCNumber";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Gender
                Map(m => m.Gender).Index(5).ConvertUsing(row =>
                {
                    dbColName = csvColName = "Gender";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Title
                Map(m => m.Title).Index(6).ConvertUsing(row =>
                {
                    dbColName = csvColName = "Title";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(row.GetField<string>(csvColName)))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //J3P_Id
                Map(m => m.J3P_Id).Index(7).ConvertUsing(row =>
                {
                    dbColName = csvColName = "J3P_Id";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(row.GetField<string>(csvColName)))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

              
            }
        }

        public sealed class PersonExport_Map : CsvClassMap<Person>
        {
            [Log]
            private bool GetDbColProps(string dbColName, ref bool isRequired, ref int maxLength)
            {
                try
                {
                    using (ResidencySelect_Entities ctx = new ResidencySelect_Entities())
                    {
                        var map = ctx.Db<PPI.Core.Domain.Entities.Person>();
                        //var tableName = map.TableName;
                        if (dbColName != "AAMCNumber")
                        {
                            isRequired = map.Properties
                                .Where(x => x.ColumnName == dbColName)
                                .Single().IsRequired;
                        }
                        else
                        {
                            isRequired = false;
                        }
                        //.ColumnName;
                        //.Where(x => x.PropertyName == "Title")

                        maxLength = map.Properties
                            .Where(x => x.ColumnName == dbColName)
                            .Single().MaxLength;
                    }

                    return (true);
                }
                catch
                {
                    return (false);     //handle in the caller                    
                }
            }

            [Log]
            public override void CreateMap()
            {
                bool isRequired = true;

                int maxLength = 0;
                string dbColName;
                string csvColName;
                string colVal;

                //Csv Row Number
                Map(m => m.UploadRowNumber).ConvertUsing(row =>
                {
                    return (row.Row);
                });

                //First Name
                Map(m => m.FirstName).Index(0).ConvertUsing(row =>
                {
                    dbColName = csvColName = "FirstName";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Last Name
                Map(m => m.LastName).Index(1).ConvertUsing(row =>
                {
                    dbColName = csvColName = "LastName";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Email
                Map(m => m.PrimaryEmail).Index(2).ConvertUsing(row =>
                {
                    dbColName = csvColName = "PrimaryEmail";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }
                    else if (!EmailService.isValidEmailAlt(colVal))
                    {
                        throw new CsvInvalidEmailException(csvColName, row.Row);
                    }

                    return (colVal);
                });

                //J3P_Id
                Map(m => m.J3P_Id).Index(3).ConvertUsing(row =>
                {
                    dbColName = csvColName = "J3P_Id";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(row.GetField<string>(csvColName)))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //AAMC Number
                Map(m => m.AAMCNumber).Index(4).ConvertUsing(row =>
                {
                    dbColName = csvColName = "AAMCNumber";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Gender
                Map(m => m.Gender).Index(5).ConvertUsing(row =>
                {
                    dbColName = csvColName = "Gender";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(colVal))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Title
                Map(m => m.Title).Index(6).ConvertUsing(row =>
                {
                    dbColName = csvColName = "Title";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(row.GetField<string>(csvColName)))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //J3P_Id
                Map(m => m.J3P_Id).Index(7).ConvertUsing(row =>
                {
                    dbColName = csvColName = "J3P_Id";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (string.IsNullOrEmpty(row.GetField<string>(csvColName)))
                    {
                        if (isRequired)
                        {
                            throw new CsvRequiredColEmptyException(csvColName, row.Row);
                        }
                    }
                    else if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });

                //Hogan ID

                Map(m => m.Hogan_Id).Index(8).ConvertUsing(row =>
                {
                    dbColName = "Hogan_Id";
                    csvColName = "HoganId";
                    colVal = row.GetField<string>(csvColName);

                    if (!GetDbColProps(dbColName, ref isRequired, ref maxLength))
                    {
                        //throw new CsvMetadataException(csvColName, row.Row);
                    }

                    if (colVal.Length > maxLength)
                    {
                        throw new CsvTextTooLongException(csvColName, row.Row, maxLength);
                    }

                    return (colVal);
                });
            }
        }

        [Log]
        public sealed class Hogan_File_Map : CsvClassMap<PPI.Core.Domain.Entities.Manual_Hogan_Import>
        {
            public override void CreateMap()
            {

                Map(m => m.ClientID);
                Map(m => m.ClientName);
                Map(m => m.Hogan_User_ID).Index(3);
                Map(m => m.First_Name).Index(4);
                Map(m => m.Last_Name).Index(5);
                Map(m => m.Gender);
                Map(m => m.HPIDate);
                Map(m => m.HDSDate);
                Map(m => m.MVPIDate);
                Map(m => m.Valid).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Empathy).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.NotAnxious).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.NoGuilt).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Calmness).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.EvenTempered).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.NoSomaticComplaint).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Trusting).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.GoodAttachment).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Competitive).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.SelfConfidence).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.NoDepression).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Leadership).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Identity).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.NoSocialAnxiety).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.LikesParties).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.LikesCrowds).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.ExperienceSeeking).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Exhibitionistic).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Entertaining).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.EasyToLiveWith).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Sensitive).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Caring).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.LikesPeople).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.NoHostility).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Moralistic).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Mastery).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Virtuous).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.NotAutonomous).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.NotSpontaneous).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.ImpulseControl).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.AvoidsTrouble).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.ScienceAbility).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Curiosity).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.ThrillSeeking).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.IntellectualGames).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.GeneratesIdeas).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Culture).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Education).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.MathAbility).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.GoodMemory).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.Reading).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RValidity).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAdjustment).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAmbition).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RSociability).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RInterpersonalSensitivity).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RPrudence).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RInquisitive).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RLearningApproach).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RServiceOrientation).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RStressTolerance).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RReliability).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RClericalPotential).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RSalesPotential).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RManagerialPotential).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PValidity).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PAdjustment).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PAmbition).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PSociability).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PInterpersonalSensitivity).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PPrudence).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PInquisitive).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PLearningApproach).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PServiceOrientation).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PStressTolerance).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PReliability).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PClericalPotential).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PSalesPotential).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PManagerialPotential).TypeConverterOption(System.Globalization.NumberStyles.Integer);

                Map(m => m.RExcitable).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RSkeptical).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RCautious).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RReserved).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RLeisurely).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RBold).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RMischievous).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RColorful).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RImaginative).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RDiligent).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RDutiful).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RUnlikeness).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PExcitable).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PSkeptical).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PCautious).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PReserved).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PLeisurely).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PBold).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PMischievous).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PColorful).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PImaginative).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PDiligent).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PDutiful).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PUnlikeness).TypeConverterOption(System.Globalization.NumberStyles.Integer);

                Map(m => m.RAesthetic_Lifestyles).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAesthetic_Beliefs).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAesthetic_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAesthetic_Aversions).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAesthetic_Preferred_Associates).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAffiliation_Lifestyles).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAffiliation_Beliefs).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAffiliation_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAffiliation_Aversions).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAffiliation_Preferred_Associates).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAltruistic_Lifestyles).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAltruistic_Beliefs).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAltruistic_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAltruistic_Aversions).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAltruistic_Preferred_Associates).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RCommercial_Lifestyles).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RCommercial_Beliefs).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RCommercial_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RCommercial_Aversions).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RCommercial_Preferred_Associates).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RHedonistic_Lifestyles).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RHedonistic_Beliefs).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RHedonistic_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RHedonistic_Aversions).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RHedonistic_Preferred_Associates).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RPower_Lifestyles).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RPower_Beliefs).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RPower_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RPower_Aversions).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RPower_Preferred_Associates).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RRecognition_Lifestyles).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RRecognition_Beliefs).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RRecognition_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RRecognition_Aversions).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RRecognition_Preferred_Associates).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RScientific_Lifestyles).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RScientific_Beliefs).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RScientific_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RScientific_Aversions).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RScientific_Preferred_Associates).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RSecurity_Lifestyles).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RSecurity_Beliefs).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RSecurity_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RSecurity_Aversions).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RSecurity_Preferred_Associates).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RTradition_Lifestyles).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RTradition_Beliefs).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RTradition_Occupational_Prferences).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RTradition_Aversions).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RTradition_Preferred_Associates).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAesthetic).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAffiliation).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RAltruistic).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RCommercial).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RHedonistic).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RPower).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RRecognition).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RScientific).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RSecurity).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.RTradition).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PAesthetic).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PAffiliation).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PAltruistic).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PCommercial).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PHedonistic).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PPower).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PRecognition).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PScientific).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PSecurity).TypeConverterOption(System.Globalization.NumberStyles.Integer);
                Map(m => m.PTradition).TypeConverterOption(System.Globalization.NumberStyles.Integer);
            }
        }

        #endregion
    }
}