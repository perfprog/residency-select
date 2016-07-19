CREATE TABLE [dbo].[ProgramSites]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SiteId] INT NOT NULL, 
    [ProgramId] INT NOT NULL, 
    CONSTRAINT [FK_ResidencyProgramSites_Site] FOREIGN KEY ([SiteId]) REFERENCES [Site]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_ResidencyProgramSitess_Program] FOREIGN KEY ([ProgramId]) REFERENCES [Program]([Id]) ON DELETE CASCADE
)
