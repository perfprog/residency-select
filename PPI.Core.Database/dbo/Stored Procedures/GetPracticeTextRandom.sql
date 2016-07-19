CREATE Procedure [dbo].[GetPracticeTextRandom]
	@PracticeTextId int,
	@Culture int,
	@Text nvarchar(max) output
as

	
	set @Text = (	select top 1 dbo.GetResxValue( IIF(PTO.AlternativePracticeTextId is null,PT.TextResxId,PT2.TextResxId),@Culture)
	from PracticeText PT
	left join PracticeTextOption PTO on PTO.PracticeTextId = PT.Id
	left Join PracticeText PT2 on PTO.AlternativePracticeTextId = PT2.Id
	where PT.Id = @PracticeTextId
	order by ABS(CHECKSUM(NewId())) % 14)
RETURN Select @Text
