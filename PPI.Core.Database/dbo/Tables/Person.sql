CREATE TABLE [dbo].[Person] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50) NOT NULL,
    [LastName]     NVARCHAR (50) NOT NULL,
    [Gender]	   NVARCHAR (50) NULL,
	[Title]		   NVARCHAR (10) NULL,
	[AAMCNumber]   NVARCHAR (50) NOT NULL,
    [PrimaryEmail] NVARCHAR (150) NOT NULL,
    [Hogan_Id]     NVARCHAR (15) NULL,
    [Hogan_Password] NVARCHAR(50) NULL, 
    [CreateDate] DATETIME2 NULL DEFAULT GETDATE(), 
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC)
);

