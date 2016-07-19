CREATE PROCEDURE [dbo].[CreateAlternativePracticeText]
	@PracticeTextId int,
	@Culture nvarchar(6),
	@Text nvarchar(max)
AS
		Declare @FriendlyName nvarchar(30)
		Declare @ResxId int			
		Declare @NewPracticeTextId int
		Set @FriendlyName = SubString(@Text,0,30);
	
	Begin
		
		exec @ResxId = dbo.CreateResx N'PracticeText_Text',@FriendlyName,@Culture,@Text
		Insert into PracticeText (TextResxId) values(@ResxId);
		set @NewPracticeTextId = @@IDENTITY
		Insert into PracticeTextOption Values (@PracticeTextId,@NewPracticeTextId)

	End
RETURN @NewPracticeTextId
