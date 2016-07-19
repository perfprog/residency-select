CREATE TABLE [dbo].[PracticeScale]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [HoganFieldId] INT NOT NULL, 
    [PracticeCategoryId] INT NOT NULL,	
	[PracticeLevelId] INT NOT NULL, 
	[ProgramId] INT NULL DEFAULT 1, 
    [LowerBound] INT NOT NULL, 
    [UpperBound] INT NOT NULL,     
    CONSTRAINT [FK_PracticeScale_HoganField] FOREIGN KEY ([HoganFieldId]) REFERENCES [HoganField]([Id]),     
	CONSTRAINT [FK_PracticeScale_PracticeCategory] FOREIGN KEY ([PracticeCategoryId]) REFERENCES [PracticeCategory]([Id]),     
	CONSTRAINT [FK_PracticeScale_PracticeLevel] FOREIGN KEY ([PracticeLevelId]) REFERENCES [PracticeLevel]([Id]), 
    CONSTRAINT [FK_PracticeScale_Program] FOREIGN KEY ([ProgramId]) REFERENCES [Program]([Id])     
)
