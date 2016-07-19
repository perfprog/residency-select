CREATE PROCEDURE [dbo].[LoadInterViewQuestions]
	@HoganFieldId int,
	@ProgramId int,
	@PracticeReportId int,
	@PracticeLevelId int,
	@InterviewQuestion varchar(max),
	@InterviewQuestion1 varchar(max),
	@InterviewQuestion2 varchar(max),
	@InterviewQuestion3 varchar(max),
	@InterviewQuestion4 varchar(max),
	@InterviewQuestion5 varchar(max)	
AS

Declare @PracticeScaleReportID int
DECLARE	@return_value Int
Declare @PracticeTextId int

Declare db_cursor Cursor FOR
		Select  
		 PSR.Id as [PracticeScaleReportID]
		--, HF.Id as [HoganField]
		--, PS.PracticeLevelId
		--, PL.Name
		from PracticeScaleReport PSR
		Join PracticeScale PS on PSR.PracticeScaleId = PS.Id and PSR.PracticeReportId = @PracticeReportId
		Join PracticeCategoryScale PCS on PS.PracticeCategoryId = PCS.PracticeCategoryId  and PCS.ProgramId = @ProgramId and PCS.HoganFieldId = @HoganFieldId
		Join PracticeCategory PC on PCS.PracticeCategoryId = PC.Id			
		Join PracticeText PT on PSR.PracticeTextId = PT.Id
		Join HoganField HF on PS.HoganFieldId = HF.Id
		Join PracticeLevel PL on PS.PracticeLevelId = PL.Id
		where ps.HoganFieldId = @HoganFieldId and PL.Id = @PracticeLevelId
		order by PS.PracticeLevelId			
	OPEN db_cursor
	FETCH NEXT FROM db_cursor INTO @PracticeScaleReportID
	WHILE @@FETCH_STATUS = 0
	
	Begin		
		--Create the Resx record
		EXEC	@return_value = [dbo].[CreateResx] @DataColumnName = N'PracticeScaleReport_InterviewPracticeText', @FriendlyName = N'Interview Question',		@Culture = N'en-US',		@Value = @InterviewQuestion
		-- Create the PracticeText Record Not and intro, no Parent
		INSERT INTO [dbo].[PracticeText] ([TextResxId], [Text], [IsIntroduction], [ParentPracticeTextId]) VALUES (@return_value, N'Do Not Use As This Is A Placeholder For Multilingual Transformations', 0, NULL)				
		-- Create the Alter Text recored
		set @PracticeTextId = @@IDENTITY
		EXEC CreateAlternativePracticeText @PracticeTextId,N'en-US',@InterviewQuestion1
		EXEC CreateAlternativePracticeText @PracticeTextId,N'en-US',@InterviewQuestion2
		EXEC CreateAlternativePracticeText @PracticeTextId,N'en-US',@InterviewQuestion3
		EXEC CreateAlternativePracticeText @PracticeTextId,N'en-US',@InterviewQuestion4
		EXEC CreateAlternativePracticeText @PracticeTextId,N'en-US',@InterviewQuestion5		
		--update the PracticeScaleReport with the Inteview PracticeText ID
		Update PracticeScaleReport set InterviewPracticeTextId = @PracticeTextId where Id = @PracticeScaleReportID
		FETCH NEXT from db_cursor INTO @PracticeScaleReportID
	End		
	CLOSE db_cursor
	DEALLOCATE db_cursor