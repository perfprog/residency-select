CREATE TABLE [dbo].[PracticeText]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TextResxId] INT NOT NULL,      	
    [Text] NVARCHAR(75) NULL DEFAULT 'Do Not Use As This Is A Placeholder For Multilingual Transformations', 
    [IsIntroduction] BIT NULL DEFAULT 0, 
    [ParentPracticeTextId] INT NULL, 
    CONSTRAINT [FK_PracticeText_Text] FOREIGN KEY ([TextResxId]) REFERENCES [Resx]([Id]), 
    CONSTRAINT [FK_PracticeText_PracticeText] FOREIGN KEY ([ParentPracticeTextId]) REFERENCES [PracticeText]([Id])
)
