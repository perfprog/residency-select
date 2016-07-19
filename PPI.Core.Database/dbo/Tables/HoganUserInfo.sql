CREATE TABLE [dbo].[HoganUserInfo]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [UserId] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [GroupName] NVARCHAR(50) NULL, 
    [ProgramId] INT NOT NULL, 
    [UploadDate] DATETIME NULL, 
    [Used] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_HoganUserInfo_Program] FOREIGN KEY ([ProgramId]) REFERENCES [Program]([Id])
)
