CREATE VIEW [dbo].[vReportTextExcel]
	AS 
	Select top 100 percent
iif(PT2.Id is null,PT.Id,PT2.Id) as [PracticeTextId],
 PC.Name as [Category], PL.Name as [Level], HF.FieldName, PT.IsIntroduction, 
iif(PT2.Id is null,dbo.GetResxValueEX(PT.TextResxId,'en-US') , dbo.GetResxValueEX(PT2.TextResxId,'en-US')) as [TEXT],'' as ReplacementText,
Intros.Text as [Short_Text], '' as ReplacementShort_Text,Intros.PracticeTextId as Short_Text_PracticeTextId,PR.Id
from 
PracticeScaleReport PSR 
Join PracticeScale PS on PSR.PracticeScaleId = PS.Id 
join PracticeReport PR on PSR.PracticeReportId = PR.Id 
join PracticeText PT on PSR.PracticeTextId = PT.Id  
left outer join PracticeTextOption PTO on PT.Id = PTO.PracticeTextId
left outer join PracticeText PT2 on PTO.AlternativePracticeTextId = PT2.Id
join PracticeCategory PC on PS.PracticeCategoryId = PC.Id
join PracticeLevel PL on PS.PracticeLevelId = PL.Id
join HoganField HF on PS.HoganFieldId = HF.Id
join (Select PT.Id as PracticeTextId,PT.ParentPracticeTextId as AltTextId,
iif(PT.ParentPracticeTextId is null,'',dbo.GetResxValueEX(PT.TextResxId,'en-US')) as Text
from
PracticeText PT) as Intros on Intros.AltTextId = PT.Id or Intros.PracticeTextId = PT.Id
order by FieldName,[Level] asc,IsIntroduction desc, TEXT


