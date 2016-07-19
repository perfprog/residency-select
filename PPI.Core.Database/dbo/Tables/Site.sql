CREATE TABLE [dbo].[Site]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY , 
    [SiteName] NVARCHAR(250) NOT NULL, 
    [FriendlyName] NVARCHAR(250) NULL, 
    [BrandingColor] NVARCHAR(10) NULL, 
    [BrandingLogo] IMAGE NULL, 
    [BrandingLogoMimeType] NVARCHAR(50) NULL,
	[BrandingBackground] IMAGE NULL,     
	[BrandingBackgroundMimeType] NVARCHAR(50) NULL
)
