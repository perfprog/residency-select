SET IDENTITY_INSERT [dbo].[Resx] ON
INSERT INTO [dbo].[Resx] ([Id], [DataColumnDescription], [DataColumnName]) VALUES (5319, N'Reports', N'EmailType_Name')
SET IDENTITY_INSERT [dbo].[Resx] OFF

SET IDENTITY_INSERT [dbo].[ResxValue] ON
INSERT INTO [dbo].[ResxValue] ([Id], [ResxId], [CultureId], [Value]) VALUES (5318, 5319, 1, N'Reports')
SET IDENTITY_INSERT [dbo].[ResxValue] OFF

--Clean out any orphans
Delete from [dbo].[Email] where Id in (Select EM.Id FROM [dbo].[Email] em left outer join [Event] EV on em.EventId = ev.Id  where  EV.Id is null)

SET IDENTITY_INSERT [dbo].[EmailType] ON
INSERT INTO [dbo].[EmailType] ([Id], [Name], [NameResxId]) VALUES (6, N'Do Not Use', 5319)
SET IDENTITY_INSERT [dbo].[EmailType] OFF

insert into dbo.[Email] (EventId,EmailTypeId,DefaultFrom)
select EventId,6,DefaultFrom from dbo.[Email] where EmailTypeId = 3
