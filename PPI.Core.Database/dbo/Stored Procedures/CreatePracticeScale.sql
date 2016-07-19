CREATE PROCEDURE [dbo].[CreatePracticeScale]
	@HoganFieldId int,
	@CatId int,
	@LevelId int,	
	@ProgramId int,
	@Lower int,
	@Upper int
	
AS
	DECLARE @ReturnID int

	begin		
	
	
	
		IF NOT EXISTS (Select ID from PracticeScale PS where PS.HoganFieldId = @HoganFieldId and PS.PracticeCategoryId = @CatId and PS.PracticeLevelId = @LevelId and PS.ProgramId = @ProgramId)
			begin
				insert into PracticeScale (HoganFieldId,PracticeCategoryId,PracticeLevelId,ProgramId,LowerBound,UpperBound) Values (@HoganFieldId,@CatId,@LevelId,@ProgramId,@Lower,@Upper)
				set @ReturnID = @@IDENTITY
			end
		else
			set @ReturnID = (Select Id from PracticeScale PS where PS.HoganFieldId = @HoganFieldId and PS.PracticeCategoryId = @CatId and PS.PracticeLevelId = @LevelId and PS.ProgramId = @ProgramId)						
		
		RETURN @ReturnID
	end	
