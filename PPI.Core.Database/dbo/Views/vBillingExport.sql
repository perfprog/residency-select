CREATE VIEW [dbo].[vBillingExport]
	AS select 
PR.ProgramName
, EV.Name
, EV.Description
, EV.Billable
, EV.Placement
, CONVERT(VARCHAR(10),EV.StartDate,110) as [StartDate]
, CONVERT(VARCHAR(10),EV.EndDate,110) as [EndDate]
, CONVERT(VARCHAR(10),EV.ReviewDate,110)  as [ReviewDate]
, dbo.GetResxValue(ET.NameResxId,1) as [EventType]
, P.FirstName
, P.LastName
, p.Hogan_Id
, p.AAMCNumber
, p.PrimaryEmail
, MHI.HPIDate as [HPICOMPLETE]
, MHI.HDSDate as [HDSCOMPLETE]
, MHI.MVPIDate as [MVPICOMPLETE]
from Event EV
join EventType ET on EV.EventTypeId = ET.Id
join PersonEvent PE on PE.EventId = EV.Id
join ProgramSites PS on EV.ProgramSitesId = PS.Id
Join Program PR on PS.ProgramId = PR.Id
join Person P on PE.PersonId = P.Id
Left Outer join Manual_Hogan_Import MHI on LTRIM(RTRIM((P.Hogan_Id))) = LTRIM(RTRIM(MHI.Hogan_User_ID))

