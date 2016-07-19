CREATE TABLE [dbo].[ReplacementExpression]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [FindValue] NVARCHAR(50) NOT NULL, 
    [Expression] NVARCHAR(MAX) NOT NULL
)
