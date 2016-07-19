CREATE TABLE [dbo].[PersonProgram]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NOT NULL, 
	[ProgramHospitalId] INT NOT NULL,     
    CONSTRAINT [FK_PersonPrograms_ResidencyProgramHospitals] FOREIGN KEY ([ProgramHospitalId]) REFERENCES [ProgramHospitals]([Id]), 
    CONSTRAINT [FK_PersonPrograms_People] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id])
)
