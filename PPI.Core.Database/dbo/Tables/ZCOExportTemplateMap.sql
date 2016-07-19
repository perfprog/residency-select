CREATE TABLE [dbo].[ZCOExportTemplateMap] (
    [Id]                  INT IDENTITY (1, 1) NOT NULL,
    [ZCOExportTemplateId] INT NOT NULL,
    [ZCOExportMapId]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ZCOExportTemplateMap_ZCOExportTemplate] FOREIGN KEY ([ZCOExportTemplateId]) REFERENCES [dbo].[ZCOExportTemplate] ([Id]),
    CONSTRAINT [FK_ZCOExportTemplateMap_ZCOExportMap] FOREIGN KEY ([ZCOExportMapId]) REFERENCES [dbo].[ZCOExportMap] ([Id])
);

