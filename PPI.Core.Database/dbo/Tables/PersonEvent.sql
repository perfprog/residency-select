CREATE TABLE [dbo].[PersonEvent]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [PersonId] INT NOT NULL, 
    [EventId] INT NOT NULL, 
    CONSTRAINT [FK_PersonEvent_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id])  On DELETE Cascade, 
    CONSTRAINT [FK_PersonEvent_Event] FOREIGN KEY ([EventId]) REFERENCES [Event]([Id])  On DELETE Cascade

)
