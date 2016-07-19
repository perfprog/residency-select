CREATE TABLE [dbo].[PracticeCategoryScale]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramId] INT NOT NULL, 
    [PracticeCategoryId] INT NOT NULL, 
    [HoganFieldId] INT NOT NULL, 
	[LowerBound] INT NOT NULL, 
    [UpperBound] INT NOT NULL,     
    CONSTRAINT [FK_PracticeCategoryScale_Program] FOREIGN KEY ([ProgramId]) REFERENCES [Program]([Id]), 
    CONSTRAINT [FK_PracticeCategoryScale_PracticeCategory] FOREIGN KEY ([PracticeCategoryId]) REFERENCES [PracticeCategory]([Id]), 
    CONSTRAINT [FK_PracticeCategoryScale_HoganField] FOREIGN KEY ([HoganFieldId]) REFERENCES [HoganField]([Id]), 
    

    


)
