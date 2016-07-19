CREATE TABLE [dbo].[HoganField]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FieldName] NVARCHAR(50) NOT NULL, 
    [LabelName] NVARCHAR(50) NULL,
	[StandardOrder] Int Null,
	[DefinitionTextResxId] INT NULL,
	[BlurbTextResxId] int NULL ,
	[HoganCategory] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_HoganField_Resx] FOREIGN KEY ([DefinitionTextResxId]) REFERENCES [Resx]([Id]),    
	CONSTRAINT [FK_HoganField_Resx2] FOREIGN KEY ([BlurbTextResxId]) REFERENCES [Resx]([Id])    
)
