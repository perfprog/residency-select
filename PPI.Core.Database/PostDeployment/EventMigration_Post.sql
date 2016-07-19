
--Drop the unused tables

IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'PersonProgramSite'))
BEGIN
	DROP TABLE [dbo].[PersonProgramSite]
END




IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND TABLE_NAME = 'vPersonSitesByProgram'))
BEGIN
	Drop View [dbo].[vPersonSitesByProgram]
END

IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND TABLE_NAME = 'vPeopleSitesByProgram'))
BEGIN
	Drop View [dbo].[vPeopleSitesByProgram]
END




IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'PersonProgramSiteEvents'))
BEGIN
	SET IDENTITY_INSERT [dbo].[Event] ON
	insert into [dbo].[Event] (Id, Name,Description,EventTypeId,StartDate,EndDate,ReviewDate,EventStatusId,ProgramSitesId)
	Select
	TE.Id, Name,Description,EventTypeId,StartDate,EndDate,ReviewDate,EventStatusId,PSE.ProgramSitesId
	From 
	tempEvent TE
	join ProgramSiteEvents PSE on PSE.EventId = TE.Id
	SET IDENTITY_INSERT [dbo].[Event] OFF
	
	insert into [dbo].[PersonEvent] (PersonId,EventId)
	Select PPSE.PersonId, PSE.EventId
	from tempPersonProgramSiteEvents PPSE
	join ProgramSiteEvents PSE on PPSE.ProgramSiteEventsId = PSE.Id
	
	DROP TABLE [dbo].[PersonProgramSiteEvents]
END

IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'ProgramSiteEvents'))
BEGIN
	

	DROP TABLE [dbo].[ProgramSiteEvents]
END



--update [PersonPracticeReport] set EventId = i.EventId
--from (SELECT PE.EventId as EventId ,PE.PersonId 
--			FROM [Residency_Select].[dbo].[PersonPracticeReport] PPR 
--			join PersonEvent PE on PPR.PersonId = PE.PersonId) i
--where i.PersonId = [PersonPracticeReport].PersonId




