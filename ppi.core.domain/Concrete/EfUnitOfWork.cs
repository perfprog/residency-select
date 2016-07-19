

namespace PPI.Core.Domain.Concrete
{
    using PPI.Core.Domain.Abstract;
    using PPI.Core.Domain.Entities;
    using System;
    public class EfUnitOfWork : IUnitOfWork
    {        
        private readonly EfGenericRepository<ZCOUserMap> _ZCOUserMapRepository;
        private readonly EfGenericRepository<ZCOExportTemplate> _ZCOExportTemplateRepository;
        private readonly EfGenericRepository<ZCOExportTemplateMap> _ZCOExportTemplateMapRepository;
        private readonly EfGenericRepository<ZCOExportMap> _ZCOExportMapRepository;
        private readonly EfGenericRepository<ScheduledEmailPerson> _ScheduledEmailPersonRepository;
        private readonly EfGenericRepository<ScheduledEmail> _ScheduledEmailRepository;
        private readonly EfGenericRepository<OrderStatus> _OrderStatusRepository;
        private readonly EfGenericRepository<OrderFormPracticeReport> _OrderFormPracticeReportRepository;
        private readonly EfGenericRepository<OrderForm> _OrderFormRepository;
        private readonly EfGenericRepository<EventPracticeReport> _EventPracticeReportRepository;
        private readonly EfGenericRepository<PersonEvent> _PersonEventRepository;
        private readonly EfGenericRepository<PersonPracticeReport> _PersonPracticeReportRepository;        
        private readonly EfGenericRepository<AspNetRole> _AspNetRoleRepository;
        private readonly EfGenericRepository<vDashboard> _vDashboardRepository;
        private readonly EfGenericRepository<HoganUserInfo> _HoganUserInfoRepository;
        private readonly EfGenericRepository<EmailStatus> _EmailStatusRepository;
        private readonly EfGenericRepository<PersonEmail> _PersonEmailRepository;
        private readonly EfGenericRepository<Email> _EmailRepository;
        private readonly EfGenericRepository<vPersonBySite> _vPersonBySiteRepository;        
        private readonly EfGenericRepository<Event> _EventRepository;
        private readonly EfGenericRepository<EventType> _EventTypeRepository;
        private readonly EfGenericRepository<EventStatus> _EventStatusRepository;
        private readonly EfGenericRepository<Culture> _CultureRepository;
        private readonly EfGenericRepository<Person> _PersonRepository;
        private readonly EfGenericRepository<Site> _SiteRepository;
        private readonly EfGenericRepository<Organization> _OrganizationsRepository; 
        private readonly EfGenericRepository<SiteUser> _SiteUserRepository;
        private readonly EfGenericRepository<Program> _ProgramRepository;        
        private readonly EfGenericRepository<ReplacementExpression> _ReplacementExpressionRepository;        
        private readonly EfGenericRepository<Manual_Hogan_Import> _HoganRepository;
        private readonly EfGenericRepository<ProgramSite> _ProgramSiteRepository;
        private readonly EfGenericRepository<ProgramSiteHoganMVPI> _ProgramSiteHoganMVPIRepository;
        private readonly EfGenericRepository<HoganMVPI> _HoganMVPIRepository;
        private readonly EfGenericRepository<PracticeScale> _PracticeScaleRepository;
        private readonly EfGenericRepository<HoganField> _HoganFieldRepository;
        private readonly EfGenericRepository<PracticeCategory> _PracticeCategoryRepository;
        private readonly EfGenericRepository<PracticeLevel> _PracticeLevelRepository;
        private readonly EfGenericRepository<PracticeText> _PracticeTextRepository;
        private readonly EfGenericRepository<ProgramPracticeReports> _ProgramPracticeReportRepository;
        private readonly EfGenericRepository<Language> _LanguageRepository;
        private readonly EfGenericRepository<PracticeScaleReport> _PracticeScaleReportRepository;
        private readonly EfGenericRepository<PracticeReport> _PracticeReportRepository;
        private readonly EfGenericRepository<ResxValue> _ResxValueRepository;
        private readonly EfSpecialRepository<UserPracticeText> _UserPracticeTextRepository;
        private readonly EfSpecialRepository<String> _GenericRepository;
        private readonly EfSpecialRepository<UserPracticeCategoryText> _UserPracticeCategoryTextRepository;        
        private readonly EfGenericRepository<AssessmentQuestion> _AssessmentQuestion;
        private readonly EfGenericRepository<AssessmentQuestionOption> _AssessmentQuestionOption;

        private readonly EfGenericRepository<AssessmentResponse> _AssessmentResponse;
        private readonly EfGenericRepository<AssessmentResponseQuestionOption> _AssessmentResponseQuestionOption;
        private readonly EfGenericRepository<SurveyType> _SurveyType;

        private readonly EfGenericRepository<EventNotBillableReason> _EventNotBillableReason;
        private readonly EfGenericRepository<EventVGSpecialty> _EventVGSpecialty;

        private readonly EfGenericRepository<OESSetup> _OESSetup;
        private readonly EfGenericRepository<OESSchedule> _OESSchedule;
        private readonly EfGenericRepository<OESPracticeReport> _OESPracticeReport;

        private ResidencySelect_Entities context = new ResidencySelect_Entities();
        public EfUnitOfWork()
        {
            _OESPracticeReport = new EfGenericRepository<OESPracticeReport>(context);
            _OESSchedule = new EfGenericRepository<OESSchedule>(context);
            _OESSetup = new EfGenericRepository<OESSetup>(context);
            _EventVGSpecialty = new EfGenericRepository<EventVGSpecialty>(context);
            _EventNotBillableReason = new EfGenericRepository<EventNotBillableReason>(context);
            _SurveyType = new EfGenericRepository<SurveyType>(context);
            _AssessmentResponseQuestionOption = new EfGenericRepository<AssessmentResponseQuestionOption>(context);    
            _AssessmentResponse = new EfGenericRepository<AssessmentResponse>(context);    
            _AssessmentQuestionOption = new EfGenericRepository<AssessmentQuestionOption>(context);       
            _AssessmentQuestion = new EfGenericRepository<AssessmentQuestion>(context);            
            _ZCOUserMapRepository = new EfGenericRepository<ZCOUserMap>(context);
            _ZCOExportTemplateRepository = new EfGenericRepository<ZCOExportTemplate>(context);
            _ZCOExportTemplateMapRepository = new EfGenericRepository<ZCOExportTemplateMap>(context);
            _ZCOExportMapRepository = new EfGenericRepository<ZCOExportMap>(context);
            _ScheduledEmailPersonRepository = new EfGenericRepository<ScheduledEmailPerson>(context);
            _ScheduledEmailRepository = new EfGenericRepository<ScheduledEmail>(context);
            _OrderStatusRepository = new EfGenericRepository<OrderStatus>(context);
            _OrderFormPracticeReportRepository = new EfGenericRepository<OrderFormPracticeReport>(context);
            _OrderFormRepository = new EfGenericRepository<OrderForm>(context);
            _EventPracticeReportRepository = new EfGenericRepository<EventPracticeReport>(context);
            _PersonEventRepository = new EfGenericRepository<PersonEvent>(context);
            _PersonPracticeReportRepository = new EfGenericRepository<PersonPracticeReport>(context);            
            _AspNetRoleRepository = new EfGenericRepository<AspNetRole>(context);
            _vDashboardRepository = new EfGenericRepository<vDashboard>(context);
            _HoganUserInfoRepository = new EfGenericRepository<HoganUserInfo>(context);
            _EmailStatusRepository = new EfGenericRepository<EmailStatus>(context);
            _PersonEmailRepository = new EfGenericRepository<PersonEmail>(context);
            _EmailRepository = new EfGenericRepository<Email>(context);
            _vPersonBySiteRepository = new EfGenericRepository<vPersonBySite>(context);                        
            _SiteUserRepository = new EfGenericRepository<SiteUser>(context);
            _EventRepository = new EfGenericRepository<Event>(context);
            _EventStatusRepository = new EfGenericRepository<EventStatus>(context);
            _EventTypeRepository = new EfGenericRepository<EventType>(context);
            _ProgramPracticeReportRepository = new EfGenericRepository<ProgramPracticeReports>(context);
            _CultureRepository = new EfGenericRepository<Culture>(context);
            _PersonRepository = new EfGenericRepository<Person>(context);
            _SiteRepository = new EfGenericRepository<Site>(context);
            _OrganizationsRepository = new EfGenericRepository<Organization>(context);
             _ProgramRepository = new EfGenericRepository<Program>(context);            
            _HoganRepository = new EfGenericRepository<Manual_Hogan_Import>(context);
            _ProgramSiteRepository = new EfGenericRepository<ProgramSite>(context);
            _ProgramSiteHoganMVPIRepository = new EfGenericRepository<ProgramSiteHoganMVPI>(context);
            _HoganMVPIRepository = new EfGenericRepository<HoganMVPI>(context);            
            _PracticeScaleRepository = new EfGenericRepository<PracticeScale>(context);
            _HoganFieldRepository = new EfGenericRepository<HoganField>(context);
            _PracticeCategoryRepository = new EfGenericRepository<PracticeCategory>(context);
            _PracticeLevelRepository = new EfGenericRepository<PracticeLevel>(context);
            _PracticeTextRepository = new EfGenericRepository<PracticeText>(context);
            _LanguageRepository = new EfGenericRepository<Language>(context);            
            _PracticeScaleReportRepository = new EfGenericRepository<PracticeScaleReport>(context);
            _PracticeReportRepository = new EfGenericRepository<PracticeReport>(context);
            _UserPracticeTextRepository = new EfSpecialRepository<UserPracticeText>(context);
            _GenericRepository = new EfSpecialRepository<String>(context);
            _UserPracticeCategoryTextRepository = new EfSpecialRepository<UserPracticeCategoryText>(context);
            _ReplacementExpressionRepository = new EfGenericRepository<ReplacementExpression>(context);
            _ProgramPracticeReportRepository = new EfGenericRepository<ProgramPracticeReports>(context);
            _ResxValueRepository = new EfGenericRepository<ResxValue>(context);
                    
        }

        public IGenericRepository<OESPracticeReport> IOESPracticeReport
        {
            get { return _OESPracticeReport; }
        }
        public IGenericRepository<OESSchedule> IOESSchedule
        {
            get { return _OESSchedule; }
        }
        public IGenericRepository<OESSetup> IOESSetup
        {
            get { return _OESSetup; }
        }

        public IGenericRepository<EventVGSpecialty> IEventVGSpecialty
        {
            get { return _EventVGSpecialty; }
        }
        public IGenericRepository<EventNotBillableReason> IEventNotBillableReason
        {
            get { return _EventNotBillableReason; }
        }
        public IGenericRepository<SurveyType> ISurveyType
        {
            get { return _SurveyType; }
        }
        public IGenericRepository<AssessmentResponse> IAssessmentResponse
        {
            get { return _AssessmentResponse; }
        }
        public IGenericRepository<AssessmentResponseQuestionOption> IAssessmentResponseQuestionOption
        {
            get { return _AssessmentResponseQuestionOption; }
        }    

        public IGenericRepository<AssessmentQuestionOption> IAssessmentQuestionOption
        {
            get { return _AssessmentQuestionOption; }
        }     
        public IGenericRepository<AssessmentQuestion> IAssessmentQuestion
        {
            get { return _AssessmentQuestion; }
        }       
        public IGenericRepository<ZCOUserMap> IZCOUserMapRepository
        {
            get { return _ZCOUserMapRepository; }
        }        
        public IGenericRepository<ZCOExportTemplate> IZCOExportTemplateRepository
        {
            get { return _ZCOExportTemplateRepository; }
        }        
        public IGenericRepository<ZCOExportTemplateMap> IZCOExportTemplateMapRepository
        {
            get { return _ZCOExportTemplateMapRepository; }
        }        
        public IGenericRepository<ZCOExportMap> IZCOExportMapRepository
        {
            get { return _ZCOExportMapRepository; }
        }        
        public IGenericRepository<ScheduledEmailPerson> IScheduledEmailPersonRepository
        {
            get { return _ScheduledEmailPersonRepository; }
        }        
        public IGenericRepository<ScheduledEmail> IScheduledEmailRepository
        {
            get { return _ScheduledEmailRepository; }
        }
        public IGenericRepository<OrderStatus> IOrderStatusRepository
        {
            get { return _OrderStatusRepository; }
        }
        public IGenericRepository<OrderForm> IOrderFormRepository
        {
            get { return _OrderFormRepository; }
        }
        public IGenericRepository<OrderFormPracticeReport> IOrderFormPracticeReportRepository
        {
            get { return _OrderFormPracticeReportRepository; }
        }
        public IGenericRepository<EventPracticeReport> IEventPracticeReportRepository
        {
            get { return _EventPracticeReportRepository; }
        }
        public IGenericRepository<PersonEvent> IPersonEventRepository
        {
            get { return _PersonEventRepository; }
        }
        public IGenericRepository<PersonPracticeReport> IPersonPracticeReportRepository
        {
            get { return _PersonPracticeReportRepository; }
        }
        public IGenericRepository<AspNetRole> IAspNetRoleRepository
        {
            get { return _AspNetRoleRepository; }
        }

        public IGenericRepository<vDashboard> IvDashboardRepository
        {
            get { return _vDashboardRepository; }
        }
        public IGenericRepository<HoganUserInfo> IHoganUserInfoRepository
        {
            get { return _HoganUserInfoRepository; }
        }

        public IGenericRepository<EmailStatus> IEmailStatusRepository
        {
            get { return _EmailStatusRepository; }
        }
        public IGenericRepository<PersonEmail> IPersonEmailRepository
        {
            get { return _PersonEmailRepository; }
        }
        public IGenericRepository<Email> IEmailRepository
        {
            get { return _EmailRepository; }
        }       
        public IGenericRepository<SiteUser> ISiteUserRepository
        {
            get { return _SiteUserRepository; }
        }
               
        public IGenericRepository<Event> IEventRepository
        {
            get { return _EventRepository; }
        }
        public IGenericRepository<EventStatus> IEventStatusRepository
        {
            get { return _EventStatusRepository; }
        }
        public IGenericRepository<EventType> IEventTypeRepository
        {
            get { return _EventTypeRepository; }
        }
        public IGenericRepository<Culture> ICultureRepository
        {
            get { return _CultureRepository; }
        }
            public IGenericRepository<Entities.ResxValue> IResxValueRepository
        {
            get { return _ResxValueRepository; }
        }

        public IGenericRepository<Entities.ReplacementExpression> IReplacementExpressionRepository
        {
            get { return _ReplacementExpressionRepository; }
        }


        public IGenericRepository<Entities.Person> IPersonRepository        
        {            
            get { return _PersonRepository; }
        }


        public IGenericRepository<Entities.PracticeReport> IPracticeReportRepository
        {
            get { return _PracticeReportRepository; }
        }

        public IGenericRepository<Entities.Site> ISiteRepository
        {
            get { return _SiteRepository; }
        }
        public IGenericRepository<Entities.Organization> IOrganizationRepository
        {
            get { return _OrganizationsRepository; }
        }
       
        public IGenericRepository<Entities.Program> IProgramRepository
        {
            get { return _ProgramRepository; }
        }

       
        public IGenericRepository<Entities.Manual_Hogan_Import> IManual_Hogan_ImportRepository
        {
            get { return _HoganRepository; }
        }
        public IGenericRepository<Entities.ProgramSiteHoganMVPI> IProgramSiteHoganMVPIRepository
        {
            get { return _ProgramSiteHoganMVPIRepository; }
        }
        [Log]        
        public void Commit()
        {
            context.SaveChanges();            
            
        }
        [Log]      
        public void Dispose()
        {
            context.Dispose();
        }


        public IGenericRepository<ProgramSite> IProgramSiteRepository
        {
            get { return _ProgramSiteRepository; }
        }
     

        public IGenericRepository<HoganMVPI> IHoganMVPIRepository
        {
            get { return _HoganMVPIRepository; }
        }


    
        public IGenericRepository<PracticeScale> IPracticeScaleRepository
        {
            get { return _PracticeScaleRepository; }
        }


        public IGenericRepository<HoganField> IHoganFieldRepository
        {
            get { return _HoganFieldRepository; }
        }

        public IGenericRepository<PracticeCategory> IPracticeCategoryRepository
        {
            get { return _PracticeCategoryRepository; }
        }

        public IGenericRepository<PracticeLevel> IPracticeLevelRepository
        {
            get { return _PracticeLevelRepository; }
        }


        public IGenericRepository<PracticeText> IPracticeTextRepository
        {
            get { return _PracticeTextRepository; }
        }


        public IGenericRepository<Language> ILanguageRepository
        {
            get { return _LanguageRepository; }
        }


        public IGenericRepository<PracticeScaleReport> IPracticeScaleReportRepository
        {
            get { return _PracticeScaleReportRepository; }
        }


        public ISpecialRepository<UserPracticeText> IUserPracticeTextRepository
        {
            get { return _UserPracticeTextRepository; }
        }

        public ISpecialRepository<UserPracticeCategoryText> IUserPracticeCategoryTextRepository
        {
            get { return _UserPracticeCategoryTextRepository; }
        }


        public IGenericRepository<ProgramPracticeReports> IProgramPracticeReportRepository
        {
            get { return _ProgramPracticeReportRepository; }
        }



        public IGenericRepository<vPersonBySite> IvPersonBySiteRepository
        {
            get { return _vPersonBySiteRepository; }
        }


        public ISpecialRepository<String> IGenericRepository
        {
            get { return _GenericRepository; }
        }
    }
}
