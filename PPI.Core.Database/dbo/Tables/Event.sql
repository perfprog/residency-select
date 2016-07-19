CREATE TABLE [dbo].[Event]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Name] NVARCHAR(250) NOT NULL, 
	[Description] NVARCHAR(250) NOT NULL,
    [EventTypeId] INT NOT NULL, 
    [StartDate] DATETIME2 NULL, 
    [EndDate] DATETIME2 NULL, 
	[ReviewDate] DATETIME2 NULL, 
    [EventStatusId] INT NOT NULL, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [Placement] INT NULL DEFAULT 0, 
    [Billable] BIT NULL DEFAULT 0, 
    [ProgramSitesId] INT NOT NULL, 
    CONSTRAINT [FK_Event_EventType] FOREIGN KEY ([EventTypeId]) REFERENCES [EventType]([Id]), 
    CONSTRAINT [FK_Event_EventStatus] FOREIGN KEY ([EventStatusId]) REFERENCES [EventStatus]([Id]), 
    CONSTRAINT [FK_Event_ProgramSites] FOREIGN KEY ([ProgramSitesId]) REFERENCES [ProgramSites]([Id])
)
