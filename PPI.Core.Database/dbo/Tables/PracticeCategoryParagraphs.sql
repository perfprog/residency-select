CREATE TABLE [dbo].[PracticeCategoryParagraphs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [HoganFieldId] INT NOT NULL, 
    [PracticeCategoryId] INT NOT NULL, 
    [ParagraphGroup] INT NOT NULL, 
    CONSTRAINT [FK_PracticeCategoryParagraphs_PracticeCategory] FOREIGN KEY ([PracticeCategoryId]) REFERENCES [PracticeCategory]([Id]), 
    CONSTRAINT [FK_PracticeCategoryParagraphs_HoganField] FOREIGN KEY ([HoganFieldId]) REFERENCES [HoganField]([Id])
)
