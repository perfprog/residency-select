CREATE PROCEDURE [dbo].[GetPracticeScaleText]
	@HoganId varchar(50),
	@Language int,
	@Report int	
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
		[HoganFieldDefinition] nvarchar(max),		
		[HoganFieldBlurb] nvarchar(max),		
		[Text] nvarchar(max),
		LanguageId int,
		CategoryId int,
		LevelId int,
		[Order] int,
		[SubOrder] int,
		[ParagraphGroup] int
	)

	Declare @ReportTemp2 Table
	(
		[PracticeTextId] int,
		[IsIntroduction] bit,
		[UserScore] int,
		[FieldName] nvarchar(50),
		[HoganFieldDefinition] nvarchar(max),		
		[HoganFieldBlurb] nvarchar(max),		
		[Text] nvarchar(max),
		LanguageId int,
		CategoryId int,
		LevelId int,
		[Order] int,
		[SubOrder] int,
		[ParagraphGroup] int
	)
	
	Declare db_cursor Cursor FOR
		Select DISTINCT HF.FieldName, HF.Id
		from PracticeScaleReport PSR 
		Join PracticeScale PS on PSR.PracticeScaleId = PS.Id
		Join HoganField HF on PS.HoganFieldId = HF.Id
		where PSR.PracticeReportId = @Report
			
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
				
		Insert INTO @ReportTemp2
			Select PT.Id,PT.IsIntroduction,@HoganValue,HF.LabelName,dbo.GetResxValue(HF.DefinitionTextResxId,@Language) as HoganFieldDefinition , dbo.GetResxValue(HF.BlurbTextResxId,@Language) as HoganFieldBlurb ,dbo.GetResxValue(PT.TextResxId,@Language)as [Text],@Language as LanguageId,PC.Id as CategoryId,PL.Id as LevelId,PP.[Order],PC.[Order], PP.ParagraphGroup
			from PracticeScaleReport PSR		
			-- Have to figure out the logic for the alternative PT2 's
			join PracticeText PT on PSR.PracticeTextId = PT.Id
			join PracticeReport PR on  PR.Id  = @Report
			Join (Select PS.Id,PS.HoganFieldId,Ps.PracticeLevelId,Ps.PracticeCategoryId from PracticeScale PS join PracticeReport PR on  PR.Id  = @Report join PracticeCategory PC on PS.PracticeCategoryId = PC.Id and PC.PracticeGroup = PR.PracticeGroup where @HoganValue between PS.LowerBound and PS.UpperBound and PS.HoganFieldId = @HoganFieldID) PS on PSR.PracticeScaleId = PS.Id					
			join PracticeLevel PL on PS.PracticeLevelId = PL.Id
			Join PracticeCategory PC on PS.PracticeCategoryId = PC.Id and PC.PracticeGroup = PR.PracticeGroup
			join HoganField HF on PS.HoganFieldId = HF.Id		
			join PracticeParagraphs PP on PP.HoganFieldId = HF.Id and PP.PracticeReportId = @Report 
			Where PSR.PracticeReportId = @Report
			order by PP.[Order],FieldName
			FETCH NEXT from db_cursor INTO @FieldName,@HoganFieldID	
	End		
	CLOSE db_cursor
	DEALLOCATE db_cursor		

	Declare @PracticeTextID int
	Declare @IsIntroduction int

	Declare db_cursor2 Cursor FOR
	Select [PracticeTextId],[IsIntroduction] from @ReportTemp2 Order by [ParagraphGroup],FieldName,[SubOrder]
	OPEN db_cursor2
	FETCH NEXT FROM db_cursor2 INTO @PracticeTextID , @IsIntroduction
	WHILE @@FETCH_STATUS = 0
	
	Begin
	
		Declare @ReturnText nvarchar(max)
		IF @IsIntroduction = 1
			--Introductions get a random PracticeText plus we append 3 random leafs from practicetext parent / child relationships
			EXEC [dbo].[GetPracticeTextIntroRandom]	@PracticeTextId = @PracticeTextID,	@Culture = @Language, @Text = @ReturnText OUTPUT						
		Else
			EXEC [dbo].[GetPracticeTextRandom]	@PracticeTextId = @PracticeTextID,	@Culture = @Language,	@Text = @ReturnText OUTPUT
		
		Insert into @ReportTemp
		select 	[UserScore],[FieldName],[HoganFieldDefinition],[HoganFieldBlurb],@ReturnText, LanguageId,	CategoryId,	LevelId,[Order],[SubOrder],	[ParagraphGroup]
		from @ReportTemp2
		Where PracticeTextId = @PracticeTextId

	FETCH NEXT from db_cursor2 INTO @PracticeTextID,@IsIntroduction
	End
	CLOSE db_cursor2
	DEALLOCATE db_cursor2		

	Select * from @ReportTemp Order by [ParagraphGroup],FieldName,[SubOrder]
	
	
	 
