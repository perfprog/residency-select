CREATE TABLE [dbo].[EventType]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [NameResxId] INT NOT NULL, 
    CONSTRAINT [FK_EventType_Resx] FOREIGN KEY ([NameResxId]) REFERENCES [Resx]([Id])

)
