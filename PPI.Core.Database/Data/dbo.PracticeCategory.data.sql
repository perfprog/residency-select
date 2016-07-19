SET IDENTITY_INSERT [dbo].[PracticeCategory] ON
INSERT INTO [dbo].[PracticeCategory] ([Id], [Name], [PracticeGroup], [Order]) VALUES (1, N'Suggestions', N'Standard', 2)
INSERT INTO [dbo].[PracticeCategory] ([Id], [Name], [PracticeGroup], [Order]) VALUES (2, N'Challenges', N'Standard', 1)
INSERT INTO [dbo].[PracticeCategory] ([Id], [Name], [PracticeGroup], [Order]) VALUES (3, N'Strength', N'Standard', 0)
INSERT INTO [dbo].[PracticeCategory] ([Id], [Name], [PracticeGroup], [Order]) VALUES (1002, N'Above', N'Match', 0)
INSERT INTO [dbo].[PracticeCategory] ([Id], [Name], [PracticeGroup], [Order]) VALUES (1003, N'In', N'Match', 0)
INSERT INTO [dbo].[PracticeCategory] ([Id], [Name], [PracticeGroup], [Order]) VALUES (1004, N'Out', N'Match', 0)
SET IDENTITY_INSERT [dbo].[PracticeCategory] OFF
