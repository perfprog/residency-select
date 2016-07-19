CREATE VIEW [dbo].[vDashboard]
	AS Select PE.*, MHI.HPIDate, MHI.HDSDate, MHI.MVPIDate,MHI.LastUpdated 
from
PersonEvent PE
join Event EV on PE.EventId = EV.Id
join Person P on PE.PersonId = P.Id
Left Outer join Manual_Hogan_Import MHI on LTRIM(RTRIM((P.Hogan_Id))) = LTRIM(RTRIM(MHI.Hogan_User_ID))