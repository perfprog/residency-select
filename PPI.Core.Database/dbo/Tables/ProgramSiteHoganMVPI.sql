CREATE TABLE [dbo].[ProgramSiteHoganMVPI]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProgramSiteId] INT NOT NULL, 
    [HoganFieldName] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_ProgramSiteHoganMVPI_ResidencyProgramHospitals] FOREIGN KEY ([ProgramSiteId]) REFERENCES [ProgramSites]([Id]) ON DELETE CASCADE
)
