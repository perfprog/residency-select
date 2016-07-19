CREATE TABLE [dbo].[EventStatus]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [NameResxId] INT NOT NULL
	CONSTRAINT [FK_EventStatus_Resx] FOREIGN KEY ([NameResxId]) REFERENCES [Resx]([Id])
)

