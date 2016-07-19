CREATE FUNCTION [dbo].[GetResxValueEX]
(
	@ResxId int,
	@Culture varchar(6)
)
RETURNS nvarchar(max)
Begin
	Declare @Value as nvarchar(max)
	Set @Value = ISNull((Select Value from ResxValue RV  where ResxId = @ResxId and CultureId = (select Id from Culture where Value= @Culture)),'No Translation Available')
	Return @Value
End