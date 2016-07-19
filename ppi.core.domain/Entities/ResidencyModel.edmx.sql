
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/26/2015 15:24:06
-- Generated from EDMX file: E:\Residency\Source\ppi.core.web.11122015\ppi.core.domain\Entities\ResidencyModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [master];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AssessmentResponse_SiteUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssessmentResponses] DROP CONSTRAINT [FK_AssessmentResponse_SiteUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_AssessmentResponseQuestionOption_AssessmentOption]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssessmentResponseQuestionOptions] DROP CONSTRAINT [FK_AssessmentResponseQuestionOption_AssessmentOption];
GO
IF OBJECT_ID(N'[dbo].[FK_AssessmentResponseQuestionOption_AssessmentQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssessmentResponseQuestionOptions] DROP CONSTRAINT [FK_AssessmentResponseQuestionOption_AssessmentQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_AssessmentResponseQuestionOption_AssessmentResponse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AssessmentResponseQuestionOptions] DROP CONSTRAINT [FK_AssessmentResponseQuestionOption_AssessmentResponse];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_User_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_User_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_Email_EmailType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Emails] DROP CONSTRAINT [FK_Email_EmailType];
GO
IF OBJECT_ID(N'[dbo].[FK_Email_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Emails] DROP CONSTRAINT [FK_Email_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailStatus_Resx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailStatus] DROP CONSTRAINT [FK_EmailStatus_Resx];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailType_Resx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailTypes] DROP CONSTRAINT [FK_EmailType_Resx];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_EventStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Event_EventStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_EventType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Event_EventType];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_Program]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Event_Program];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_ProgramSites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Event_ProgramSites];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_Site]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Event_Site];
GO
IF OBJECT_ID(N'[dbo].[FK_EventPracticeReport_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventPracticeReports] DROP CONSTRAINT [FK_EventPracticeReport_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_EventPracticeReport_IntroductionResx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventPracticeReports] DROP CONSTRAINT [FK_EventPracticeReport_IntroductionResx];
GO
IF OBJECT_ID(N'[dbo].[FK_EventPracticeReport_IntroductionThreeResx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventPracticeReports] DROP CONSTRAINT [FK_EventPracticeReport_IntroductionThreeResx];
GO
IF OBJECT_ID(N'[dbo].[FK_EventPracticeReport_IntroductionTwoResx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventPracticeReports] DROP CONSTRAINT [FK_EventPracticeReport_IntroductionTwoResx];
GO
IF OBJECT_ID(N'[dbo].[FK_EventPracticeReport_PracticeReport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventPracticeReports] DROP CONSTRAINT [FK_EventPracticeReport_PracticeReport];
GO
IF OBJECT_ID(N'[dbo].[FK_EventPracticeReport_TitleResx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventPracticeReports] DROP CONSTRAINT [FK_EventPracticeReport_TitleResx];
GO
IF OBJECT_ID(N'[dbo].[FK_EventStatus_Resx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventStatus] DROP CONSTRAINT [FK_EventStatus_Resx];
GO
IF OBJECT_ID(N'[dbo].[FK_EventType_Resx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventTypes] DROP CONSTRAINT [FK_EventType_Resx];
GO
IF OBJECT_ID(N'[dbo].[FK_HoganField_Resx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HoganFields] DROP CONSTRAINT [FK_HoganField_Resx];
GO
IF OBJECT_ID(N'[dbo].[FK_HoganField_Resx2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HoganFields] DROP CONSTRAINT [FK_HoganField_Resx2];
GO
IF OBJECT_ID(N'[dbo].[FK_HoganUserInfo_Program]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HoganUserInfoes] DROP CONSTRAINT [FK_HoganUserInfo_Program];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderForm_EventType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderForms] DROP CONSTRAINT [FK_OrderForm_EventType];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderForm_OrderStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderForms] DROP CONSTRAINT [FK_OrderForm_OrderStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderForm_Program]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderForms] DROP CONSTRAINT [FK_OrderForm_Program];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderFormPracticeReport_OrderForm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderFormPracticeReports] DROP CONSTRAINT [FK_OrderFormPracticeReport_OrderForm];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderFormPracticeReport_PracticeReport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderFormPracticeReports] DROP CONSTRAINT [FK_OrderFormPracticeReport_PracticeReport];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonEmail_Email]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonEmails] DROP CONSTRAINT [FK_PersonEmail_Email];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonEmail_EmailStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonEmails] DROP CONSTRAINT [FK_PersonEmail_EmailStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonEmail_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonEmails] DROP CONSTRAINT [FK_PersonEmail_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonEvent_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonEvents] DROP CONSTRAINT [FK_PersonEvent_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonEvent_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonEvents] DROP CONSTRAINT [FK_PersonEvent_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPracticeReport_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonPracticeReports] DROP CONSTRAINT [FK_PersonPracticeReport_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPracticeReport_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonPracticeReports] DROP CONSTRAINT [FK_PersonPracticeReport_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPracticeReport_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonPracticeReports] DROP CONSTRAINT [FK_PersonPracticeReport_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPracticeReport_PracticeReport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonPracticeReports] DROP CONSTRAINT [FK_PersonPracticeReport_PracticeReport];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeCategoryParagraphs_HoganField]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeCategoryParagraphs] DROP CONSTRAINT [FK_PracticeCategoryParagraphs_HoganField];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeCategoryParagraphs_PracticeCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeCategoryParagraphs] DROP CONSTRAINT [FK_PracticeCategoryParagraphs_PracticeCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeCategoryScale_HoganField]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeCategoryScales] DROP CONSTRAINT [FK_PracticeCategoryScale_HoganField];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeCategoryScale_PracticeCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeCategoryScales] DROP CONSTRAINT [FK_PracticeCategoryScale_PracticeCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeCategoryScale_Program]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeCategoryScales] DROP CONSTRAINT [FK_PracticeCategoryScale_Program];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeParagraphs_PracticeReport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeParagraphs] DROP CONSTRAINT [FK_PracticeParagraphs_PracticeReport];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeReport_IntroductionResx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeReports] DROP CONSTRAINT [FK_PracticeReport_IntroductionResx];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeReport_IntroductionThreeResx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeReports] DROP CONSTRAINT [FK_PracticeReport_IntroductionThreeResx];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeReport_IntroductionTwoResx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeReports] DROP CONSTRAINT [FK_PracticeReport_IntroductionTwoResx];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeReport_Resx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeReports] DROP CONSTRAINT [FK_PracticeReport_Resx];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeScale_HoganField]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeScales] DROP CONSTRAINT [FK_PracticeScale_HoganField];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeScale_PracticeCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeScales] DROP CONSTRAINT [FK_PracticeScale_PracticeCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeScale_PracticeLevel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeScales] DROP CONSTRAINT [FK_PracticeScale_PracticeLevel];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeScale_Program]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeScales] DROP CONSTRAINT [FK_PracticeScale_Program];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeScaleReport_PracticeReport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeScaleReports] DROP CONSTRAINT [FK_PracticeScaleReport_PracticeReport];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeScaleReport_PracticeScale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeScaleReports] DROP CONSTRAINT [FK_PracticeScaleReport_PracticeScale];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeScaleReport_PracticeText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeScaleReports] DROP CONSTRAINT [FK_PracticeScaleReport_PracticeText];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeScaleReport_PracticeText2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeScaleReports] DROP CONSTRAINT [FK_PracticeScaleReport_PracticeText2];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeText_PracticeText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeTexts] DROP CONSTRAINT [FK_PracticeText_PracticeText];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeText_Text]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeTexts] DROP CONSTRAINT [FK_PracticeText_Text];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeTextOption_AlternativePracticeText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeTextOptions] DROP CONSTRAINT [FK_PracticeTextOption_AlternativePracticeText];
GO
IF OBJECT_ID(N'[dbo].[FK_PracticeTextOption_PracticeText]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PracticeTextOptions] DROP CONSTRAINT [FK_PracticeTextOption_PracticeText];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramPracticeReports_PracticeReport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramPracticeReports] DROP CONSTRAINT [FK_ProgramPracticeReports_PracticeReport];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramPracticeReports_Program]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramPracticeReports] DROP CONSTRAINT [FK_ProgramPracticeReports_Program];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramSiteHoganMVPI_ResidencyProgramHospitals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramSiteHoganMVPIs] DROP CONSTRAINT [FK_ProgramSiteHoganMVPI_ResidencyProgramHospitals];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidencyProgramSites_Site]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramSites] DROP CONSTRAINT [FK_ResidencyProgramSites_Site];
GO
IF OBJECT_ID(N'[dbo].[FK_ResidencyProgramSitess_Program]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramSites] DROP CONSTRAINT [FK_ResidencyProgramSitess_Program];
GO
IF OBJECT_ID(N'[dbo].[FK_ResxValue_Culture]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResxValues] DROP CONSTRAINT [FK_ResxValue_Culture];
GO
IF OBJECT_ID(N'[dbo].[FK_ResxValue_Resx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResxValues] DROP CONSTRAINT [FK_ResxValue_Resx];
GO
IF OBJECT_ID(N'[dbo].[FK_ScheduledEmail_Email]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScheduledEmails] DROP CONSTRAINT [FK_ScheduledEmail_Email];
GO
IF OBJECT_ID(N'[dbo].[FK_ScheduledEmailPerson_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScheduledEmailPersons] DROP CONSTRAINT [FK_ScheduledEmailPerson_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_ScheduledEmailPerson_ScheduledEmail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScheduledEmailPersons] DROP CONSTRAINT [FK_ScheduledEmailPerson_ScheduledEmail];
GO
IF OBJECT_ID(N'[dbo].[FK_SiteUsers_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SiteUsers] DROP CONSTRAINT [FK_SiteUsers_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_SiteUsers_Site]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SiteUsers] DROP CONSTRAINT [FK_SiteUsers_Site];
GO
IF OBJECT_ID(N'[dbo].[FK_vPersonBySitePerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[vPersonBySites] DROP CONSTRAINT [FK_vPersonBySitePerson];
GO
IF OBJECT_ID(N'[dbo].[FK_ZCOExportTemplateMap_ZCOExportMap]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ZCOExportTemplateMaps] DROP CONSTRAINT [FK_ZCOExportTemplateMap_ZCOExportMap];
GO
IF OBJECT_ID(N'[dbo].[FK_ZCOExportTemplateMap_ZCOExportTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ZCOExportTemplateMaps] DROP CONSTRAINT [FK_ZCOExportTemplateMap_ZCOExportTemplate];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[AssessmentOptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssessmentOptions];
GO
IF OBJECT_ID(N'[dbo].[AssessmentQuestions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssessmentQuestions];
GO
IF OBJECT_ID(N'[dbo].[AssessmentResponseQuestionOptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssessmentResponseQuestionOptions];
GO
IF OBJECT_ID(N'[dbo].[AssessmentResponses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssessmentResponses];
GO
IF OBJECT_ID(N'[dbo].[Cultures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cultures];
GO
IF OBJECT_ID(N'[dbo].[Emails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Emails];
GO
IF OBJECT_ID(N'[dbo].[EmailStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailStatus];
GO
IF OBJECT_ID(N'[dbo].[EmailTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailTypes];
GO
IF OBJECT_ID(N'[dbo].[EventPracticeReports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventPracticeReports];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[EventStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventStatus];
GO
IF OBJECT_ID(N'[dbo].[EventTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventTypes];
GO
IF OBJECT_ID(N'[dbo].[HoganFields]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HoganFields];
GO
IF OBJECT_ID(N'[dbo].[HoganMVPIs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HoganMVPIs];
GO
IF OBJECT_ID(N'[dbo].[HoganUserInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HoganUserInfoes];
GO
IF OBJECT_ID(N'[dbo].[Languages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Languages];
GO
IF OBJECT_ID(N'[dbo].[Manual_Hogan_Import]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Manual_Hogan_Import];
GO
IF OBJECT_ID(N'[dbo].[OrderFormPracticeReports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderFormPracticeReports];
GO
IF OBJECT_ID(N'[dbo].[OrderForms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderForms];
GO
IF OBJECT_ID(N'[dbo].[OrderStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderStatus];
GO
IF OBJECT_ID(N'[dbo].[Organizations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Organizations];
GO
IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[PersonEmails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonEmails];
GO
IF OBJECT_ID(N'[dbo].[PersonEvents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonEvents];
GO
IF OBJECT_ID(N'[dbo].[PersonPracticeReports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonPracticeReports];
GO
IF OBJECT_ID(N'[dbo].[PracticeCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PracticeCategories];
GO
IF OBJECT_ID(N'[dbo].[PracticeCategoryParagraphs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PracticeCategoryParagraphs];
GO
IF OBJECT_ID(N'[dbo].[PracticeCategoryScales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PracticeCategoryScales];
GO
IF OBJECT_ID(N'[dbo].[PracticeLevels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PracticeLevels];
GO
IF OBJECT_ID(N'[dbo].[PracticeParagraphs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PracticeParagraphs];
GO
IF OBJECT_ID(N'[dbo].[PracticeReports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PracticeReports];
GO
IF OBJECT_ID(N'[dbo].[PracticeScaleReports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PracticeScaleReports];
GO
IF OBJECT_ID(N'[dbo].[PracticeScales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PracticeScales];
GO
IF OBJECT_ID(N'[dbo].[PracticeTextOptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PracticeTextOptions];
GO
IF OBJECT_ID(N'[dbo].[PracticeTexts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PracticeTexts];
GO
IF OBJECT_ID(N'[dbo].[ProgramPracticeReports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramPracticeReports];
GO
IF OBJECT_ID(N'[dbo].[Programs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Programs];
GO
IF OBJECT_ID(N'[dbo].[ProgramSiteHoganMVPIs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramSiteHoganMVPIs];
GO
IF OBJECT_ID(N'[dbo].[ProgramSites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramSites];
GO
IF OBJECT_ID(N'[dbo].[ReplacementExpressions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReplacementExpressions];
GO
IF OBJECT_ID(N'[dbo].[Resxes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Resxes];
GO
IF OBJECT_ID(N'[dbo].[ResxValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResxValues];
GO
IF OBJECT_ID(N'[dbo].[ScheduledEmailPersons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScheduledEmailPersons];
GO
IF OBJECT_ID(N'[dbo].[ScheduledEmails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScheduledEmails];
GO
IF OBJECT_ID(N'[dbo].[Sites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sites];
GO
IF OBJECT_ID(N'[dbo].[SiteUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SiteUsers];
GO
IF OBJECT_ID(N'[dbo].[tempEvents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tempEvents];
GO
IF OBJECT_ID(N'[dbo].[tempPersonProgramSiteEvents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tempPersonProgramSiteEvents];
GO
IF OBJECT_ID(N'[dbo].[vPersonBySites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[vPersonBySites];
GO
IF OBJECT_ID(N'[dbo].[ZCOExportMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZCOExportMaps];
GO
IF OBJECT_ID(N'[dbo].[ZCOExportTemplateMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZCOExportTemplateMaps];
GO
IF OBJECT_ID(N'[dbo].[ZCOExportTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZCOExportTemplates];
GO
IF OBJECT_ID(N'[dbo].[ZCOUserMaps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZCOUserMaps];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [UserId] nvarchar(128)  NOT NULL,
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL
);
GO

-- Creating table 'AssessmentOptions'
CREATE TABLE [dbo].[AssessmentOptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Option] nvarchar(100)  NOT NULL,
    [Description] nvarchar(500)  NULL,
    [Order] int  NOT NULL
);
GO

-- Creating table 'AssessmentQuestions'
CREATE TABLE [dbo].[AssessmentQuestions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Question] nvarchar(500)  NOT NULL,
    [Order] int  NOT NULL
);
GO

-- Creating table 'AssessmentResponses'
CREATE TABLE [dbo].[AssessmentResponses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SiteUserId] int  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'AssessmentResponseQuestionOptions'
CREATE TABLE [dbo].[AssessmentResponseQuestionOptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AssessmentResponseId] int  NOT NULL,
    [AssessmentQuestionId] int  NOT NULL,
    [AssessmentOptionId] int  NOT NULL
);
GO

-- Creating table 'Cultures'
CREATE TABLE [dbo].[Cultures] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Emails'
CREATE TABLE [dbo].[Emails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EventId] int  NULL,
    [EmailTypeId] int  NULL,
    [DefaultFrom] nvarchar(150)  NULL,
    [Subject] nvarchar(250)  NULL,
    [Introduction] nvarchar(max)  NULL,
    [Closing] nvarchar(max)  NULL
);
GO

-- Creating table 'EmailTypes'
CREATE TABLE [dbo].[EmailTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [NameResxId] int  NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [EventTypeId] int  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [ReviewDate] datetime  NULL,
    [EventStatusId] int  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [Placement] int  NULL,
    [Billable] bit  NULL,
    [ProgramSitesId] int  NULL,
    [OrganizationId] int  NULL,
    [SiteId] int  NULL,
    [ProgramId] int  NULL
);
GO

-- Creating table 'EventPracticeReports'
CREATE TABLE [dbo].[EventPracticeReports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EventId] int  NOT NULL,
    [PracticeReportId] int  NOT NULL,
    [ReportTitleResxId] int  NULL,
    [DefaultLogo] varbinary(max)  NULL,
    [DefaultLogoMimeType] nvarchar(50)  NULL,
    [DefaultBackground] varbinary(max)  NULL,
    [DefaultBackgroundMimeType] nvarchar(50)  NULL,
    [DefaultColor] nvarchar(10)  NULL,
    [IntroductionResxId] int  NULL,
    [IntroductionTwoResxId] int  NULL,
    [IntroductionThreeResxId] int  NULL
);
GO

-- Creating table 'EventTypes'
CREATE TABLE [dbo].[EventTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameResxId] int  NOT NULL
);
GO

-- Creating table 'HoganFields'
CREATE TABLE [dbo].[HoganFields] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FieldName] nvarchar(50)  NOT NULL,
    [LabelName] nvarchar(50)  NULL,
    [StandardOrder] int  NULL,
    [DefinitionTextResxId] int  NULL,
    [BlurbTextResxId] int  NULL,
    [HoganCategory] nvarchar(50)  NULL
);
GO

-- Creating table 'HoganMVPIs'
CREATE TABLE [dbo].[HoganMVPIs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FieldName] varchar(50)  NULL,
    [LabelName] varchar(50)  NULL
);
GO

-- Creating table 'HoganUserInfoes'
CREATE TABLE [dbo].[HoganUserInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [GroupName] nvarchar(50)  NULL,
    [ProgramId] int  NOT NULL,
    [UploadDate] datetime  NULL,
    [Used] bit  NOT NULL
);
GO

-- Creating table 'Languages'
CREATE TABLE [dbo].[Languages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL
);
GO

-- Creating table 'Manual_Hogan_Import'
CREATE TABLE [dbo].[Manual_Hogan_Import] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [ClientID] varchar(50)  NULL,
    [ClientName] varchar(50)  NULL,
    [GroupName] varchar(50)  NULL,
    [Hogan_User_ID] varchar(50)  NOT NULL,
    [First_Name] varchar(50)  NULL,
    [Last_Name] varchar(50)  NULL,
    [Gender] varchar(50)  NULL,
    [HPIDate] varchar(50)  NOT NULL,
    [Valid] int  NULL,
    [Empathy] int  NULL,
    [NotAnxious] int  NULL,
    [NoGuilt] int  NULL,
    [Calmness] int  NULL,
    [EvenTempered] int  NULL,
    [NoSomaticComplaint] int  NULL,
    [Trusting] int  NULL,
    [GoodAttachment] int  NULL,
    [Competitive] int  NULL,
    [SelfConfidence] int  NULL,
    [NoDepression] int  NULL,
    [Leadership] int  NULL,
    [Identity] int  NULL,
    [NoSocialAnxiety] int  NULL,
    [LikesParties] int  NULL,
    [LikesCrowds] int  NULL,
    [ExperienceSeeking] int  NULL,
    [Exhibitionistic] int  NULL,
    [Entertaining] int  NULL,
    [EasyToLiveWith] int  NULL,
    [Sensitive] int  NULL,
    [Caring] int  NULL,
    [LikesPeople] int  NULL,
    [NoHostility] int  NULL,
    [Moralistic] int  NULL,
    [Mastery] int  NULL,
    [Virtuous] int  NULL,
    [NotAutonomous] int  NULL,
    [NotSpontaneous] int  NULL,
    [ImpulseControl] int  NULL,
    [AvoidsTrouble] int  NULL,
    [ScienceAbility] int  NULL,
    [Curiosity] int  NULL,
    [ThrillSeeking] int  NULL,
    [IntellectualGames] int  NULL,
    [GeneratesIdeas] int  NULL,
    [Culture] int  NULL,
    [Education] int  NULL,
    [MathAbility] int  NULL,
    [GoodMemory] int  NULL,
    [Reading] int  NULL,
    [RValidity] int  NULL,
    [RAdjustment] int  NULL,
    [RAmbition] int  NULL,
    [RSociability] int  NULL,
    [RInterpersonalSensitivity] int  NULL,
    [RPrudence] int  NULL,
    [RInquisitive] int  NULL,
    [RLearningApproach] int  NULL,
    [RServiceOrientation] int  NULL,
    [RStressTolerance] int  NULL,
    [RReliability] int  NULL,
    [RClericalPotential] int  NULL,
    [RSalesPotential] int  NULL,
    [RManagerialPotential] int  NULL,
    [PValidity] int  NULL,
    [PAdjustment] int  NULL,
    [PAmbition] int  NULL,
    [PSociability] int  NULL,
    [PInterpersonalSensitivity] int  NULL,
    [PPrudence] int  NULL,
    [PInquisitive] int  NULL,
    [PLearningApproach] int  NULL,
    [PServiceOrientation] int  NULL,
    [PStressTolerance] int  NULL,
    [PReliability] int  NULL,
    [PClericalPotential] int  NULL,
    [PSalesPotential] int  NULL,
    [PManagerialPotential] int  NULL,
    [HDSDate] varchar(50)  NULL,
    [RExcitable] int  NULL,
    [RSkeptical] int  NULL,
    [RCautious] int  NULL,
    [RReserved] int  NULL,
    [RLeisurely] int  NULL,
    [RBold] int  NULL,
    [RMischievous] int  NULL,
    [RColorful] int  NULL,
    [RImaginative] int  NULL,
    [RDiligent] int  NULL,
    [RDutiful] int  NULL,
    [RUnlikeness] int  NULL,
    [PExcitable] int  NULL,
    [PSkeptical] int  NULL,
    [PCautious] int  NULL,
    [PReserved] int  NULL,
    [PLeisurely] int  NULL,
    [PBold] int  NULL,
    [PMischievous] int  NULL,
    [PColorful] int  NULL,
    [PImaginative] int  NULL,
    [PDiligent] int  NULL,
    [PDutiful] int  NULL,
    [PUnlikeness] int  NULL,
    [MVPIDate] varchar(50)  NULL,
    [RAesthetic_Lifestyles] int  NULL,
    [RAesthetic_Beliefs] int  NULL,
    [RAesthetic_Occupational_Prferences] int  NULL,
    [RAesthetic_Aversions] int  NULL,
    [RAesthetic_Preferred_Associates] int  NULL,
    [RAffiliation_Lifestyles] int  NULL,
    [RAffiliation_Beliefs] int  NULL,
    [RAffiliation_Occupational_Prferences] int  NULL,
    [RAffiliation_Aversions] int  NULL,
    [RAffiliation_Preferred_Associates] int  NULL,
    [RAltruistic_Lifestyles] int  NULL,
    [RAltruistic_Beliefs] int  NULL,
    [RAltruistic_Occupational_Prferences] int  NULL,
    [RAltruistic_Aversions] int  NULL,
    [RAltruistic_Preferred_Associates] int  NULL,
    [RCommercial_Lifestyles] int  NULL,
    [RCommercial_Beliefs] int  NULL,
    [RCommercial_Occupational_Prferences] int  NULL,
    [RCommercial_Aversions] int  NULL,
    [RCommercial_Preferred_Associates] int  NULL,
    [RHedonistic_Lifestyles] int  NULL,
    [RHedonistic_Beliefs] int  NULL,
    [RHedonistic_Occupational_Prferences] int  NULL,
    [RHedonistic_Aversions] int  NULL,
    [RHedonistic_Preferred_Associates] int  NULL,
    [RPower_Lifestyles] int  NULL,
    [RPower_Beliefs] int  NULL,
    [RPower_Occupational_Prferences] int  NULL,
    [RPower_Aversions] int  NULL,
    [RPower_Preferred_Associates] int  NULL,
    [RRecognition_Lifestyles] int  NULL,
    [RRecognition_Beliefs] int  NULL,
    [RRecognition_Occupational_Prferences] int  NULL,
    [RRecognition_Aversions] int  NULL,
    [RRecognition_Preferred_Associates] int  NULL,
    [RScientific_Lifestyles] int  NULL,
    [RScientific_Beliefs] int  NULL,
    [RScientific_Occupational_Prferences] int  NULL,
    [RScientific_Aversions] int  NULL,
    [RScientific_Preferred_Associates] int  NULL,
    [RSecurity_Lifestyles] int  NULL,
    [RSecurity_Beliefs] int  NULL,
    [RSecurity_Occupational_Prferences] int  NULL,
    [RSecurity_Aversions] int  NULL,
    [RSecurity_Preferred_Associates] int  NULL,
    [RTradition_Lifestyles] int  NULL,
    [RTradition_Beliefs] int  NULL,
    [RTradition_Occupational_Prferences] int  NULL,
    [RTradition_Aversions] int  NULL,
    [RTradition_Preferred_Associates] int  NULL,
    [RAesthetic] int  NULL,
    [RAffiliation] int  NULL,
    [RAltruistic] int  NULL,
    [RCommercial] int  NULL,
    [RHedonistic] int  NULL,
    [RPower] int  NULL,
    [RRecognition] int  NULL,
    [RScientific] int  NULL,
    [RSecurity] int  NULL,
    [RTradition] int  NULL,
    [PAesthetic] int  NULL,
    [PAffiliation] int  NULL,
    [PAltruistic] int  NULL,
    [PCommercial] int  NULL,
    [PHedonistic] int  NULL,
    [PPower] int  NULL,
    [PRecognition] int  NULL,
    [PScientific] int  NULL,
    [PSecurity] int  NULL,
    [PTradition] int  NULL,
    [LastUpdated] datetime  NOT NULL
);
GO

-- Creating table 'OrderForms'
CREATE TABLE [dbo].[OrderForms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderStatusId] int  NULL,
    [OrderDate] datetime  NOT NULL,
    [Site] nvarchar(150)  NULL,
    [ProgramId] int  NOT NULL,
    [EventName] nvarchar(150)  NOT NULL,
    [EventDescription] nvarchar(150)  NULL,
    [Billable] bit  NULL,
    [SalesType] nvarchar(50)  NULL,
    [EventTypeId] int  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [ReviewDate] datetime  NULL,
    [MatchDate] datetime  NULL,
    [Placement] int  NULL,
    [Info] nvarchar(max)  NULL
);
GO

-- Creating table 'OrderFormPracticeReports'
CREATE TABLE [dbo].[OrderFormPracticeReports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderFormId] int  NOT NULL,
    [PracticeReportId] int  NOT NULL
);
GO

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [Gender] nvarchar(50)  NULL,
    [Title] nvarchar(10)  NULL,
    [AAMCNumber] nvarchar(50)  NOT NULL,
    [PrimaryEmail] nvarchar(150)  NOT NULL,
    [Hogan_Id] nvarchar(15)  NULL,
    [Hogan_Password] nvarchar(50)  NULL,
    [CreateDate] datetime  NULL,
    [vDashboardId] int  NOT NULL,
    [vDashboardPersonId] int  NOT NULL,
    [vDashboardEventId] int  NOT NULL,
    [vDashboardId1] int  NOT NULL,
    [vDashboardPersonId1] int  NOT NULL,
    [vDashboardEventId1] int  NOT NULL
);
GO

-- Creating table 'PersonEmails'
CREATE TABLE [dbo].[PersonEmails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonId] int  NOT NULL,
    [EmailId] int  NOT NULL,
    [EmailStatusId] int  NULL,
    [SentDate] datetime  NULL,
    [EmailBody] nvarchar(max)  NOT NULL,
    [RetryCount] int  NOT NULL,
    [ErrorMessage] nvarchar(max)  NULL,
    [Origin] varchar(10)  NULL
);
GO

-- Creating table 'PersonEvents'
CREATE TABLE [dbo].[PersonEvents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonId] int  NOT NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'PersonPracticeReports'
CREATE TABLE [dbo].[PersonPracticeReports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonId] int  NOT NULL,
    [PracticeReportId] int  NOT NULL,
    [RunDate] datetime  NULL,
    [AspNetUsersId] nvarchar(128)  NOT NULL,
    [EventId] int  NULL
);
GO

-- Creating table 'PracticeCategories'
CREATE TABLE [dbo].[PracticeCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [PracticeGroup] nvarchar(50)  NULL,
    [Order] int  NULL
);
GO

-- Creating table 'PracticeCategoryParagraphs'
CREATE TABLE [dbo].[PracticeCategoryParagraphs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HoganFieldId] int  NOT NULL,
    [PracticeCategoryId] int  NOT NULL,
    [ParagraphGroup] int  NOT NULL
);
GO

-- Creating table 'PracticeCategoryScales'
CREATE TABLE [dbo].[PracticeCategoryScales] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProgramId] int  NOT NULL,
    [PracticeCategoryId] int  NOT NULL,
    [HoganFieldId] int  NOT NULL,
    [LowerBound] int  NOT NULL,
    [UpperBound] int  NOT NULL
);
GO

-- Creating table 'PracticeLevels'
CREATE TABLE [dbo].[PracticeLevels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL
);
GO

-- Creating table 'PracticeParagraphs'
CREATE TABLE [dbo].[PracticeParagraphs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HoganFieldId] int  NOT NULL,
    [ParagraphGroup] int  NOT NULL,
    [Order] int  NOT NULL,
    [PracticeReportId] int  NOT NULL
);
GO

-- Creating table 'PracticeReports'
CREATE TABLE [dbo].[PracticeReports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [ReportTitle] nvarchar(75)  NULL,
    [ReportTitleResxId] int  NULL,
    [PracticeGroup] nvarchar(50)  NULL,
    [DefaultLogo] varbinary(max)  NULL,
    [DefaultLogoMimeType] nvarchar(50)  NULL,
    [DefaultBackground] varbinary(max)  NULL,
    [DefaultBackgroundMimeType] nvarchar(50)  NULL,
    [DefaultColor] nvarchar(10)  NULL,
    [Introduction] nvarchar(75)  NULL,
    [IntroductionResxId] int  NULL,
    [IntroductionTwo] nvarchar(75)  NULL,
    [IntroductionTwoResxId] int  NULL,
    [IntroductionThree] nvarchar(75)  NULL,
    [IntroductionThreeResxId] int  NULL,
    [Active] bit  NULL
);
GO

-- Creating table 'PracticeScales'
CREATE TABLE [dbo].[PracticeScales] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HoganFieldId] int  NOT NULL,
    [PracticeCategoryId] int  NOT NULL,
    [PracticeLevelId] int  NOT NULL,
    [ProgramId] int  NULL,
    [LowerBound] int  NOT NULL,
    [UpperBound] int  NOT NULL
);
GO

-- Creating table 'PracticeScaleReports'
CREATE TABLE [dbo].[PracticeScaleReports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PracticeScaleId] int  NOT NULL,
    [PracticeReportId] int  NOT NULL,
    [Order] int  NOT NULL,
    [SubOrder] int  NULL,
    [PracticeTextId] int  NOT NULL,
    [InterviewPracticeTextId] int  NULL
);
GO

-- Creating table 'PracticeTexts'
CREATE TABLE [dbo].[PracticeTexts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TextResxId] int  NOT NULL,
    [Text] nvarchar(75)  NULL,
    [IsIntroduction] bit  NULL,
    [ParentPracticeTextId] int  NULL
);
GO

-- Creating table 'PracticeTextOptions'
CREATE TABLE [dbo].[PracticeTextOptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PracticeTextId] int  NOT NULL,
    [AlternativePracticeTextId] int  NULL
);
GO

-- Creating table 'Programs'
CREATE TABLE [dbo].[Programs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProgramName] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'ProgramPracticeReports'
CREATE TABLE [dbo].[ProgramPracticeReports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProgramId] int  NOT NULL,
    [PracticeReportId] int  NOT NULL
);
GO

-- Creating table 'ProgramSiteHoganMVPIs'
CREATE TABLE [dbo].[ProgramSiteHoganMVPIs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProgramSiteId] int  NOT NULL,
    [HoganFieldName] varchar(max)  NOT NULL
);
GO

-- Creating table 'ProgramSites'
CREATE TABLE [dbo].[ProgramSites] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SiteId] int  NOT NULL,
    [ProgramId] int  NOT NULL
);
GO

-- Creating table 'ReplacementExpressions'
CREATE TABLE [dbo].[ReplacementExpressions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FindValue] nvarchar(50)  NOT NULL,
    [Expression] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Resxes'
CREATE TABLE [dbo].[Resxes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DataColumnDescription] nvarchar(150)  NOT NULL,
    [DataColumnName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ResxValues'
CREATE TABLE [dbo].[ResxValues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ResxId] int  NOT NULL,
    [CultureId] int  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ScheduledEmails'
CREATE TABLE [dbo].[ScheduledEmails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScheduleDate] datetime  NOT NULL,
    [StartDate] datetime  NULL,
    [CompletedDate] datetime  NULL,
    [EmailId] int  NULL
);
GO

-- Creating table 'ScheduledEmailPersons'
CREATE TABLE [dbo].[ScheduledEmailPersons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScheduledEmailId] int  NOT NULL,
    [PersonId] int  NOT NULL,
    [CompletedDate] datetime  NULL,
    [Enabled] bit  NULL
);
GO

-- Creating table 'Sites'
CREATE TABLE [dbo].[Sites] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SiteName] nvarchar(250)  NOT NULL,
    [FriendlyName] nvarchar(250)  NULL,
    [BrandingColor] nvarchar(10)  NULL,
    [BrandingLogo] varbinary(max)  NULL,
    [BrandingLogoMimeType] nvarchar(50)  NULL,
    [BrandingBackground] varbinary(max)  NULL,
    [BrandingBackgroundMimeType] nvarchar(50)  NULL,
    [OrganizationId] int  NULL
);
GO

-- Creating table 'SiteUsers'
CREATE TABLE [dbo].[SiteUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SiteId] int  NOT NULL,
    [AspNetUsersId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'ZCOExportMaps'
CREATE TABLE [dbo].[ZCOExportMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HoganColName] varchar(500)  NOT NULL,
    [ZCOColName] varchar(500)  NOT NULL,
    [Position] int  NULL
);
GO

-- Creating table 'ZCOExportTemplates'
CREATE TABLE [dbo].[ZCOExportTemplates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TemplateName] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'ZCOExportTemplateMaps'
CREATE TABLE [dbo].[ZCOExportTemplateMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ZCOExportTemplateId] int  NOT NULL,
    [ZCOExportMapId] int  NOT NULL
);
GO

-- Creating table 'ZCOUserMaps'
CREATE TABLE [dbo].[ZCOUserMaps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RSUser] varchar(100)  NOT NULL,
    [ZCOUser] varchar(100)  NOT NULL,
    [ZCOPwd] varchar(100)  NOT NULL
);
GO

-- Creating table 'tempEvents'
CREATE TABLE [dbo].[tempEvents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [EventTypeId] int  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [ReviewDate] datetime  NULL,
    [EventStatusId] int  NOT NULL
);
GO

-- Creating table 'tempPersonProgramSiteEvents'
CREATE TABLE [dbo].[tempPersonProgramSiteEvents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonId] int  NOT NULL,
    [ProgramSiteEventsId] int  NOT NULL
);
GO

-- Creating table 'vPersonBySites'
CREATE TABLE [dbo].[vPersonBySites] (
    [PersonId] int  NOT NULL,
    [SiteId] int  NOT NULL
);
GO

-- Creating table 'EmailStatus'
CREATE TABLE [dbo].[EmailStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameResxId] int  NOT NULL
);
GO

-- Creating table 'EventStatus'
CREATE TABLE [dbo].[EventStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameResxId] int  NOT NULL
);
GO

-- Creating table 'OrderStatus'
CREATE TABLE [dbo].[OrderStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'vDashboard'
CREATE TABLE [dbo].[vDashboard] (
    [Id] int  NOT NULL,
    [PersonId] int  NOT NULL,
    [EventId] int  NOT NULL,
    [HPIDate] varchar(50)  NULL,
    [HDSDate] varchar(50)  NULL,
    [MVPIDate] varchar(50)  NULL,
    [LastUpdated] datetime  NULL
);
GO

-- Creating table 'Organizations'
CREATE TABLE [dbo].[Organizations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrganizationName] nvarchar(150)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId], [LoginProvider], [ProviderKey] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([UserId], [LoginProvider], [ProviderKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AssessmentOptions'
ALTER TABLE [dbo].[AssessmentOptions]
ADD CONSTRAINT [PK_AssessmentOptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AssessmentQuestions'
ALTER TABLE [dbo].[AssessmentQuestions]
ADD CONSTRAINT [PK_AssessmentQuestions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AssessmentResponses'
ALTER TABLE [dbo].[AssessmentResponses]
ADD CONSTRAINT [PK_AssessmentResponses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AssessmentResponseQuestionOptions'
ALTER TABLE [dbo].[AssessmentResponseQuestionOptions]
ADD CONSTRAINT [PK_AssessmentResponseQuestionOptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cultures'
ALTER TABLE [dbo].[Cultures]
ADD CONSTRAINT [PK_Cultures]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [PK_Emails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmailTypes'
ALTER TABLE [dbo].[EmailTypes]
ADD CONSTRAINT [PK_EmailTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventPracticeReports'
ALTER TABLE [dbo].[EventPracticeReports]
ADD CONSTRAINT [PK_EventPracticeReports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventTypes'
ALTER TABLE [dbo].[EventTypes]
ADD CONSTRAINT [PK_EventTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HoganFields'
ALTER TABLE [dbo].[HoganFields]
ADD CONSTRAINT [PK_HoganFields]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HoganMVPIs'
ALTER TABLE [dbo].[HoganMVPIs]
ADD CONSTRAINT [PK_HoganMVPIs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HoganUserInfoes'
ALTER TABLE [dbo].[HoganUserInfoes]
ADD CONSTRAINT [PK_HoganUserInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Languages'
ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Manual_Hogan_Import'
ALTER TABLE [dbo].[Manual_Hogan_Import]
ADD CONSTRAINT [PK_Manual_Hogan_Import]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderForms'
ALTER TABLE [dbo].[OrderForms]
ADD CONSTRAINT [PK_OrderForms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderFormPracticeReports'
ALTER TABLE [dbo].[OrderFormPracticeReports]
ADD CONSTRAINT [PK_OrderFormPracticeReports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonEmails'
ALTER TABLE [dbo].[PersonEmails]
ADD CONSTRAINT [PK_PersonEmails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonEvents'
ALTER TABLE [dbo].[PersonEvents]
ADD CONSTRAINT [PK_PersonEvents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonPracticeReports'
ALTER TABLE [dbo].[PersonPracticeReports]
ADD CONSTRAINT [PK_PersonPracticeReports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PracticeCategories'
ALTER TABLE [dbo].[PracticeCategories]
ADD CONSTRAINT [PK_PracticeCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PracticeCategoryParagraphs'
ALTER TABLE [dbo].[PracticeCategoryParagraphs]
ADD CONSTRAINT [PK_PracticeCategoryParagraphs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PracticeCategoryScales'
ALTER TABLE [dbo].[PracticeCategoryScales]
ADD CONSTRAINT [PK_PracticeCategoryScales]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PracticeLevels'
ALTER TABLE [dbo].[PracticeLevels]
ADD CONSTRAINT [PK_PracticeLevels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PracticeParagraphs'
ALTER TABLE [dbo].[PracticeParagraphs]
ADD CONSTRAINT [PK_PracticeParagraphs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PracticeReports'
ALTER TABLE [dbo].[PracticeReports]
ADD CONSTRAINT [PK_PracticeReports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PracticeScales'
ALTER TABLE [dbo].[PracticeScales]
ADD CONSTRAINT [PK_PracticeScales]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PracticeScaleReports'
ALTER TABLE [dbo].[PracticeScaleReports]
ADD CONSTRAINT [PK_PracticeScaleReports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PracticeTexts'
ALTER TABLE [dbo].[PracticeTexts]
ADD CONSTRAINT [PK_PracticeTexts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PracticeTextOptions'
ALTER TABLE [dbo].[PracticeTextOptions]
ADD CONSTRAINT [PK_PracticeTextOptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [PK_Programs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProgramPracticeReports'
ALTER TABLE [dbo].[ProgramPracticeReports]
ADD CONSTRAINT [PK_ProgramPracticeReports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProgramSiteHoganMVPIs'
ALTER TABLE [dbo].[ProgramSiteHoganMVPIs]
ADD CONSTRAINT [PK_ProgramSiteHoganMVPIs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProgramSites'
ALTER TABLE [dbo].[ProgramSites]
ADD CONSTRAINT [PK_ProgramSites]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReplacementExpressions'
ALTER TABLE [dbo].[ReplacementExpressions]
ADD CONSTRAINT [PK_ReplacementExpressions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Resxes'
ALTER TABLE [dbo].[Resxes]
ADD CONSTRAINT [PK_Resxes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ResxValues'
ALTER TABLE [dbo].[ResxValues]
ADD CONSTRAINT [PK_ResxValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ScheduledEmails'
ALTER TABLE [dbo].[ScheduledEmails]
ADD CONSTRAINT [PK_ScheduledEmails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ScheduledEmailPersons'
ALTER TABLE [dbo].[ScheduledEmailPersons]
ADD CONSTRAINT [PK_ScheduledEmailPersons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sites'
ALTER TABLE [dbo].[Sites]
ADD CONSTRAINT [PK_Sites]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SiteUsers'
ALTER TABLE [dbo].[SiteUsers]
ADD CONSTRAINT [PK_SiteUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ZCOExportMaps'
ALTER TABLE [dbo].[ZCOExportMaps]
ADD CONSTRAINT [PK_ZCOExportMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ZCOExportTemplates'
ALTER TABLE [dbo].[ZCOExportTemplates]
ADD CONSTRAINT [PK_ZCOExportTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ZCOExportTemplateMaps'
ALTER TABLE [dbo].[ZCOExportTemplateMaps]
ADD CONSTRAINT [PK_ZCOExportTemplateMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ZCOUserMaps'
ALTER TABLE [dbo].[ZCOUserMaps]
ADD CONSTRAINT [PK_ZCOUserMaps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [Name], [Description], [EventTypeId], [EventStatusId] in table 'tempEvents'
ALTER TABLE [dbo].[tempEvents]
ADD CONSTRAINT [PK_tempEvents]
    PRIMARY KEY CLUSTERED ([Id], [Name], [Description], [EventTypeId], [EventStatusId] ASC);
GO

-- Creating primary key on [Id], [PersonId], [ProgramSiteEventsId] in table 'tempPersonProgramSiteEvents'
ALTER TABLE [dbo].[tempPersonProgramSiteEvents]
ADD CONSTRAINT [PK_tempPersonProgramSiteEvents]
    PRIMARY KEY CLUSTERED ([Id], [PersonId], [ProgramSiteEventsId] ASC);
GO

-- Creating primary key on [PersonId], [SiteId] in table 'vPersonBySites'
ALTER TABLE [dbo].[vPersonBySites]
ADD CONSTRAINT [PK_vPersonBySites]
    PRIMARY KEY CLUSTERED ([PersonId], [SiteId] ASC);
GO

-- Creating primary key on [Id] in table 'EmailStatus'
ALTER TABLE [dbo].[EmailStatus]
ADD CONSTRAINT [PK_EmailStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventStatus'
ALTER TABLE [dbo].[EventStatus]
ADD CONSTRAINT [PK_EventStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderStatus'
ALTER TABLE [dbo].[OrderStatus]
ADD CONSTRAINT [PK_OrderStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [PersonId], [EventId] in table 'vDashboard'
ALTER TABLE [dbo].[vDashboard]
ADD CONSTRAINT [PK_vDashboard]
    PRIMARY KEY CLUSTERED ([Id], [PersonId], [EventId] ASC);
GO

-- Creating primary key on [Id] in table 'Organizations'
ALTER TABLE [dbo].[Organizations]
ADD CONSTRAINT [PK_Organizations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_User_Id]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_User_Id'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_User_Id]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsersId] in table 'PersonPracticeReports'
ALTER TABLE [dbo].[PersonPracticeReports]
ADD CONSTRAINT [FK_PersonPracticeReport_AspNetUsers]
    FOREIGN KEY ([AspNetUsersId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPracticeReport_AspNetUsers'
CREATE INDEX [IX_FK_PersonPracticeReport_AspNetUsers]
ON [dbo].[PersonPracticeReports]
    ([AspNetUsersId]);
GO

-- Creating foreign key on [AspNetUsersId] in table 'SiteUsers'
ALTER TABLE [dbo].[SiteUsers]
ADD CONSTRAINT [FK_SiteUsers_AspNetUsers]
    FOREIGN KEY ([AspNetUsersId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SiteUsers_AspNetUsers'
CREATE INDEX [IX_FK_SiteUsers_AspNetUsers]
ON [dbo].[SiteUsers]
    ([AspNetUsersId]);
GO

-- Creating foreign key on [AssessmentOptionId] in table 'AssessmentResponseQuestionOptions'
ALTER TABLE [dbo].[AssessmentResponseQuestionOptions]
ADD CONSTRAINT [FK_AssessmentResponseQuestionOption_AssessmentOption]
    FOREIGN KEY ([AssessmentOptionId])
    REFERENCES [dbo].[AssessmentOptions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AssessmentResponseQuestionOption_AssessmentOption'
CREATE INDEX [IX_FK_AssessmentResponseQuestionOption_AssessmentOption]
ON [dbo].[AssessmentResponseQuestionOptions]
    ([AssessmentOptionId]);
GO

-- Creating foreign key on [AssessmentQuestionId] in table 'AssessmentResponseQuestionOptions'
ALTER TABLE [dbo].[AssessmentResponseQuestionOptions]
ADD CONSTRAINT [FK_AssessmentResponseQuestionOption_AssessmentQuestion]
    FOREIGN KEY ([AssessmentQuestionId])
    REFERENCES [dbo].[AssessmentQuestions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AssessmentResponseQuestionOption_AssessmentQuestion'
CREATE INDEX [IX_FK_AssessmentResponseQuestionOption_AssessmentQuestion]
ON [dbo].[AssessmentResponseQuestionOptions]
    ([AssessmentQuestionId]);
GO

-- Creating foreign key on [SiteUserId] in table 'AssessmentResponses'
ALTER TABLE [dbo].[AssessmentResponses]
ADD CONSTRAINT [FK_AssessmentResponse_SiteUsers]
    FOREIGN KEY ([SiteUserId])
    REFERENCES [dbo].[SiteUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AssessmentResponse_SiteUsers'
CREATE INDEX [IX_FK_AssessmentResponse_SiteUsers]
ON [dbo].[AssessmentResponses]
    ([SiteUserId]);
GO

-- Creating foreign key on [AssessmentResponseId] in table 'AssessmentResponseQuestionOptions'
ALTER TABLE [dbo].[AssessmentResponseQuestionOptions]
ADD CONSTRAINT [FK_AssessmentResponseQuestionOption_AssessmentResponse]
    FOREIGN KEY ([AssessmentResponseId])
    REFERENCES [dbo].[AssessmentResponses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AssessmentResponseQuestionOption_AssessmentResponse'
CREATE INDEX [IX_FK_AssessmentResponseQuestionOption_AssessmentResponse]
ON [dbo].[AssessmentResponseQuestionOptions]
    ([AssessmentResponseId]);
GO

-- Creating foreign key on [CultureId] in table 'ResxValues'
ALTER TABLE [dbo].[ResxValues]
ADD CONSTRAINT [FK_ResxValue_Culture]
    FOREIGN KEY ([CultureId])
    REFERENCES [dbo].[Cultures]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResxValue_Culture'
CREATE INDEX [IX_FK_ResxValue_Culture]
ON [dbo].[ResxValues]
    ([CultureId]);
GO

-- Creating foreign key on [EmailTypeId] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [FK_Email_EmailType]
    FOREIGN KEY ([EmailTypeId])
    REFERENCES [dbo].[EmailTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Email_EmailType'
CREATE INDEX [IX_FK_Email_EmailType]
ON [dbo].[Emails]
    ([EmailTypeId]);
GO

-- Creating foreign key on [EventId] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [FK_Email_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Email_Event'
CREATE INDEX [IX_FK_Email_Event]
ON [dbo].[Emails]
    ([EventId]);
GO

-- Creating foreign key on [EmailId] in table 'PersonEmails'
ALTER TABLE [dbo].[PersonEmails]
ADD CONSTRAINT [FK_PersonEmail_Email]
    FOREIGN KEY ([EmailId])
    REFERENCES [dbo].[Emails]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEmail_Email'
CREATE INDEX [IX_FK_PersonEmail_Email]
ON [dbo].[PersonEmails]
    ([EmailId]);
GO

-- Creating foreign key on [EmailId] in table 'ScheduledEmails'
ALTER TABLE [dbo].[ScheduledEmails]
ADD CONSTRAINT [FK_ScheduledEmail_Email]
    FOREIGN KEY ([EmailId])
    REFERENCES [dbo].[Emails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScheduledEmail_Email'
CREATE INDEX [IX_FK_ScheduledEmail_Email]
ON [dbo].[ScheduledEmails]
    ([EmailId]);
GO

-- Creating foreign key on [NameResxId] in table 'EmailTypes'
ALTER TABLE [dbo].[EmailTypes]
ADD CONSTRAINT [FK_EmailType_Resx]
    FOREIGN KEY ([NameResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmailType_Resx'
CREATE INDEX [IX_FK_EmailType_Resx]
ON [dbo].[EmailTypes]
    ([NameResxId]);
GO

-- Creating foreign key on [EventTypeId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_Event_EventType]
    FOREIGN KEY ([EventTypeId])
    REFERENCES [dbo].[EventTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_EventType'
CREATE INDEX [IX_FK_Event_EventType]
ON [dbo].[Events]
    ([EventTypeId]);
GO

-- Creating foreign key on [ProgramId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_Event_Program]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_Program'
CREATE INDEX [IX_FK_Event_Program]
ON [dbo].[Events]
    ([ProgramId]);
GO

-- Creating foreign key on [ProgramSitesId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_Event_ProgramSites]
    FOREIGN KEY ([ProgramSitesId])
    REFERENCES [dbo].[ProgramSites]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_ProgramSites'
CREATE INDEX [IX_FK_Event_ProgramSites]
ON [dbo].[Events]
    ([ProgramSitesId]);
GO

-- Creating foreign key on [SiteId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_Event_Site]
    FOREIGN KEY ([SiteId])
    REFERENCES [dbo].[Sites]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_Site'
CREATE INDEX [IX_FK_Event_Site]
ON [dbo].[Events]
    ([SiteId]);
GO

-- Creating foreign key on [EventId] in table 'EventPracticeReports'
ALTER TABLE [dbo].[EventPracticeReports]
ADD CONSTRAINT [FK_EventPracticeReport_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventPracticeReport_Event'
CREATE INDEX [IX_FK_EventPracticeReport_Event]
ON [dbo].[EventPracticeReports]
    ([EventId]);
GO

-- Creating foreign key on [EventId] in table 'PersonEvents'
ALTER TABLE [dbo].[PersonEvents]
ADD CONSTRAINT [FK_PersonEvent_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEvent_Event'
CREATE INDEX [IX_FK_PersonEvent_Event]
ON [dbo].[PersonEvents]
    ([EventId]);
GO

-- Creating foreign key on [EventId] in table 'PersonPracticeReports'
ALTER TABLE [dbo].[PersonPracticeReports]
ADD CONSTRAINT [FK_PersonPracticeReport_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPracticeReport_Event'
CREATE INDEX [IX_FK_PersonPracticeReport_Event]
ON [dbo].[PersonPracticeReports]
    ([EventId]);
GO

-- Creating foreign key on [IntroductionResxId] in table 'EventPracticeReports'
ALTER TABLE [dbo].[EventPracticeReports]
ADD CONSTRAINT [FK_EventPracticeReport_IntroductionResx]
    FOREIGN KEY ([IntroductionResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventPracticeReport_IntroductionResx'
CREATE INDEX [IX_FK_EventPracticeReport_IntroductionResx]
ON [dbo].[EventPracticeReports]
    ([IntroductionResxId]);
GO

-- Creating foreign key on [IntroductionThreeResxId] in table 'EventPracticeReports'
ALTER TABLE [dbo].[EventPracticeReports]
ADD CONSTRAINT [FK_EventPracticeReport_IntroductionThreeResx]
    FOREIGN KEY ([IntroductionThreeResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventPracticeReport_IntroductionThreeResx'
CREATE INDEX [IX_FK_EventPracticeReport_IntroductionThreeResx]
ON [dbo].[EventPracticeReports]
    ([IntroductionThreeResxId]);
GO

-- Creating foreign key on [IntroductionTwoResxId] in table 'EventPracticeReports'
ALTER TABLE [dbo].[EventPracticeReports]
ADD CONSTRAINT [FK_EventPracticeReport_IntroductionTwoResx]
    FOREIGN KEY ([IntroductionTwoResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventPracticeReport_IntroductionTwoResx'
CREATE INDEX [IX_FK_EventPracticeReport_IntroductionTwoResx]
ON [dbo].[EventPracticeReports]
    ([IntroductionTwoResxId]);
GO

-- Creating foreign key on [PracticeReportId] in table 'EventPracticeReports'
ALTER TABLE [dbo].[EventPracticeReports]
ADD CONSTRAINT [FK_EventPracticeReport_PracticeReport]
    FOREIGN KEY ([PracticeReportId])
    REFERENCES [dbo].[PracticeReports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventPracticeReport_PracticeReport'
CREATE INDEX [IX_FK_EventPracticeReport_PracticeReport]
ON [dbo].[EventPracticeReports]
    ([PracticeReportId]);
GO

-- Creating foreign key on [ReportTitleResxId] in table 'EventPracticeReports'
ALTER TABLE [dbo].[EventPracticeReports]
ADD CONSTRAINT [FK_EventPracticeReport_TitleResx]
    FOREIGN KEY ([ReportTitleResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventPracticeReport_TitleResx'
CREATE INDEX [IX_FK_EventPracticeReport_TitleResx]
ON [dbo].[EventPracticeReports]
    ([ReportTitleResxId]);
GO

-- Creating foreign key on [NameResxId] in table 'EventTypes'
ALTER TABLE [dbo].[EventTypes]
ADD CONSTRAINT [FK_EventType_Resx]
    FOREIGN KEY ([NameResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventType_Resx'
CREATE INDEX [IX_FK_EventType_Resx]
ON [dbo].[EventTypes]
    ([NameResxId]);
GO

-- Creating foreign key on [EventTypeId] in table 'OrderForms'
ALTER TABLE [dbo].[OrderForms]
ADD CONSTRAINT [FK_OrderForm_EventType]
    FOREIGN KEY ([EventTypeId])
    REFERENCES [dbo].[EventTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderForm_EventType'
CREATE INDEX [IX_FK_OrderForm_EventType]
ON [dbo].[OrderForms]
    ([EventTypeId]);
GO

-- Creating foreign key on [DefinitionTextResxId] in table 'HoganFields'
ALTER TABLE [dbo].[HoganFields]
ADD CONSTRAINT [FK_HoganField_Resx]
    FOREIGN KEY ([DefinitionTextResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HoganField_Resx'
CREATE INDEX [IX_FK_HoganField_Resx]
ON [dbo].[HoganFields]
    ([DefinitionTextResxId]);
GO

-- Creating foreign key on [BlurbTextResxId] in table 'HoganFields'
ALTER TABLE [dbo].[HoganFields]
ADD CONSTRAINT [FK_HoganField_Resx2]
    FOREIGN KEY ([BlurbTextResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HoganField_Resx2'
CREATE INDEX [IX_FK_HoganField_Resx2]
ON [dbo].[HoganFields]
    ([BlurbTextResxId]);
GO

-- Creating foreign key on [HoganFieldId] in table 'PracticeCategoryParagraphs'
ALTER TABLE [dbo].[PracticeCategoryParagraphs]
ADD CONSTRAINT [FK_PracticeCategoryParagraphs_HoganField]
    FOREIGN KEY ([HoganFieldId])
    REFERENCES [dbo].[HoganFields]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeCategoryParagraphs_HoganField'
CREATE INDEX [IX_FK_PracticeCategoryParagraphs_HoganField]
ON [dbo].[PracticeCategoryParagraphs]
    ([HoganFieldId]);
GO

-- Creating foreign key on [HoganFieldId] in table 'PracticeCategoryScales'
ALTER TABLE [dbo].[PracticeCategoryScales]
ADD CONSTRAINT [FK_PracticeCategoryScale_HoganField]
    FOREIGN KEY ([HoganFieldId])
    REFERENCES [dbo].[HoganFields]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeCategoryScale_HoganField'
CREATE INDEX [IX_FK_PracticeCategoryScale_HoganField]
ON [dbo].[PracticeCategoryScales]
    ([HoganFieldId]);
GO

-- Creating foreign key on [HoganFieldId] in table 'PracticeScales'
ALTER TABLE [dbo].[PracticeScales]
ADD CONSTRAINT [FK_PracticeScale_HoganField]
    FOREIGN KEY ([HoganFieldId])
    REFERENCES [dbo].[HoganFields]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeScale_HoganField'
CREATE INDEX [IX_FK_PracticeScale_HoganField]
ON [dbo].[PracticeScales]
    ([HoganFieldId]);
GO

-- Creating foreign key on [ProgramId] in table 'HoganUserInfoes'
ALTER TABLE [dbo].[HoganUserInfoes]
ADD CONSTRAINT [FK_HoganUserInfo_Program]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HoganUserInfo_Program'
CREATE INDEX [IX_FK_HoganUserInfo_Program]
ON [dbo].[HoganUserInfoes]
    ([ProgramId]);
GO

-- Creating foreign key on [ProgramId] in table 'OrderForms'
ALTER TABLE [dbo].[OrderForms]
ADD CONSTRAINT [FK_OrderForm_Program]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderForm_Program'
CREATE INDEX [IX_FK_OrderForm_Program]
ON [dbo].[OrderForms]
    ([ProgramId]);
GO

-- Creating foreign key on [OrderFormId] in table 'OrderFormPracticeReports'
ALTER TABLE [dbo].[OrderFormPracticeReports]
ADD CONSTRAINT [FK_OrderFormPracticeReport_OrderForm]
    FOREIGN KEY ([OrderFormId])
    REFERENCES [dbo].[OrderForms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderFormPracticeReport_OrderForm'
CREATE INDEX [IX_FK_OrderFormPracticeReport_OrderForm]
ON [dbo].[OrderFormPracticeReports]
    ([OrderFormId]);
GO

-- Creating foreign key on [PracticeReportId] in table 'OrderFormPracticeReports'
ALTER TABLE [dbo].[OrderFormPracticeReports]
ADD CONSTRAINT [FK_OrderFormPracticeReport_PracticeReport]
    FOREIGN KEY ([PracticeReportId])
    REFERENCES [dbo].[PracticeReports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderFormPracticeReport_PracticeReport'
CREATE INDEX [IX_FK_OrderFormPracticeReport_PracticeReport]
ON [dbo].[OrderFormPracticeReports]
    ([PracticeReportId]);
GO

-- Creating foreign key on [PersonId] in table 'PersonEmails'
ALTER TABLE [dbo].[PersonEmails]
ADD CONSTRAINT [FK_PersonEmail_Person]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEmail_Person'
CREATE INDEX [IX_FK_PersonEmail_Person]
ON [dbo].[PersonEmails]
    ([PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'PersonEvents'
ALTER TABLE [dbo].[PersonEvents]
ADD CONSTRAINT [FK_PersonEvent_Person]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEvent_Person'
CREATE INDEX [IX_FK_PersonEvent_Person]
ON [dbo].[PersonEvents]
    ([PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'PersonPracticeReports'
ALTER TABLE [dbo].[PersonPracticeReports]
ADD CONSTRAINT [FK_PersonPracticeReport_Person]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPracticeReport_Person'
CREATE INDEX [IX_FK_PersonPracticeReport_Person]
ON [dbo].[PersonPracticeReports]
    ([PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'ScheduledEmailPersons'
ALTER TABLE [dbo].[ScheduledEmailPersons]
ADD CONSTRAINT [FK_ScheduledEmailPerson_Person]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScheduledEmailPerson_Person'
CREATE INDEX [IX_FK_ScheduledEmailPerson_Person]
ON [dbo].[ScheduledEmailPersons]
    ([PersonId]);
GO

-- Creating foreign key on [PracticeReportId] in table 'PersonPracticeReports'
ALTER TABLE [dbo].[PersonPracticeReports]
ADD CONSTRAINT [FK_PersonPracticeReport_PracticeReport]
    FOREIGN KEY ([PracticeReportId])
    REFERENCES [dbo].[PracticeReports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPracticeReport_PracticeReport'
CREATE INDEX [IX_FK_PersonPracticeReport_PracticeReport]
ON [dbo].[PersonPracticeReports]
    ([PracticeReportId]);
GO

-- Creating foreign key on [PracticeCategoryId] in table 'PracticeCategoryParagraphs'
ALTER TABLE [dbo].[PracticeCategoryParagraphs]
ADD CONSTRAINT [FK_PracticeCategoryParagraphs_PracticeCategory]
    FOREIGN KEY ([PracticeCategoryId])
    REFERENCES [dbo].[PracticeCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeCategoryParagraphs_PracticeCategory'
CREATE INDEX [IX_FK_PracticeCategoryParagraphs_PracticeCategory]
ON [dbo].[PracticeCategoryParagraphs]
    ([PracticeCategoryId]);
GO

-- Creating foreign key on [PracticeCategoryId] in table 'PracticeCategoryScales'
ALTER TABLE [dbo].[PracticeCategoryScales]
ADD CONSTRAINT [FK_PracticeCategoryScale_PracticeCategory]
    FOREIGN KEY ([PracticeCategoryId])
    REFERENCES [dbo].[PracticeCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeCategoryScale_PracticeCategory'
CREATE INDEX [IX_FK_PracticeCategoryScale_PracticeCategory]
ON [dbo].[PracticeCategoryScales]
    ([PracticeCategoryId]);
GO

-- Creating foreign key on [PracticeCategoryId] in table 'PracticeScales'
ALTER TABLE [dbo].[PracticeScales]
ADD CONSTRAINT [FK_PracticeScale_PracticeCategory]
    FOREIGN KEY ([PracticeCategoryId])
    REFERENCES [dbo].[PracticeCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeScale_PracticeCategory'
CREATE INDEX [IX_FK_PracticeScale_PracticeCategory]
ON [dbo].[PracticeScales]
    ([PracticeCategoryId]);
GO

-- Creating foreign key on [ProgramId] in table 'PracticeCategoryScales'
ALTER TABLE [dbo].[PracticeCategoryScales]
ADD CONSTRAINT [FK_PracticeCategoryScale_Program]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeCategoryScale_Program'
CREATE INDEX [IX_FK_PracticeCategoryScale_Program]
ON [dbo].[PracticeCategoryScales]
    ([ProgramId]);
GO

-- Creating foreign key on [PracticeLevelId] in table 'PracticeScales'
ALTER TABLE [dbo].[PracticeScales]
ADD CONSTRAINT [FK_PracticeScale_PracticeLevel]
    FOREIGN KEY ([PracticeLevelId])
    REFERENCES [dbo].[PracticeLevels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeScale_PracticeLevel'
CREATE INDEX [IX_FK_PracticeScale_PracticeLevel]
ON [dbo].[PracticeScales]
    ([PracticeLevelId]);
GO

-- Creating foreign key on [PracticeReportId] in table 'PracticeParagraphs'
ALTER TABLE [dbo].[PracticeParagraphs]
ADD CONSTRAINT [FK_PracticeParagraphs_PracticeReport]
    FOREIGN KEY ([PracticeReportId])
    REFERENCES [dbo].[PracticeReports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeParagraphs_PracticeReport'
CREATE INDEX [IX_FK_PracticeParagraphs_PracticeReport]
ON [dbo].[PracticeParagraphs]
    ([PracticeReportId]);
GO

-- Creating foreign key on [IntroductionResxId] in table 'PracticeReports'
ALTER TABLE [dbo].[PracticeReports]
ADD CONSTRAINT [FK_PracticeReport_IntroductionResx]
    FOREIGN KEY ([IntroductionResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeReport_IntroductionResx'
CREATE INDEX [IX_FK_PracticeReport_IntroductionResx]
ON [dbo].[PracticeReports]
    ([IntroductionResxId]);
GO

-- Creating foreign key on [IntroductionThreeResxId] in table 'PracticeReports'
ALTER TABLE [dbo].[PracticeReports]
ADD CONSTRAINT [FK_PracticeReport_IntroductionThreeResx]
    FOREIGN KEY ([IntroductionThreeResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeReport_IntroductionThreeResx'
CREATE INDEX [IX_FK_PracticeReport_IntroductionThreeResx]
ON [dbo].[PracticeReports]
    ([IntroductionThreeResxId]);
GO

-- Creating foreign key on [IntroductionTwoResxId] in table 'PracticeReports'
ALTER TABLE [dbo].[PracticeReports]
ADD CONSTRAINT [FK_PracticeReport_IntroductionTwoResx]
    FOREIGN KEY ([IntroductionTwoResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeReport_IntroductionTwoResx'
CREATE INDEX [IX_FK_PracticeReport_IntroductionTwoResx]
ON [dbo].[PracticeReports]
    ([IntroductionTwoResxId]);
GO

-- Creating foreign key on [ReportTitleResxId] in table 'PracticeReports'
ALTER TABLE [dbo].[PracticeReports]
ADD CONSTRAINT [FK_PracticeReport_Resx]
    FOREIGN KEY ([ReportTitleResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeReport_Resx'
CREATE INDEX [IX_FK_PracticeReport_Resx]
ON [dbo].[PracticeReports]
    ([ReportTitleResxId]);
GO

-- Creating foreign key on [PracticeReportId] in table 'PracticeScaleReports'
ALTER TABLE [dbo].[PracticeScaleReports]
ADD CONSTRAINT [FK_PracticeScaleReport_PracticeReport]
    FOREIGN KEY ([PracticeReportId])
    REFERENCES [dbo].[PracticeReports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeScaleReport_PracticeReport'
CREATE INDEX [IX_FK_PracticeScaleReport_PracticeReport]
ON [dbo].[PracticeScaleReports]
    ([PracticeReportId]);
GO

-- Creating foreign key on [PracticeReportId] in table 'ProgramPracticeReports'
ALTER TABLE [dbo].[ProgramPracticeReports]
ADD CONSTRAINT [FK_ProgramPracticeReports_PracticeReport]
    FOREIGN KEY ([PracticeReportId])
    REFERENCES [dbo].[PracticeReports]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramPracticeReports_PracticeReport'
CREATE INDEX [IX_FK_ProgramPracticeReports_PracticeReport]
ON [dbo].[ProgramPracticeReports]
    ([PracticeReportId]);
GO

-- Creating foreign key on [ProgramId] in table 'PracticeScales'
ALTER TABLE [dbo].[PracticeScales]
ADD CONSTRAINT [FK_PracticeScale_Program]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeScale_Program'
CREATE INDEX [IX_FK_PracticeScale_Program]
ON [dbo].[PracticeScales]
    ([ProgramId]);
GO

-- Creating foreign key on [PracticeScaleId] in table 'PracticeScaleReports'
ALTER TABLE [dbo].[PracticeScaleReports]
ADD CONSTRAINT [FK_PracticeScaleReport_PracticeScale]
    FOREIGN KEY ([PracticeScaleId])
    REFERENCES [dbo].[PracticeScales]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeScaleReport_PracticeScale'
CREATE INDEX [IX_FK_PracticeScaleReport_PracticeScale]
ON [dbo].[PracticeScaleReports]
    ([PracticeScaleId]);
GO

-- Creating foreign key on [PracticeTextId] in table 'PracticeScaleReports'
ALTER TABLE [dbo].[PracticeScaleReports]
ADD CONSTRAINT [FK_PracticeScaleReport_PracticeText]
    FOREIGN KEY ([PracticeTextId])
    REFERENCES [dbo].[PracticeTexts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeScaleReport_PracticeText'
CREATE INDEX [IX_FK_PracticeScaleReport_PracticeText]
ON [dbo].[PracticeScaleReports]
    ([PracticeTextId]);
GO

-- Creating foreign key on [InterviewPracticeTextId] in table 'PracticeScaleReports'
ALTER TABLE [dbo].[PracticeScaleReports]
ADD CONSTRAINT [FK_PracticeScaleReport_PracticeText2]
    FOREIGN KEY ([InterviewPracticeTextId])
    REFERENCES [dbo].[PracticeTexts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeScaleReport_PracticeText2'
CREATE INDEX [IX_FK_PracticeScaleReport_PracticeText2]
ON [dbo].[PracticeScaleReports]
    ([InterviewPracticeTextId]);
GO

-- Creating foreign key on [ParentPracticeTextId] in table 'PracticeTexts'
ALTER TABLE [dbo].[PracticeTexts]
ADD CONSTRAINT [FK_PracticeText_PracticeText]
    FOREIGN KEY ([ParentPracticeTextId])
    REFERENCES [dbo].[PracticeTexts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeText_PracticeText'
CREATE INDEX [IX_FK_PracticeText_PracticeText]
ON [dbo].[PracticeTexts]
    ([ParentPracticeTextId]);
GO

-- Creating foreign key on [TextResxId] in table 'PracticeTexts'
ALTER TABLE [dbo].[PracticeTexts]
ADD CONSTRAINT [FK_PracticeText_Text]
    FOREIGN KEY ([TextResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeText_Text'
CREATE INDEX [IX_FK_PracticeText_Text]
ON [dbo].[PracticeTexts]
    ([TextResxId]);
GO

-- Creating foreign key on [AlternativePracticeTextId] in table 'PracticeTextOptions'
ALTER TABLE [dbo].[PracticeTextOptions]
ADD CONSTRAINT [FK_PracticeTextOption_AlternativePracticeText]
    FOREIGN KEY ([AlternativePracticeTextId])
    REFERENCES [dbo].[PracticeTexts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeTextOption_AlternativePracticeText'
CREATE INDEX [IX_FK_PracticeTextOption_AlternativePracticeText]
ON [dbo].[PracticeTextOptions]
    ([AlternativePracticeTextId]);
GO

-- Creating foreign key on [PracticeTextId] in table 'PracticeTextOptions'
ALTER TABLE [dbo].[PracticeTextOptions]
ADD CONSTRAINT [FK_PracticeTextOption_PracticeText]
    FOREIGN KEY ([PracticeTextId])
    REFERENCES [dbo].[PracticeTexts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PracticeTextOption_PracticeText'
CREATE INDEX [IX_FK_PracticeTextOption_PracticeText]
ON [dbo].[PracticeTextOptions]
    ([PracticeTextId]);
GO

-- Creating foreign key on [ProgramId] in table 'ProgramPracticeReports'
ALTER TABLE [dbo].[ProgramPracticeReports]
ADD CONSTRAINT [FK_ProgramPracticeReports_Program]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramPracticeReports_Program'
CREATE INDEX [IX_FK_ProgramPracticeReports_Program]
ON [dbo].[ProgramPracticeReports]
    ([ProgramId]);
GO

-- Creating foreign key on [ProgramId] in table 'ProgramSites'
ALTER TABLE [dbo].[ProgramSites]
ADD CONSTRAINT [FK_ResidencyProgramSitess_Program]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidencyProgramSitess_Program'
CREATE INDEX [IX_FK_ResidencyProgramSitess_Program]
ON [dbo].[ProgramSites]
    ([ProgramId]);
GO

-- Creating foreign key on [ProgramSiteId] in table 'ProgramSiteHoganMVPIs'
ALTER TABLE [dbo].[ProgramSiteHoganMVPIs]
ADD CONSTRAINT [FK_ProgramSiteHoganMVPI_ResidencyProgramHospitals]
    FOREIGN KEY ([ProgramSiteId])
    REFERENCES [dbo].[ProgramSites]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramSiteHoganMVPI_ResidencyProgramHospitals'
CREATE INDEX [IX_FK_ProgramSiteHoganMVPI_ResidencyProgramHospitals]
ON [dbo].[ProgramSiteHoganMVPIs]
    ([ProgramSiteId]);
GO

-- Creating foreign key on [SiteId] in table 'ProgramSites'
ALTER TABLE [dbo].[ProgramSites]
ADD CONSTRAINT [FK_ResidencyProgramSites_Site]
    FOREIGN KEY ([SiteId])
    REFERENCES [dbo].[Sites]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResidencyProgramSites_Site'
CREATE INDEX [IX_FK_ResidencyProgramSites_Site]
ON [dbo].[ProgramSites]
    ([SiteId]);
GO

-- Creating foreign key on [ResxId] in table 'ResxValues'
ALTER TABLE [dbo].[ResxValues]
ADD CONSTRAINT [FK_ResxValue_Resx]
    FOREIGN KEY ([ResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResxValue_Resx'
CREATE INDEX [IX_FK_ResxValue_Resx]
ON [dbo].[ResxValues]
    ([ResxId]);
GO

-- Creating foreign key on [ScheduledEmailId] in table 'ScheduledEmailPersons'
ALTER TABLE [dbo].[ScheduledEmailPersons]
ADD CONSTRAINT [FK_ScheduledEmailPerson_ScheduledEmail]
    FOREIGN KEY ([ScheduledEmailId])
    REFERENCES [dbo].[ScheduledEmails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScheduledEmailPerson_ScheduledEmail'
CREATE INDEX [IX_FK_ScheduledEmailPerson_ScheduledEmail]
ON [dbo].[ScheduledEmailPersons]
    ([ScheduledEmailId]);
GO

-- Creating foreign key on [SiteId] in table 'SiteUsers'
ALTER TABLE [dbo].[SiteUsers]
ADD CONSTRAINT [FK_SiteUsers_Site]
    FOREIGN KEY ([SiteId])
    REFERENCES [dbo].[Sites]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SiteUsers_Site'
CREATE INDEX [IX_FK_SiteUsers_Site]
ON [dbo].[SiteUsers]
    ([SiteId]);
GO

-- Creating foreign key on [ZCOExportMapId] in table 'ZCOExportTemplateMaps'
ALTER TABLE [dbo].[ZCOExportTemplateMaps]
ADD CONSTRAINT [FK_ZCOExportTemplateMap_ZCOExportMap]
    FOREIGN KEY ([ZCOExportMapId])
    REFERENCES [dbo].[ZCOExportMaps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ZCOExportTemplateMap_ZCOExportMap'
CREATE INDEX [IX_FK_ZCOExportTemplateMap_ZCOExportMap]
ON [dbo].[ZCOExportTemplateMaps]
    ([ZCOExportMapId]);
GO

-- Creating foreign key on [ZCOExportTemplateId] in table 'ZCOExportTemplateMaps'
ALTER TABLE [dbo].[ZCOExportTemplateMaps]
ADD CONSTRAINT [FK_ZCOExportTemplateMap_ZCOExportTemplate]
    FOREIGN KEY ([ZCOExportTemplateId])
    REFERENCES [dbo].[ZCOExportTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ZCOExportTemplateMap_ZCOExportTemplate'
CREATE INDEX [IX_FK_ZCOExportTemplateMap_ZCOExportTemplate]
ON [dbo].[ZCOExportTemplateMaps]
    ([ZCOExportTemplateId]);
GO

-- Creating foreign key on [NameResxId] in table 'EmailStatus'
ALTER TABLE [dbo].[EmailStatus]
ADD CONSTRAINT [FK_EmailStatus_Resx]
    FOREIGN KEY ([NameResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmailStatus_Resx'
CREATE INDEX [IX_FK_EmailStatus_Resx]
ON [dbo].[EmailStatus]
    ([NameResxId]);
GO

-- Creating foreign key on [EmailStatusId] in table 'PersonEmails'
ALTER TABLE [dbo].[PersonEmails]
ADD CONSTRAINT [FK_PersonEmail_EmailStatus]
    FOREIGN KEY ([EmailStatusId])
    REFERENCES [dbo].[EmailStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEmail_EmailStatus'
CREATE INDEX [IX_FK_PersonEmail_EmailStatus]
ON [dbo].[PersonEmails]
    ([EmailStatusId]);
GO

-- Creating foreign key on [EventStatusId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_Event_EventStatus]
    FOREIGN KEY ([EventStatusId])
    REFERENCES [dbo].[EventStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_EventStatus'
CREATE INDEX [IX_FK_Event_EventStatus]
ON [dbo].[Events]
    ([EventStatusId]);
GO

-- Creating foreign key on [NameResxId] in table 'EventStatus'
ALTER TABLE [dbo].[EventStatus]
ADD CONSTRAINT [FK_EventStatus_Resx]
    FOREIGN KEY ([NameResxId])
    REFERENCES [dbo].[Resxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventStatus_Resx'
CREATE INDEX [IX_FK_EventStatus_Resx]
ON [dbo].[EventStatus]
    ([NameResxId]);
GO

-- Creating foreign key on [OrderStatusId] in table 'OrderForms'
ALTER TABLE [dbo].[OrderForms]
ADD CONSTRAINT [FK_OrderForm_OrderStatus]
    FOREIGN KEY ([OrderStatusId])
    REFERENCES [dbo].[OrderStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderForm_OrderStatus'
CREATE INDEX [IX_FK_OrderForm_OrderStatus]
ON [dbo].[OrderForms]
    ([OrderStatusId]);
GO

-- Creating foreign key on [PersonId] in table 'vDashboard'
ALTER TABLE [dbo].[vDashboard]
ADD CONSTRAINT [FK_vDashboardPerson]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_vDashboardPerson'
CREATE INDEX [IX_FK_vDashboardPerson]
ON [dbo].[vDashboard]
    ([PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'vPersonBySites'
ALTER TABLE [dbo].[vPersonBySites]
ADD CONSTRAINT [FK_vPersonBySitePerson]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------