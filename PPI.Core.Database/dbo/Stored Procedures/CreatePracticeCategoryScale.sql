CREATE PROCEDURE [dbo].[CreatePracticeCategoryScale]
	@ProgramId int,
	@PracticeCategoryId int,
	@HoganFieldId int,
	@Lower int,
	@Upper int
AS
	Begin
		IF Not Exists (Select ID from PracticeCategoryScale where ProgramId = @ProgramId and HoganFieldId = @HoganFieldId and PracticeCategoryId = @PracticeCategoryId)
			Insert into PracticeCategoryScale (ProgramId,PracticeCategoryId,HoganFieldId,LowerBound,UpperBound) Values (@ProgramId,@PracticeCategoryId,@HoganFieldId,@Lower,@Upper)
	End

	
RETURN 0
