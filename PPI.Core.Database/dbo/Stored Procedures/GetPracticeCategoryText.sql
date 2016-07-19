CREATE PROCEDURE [dbo].[GetPracticeCategoryText]
	@HoganId varchar(50),
	@Language int,
	@Report int	,
	@ProgramId int
AS

	
	Declare @FieldName varchar(50)
	Declare @HoganValue int
	Declare @HoganFieldID int
	Declare @DynamicSql nvarchar(1000)
	Declare @Parm nvarchar(1000)

	
	Declare	@FFieldName varchar(50)
	Declare @FText varchar(MAX)
	Declare @FLanguageId int
	Declare @FCategoryId int
	Declare @FLevelId int
	Declare @FOrder int
	Declare @FSubOrder int
	Declare @FParagraphGroup int
	
	Declare @ReportTemp Table
	(
		[UserScore] int,
		[FieldName] nvarchar(50),		
		[HoganCategory] nvarchar(50),
		[Text] nvarchar(max),		
		[CategoryId] int,		
		[CatName] nvarchar(20),
		[Order] int,				
		[LowerBound1] int,
		[UpperBound1] int,
		[Color1]int,
		[LowerBound2] int,
		[UpperBound2] int,
		[Color2]int,
		[LowerBound3] int,
		[UpperBound3] int,
		[Color3]int,
		[LowerBound4] int,
		[UpperBound4] int,
		[Color4]int,
		[InterviewQuestion] nvarchar(max) 

		
	)

	Declare @ReportTemp2 Table
	(
		[PracticeTextId] int,
		[IsIntroduction] bit,
		[UserScore] int,
		[FieldName] nvarchar(50),
		[HoganCategory] nvarchar(50),	
		[Text] nvarchar(max),		
		[CategoryId] int,		
		[CatName] nvarchar(20),
		[Order] int,
		[InterviewPracticeTextId] int
	)
	
	Declare db_cursor Cursor FOR
		Select DISTINCT HF.FieldName, HF.Id
		from PracticeScaleReport PSR 
		Join PracticeScale PS on PSR.PracticeScaleId = PS.Id
		Join HoganField HF on PS.HoganFieldId = HF.Id
		where PSR.PracticeReportId = @Report and PS.ProgramId = @ProgramId 
			
	OPEN db_cursor
	FETCH NEXT FROM db_cursor INTO @FieldName, @HoganFieldID
	WHILE @@FETCH_STATUS = 0
	
	Begin		
		
		--Get the value from the hogan data for the field
		Set @DynamicSql = N'Select @FName=' +@FieldName+ ' from Manual_Hogan_Import where Hogan_User_ID = @Hogan';
		set @Parm = N'@FName int OUTPUT,@Hogan varchar(50)';
		Declare @HoganResult int;
		--Set @DynamicSql = N'Select ' + @FieldName + ' from Manual_Hogan_Import where Hogan_User_ID =' + @HoganId
		Exec dbo.sp_executesql @DynamicSql, @Parm, @Hogan = @HoganId, @FName = @HoganResult OUTPUT
		Set @HoganValue = (Select @HoganResult)
				
		--[UserScore] int,
		--[FieldName] nvarchar(50),		
		--[HoganCategory] nvarchar(50),
		--[Text] nvarchar(max),		
		--[CategoryId] int,		
		--[CatName] nvarchar(20),
		--[Order] int,				

		Insert INTO @ReportTemp2 ([PracticeTextId],[IsIntroduction],[UserScore],[FieldName],[HoganCategory],[Text],[CategoryId],[CatName],[Order],[InterviewPracticeTextId])
			Select PSR.PracticeTextId,PT.IsIntroduction,@HoganValue,HF.LabelName, HF.HoganCategory, dbo.GetResxValue(PT.TextResxId,@Language),PC.Id,PC.Name,PSR.[Order],PSR.InterviewPracticeTextId from
			PracticeScaleReport PSR
			Join 
			(Select PS.Id,PS.HoganFieldId,Ps.PracticeLevelId,Ps.PracticeCategoryId,PS.LowerBound,PS.UpperBound 
			from PracticeScale PS join PracticeReport PR on PR.Id  = @Report join PracticeCategory PC on PS.PracticeCategoryId = PC.Id and PC.PracticeGroup = PR.PracticeGroup 
			where @HoganValue between PS.LowerBound and PS.UpperBound and PS.HoganFieldId =  @HoganFieldID and PS.ProgramId = @ProgramId) PS on PSR.PracticeScaleId = PS.Id and PSR.PracticeReportId = @Report
			Join PracticeCategoryScale PCS on PS.PracticeCategoryId = PCS.PracticeCategoryId and @HoganValue between PCS.LowerBound and PCS.UpperBound and PCS.HoganFieldId=@HoganFieldID and PCS.ProgramId = @ProgramId
			Join PracticeCategory PC on PCS.PracticeCategoryId = PC.Id			
			Join PracticeText PT on PSR.PracticeTextId = PT.Id
			Join HoganField HF on PS.HoganFieldId = HF.Id
		FETCH NEXT from db_cursor INTO @FieldName,@HoganFieldID	
	End		
	CLOSE db_cursor
	DEALLOCATE db_cursor		
	
	
	Declare @PracticeTextID int
	Declare @IsIntroduction int
	Declare @HoganLabel varchar(50)
	Declare @InterviewPracticeTextId int

	Declare db_cursor2 Cursor FOR
	Select [PracticeTextId],[IsIntroduction],[FieldName],[InterviewPracticeTextId] from @ReportTemp2 Order by [Order]
	OPEN db_cursor2
	FETCH NEXT FROM db_cursor2 INTO @PracticeTextID , @IsIntroduction,@HoganLabel,@InterviewPracticeTextId
	WHILE @@FETCH_STATUS = 0
	
	Begin
	
		Declare @ReturnText nvarchar(max)
		Declare @InterviewReturnText nvarchar(max)
		IF @IsIntroduction = 1
			--Introductions get a random PracticeText plus we append 3 random leafs from practicetext parent / child relationships
			begin
				EXEC [dbo].[GetPracticeTextIntroRandom]	@PracticeTextId = @PracticeTextID,	@Culture = @Language, @Text = @ReturnText OUTPUT						
			end
		Else
		begin
			EXEC [dbo].[GetPracticeTextRandom]	@PracticeTextId = @PracticeTextID,	@Culture = @Language,	@Text = @ReturnText OUTPUT
		end
		--Interview Variations				
		EXEC [dbo].[GetPracticeTextRandom]	@PracticeTextId = @InterviewPracticeTextId,	@Culture = @Language,	@Text = @InterviewReturnText OUTPUT		
		
		Declare @lower int
		Declare @upper int
		Declare @Color	int
		Declare @i int = 0
		declare @LowerBound1 int = 0
		declare @UpperBound1 int = 0
		declare @Color1	int = 0
		declare @LowerBound2 int = 0
		declare @UpperBound2 int = 0
		declare @Color2	int = 0
		declare @LowerBound3 int = 0
		declare @UpperBound3 int = 0
		declare @Color3	int = 0
		declare @LowerBound4 int = 0
		declare @UpperBound4 int = 0
		declare @Color4	int = 0

		declare db_cursorRange Cursor FOR
			Select LowerBound,UpperBound,PracticeCategoryId from PracticeCategoryScale PCS join HoganField HF on PCS.HoganFieldId = HF.Id 
			where HF.LabelName = @HoganLabel and PCS.ProgramId = @ProgramId
			order by LowerBound+UpperBound
		Open db_cursorRange
		FETCH NEXT FROM db_cursorRange INTO @Lower, @Upper, @Color
		WHILE @@FETCH_STATUS = 0
		Begin
			set @i = @i + 1
			
			If @i = 1
				begin
					set @LowerBound1 = @lower
					set @UpperBound1 = @Upper
					Set @Color1 = @Color
				end
			else if @i = 2
				begin
					set @LowerBound2 = @lower
					set @UpperBound2 = @Upper
					Set @Color2 = @Color
				end
			else if @i = 3
			begin
				set @LowerBound3 = @lower
				set @UpperBound3 = @Upper
				Set @Color3 = @Color
			end
			Else if @i = 4
			begin
				set @LowerBound4 = @lower
				set @UpperBound4 = @Upper
				Set @Color4 = @Color
			end
			FETCH NEXT FROM db_cursorRange INTO @Lower, @Upper, @Color
		End
			CLOSE db_cursorRange
			DEALLOCATE db_cursorRange	

		Insert into @ReportTemp ([UserScore],[FieldName],[HoganCategory],[Text],[CategoryId],[CatName],[Order],[LowerBound1],[UpperBound1],[Color1],[LowerBound2],[UpperBound2],[Color2],[LowerBound3],[UpperBound3],[Color3],[LowerBound4],[UpperBound4],[Color4],[InterviewQuestion])
		select 	[UserScore],[FieldName],[HoganCategory],@ReturnText,[CategoryId],[CatName],[Order],
		@LowerBound1 as [LowerBound1],
		@UpperBound1 as [UpperBound1],
		@Color1 as [Color1],
		@LowerBound2 as [LowerBound2],
		@UpperBound2 as [UpperBound2],
		@Color2 as [Color2],
		@LowerBound3 as [LowerBound3],
		@UpperBound3 as [UpperBound3],
		@Color3 as [Color3],
		@LowerBound4 as [LowerBound4],
		@UpperBound4 as [UpperBound4],
		@Color4 as [Color4],
		@InterviewReturnText
		from @ReportTemp2
		Where PracticeTextId = @PracticeTextId

	FETCH NEXT from db_cursor2 INTO @PracticeTextID,@IsIntroduction,@HoganLabel,@InterviewPracticeTextId
	End
	CLOSE db_cursor2
	DEALLOCATE db_cursor2		

	Select * from @ReportTemp Order by [Order]