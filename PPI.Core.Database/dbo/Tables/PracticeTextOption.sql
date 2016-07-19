CREATE TABLE [dbo].[PracticeTextOption]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PracticeTextId] INT NOT NULL, 
    [AlternativePracticeTextId] INT NULL, 
    CONSTRAINT [FK_PracticeTextOption_PracticeText] FOREIGN KEY ([PracticeTextId]) REFERENCES [PracticeText]([Id]), 
    CONSTRAINT [FK_PracticeTextOption_AlternativePracticeText] FOREIGN KEY ([AlternativePracticeTextId]) REFERENCES [PracticeText]([Id])
)
