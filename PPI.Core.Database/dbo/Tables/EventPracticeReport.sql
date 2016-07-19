CREATE TABLE [dbo].[EventPracticeReport]
(
	[Id] INT NOT NULL Identity PRIMARY KEY, 
    [EventId] INT NOT NULL, 
    [PracticeReportId] INT NOT NULL, 
    [ReportTitleResxId] INT NULL, 
    [DefaultLogo] IMAGE NULL, 
    [DefaultLogoMimeType] NVARCHAR(50) NULL, 
    [DefaultBackground] IMAGE NULL, 
    [DefaultBackgroundMimeType] NVARCHAR(50) NULL, 
    [DefaultColor] NVARCHAR(10) NULL, 
    [IntroductionResxId] INT NULL, 
    [IntroductionTwoResxId] INT NULL, 
    [IntroductionThreeResxId] INT NULL, 
    CONSTRAINT [FK_EventPracticeReport_Event] FOREIGN KEY ([EventId]) REFERENCES [Event]([Id]) ON Delete Cascade, 
    CONSTRAINT [FK_EventPracticeReport_PracticeReport] FOREIGN KEY ([PracticeReportId]) REFERENCES [PracticeReport]([Id]), 
    CONSTRAINT [FK_EventPracticeReport_IntroductionResx] FOREIGN KEY (IntroductionResxId) REFERENCES [Resx]([Id]), 
    CONSTRAINT [FK_EventPracticeReport_IntroductionTwoResx] FOREIGN KEY (IntroductionTwoResxId) REFERENCES [Resx]([Id]), 
    CONSTRAINT [FK_EventPracticeReport_IntroductionThreeResx] FOREIGN KEY (IntroductionThreeResxId) REFERENCES [Resx]([Id]), 
    CONSTRAINT [FK_EventPracticeReport_TitleResx] FOREIGN KEY ([ReportTitleResxId]) REFERENCES [Resx]([Id])
	
)
