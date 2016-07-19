CREATE TABLE [dbo].[ScheduledEmailPerson] (
    [Id]               INT      IDENTITY (1, 1) NOT NULL,
    [ScheduledEmailId] INT      NOT NULL,
    [PersonId]         INT      NOT NULL,
    [CompletedDate]        DATETIME NULL,
    [enabled]          BIT      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduledEmailPerson_ScheduledEmail] FOREIGN KEY ([ScheduledEmailId]) REFERENCES [dbo].[ScheduledEmail] ([Id]),
    CONSTRAINT [FK_ScheduledEmailPerson_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
);

