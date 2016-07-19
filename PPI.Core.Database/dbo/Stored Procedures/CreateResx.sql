CREATE PROCEDURE [dbo].[CreateResx]
	@DataColumnName nvarchar(150),
	@FriendlyName nvarchar(50),
	@Culture varchar(6),
	@Value nvarchar(max)
AS
	declare @resxId int
	declare @CultureId int
	begin
		insert into Resx values (@FriendlyName,@DataColumnName)
		Set @CultureId = (Select Id from Culture where Culture.Value = @Culture)
		if @CultureId <= 0
			begin
				Set @CultureId = 1				
			end
		Set @resxId = @@IDENTITY
		insert into ResxValue values (@resxId,@CultureId,@Value)
	end
	
RETURN @resxId