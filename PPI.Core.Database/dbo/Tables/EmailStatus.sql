CREATE TABLE [dbo].[EmailStatus]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [NameResxId] INT NOT NULL, 
    CONSTRAINT [FK_EmailStatus_Resx] FOREIGN KEY (NameResxId) REFERENCES [Resx]([Id])
)
