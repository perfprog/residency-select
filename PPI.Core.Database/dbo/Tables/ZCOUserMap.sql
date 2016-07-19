CREATE TABLE [dbo].[ZCOUserMap] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [RSUser] VARCHAR (100) NOT NULL,
    [ZCOUser]   VARCHAR (100) NOT NULL,
	[ZCOPwd]   VARCHAR (100) NOT NULL,    
    PRIMARY KEY CLUSTERED ([Id] ASC)
);