CREATE TABLE [dbo].[ResxValue]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ResxId] INT NOT NULL, 
    [CultureId] INT NOT NULL, 
    [Value] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_ResxValue_Culture] FOREIGN KEY ([CultureId]) REFERENCES [Culture]([Id]), 
    CONSTRAINT [FK_ResxValue_Resx] FOREIGN KEY ([ResxId]) REFERENCES [Resx]([Id]) ON DELETE CASCADE
)