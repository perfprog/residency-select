CREATE TABLE [dbo].[PracticeScaleReport]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PracticeScaleId] INT NOT NULL, 
    [PracticeReportId] INT NOT NULL, 
    [Order] INT NOT NULL DEFAULT 0, 
    [SubOrder] INT NULL, 
    [PracticeTextId] INT NOT NULL, 
    [InterviewPracticeTextId] INT NULL, 
    CONSTRAINT [FK_PracticeScaleReport_PracticeScale] FOREIGN KEY ([PracticeScaleId]) REFERENCES [PracticeScale]([Id]), 
    CONSTRAINT [FK_PracticeScaleReport_PracticeReport] FOREIGN KEY ([PracticeReportId]) REFERENCES [PracticeReport]([Id]), 
    CONSTRAINT [FK_PracticeScaleReport_PracticeText] FOREIGN KEY ([PracticeTextId]) REFERENCES [PracticeText]([Id]), 
    CONSTRAINT [FK_PracticeScaleReport_PracticeText2] FOREIGN KEY ([InterviewPracticeTextId]) REFERENCES [PracticeText]([Id])
)
