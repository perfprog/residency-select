CREATE TABLE [dbo].[SiteUsers]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
	[SiteId] INT NOT Null,
    [AspNetUsersId] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [FK_SiteUsers_AspNetUsers] FOREIGN KEY ([AspNetUsersId]) REFERENCES [AspNetUsers]([Id])  ON DELETE CASCADE, 
    CONSTRAINT [FK_SiteUsers_Site] FOREIGN KEY ([SiteId]) REFERENCES [Site]([Id])  ON DELETE CASCADE
)
