CREATE PROCEDURE [dbo].[GetPracticeScaleParagraphText]
	@HoganId varchar(50),
	@Language int,
	@Report int
AS
	--Select DISTINCT HF.FieldName
	--	from PracticeScaleReport PSR 
	--	Join PracticeScale PS on PSR.PracticeScaleId = PS.Id
	--	Join HoganField HF on PS.HoganFieldId = HF.Id
	--	where PSR.PracticeReportId = @Report
	
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
		FieldName varchar(50),
		Text nvarchar(max),
		LanguageId int,
		CategoryId int,
		LevelId int,
		[Order] int,
		[SubOrder] int,
		[ParagraphGroup] int
	)
	Declare @ReportTempFormatted Table
	(
		FieldName varchar(50),
		Text nvarchar(max),
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
				
		Insert INTO @ReportTemp
			Select HF.FieldName,dbo.GetResxValue(PT.TextResxId,@Language)as [Text],@Language as LanguageId,PC.Id as CategoryId,PL.Id as LevelId,PSR.[Order],PSR.SubOrder, PCP.ParagraphGroup
			from PracticeScaleReport PSR			
			join PracticeText PT on PSR.PracticeTextId = PT.Id
			Join (Select * from PracticeScale PS where @HoganValue between PS.LowerBound and PS.UpperBound and PS.HoganFieldId = @HoganFieldID) PS on PSR.PracticeScaleId = PS.Id
			join PracticeLevel PL on PS.PracticeLevelId = PL.Id
			Join PracticeCategory PC on PS.PracticeCategoryId = PC.Id
			join HoganField HF on PS.HoganFieldId = HF.Id		
			join PracticeCategoryParagraphs PCP on PCP.HoganFieldId = HF.Id and PCP.PracticeCategoryId = PC.Id
			Where PSR.PracticeReportId = @Report
			order by FieldName,PSR.[Order],PSR.[SubOrder]
			FETCH NEXT from db_cursor INTO @FieldName,@HoganFieldID	
	End		
	CLOSE db_cursor
	DEALLOCATE db_cursor	

	--
	
	Declare @CurrentOrder int
	--Declare @CurrentSubOrder int
	Declare @PreviousParagraph int
	Declare @ParagraphText varchar(max)
	Declare @PreviousOrder int	

	Declare db_cursor_formatt Cursor FOR
		Select FieldName,[Text],LanguageId,CategoryId,LevelId,[Order],SubOrder,ParagraphGroup from @ReportTemp Order by [Order],[SubOrder]
	OPEN db_cursor_formatt
	FETCH NEXT FROM db_cursor_formatt INTO @FFieldName,@FText,@FLanguageId,@FCategoryId,@FLevelId,@FOrder,@FSubOrder,@FParagraphGroup
	set @ParagraphText = ''
	Set @PreviousParagraph = 99
	WHILE @@FETCH_STATUS = 0
	Begin			

		IF (@PreviousParagraph = @FParagraphGroup or @PreviousParagraph = 99)	
			if (@PreviousParagraph = 99)
				begin
					set @ParagraphText = @ParagraphText + @FText
					set @PreviousParagraph = @FParagraphGroup					
				end
			else 
				--Standard 2 spaces for sentances after first paragraph
				begin
				set @ParagraphText = @ParagraphText + '  ' +@FText
				set @PreviousOrder = @FOrder				
				end
		Else
			begin
				Insert into @ReportTempFormatted Values('Combined',@ParagraphText,@FLanguageId,@FCategoryId,@FLevelId,@PreviousOrder,0,@PreviousParagraph)
				set @ParagraphText = @FText			
			end 
		set @PreviousParagraph = @FParagraphGroup
		FETCH NEXT FROM db_cursor_formatt INTO @FFieldName,@FText,@FLanguageId,@FCategoryId,@FLevelId,@FOrder,@FSubOrder,@FParagraphGroup		
		if @@FETCH_STATUS <> 0
		-- last row
			Insert into @ReportTempFormatted Values('Combined',@ParagraphText,@FLanguageId,@FCategoryId,@FLevelId,@FOrder,0,@PreviousParagraph)
	End
	CLOSE db_cursor_formatt
	DEALLOCATE db_cursor_formatt
	Select * from @ReportTempFormatted Order by [Order]