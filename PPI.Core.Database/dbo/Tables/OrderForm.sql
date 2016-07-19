CREATE TABLE [dbo].[OrderForm]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [OrderStatusId] INT NULL,
	[OrderDate] DATETIME2 NOT NULL, 
    [Site] NVARCHAR(150) NULL, 
    [ProgramId] INT NOT NULL, 
    [EventName] NVARCHAR(150) NOT NULL, 
    [EventDescription] NVARCHAR(150) NULL, 
    [Billable] BIT NULL DEFAULT 0, 
    [SalesType] NVARCHAR(50) NULL, 
    [EventTypeId] INT NOT NULL, 
    [StartDate] DATETIME2 NULL,
	[EndDate] DATETIME2 NULL, 
    [ReviewDate] DATETIME2 NULL, 
    [MatchDate] DATETIME2 NULL, 
    [Placement] INT NULL,      
    [Info] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_OrderForm_Program] FOREIGN KEY ([ProgramId]) REFERENCES [Program]([Id]), 
    CONSTRAINT [FK_OrderForm_EventType] FOREIGN KEY ([EventTypeId]) REFERENCES [EventType]([Id]), 
    CONSTRAINT [FK_OrderForm_OrderStatus] FOREIGN KEY ([OrderStatusId]) REFERENCES [OrderStatus]([Id])
)
