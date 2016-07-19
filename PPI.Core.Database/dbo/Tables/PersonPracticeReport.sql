CREATE TABLE [dbo].[PersonPracticeReport]
(
	[Id] INT NOT NULL Identity PRIMARY KEY, 
    [PersonId] INT NOT NULL, 
    [PracticeReportId] INT NOT NULL, 
    [RunDate] DATETIME2 NULL DEFAULT GETDATE(), 
    [AspNetUsersId] NVARCHAR(128) NOT NULL, 
    [EventId] INT NULL, 
    CONSTRAINT [FK_PersonPracticeReport_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id]) On DELETE Cascade,
    CONSTRAINT [FK_PersonPracticeReport_PracticeReport] FOREIGN KEY ([PracticeReportId]) REFERENCES [PracticeReport]([Id]), 
    CONSTRAINT [FK_PersonPracticeReport_Event] FOREIGN KEY ([EventId]) REFERENCES [Event]([Id]) On Delete Cascade, 
    CONSTRAINT [FK_PersonPracticeReport_AspNetUsers] FOREIGN KEY ([AspNetUsersId]) REFERENCES [AspNetUsers]([Id])
)
