SET IDENTITY_INSERT [dbo].[OrderStatus] ON
INSERT INTO [dbo].[OrderStatus] ([Id], [Name]) VALUES (1, N'InProcess')
INSERT INTO [dbo].[OrderStatus] ([Id], [Name]) VALUES (2, N'Complete')
SET IDENTITY_INSERT [dbo].[OrderStatus] OFF