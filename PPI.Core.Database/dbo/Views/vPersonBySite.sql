CREATE VIEW [dbo].[vPersonBySite]
	AS SELECT DISTINCT P.Id as PersonId, PS.SiteId 
	FROM [Person] P
	join PersonEvent PE on PE.PersonId = P.Id
	Join [Event] EV on EV.Id = PE.EventId
	Join [ProgramSites] PS on EV.ProgramSitesId = PS.Id
