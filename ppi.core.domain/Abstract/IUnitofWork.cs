using System;
using PPI.Core.Domain.Entities;

namespace PPI.Core.Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ZCOUserMap> IZCOUserMapRepository { get; }        
        IGenericRepository<ZCOExportTemplate> IZCOExportTemplateRepository { get; }        
        IGenericRepository<ZCOExportTemplateMap> IZCOExportTemplateMapRepository { get; }        
        IGenericRepository<ZCOExportMap> IZCOExportMapRepository { get; }        
        IGenericRepository<ScheduledEmailPerson> IScheduledEmailPersonRepository { get; }        
        IGenericRepository<ScheduledEmail> IScheduledEmailRepository { get; }
        IGenericRepository<OrderStatus> IOrderStatusRepository { get; }
        IGenericRepository<OrderFormPracticeReport> IOrderFormPracticeReportRepository { get; }
        IGenericRepository<OrderForm> IOrderFormRepository { get; }
        IGenericRepository<EventPracticeReport> IEventPracticeReportRepository { get; }
        IGenericRepository<PersonEvent> IPersonEventRepository { get; }
        IGenericRepository<PersonPracticeReport> IPersonPracticeReportRepository { get; }
        IGenericRepository<AspNetRole> IAspNetRoleRepository { get; }        
        IGenericRepository<vDashboard> IvDashboardRepository { get; }
        IGenericRepository<HoganUserInfo> IHoganUserInfoRepository { get; }
        IGenericRepository<EmailStatus> IEmailStatusRepository { get; }
        IGenericRepository<PersonEmail> IPersonEmailRepository { get; }
        IGenericRepository<Email> IEmailRepository { get; }
        IGenericRepository<vPersonBySite> IvPersonBySiteRepository { get; }        
        IGenericRepository<Event> IEventRepository { get; }
        IGenericRepository<EventStatus> IEventStatusRepository { get; }
        IGenericRepository<EventType> IEventTypeRepository { get; } 
         IGenericRepository<Culture> ICultureRepository { get; } 
         IGenericRepository<Person> IPersonRepository {get;}
         IGenericRepository<Site> ISiteRepository { get; }
         IGenericRepository<Organization> IOrganizationRepository { get; }     
         IGenericRepository<SiteUser> ISiteUserRepository { get; }
         IGenericRepository<Program> IProgramRepository { get; }
         IGenericRepository<ReplacementExpression> IReplacementExpressionRepository { get; }
         IGenericRepository<Manual_Hogan_Import> IManual_Hogan_ImportRepository { get; }
         IGenericRepository<ProgramSite> IProgramSiteRepository { get; }
         IGenericRepository<ProgramSiteHoganMVPI> IProgramSiteHoganMVPIRepository { get; }
         IGenericRepository<HoganMVPI> IHoganMVPIRepository { get; }         
         IGenericRepository<PracticeScale> IPracticeScaleRepository { get; }
         IGenericRepository<HoganField> IHoganFieldRepository { get; }
         IGenericRepository<PracticeCategory> IPracticeCategoryRepository { get; }
         IGenericRepository<PracticeLevel> IPracticeLevelRepository { get; }
         IGenericRepository<ProgramPracticeReports> IProgramPracticeReportRepository { get; }
         IGenericRepository<PracticeText> IPracticeTextRepository { get; }
         IGenericRepository<Language> ILanguageRepository { get; }
         IGenericRepository<PracticeScaleReport> IPracticeScaleReportRepository { get; }
         IGenericRepository<PracticeReport> IPracticeReportRepository { get; }    
         IGenericRepository<ResxValue> IResxValueRepository { get; }            
         ISpecialRepository<UserPracticeText> IUserPracticeTextRepository { get; }
         ISpecialRepository<UserPracticeCategoryText> IUserPracticeCategoryTextRepository { get; }
         ISpecialRepository<String> IGenericRepository { get; }         
         IGenericRepository<AssessmentQuestion> IAssessmentQuestion { get; }
         IGenericRepository<AssessmentQuestionOption> IAssessmentQuestionOption { get; }
         IGenericRepository<AssessmentResponse> IAssessmentResponse { get; }
         IGenericRepository<AssessmentResponseQuestionOption> IAssessmentResponseQuestionOption { get; }
         IGenericRepository<SurveyType> ISurveyType { get; }
         IGenericRepository<EventNotBillableReason> IEventNotBillableReason { get; }
         IGenericRepository<EventVGSpecialty> IEventVGSpecialty { get; }
         IGenericRepository<OESSetup> IOESSetup { get; }
         IGenericRepository<OESSchedule> IOESSchedule { get; }
         IGenericRepository<OESPracticeReport> IOESPracticeReport { get; }

        void Commit();
    }
}

