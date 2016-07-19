CREATE TABLE [dbo].[EmailType]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Name] NVARCHAR(50) NOT NULL, 
    [NameResxId] INT NOT NULL, 
    CONSTRAINT [FK_EmailType_Resx] FOREIGN KEY ([NameResxId]) REFERENCES [Resx]([Id])
)
