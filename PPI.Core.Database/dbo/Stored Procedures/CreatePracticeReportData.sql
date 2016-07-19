CREATE PROCEDURE [dbo].[CreatePracticeReportData]	
	@HoganFieldId int,
	@LevelId int,
	@CatId int,
	@Text nvarchar(max),
	@Culture nvarchar(6),
	@ProgramId int,
	@ReportId int

AS
	Begin
		--Create the Resx Tex
		Declare @FriendlyName nvarchar(30)
		Declare @ResxId int
		Declare @PracticeTextId int
		Declare @PracticeScaleId int
		Declare @HoganOrder int

		Set @PracticeScaleId = (Select Id from PracticeScale where PracticeCategoryId = @CatId and PracticeLevelId = @LevelId and HoganFieldId = @HoganFieldId and ProgramId = @ProgramId)
		Set @HoganOrder = (Select StandardOrder from HoganField where HoganField.Id = @HoganFieldId)
		--Check if this exists already dont insert another one
		if Not Exists (Select Id from PracticeScaleReport PSR where PSR.PracticeScaleId=@PracticeScaleId and PSR.PracticeReportId=@ReportId)
			begin
					Set @FriendlyName = SubString(@Text,0,30);
					exec @ResxId = dbo.CreateResx N'PracticeText_Text',@FriendlyName,@Culture,@Text
					Insert into PracticeText (TextResxId) values(@ResxId);
					set @PracticeTextId = @@IDENTITY			
					Insert into PracticeScaleReport (PracticeScaleId,PracticeReportId,[Order],[SubOrder],PracticeTextId) Values
					(@PracticeScaleId,@ReportId,@HoganOrder,0,@PracticeTextId)
					Insert into PracticeTextOption (PracticeTextId,AlternativePracticeTextId) Values (@PracticeTextId, null)
			end
		else
		Begin
			set @PracticeTextId = (Select PracticeTextId from PracticeScaleReport PSR where PSR.PracticeScaleId=@PracticeScaleId and PSR.PracticeReportId=@ReportId)
		End
	End
RETURN @PracticeTextId