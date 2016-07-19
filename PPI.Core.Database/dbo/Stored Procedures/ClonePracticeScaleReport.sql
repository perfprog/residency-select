CREATE PROCEDURE [dbo].[ClonePracticeScaleReport]
	@PracticeScaleReportId int,
	@OrginalProgramId int,
	@NewProgramId int,
	@NewText bit
AS
	Declare @Order int
	Declare @SubOrder int
	Declare @PracticeTextID int
	Declare @InterviewPracticeTextID int
	Declare @HoganFieldID int
	Declare @PracticeCategoryId int
	Declare @PracticeLevelId int
	Declare @LowerBound int
	Declare @UpperBound int
	Declare @NewPracticeScaleID int

	Declare @ExistingReportScales TABLE
	(
		[Order] INT
		,SubOrder INT
		,PracticeTextID INT
		,InterviewPracticeTextID INT
		,HoganFieldID INT
		,PracticeCategoryId INT
		,PracticeLevelId INT
		,LowerBound INT
		,UpperBound INT
	)
	
	INSERT INTO @ExistingReportScales
	SELECT PSR.[Order],PSR.SubOrder,PSR.PracticeTextID,PSR.InterviewPracticeTextID,PS.HoganFieldID,PS.PracticeCategoryId,PS.PracticeLevelId,PS.LowerBound,PS.UpperBound FROM [PracticeScaleReport] PSR 
		Join [PracticeScale]  PS on PSR.PracticeScaleId = PS.Id
		where PracticeReportId = @PracticeScaleReportId and PS.ProgramId = @OrginalProgramId
			
	Declare db_cursor Cursor FOR 
		Select * from @ExistingReportScales
	OPEN db_cursor
	FETCH NEXT FROM db_cursor INTO @Order,@SubOrder,@PracticeTextID,@InterviewPracticeTextID,@HoganFieldID,@PracticeCategoryId,@PracticeLevelId,@LowerBound,@UpperBound
	WHILE @@FETCH_STATUS = 0
	BEGIN
		Exec @NewPracticeScaleID = CreatePracticeScale @HoganFieldID, @PracticeCategoryId, @PracticeLevelId, @NewProgramId, @LowerBound, @UpperBound		
		Insert Into PracticeScaleReport VALUES(@NewPracticeScaleID, @PracticeScaleReportId,@Order,@SubOrder,@PracticeTextID,@InterviewPracticeTextID)		
	FETCH NEXT from db_cursor INTO @Order,@SubOrder,@PracticeTextID,@InterviewPracticeTextID,@HoganFieldID,@PracticeCategoryId,@PracticeLevelId,@LowerBound,@UpperBound
	END
	CLOSE db_cursor
	DEALLOCATE db_cursor

	Insert into PracticeCategoryScale
	SELECT @NewProgramId as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = @NewProgramId) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = @OrginalProgramId and PCS2.Id is null

	RETURN 0