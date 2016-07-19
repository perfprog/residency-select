CREATE VIEW [dbo].[vPeopleSitesByProgram]
	AS SELECT [PersonId]
      ,[ProgramSiteId]
      ,H.SiteName
      ,H.id as SiteId
  FROM PersonProgramSite as cp
  join [ProgramSites] ph on ph.id = cp.[ProgramSiteId]
  join [Site] h on ph.SiteId = h.id
