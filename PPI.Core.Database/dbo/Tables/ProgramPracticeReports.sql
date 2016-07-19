CREATE TABLE [dbo].[ProgramPracticeReports]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [ProgramId] INT NOT NULL, 
    [PracticeReportId] INT NOT NULL, 
    CONSTRAINT [FK_ProgramPracticeReports_Program] FOREIGN KEY ([ProgramId]) REFERENCES [Program]([Id]), 
    CONSTRAINT [FK_ProgramPracticeReports_PracticeReport] FOREIGN KEY ([PracticeReportId]) REFERENCES [PracticeReport]([Id])
)
