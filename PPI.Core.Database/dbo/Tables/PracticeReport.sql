CREATE TABLE [dbo].[PracticeReport]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [ReportTitle] NVARCHAR(75) NULL DEFAULT 'Do Not Use As This Is A Placeholder For Multilingual Transformations', 
	[ReportTitleResxId] int,
	[PracticeGroup] NVARCHAR(50) NULL DEFAULT 'Standard',
    [DefaultLogo] IMAGE NULL, 
	[DefaultLogoMimeType] NVARCHAR(50) NULL, 
    [DefaultBackground] IMAGE NULL, 
	[DefaultBackgroundMimeType] NVARCHAR(50) NULL, 
    [DefaultColor] NVARCHAR(10) NULL, 
    [Introduction] NVARCHAR(75) NULL DEFAULT 'Do Not Use As This Is A Placeholder For Multilingual Transformations', 
    [IntroductionResxId] int,
	[IntroductionTwo] NVARCHAR(75) NULL DEFAULT 'Do Not Use As This Is A Placeholder For Multilingual Transformations', 
    [IntroductionTwoResxId] int,
	[IntroductionThree] NVARCHAR(75) NULL DEFAULT 'Do Not Use As This Is A Placeholder For Multilingual Transformations', 
	[IntroductionThreeResxId] int,
    [Active] BIT NULL DEFAULT 0, 
    CONSTRAINT [FK_PracticeReport_Resx] FOREIGN KEY ([ReportTitleResxId]) REFERENCES [Resx]([Id]),
	CONSTRAINT [FK_PracticeReport_IntroductionResx] FOREIGN KEY (IntroductionResxId) REFERENCES [Resx]([Id]),
	CONSTRAINT [FK_PracticeReport_IntroductionTwoResx] FOREIGN KEY (IntroductionTwoResxId) REFERENCES [Resx]([Id]),
	CONSTRAINT [FK_PracticeReport_IntroductionThreeResx] FOREIGN KEY (IntroductionThreeResxId) REFERENCES [Resx]([Id])

)
