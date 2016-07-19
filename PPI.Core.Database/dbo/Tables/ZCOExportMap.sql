CREATE TABLE [dbo].[ZCOExportMap] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [HoganColName] VARCHAR (500) NOT NULL,
    [ZCOColName]   VARCHAR (500) NOT NULL,
    [Position]     INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

