CREATE TABLE [dbo].[PracticeCategory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [PracticeGroup] NVARCHAR(50) NULL, 
    [Order] INT NULL DEFAULT 0
)
