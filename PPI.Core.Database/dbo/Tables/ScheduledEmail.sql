create TABLE [dbo].[ScheduledEmail] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [ScheduleDate]  DATETIME NOT NULL,
    [StartDate]     DATETIME NULL,
    [CompletedDate] DATETIME NULL,
    [EmailId] INT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),    
	CONSTRAINT [FK_ScheduledEmail_Email] FOREIGN KEY ([EmailId]) REFERENCES [dbo].[Email] ([Id])
);

