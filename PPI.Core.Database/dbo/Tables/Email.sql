CREATE TABLE [dbo].[Email]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [EventId] INT NULL, 
	[EmailTypeId] int NULL,
    [DefaultFrom] NVARCHAR(150) NULL, 
    [Subject] NVARCHAR(250) NULL, 
    [Introduction] NVARCHAR(MAX) NULL, 
    [Closing] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Email_Event] FOREIGN KEY ([EventId]) REFERENCES [Event]([Id])ON Delete Cascade, 
    CONSTRAINT [FK_Email_EmailType] FOREIGN KEY ([EmailTypeId]) REFERENCES [EmailType]([Id])
)
