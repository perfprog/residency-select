CREATE TABLE [dbo].[PersonEmail]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
	[PersonId] INT NOT NULL,
    [EmailId] INT NOT NULL,     
    [EmailStatusId] INT NULL,
	[SentDate] DATETIME NULL, 
    [EmailBody] NVARCHAR(MAX) NOT NULL, 
    [RetryCount] INT NOT NULL DEFAULT 0, 
    [ErrorMessage] NVARCHAR(MAX) NULL, 
    [Origin] VARCHAR(10) NULL, 
    CONSTRAINT [FK_PersonEmail_EmailStatus] FOREIGN KEY ([EmailStatusId]) REFERENCES [EmailStatus]([Id]), 
    CONSTRAINT [FK_PersonEmail_Email] FOREIGN KEY ([EmailId]) REFERENCES [Email]([Id]) On DELETE Cascade, 
    CONSTRAINT [FK_PersonEmail_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id]) On DELETE Cascade
	 
)
