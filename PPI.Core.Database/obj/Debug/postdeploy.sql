/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
--For First Time Deployments uncomment below data
--------------------------------------------------------------------------------------
--Setup Default Perform User
--:r .\Data\dbo.AspNetRoles.data.sql
--:r .\Data\dbo.AspNetUsers.data.sql
--:r .\Data\dbo.AspNetUserRoles.data.sql

--:r .\Data\dbo.Culture.data.sql
--:r .\Data\dbo.HoganMVPI.data.sql
--:r .\Data\dbo.Site.data.sql
--:r .\Data\dbo.Resx.data.sql
--:r .\Data\dbo.ResxValue.data.sql
--:r .\Data\dbo.HoganField.data.sql
--:r .\Data\dbo.Program.data.sql
--:r .\Data\dbo.PracticeReport.data.sql
--:r .\Data\dbo.PracticeLevel.data.sql
--:r .\Data\dbo.PracticeCategory.data.sql

--:r .\Data\dbo.PracticeScale.data.sql
--:r .\Data\dbo.PracticeText.data.sql
--:r .\Data\dbo.PracticeTextOption.data.sql
--:r .\Data\dbo.PracticeScaleReport.data.sql

--:r .\Data\dbo.ReplacementExpression.data.sql
--:r .\Data\dbo.PracticeCategoryParagraphs.data.sql
--:r .\Data\dbo.PracticeCategoryScale.data.sql
--:r .\Data\dbo.PracticeParagraphs.data.sql

--:r .\Data\dbo.ProgramSites.data.sql
--:r .\Data\dbo.ProgramSiteHoganMVPI.data.sql
--:r .\Data\dbo.ProgramPracticeReports.data.sql
--:r .\Data\dbo.EventStatus.data.sql
--:r .\Data\dbo.EventType.data.sql
--:r .\Data\dbo.EmailStatus.data.sql
--:r .\Data\dbo.EmailType.data.sql

--THIS IS ONCE DO NOT USE ON Databases after 12/2/2014
--:r .\Data\dbo.InterviewQuestion_OneTime.data.sql

-- New Expression update,  do not run on a full restore data is in the data.sql file

-- 12/8/2004
-- INSERT INTO [dbo].[ReplacementExpression] ([FindValue], [Expression]) VALUES (N'<Title>', N'title')
------------------------------------------------------------------------------------------

--1/2 For event migration to new table structures
--:r .\PostDeployment\EventMigration_Post.sql

--1/26

-- :r .\Data\dbo.OrderStatus.data.sql

--Item14 release
-- :r .\PostDeployment\Item14_Release.sql

--HPI / HDS / MVPI Data Reports

--:r .\PostDeployment\DataReport_Migrations.sql

--Zco Portal Scripts
 --:r .\Data\dbo.ZCOExportMap.data.sql
 --:r .\Data\dbo.ZCOExportTemplate.data.sql
 --:r .\Data\dbo.ZCOExportTemplateMap.data.sql
 --:r .\Data\dbo.ZCOUserMap.data.sql


 -- Report Data FIXES
 -- :r .\PostDeployment\ReportDataFixes.sql
GO
