CREATE Procedure [dbo].[GetPracticeTextIntroRandom]
@PracticeTextId int,
	@Culture int,
	@Text nvarchar(max) output
as
	Declare @PrimaryPracticeTextId int

	Declare @IntroTemp Table
	(
		[Statements] nvarchar(max),
		[Text] nvarchar(max)
	);


With TempTable ([Text],PracticeTextID)
	as
	(
		select top 1 dbo.GetResxValue( IIF(PTO.AlternativePracticeTextId is null,PT.TextResxId,PT2.TextResxId),@Culture),PT.Id
		from PracticeText PT
		left join PracticeTextOption PTO on PTO.PracticeTextId = PT.Id
		left Join PracticeText PT2 on PTO.AlternativePracticeTextId = PT2.Id
		where PT.Id = @PracticeTextId
		order by ABS(CHECKSUM(NewId())) % 14
	) 
	Insert into @IntroTemp ([Statements] ,	[Text] )
	Select top 3 dbo.GetResxValue(PT.TextResxId,1) as Statements, TT.[Text] 
	from PracticeText PT
	Join TempTable TT on PT.ParentPracticeTextId = TT.PracticeTextID
	order by ABS(CHECKSUM(NewId())) % 14;

	Declare @Statements nvarchar(max)
	Declare @PrimaryText nvarchar(max)
	Declare @count int = 0

	Declare db_cursor Cursor FOR
		Select * from @IntroTemp


				
	OPEN db_cursor
	FETCH NEXT FROM db_cursor INTO @Statements, @PrimaryText
	WHILE @@FETCH_STATUS = 0		
	Begin		
		if @count = 0
			Set @Text = @PrimaryText + ' ' + @Statements
		else
			set @Text = @Text + ' ' + @Statements
		
		set @count = @count + 1
	FETCH NEXT FROM db_cursor INTO @Statements, @PrimaryText
	End 
	
	CLOSE db_cursor
	DEALLOCATE db_cursor
RETURN Select @Text


