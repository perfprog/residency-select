CREATE TABLE [dbo].[PracticeParagraphs]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [HoganFieldId] INT NOT NULL, 
    [ParagraphGroup] INT NOT NULL, 
    [Order] INT NOT NULL DEFAULT 0, 
    [PracticeReportId] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_PracticeParagraphs_PracticeReport] FOREIGN KEY ([PracticeReportId]) REFERENCES [PracticeReport]([Id])	
)
