CREATE FUNCTION [dbo].[GetResxValue]
(
	@ResxId int,
	@CultureId int
)
RETURNS nvarchar(max)
Begin
	Declare @Value as nvarchar(max)
	Set @Value = ISNull((Select Value from ResxValue where ResxId = @ResxId and CultureId = @CultureId),'No Translation Available')
	Return @Value
End