
--Fix some Hogan Fields Data

UPDATE [dbo].[HoganField] SET [FieldName] = N'PCautious',[LabelName] = N'Cautious' WHERE [Id] = 85
UPDATE [dbo].[HoganField] SET [FieldName] = N'PReserved',[LabelName] = N'Reserved' WHERE [Id] = 86
UPDATE [dbo].[HoganField] SET [FieldName] = N'PLeisurely',[LabelName] = N'Leisurely' WHERE [Id] = 87
UPDATE [dbo].[HoganField] SET [FieldName] = N'PMischievous',[LabelName] = N'Mischievous' WHERE [Id] = 89
UPDATE [dbo].[HoganField] SET [FieldName] = N'PColorful',[LabelName] = N'Colorful' WHERE [Id] = 90
UPDATE [dbo].[HoganField] SET [FieldName] = N'PDiligent',[LabelName] = N'Diligent' WHERE [Id] = 92
UPDATE [dbo].[HoganField] SET [FieldName] = N'PAesthetic',[LabelName] = N'Aesthetic' WHERE [Id] = 155
UPDATE [dbo].[HoganField] SET [FieldName] = N'PAffiliation',[LabelName] = N'Affiliation' WHERE [Id] = 156
UPDATE [dbo].[HoganField] SET [FieldName] = N'PHedonistic',[LabelName] = N'Hedonistic' WHERE [Id] = 159
UPDATE [dbo].[HoganField] SET [FieldName] = N'PSecurity',[LabelName] = N'Security' WHERE [Id] = 163

 -- update [Residency_Select].[dbo].[PracticeCategoryScale]
 -- set [UpperBound] = 60
 -- where Id = 75

 --update [Residency_Select].[dbo].[PracticeCategoryScale]
 -- set [UpperBound] = 95
 -- where Id = 119

 --   update [Residency_Select].[dbo].[PracticeCategoryScale]
 -- set [UpperBound] = 60
 -- where Id = 77

 --update [Residency_Select].[dbo].[PracticeCategoryScale]
 -- set [UpperBound] = 95
 -- where Id = 139

--Create Report Title
DECLARE	@return_value Int

EXEC	@return_value = [dbo].[CreateResx]
		@DataColumnName = N'PracticeReport_ReportTitle',
		@FriendlyName = N'HPI Data Report',
		@Culture = N'en-US',
		@Value = N'HPI Data Report'

SELECT	@return_value as 'Return Value'

SET IDENTITY_INSERT [dbo].[PracticeReport] ON
insert into PracticeReport (ID,Name,ReportTitle,ReportTitleResxId,PracticeGroup, DefaultLogo,DefaultLogoMimeType,DefaultBackground,DefaultBackgroundMimeType,DefaultColor,Introduction, IntroductionResxId,IntroductionTwo,IntroductionTwoResxId,IntroductionThree,IntroductionThreeResxId,Active)
Select 12,'HPI DATA' as Name,ReportTitle,@return_value,PracticeGroup, DefaultLogo,DefaultLogoMimeType,DefaultBackground,DefaultBackgroundMimeType,DefaultColor,Introduction, IntroductionResxId,IntroductionTwo,IntroductionTwoResxId,IntroductionThree,IntroductionThreeResxId,Active from PracticeReport where ID = 9
SET IDENTITY_INSERT [dbo].[PracticeReport] OFF


EXEC	@return_value = [dbo].[CreateResx]
		@DataColumnName = N'PracticeReport_ReportTitle',
		@FriendlyName = N'HDS Data Report',
		@Culture = N'en-US',
		@Value = N'HDS Data Report'

SELECT	@return_value as 'Return Value'

SET IDENTITY_INSERT [dbo].[PracticeReport] ON
insert into PracticeReport (ID,Name,ReportTitle,ReportTitleResxId,PracticeGroup, DefaultLogo,DefaultLogoMimeType,DefaultBackground,DefaultBackgroundMimeType,DefaultColor,Introduction, IntroductionResxId,IntroductionTwo,IntroductionTwoResxId,IntroductionThree,IntroductionThreeResxId,Active)
Select 13,'HDS DATA' as Name,ReportTitle,@return_value,PracticeGroup, DefaultLogo,DefaultLogoMimeType,DefaultBackground,DefaultBackgroundMimeType,DefaultColor,Introduction, IntroductionResxId,IntroductionTwo,IntroductionTwoResxId,IntroductionThree,IntroductionThreeResxId,Active from PracticeReport where ID = 9
SET IDENTITY_INSERT [dbo].[PracticeReport] OFF




EXEC	@return_value = [dbo].[CreateResx]
		@DataColumnName = N'PracticeReport_ReportTitle',
		@FriendlyName = N'MVPI Data Report',
		@Culture = N'en-US',
		@Value = N'MVPI Data Report'

SELECT	@return_value as 'Return Value'

SET IDENTITY_INSERT [dbo].[PracticeReport] ON
insert into PracticeReport (ID,Name,ReportTitle,ReportTitleResxId,PracticeGroup, DefaultLogo,DefaultLogoMimeType,DefaultBackground,DefaultBackgroundMimeType,DefaultColor,Introduction, IntroductionResxId,IntroductionTwo,IntroductionTwoResxId,IntroductionThree,IntroductionThreeResxId,Active)
Select 14,'MVPI DATA' as Name,ReportTitle,@return_value,PracticeGroup, DefaultLogo,DefaultLogoMimeType,DefaultBackground,DefaultBackgroundMimeType,DefaultColor,Introduction, IntroductionResxId,IntroductionTwo,IntroductionTwoResxId,IntroductionThree,IntroductionThreeResxId,Active from PracticeReport where ID = 9
SET IDENTITY_INSERT [dbo].[PracticeReport] OFF

--- HPI SCALES
exec CreatePracticeScale 58,1002,1,1,0,34
exec CreatePracticeScale 58,1002,2,1,35,65
exec CreatePracticeScale 58,1002,3,1,65,100

exec CreatePracticeScale 58,1003,1,1,0,34
exec CreatePracticeScale 58,1003,2,1,35,65
exec CreatePracticeScale 58,1003,3,1,65,100

exec CreatePracticeScale 58,1004,1,1,0,34
exec CreatePracticeScale 58,1004,2,1,35,65
exec CreatePracticeScale 58,1004,3,1,65,100
-------------------------
exec CreatePracticeScale 59,1002,1,1,0,34
exec CreatePracticeScale 59,1002,2,1,35,65
exec CreatePracticeScale 59,1002,3,1,65,100

exec CreatePracticeScale 59,1003,1,1,0,34
exec CreatePracticeScale 59,1003,2,1,35,65
exec CreatePracticeScale 59,1003,3,1,65,100

exec CreatePracticeScale 59,1004,1,1,0,34
exec CreatePracticeScale 59,1004,2,1,35,65
exec CreatePracticeScale 59,1004,3,1,65,100
-------------------------
exec CreatePracticeScale 60,1002,1,1,0,34
exec CreatePracticeScale 60,1002,2,1,35,65
exec CreatePracticeScale 60,1002,3,1,65,100

exec CreatePracticeScale 60,1003,1,1,0,34
exec CreatePracticeScale 60,1003,2,1,35,65
exec CreatePracticeScale 60,1003,3,1,65,100

exec CreatePracticeScale 60,1004,1,1,0,34
exec CreatePracticeScale 60,1004,2,1,35,65
exec CreatePracticeScale 60,1004,3,1,65,100
--------------------
exec CreatePracticeScale 61,1002,1,1,0,34
exec CreatePracticeScale 61,1002,2,1,35,65
exec CreatePracticeScale 61,1002,3,1,65,100

exec CreatePracticeScale 61,1003,1,1,0,34
exec CreatePracticeScale 61,1003,2,1,35,65
exec CreatePracticeScale 61,1003,3,1,65,100

exec CreatePracticeScale 61,1004,1,1,0,34
exec CreatePracticeScale 61,1004,2,1,35,65
exec CreatePracticeScale 61,1004,3,1,65,100
--------------------
exec CreatePracticeScale 62,1002,1,1,0,34
exec CreatePracticeScale 62,1002,2,1,35,65
exec CreatePracticeScale 62,1002,3,1,65,100

exec CreatePracticeScale 62,1003,1,1,0,34
exec CreatePracticeScale 62,1003,2,1,35,65
exec CreatePracticeScale 62,1003,3,1,65,100

exec CreatePracticeScale 62,1004,1,1,0,34
exec CreatePracticeScale 62,1004,2,1,35,65
exec CreatePracticeScale 62,1004,3,1,65,100
--------------------
exec CreatePracticeScale 63,1002,1,1,0,34
exec CreatePracticeScale 63,1002,2,1,35,65
exec CreatePracticeScale 63,1002,3,1,65,100

exec CreatePracticeScale 63,1003,1,1,0,34
exec CreatePracticeScale 63,1003,2,1,35,65
exec CreatePracticeScale 63,1003,3,1,65,100

exec CreatePracticeScale 63,1004,1,1,0,34
exec CreatePracticeScale 63,1004,2,1,35,65
exec CreatePracticeScale 63,1004,3,1,65,100
--------------------
exec CreatePracticeScale 64,1002,1,1,0,34
exec CreatePracticeScale 64,1002,2,1,35,65
exec CreatePracticeScale 64,1002,3,1,65,100

exec CreatePracticeScale 64,1003,1,1,0,34
exec CreatePracticeScale 64,1003,2,1,35,65
exec CreatePracticeScale 64,1003,3,1,65,100

exec CreatePracticeScale 64,1004,1,1,0,34
exec CreatePracticeScale 64,1004,2,1,35,65
exec CreatePracticeScale 64,1004,3,1,65,100
------ HDS
--------------------
exec CreatePracticeScale 83,1002,1,1,0,10
exec CreatePracticeScale 83,1002,2,1,11,89
exec CreatePracticeScale 83,1002,3,1,90,100

exec CreatePracticeScale 83,1003,1,1,0,10
exec CreatePracticeScale 83,1003,2,1,11,89
exec CreatePracticeScale 83,1003,3,1,90,100

exec CreatePracticeScale 83,1004,1,1,0,10
exec CreatePracticeScale 83,1004,2,1,11,89
exec CreatePracticeScale 83,1004,3,1,90,100
--------------------
exec CreatePracticeScale 84,1002,1,1,0,10
exec CreatePracticeScale 84,1002,2,1,11,89
exec CreatePracticeScale 84,1002,3,1,90,100

exec CreatePracticeScale 84,1003,1,1,0,10
exec CreatePracticeScale 84,1003,2,1,11,89
exec CreatePracticeScale 84,1003,3,1,90,100

exec CreatePracticeScale 84,1004,1,1,0,10
exec CreatePracticeScale 84,1004,2,1,11,89
exec CreatePracticeScale 84,1004,3,1,90,100
--------------------
exec CreatePracticeScale 85,1002,1,1,0,10
exec CreatePracticeScale 85,1002,2,1,11,89
exec CreatePracticeScale 85,1002,3,1,90,100

exec CreatePracticeScale 85,1003,1,1,0,10
exec CreatePracticeScale 85,1003,2,1,11,89
exec CreatePracticeScale 85,1003,3,1,90,100

exec CreatePracticeScale 85,1004,1,1,0,10
exec CreatePracticeScale 85,1004,2,1,11,89
exec CreatePracticeScale 85,1004,3,1,90,100
--------------------
exec CreatePracticeScale 86,1002,1,1,0,10
exec CreatePracticeScale 86,1002,2,1,11,89
exec CreatePracticeScale 86,1002,3,1,90,100

exec CreatePracticeScale 86,1003,1,1,0,10
exec CreatePracticeScale 86,1003,2,1,11,89
exec CreatePracticeScale 86,1003,3,1,90,100

exec CreatePracticeScale 86,1004,1,1,0,10
exec CreatePracticeScale 86,1004,2,1,11,89
exec CreatePracticeScale 86,1004,3,1,90,100
--------------------
exec CreatePracticeScale 87,1002,1,1,0,10
exec CreatePracticeScale 87,1002,2,1,11,89
exec CreatePracticeScale 87,1002,3,1,90,100

exec CreatePracticeScale 87,1003,1,1,0,10
exec CreatePracticeScale 87,1003,2,1,11,89
exec CreatePracticeScale 87,1003,3,1,90,100

exec CreatePracticeScale 87,1004,1,1,0,10
exec CreatePracticeScale 87,1004,2,1,11,89
exec CreatePracticeScale 87,1004,3,1,90,100
--------------------
exec CreatePracticeScale 88,1002,1,1,0,10
exec CreatePracticeScale 88,1002,2,1,11,89
exec CreatePracticeScale 88,1002,3,1,90,100

exec CreatePracticeScale 88,1003,1,1,0,10
exec CreatePracticeScale 88,1003,2,1,11,89
exec CreatePracticeScale 88,1003,3,1,90,100

exec CreatePracticeScale 88,1004,1,1,0,10
exec CreatePracticeScale 88,1004,2,1,11,89
exec CreatePracticeScale 88,1004,3,1,90,100
--------------------
exec CreatePracticeScale 89,1002,1,1,0,10
exec CreatePracticeScale 89,1002,2,1,11,89
exec CreatePracticeScale 89,1002,3,1,90,100

exec CreatePracticeScale 89,1003,1,1,0,10
exec CreatePracticeScale 89,1003,2,1,11,89
exec CreatePracticeScale 89,1003,3,1,90,100

exec CreatePracticeScale 89,1004,1,1,0,10
exec CreatePracticeScale 89,1004,2,1,11,89
exec CreatePracticeScale 89,1004,3,1,90,100
--------------------
exec CreatePracticeScale 90,1002,1,1,0,10
exec CreatePracticeScale 90,1002,2,1,11,89
exec CreatePracticeScale 90,1002,3,1,90,100

exec CreatePracticeScale 90,1003,1,1,0,10
exec CreatePracticeScale 90,1003,2,1,11,89
exec CreatePracticeScale 90,1003,3,1,90,100

exec CreatePracticeScale 90,1004,1,1,0,10
exec CreatePracticeScale 90,1004,2,1,11,89
exec CreatePracticeScale 90,1004,3,1,90,100
--------------------
exec CreatePracticeScale 91,1002,1,1,0,10
exec CreatePracticeScale 91,1002,2,1,11,89
exec CreatePracticeScale 91,1002,3,1,90,100

exec CreatePracticeScale 91,1003,1,1,0,10
exec CreatePracticeScale 91,1003,2,1,11,89
exec CreatePracticeScale 91,1003,3,1,90,100

exec CreatePracticeScale 91,1004,1,1,0,10
exec CreatePracticeScale 91,1004,2,1,11,89
exec CreatePracticeScale 91,1004,3,1,90,100
--------------------
exec CreatePracticeScale 92,1002,1,1,0,10
exec CreatePracticeScale 92,1002,2,1,11,89
exec CreatePracticeScale 92,1002,3,1,90,100

exec CreatePracticeScale 92,1003,1,1,0,10
exec CreatePracticeScale 92,1003,2,1,11,89
exec CreatePracticeScale 92,1003,3,1,90,100

exec CreatePracticeScale 92,1004,1,1,0,10
exec CreatePracticeScale 92,1004,2,1,11,89
exec CreatePracticeScale 92,1004,3,1,90,100
--------------------
exec CreatePracticeScale 93,1002,1,1,0,10
exec CreatePracticeScale 93,1002,2,1,11,89
exec CreatePracticeScale 93,1002,3,1,90,100

exec CreatePracticeScale 93,1003,1,1,0,10
exec CreatePracticeScale 93,1003,2,1,11,89
exec CreatePracticeScale 93,1003,3,1,90,100

exec CreatePracticeScale 93,1004,1,1,0,10
exec CreatePracticeScale 93,1004,2,1,11,89
exec CreatePracticeScale 93,1004,3,1,90,100
------MVPI
--------------------
exec CreatePracticeScale 155,1002,1,1,0,34
exec CreatePracticeScale 155,1002,2,1,35,65
exec CreatePracticeScale 155,1002,3,1,66,100

exec CreatePracticeScale 155,1003,1,1,0,34
exec CreatePracticeScale 155,1003,2,1,35,65
exec CreatePracticeScale 155,1003,3,1,66,100

exec CreatePracticeScale 155,1004,1,1,0,34
exec CreatePracticeScale 155,1004,2,1,35,65
exec CreatePracticeScale 155,1004,3,1,66,100
--------------------
exec CreatePracticeScale 156,1002,1,1,0,34
exec CreatePracticeScale 156,1002,2,1,35,65
exec CreatePracticeScale 156,1002,3,1,66,100

exec CreatePracticeScale 156,1003,1,1,0,34
exec CreatePracticeScale 156,1003,2,1,35,65
exec CreatePracticeScale 156,1003,3,1,66,100

exec CreatePracticeScale 156,1004,1,1,0,34
exec CreatePracticeScale 156,1004,2,1,35,65
exec CreatePracticeScale 156,1004,3,1,66,100
--------------------
exec CreatePracticeScale 157,1002,1,1,0,34
exec CreatePracticeScale 157,1002,2,1,35,65
exec CreatePracticeScale 157,1002,3,1,66,100

exec CreatePracticeScale 157,1003,1,1,0,34
exec CreatePracticeScale 157,1003,2,1,35,65
exec CreatePracticeScale 157,1003,3,1,66,100

exec CreatePracticeScale 157,1004,1,1,0,34
exec CreatePracticeScale 157,1004,2,1,35,65
exec CreatePracticeScale 157,1004,3,1,66,100
--------------------
exec CreatePracticeScale 158,1002,1,1,0,34
exec CreatePracticeScale 158,1002,2,1,35,65
exec CreatePracticeScale 158,1002,3,1,66,100

exec CreatePracticeScale 158,1003,1,1,0,34
exec CreatePracticeScale 158,1003,2,1,35,65
exec CreatePracticeScale 158,1003,3,1,66,100

exec CreatePracticeScale 158,1004,1,1,0,34
exec CreatePracticeScale 158,1004,2,1,35,65
exec CreatePracticeScale 158,1004,3,1,66,100
--------------------
exec CreatePracticeScale 160,1002,1,1,0,34
exec CreatePracticeScale 160,1002,2,1,35,65
exec CreatePracticeScale 160,1002,3,1,66,100

exec CreatePracticeScale 160,1003,1,1,0,34
exec CreatePracticeScale 160,1003,2,1,35,65
exec CreatePracticeScale 160,1003,3,1,66,100

exec CreatePracticeScale 160,1004,1,1,0,34
exec CreatePracticeScale 160,1004,2,1,35,65
exec CreatePracticeScale 160,1004,3,1,66,100
--------------------
exec CreatePracticeScale 161,1002,1,1,0,34
exec CreatePracticeScale 161,1002,2,1,35,65
exec CreatePracticeScale 161,1002,3,1,66,100

exec CreatePracticeScale 161,1003,1,1,0,34
exec CreatePracticeScale 161,1003,2,1,35,65
exec CreatePracticeScale 161,1003,3,1,66,100

exec CreatePracticeScale 161,1004,1,1,0,34
exec CreatePracticeScale 161,1004,2,1,35,65
exec CreatePracticeScale 161,1004,3,1,66,100
--------------------
exec CreatePracticeScale 162,1002,1,1,0,34
exec CreatePracticeScale 162,1002,2,1,35,65
exec CreatePracticeScale 162,1002,3,1,66,100

exec CreatePracticeScale 162,1003,1,1,0,34
exec CreatePracticeScale 162,1003,2,1,35,65
exec CreatePracticeScale 162,1003,3,1,66,100

exec CreatePracticeScale 162,1004,1,1,0,34
exec CreatePracticeScale 162,1004,2,1,35,65
exec CreatePracticeScale 162,1004,3,1,66,100
--------------------
exec CreatePracticeScale 163,1002,1,1,0,34
exec CreatePracticeScale 163,1002,2,1,35,65
exec CreatePracticeScale 163,1002,3,1,66,100

exec CreatePracticeScale 163,1003,1,1,0,34
exec CreatePracticeScale 163,1003,2,1,35,65
exec CreatePracticeScale 163,1003,3,1,66,100

exec CreatePracticeScale 163,1004,1,1,0,34
exec CreatePracticeScale 163,1004,2,1,35,65
exec CreatePracticeScale 163,1004,3,1,66,100
--------------------
exec CreatePracticeScale 164,1002,1,1,0,34
exec CreatePracticeScale 164,1002,2,1,35,65
exec CreatePracticeScale 164,1002,3,1,66,100

exec CreatePracticeScale 164,1003,1,1,0,34
exec CreatePracticeScale 164,1003,2,1,35,65
exec CreatePracticeScale 164,1003,3,1,66,100

exec CreatePracticeScale 164,1004,1,1,0,34
exec CreatePracticeScale 164,1004,2,1,35,65
exec CreatePracticeScale 164,1004,3,1,66,100

--Text

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,59,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,59,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,59,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,60,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,60,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,60,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,63,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,63,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,63,0,30

--HDS

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,84,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,84,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,84,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,83,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,83,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,83,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,85,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,85,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,85,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,86,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,86,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,86,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,87,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,87,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,87,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,88,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,88,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,88,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,89,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,89,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,89,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,90,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,90,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,90,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,91,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,91,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,91,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,92,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,92,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,92,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,93,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,93,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,93,0,30

--MVPI
Exec [dbo].[CreatePracticeCategoryScale] 1,1002,155,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,155,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,155,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,156,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,156,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,156,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,157,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,157,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,157,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,158,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,158,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,158,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,160,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,160,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,160,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,161,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,161,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,161,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,162,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,162,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,162,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,163,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,163,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,163,0,30

Exec [dbo].[CreatePracticeCategoryScale] 1,1002,164,61,100
Exec [dbo].[CreatePracticeCategoryScale] 1,1003,164,31,60
Exec [dbo].[CreatePracticeCategoryScale] 1,1004,164,0,30


EXEC	[dbo].[CreatePracticeReportData] 58,1,1002,N'Will admit mistakes',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 58,1,1003,N'Will admit mistakes',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 58,1,1004,N'Tends to need a lot of reassurance',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 58,2,1002,N'Will listen to suggestions',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 58,2,1003,N'Will listen to suggestions',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 58,2,1004,N'Can be defensive about faults',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 58,3,1002,N'Calm and confident',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 58,3,1003,N'Calm and confident',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 58,3,1004,N'May not listen to feedback',N'en-US',1,12

EXEC	[dbo].[CreatePracticeReportData] 59,3,1004,N'May be overly competitive',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 59,3,1002,N'Energetic, confident and driven',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 59,3,1003,N'Energetic, confident and driven',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 59,2,1002,N'Supportive of team efforts',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 59,2,1003,N'Supportive of team efforts',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 59,2,1004,N'May tend to avoid leadership positions',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 59,1,1002,N'Modest and self-effacing',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 59,1,1003,N'Modest and self-effacing',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 59,1,1004,N'Avoids taking charge',N'en-US',1,12

EXEC	[dbo].[CreatePracticeReportData] 60,3,1002,N'Outgoing and socially self-confident',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 60,3,1003,N'Outgoing and socially self-confident',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 60,1,1002,N'Willing to work alone',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 60,1,1003,N'Willing to work alone',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 60,1,1004,N'May misread social situations',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 60,2,1002,N'Approachable, pleasant and willing to listen',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 60,2,1003,N'Approachable, pleasant and willing to listen',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 60,2,1004,N'May keep opinions to <himself> in meetings',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 60,3,1004,N'Likely to interrupt others',N'en-US',1,12

EXEC	[dbo].[CreatePracticeReportData] 61,3,1004,N'May promise more than can be delivered',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 61,3,1002,N'Warm, friendly and responsive to others',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 61,3,1003,N'Warm, friendly and responsive to others',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 61,2,1002,N'Tactful, considerate and patient',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 61,2,1003,N'Tactful, considerate and patient',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 61,2,1004,N'May be impatient with others',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 61,1,1002,N'Not easily intimidated',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 61,1,1003,N'Not easily intimidated',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 61,1,1004,N'Indifferent to the feelings of others',N'en-US',1,12

EXEC	[dbo].[CreatePracticeReportData] 62,1,1002,N'Flexible and comfortable with uncertainty',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 62,1,1003,N'Flexible and comfortable with uncertainty',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 62,1,1004,N'Can be very impulsive',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 62,2,1002,N'Can adapt to changing conditions',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 62,2,1003,N'Can adapt to changing conditions',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 62,3,1002,N'Conscientious, planful and self-disciplined',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 62,3,1003,N'Conscientious, planful and self-disciplined',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 62,2,1004,N'May need to be more flexible in ambigous situations',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 62,3,1004,N'May be very inflexible',N'en-US',1,12

EXEC	[dbo].[CreatePracticeReportData] 63,3,1004,N'May downplay operational challenges',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 63,1,1002,N'Detailed and process-oriented',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 63,1,1003,N'Detailed and process-oriented',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 63,1,1004,N'May have difficulty grasping the big picture',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 63,2,1002,N'Open minded and curious',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 63,2,1003,N'Open minded and curious',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 63,2,1004,N'May lose sight of the bigger picture',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 63,3,1002,N'Very effective in problem-solving situations',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 63,3,1003,N'Very effective in problem-solving situations',N'en-US',1,12

EXEC	[dbo].[CreatePracticeReportData] 64,3,1002,N'Well informed and interested in opportunities to learn',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 64,3,1003,N'Well informed and interested in opportunities to learn',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 64,1,1002,N'Is a hands-on learner',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 64,1,1003,N'Is a hands-on learner',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 64,1,1004,N'May not stay up to date',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 64,2,1002,N'Will stay informed on topics of interest',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 64,2,1003,N'Will stay informed on topics of interest',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 64,2,1004,N'May need to be encouraged to stay up to date',N'en-US',1,12
EXEC	[dbo].[CreatePracticeReportData] 64,3,1004,N'May come across as know-it-all',N'en-US',1,12


-- HDS Text



exec	@return_value = [dbo].[CreatePracticeReportData] 83,1,1002,N'Generally in a good mood',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Usually enthusiastic and positive'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 83,1,1003,N'Generally in a good mood',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Usually enthusiastic and positive'

exec	@return_value = [dbo].[CreatePracticeReportData] 83,1,1004,N'Convey a lack of urgency',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Little excitement or passion'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 83,2,1002,N'Handle frustration without getting upset',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Rarely gets angry with others'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 83,2,1003,N'Handle frustration without getting upset',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Rarely gets angry with others'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 83,2,1004,N'May lack a sense of urgency',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May seem to be disengaged'

exec	@return_value = [dbo].[CreatePracticeReportData] 83,3,1002,N'Sometimes may lose composure',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Sometimes get angry with others'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 83,3,1003,N'Sometimes may lose composure',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Sometimes get angry with others'

exec	@return_value = [dbo].[CreatePracticeReportData] 83,3,1004,N'Easily irritated and hard to please',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Moody easily annoyed and emotionally volatile'
	


exec	@return_value = [dbo].[CreatePracticeReportData] 84,1,1002,N'Accepts information uncritically
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Seems bright and perceptive
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 84,1,1003,N'Accepts information uncritically
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Seems bright and perceptive
'

exec	@return_value = [dbo].[CreatePracticeReportData] 84,1,1004,N'Niave acceptance of intent
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Failure to look for details when necessary
'

exec	@return_value = [dbo].[CreatePracticeReportData] 84,2,1002,N'Seem insightful about the motives of others
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Seems open-minded and tolerant
'

exec	@return_value = [dbo].[CreatePracticeReportData] 84,2,1003,N'Seem insightful about the motives of others
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Seems open-minded and tolerant
'

exec	@return_value = [dbo].[CreatePracticeReportData] 84,2,1004,N'May seem overly trusting
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be disinterested in organizational issues
'

exec	@return_value = [dbo].[CreatePracticeReportData] 84,3,1002,N'Sometimes may  be argumentative
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be hard to coach
'

exec	@return_value = [dbo].[CreatePracticeReportData] 84,3,1003,N'Sometimes may  be argumentative
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be hard to coach
'

exec	@return_value = [dbo].[CreatePracticeReportData] 84,3,1004,N'Challenges and blames others
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Distrustful and focused on the negative
'

-------------------------
exec	@return_value = [dbo].[CreatePracticeReportData] 85,1,1002,N'Will take action when needed
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Will act decisively
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 85,1,1003,N'Will take action when needed
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Will act decisively
'

exec	@return_value = [dbo].[CreatePracticeReportData] 85,1,1004,N'Take unnecessary chances`
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Fail to adequately balance cost-benefit
'

exec	@return_value = [dbo].[CreatePracticeReportData] 85,2,1002,N'May be initially reluctant to try new things
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be motivated by a fear of failure
'

exec	@return_value = [dbo].[CreatePracticeReportData] 85,2,1003,N'May be initially reluctant to try new things
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be motivated by a fear of failure
'

exec	@return_value = [dbo].[CreatePracticeReportData] 85,2,1004,N'May not consider potential pitfalls
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Take action without considering all potential risks
'

exec	@return_value = [dbo].[CreatePracticeReportData] 85,3,1002,N'Tries to avoid negative outcomes
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Tries to minimize risks
'

exec	@return_value = [dbo].[CreatePracticeReportData] 85,3,1003,N'Tries to avoid negative outcomes
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Tries to minimize risks
'

exec	@return_value = [dbo].[CreatePracticeReportData] 85,3,1004,N'Unassertive and resistant to change
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Risk averse and slow to make decisions
'
	
	

exec	@return_value = [dbo].[CreatePracticeReportData] 86,1,1002,N'Polite and considerate
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be slow  to seek help when stressed
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 86,1,1003,N'Polite and considerate
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be slow  to seek help when stressed
'

exec	@return_value = [dbo].[CreatePracticeReportData] 86,1,1004,N'Avoid potential conflict situations 
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Overly sensitive to the feelings of others
'

exec	@return_value = [dbo].[CreatePracticeReportData] 86,2,1002,N'Polite and considerate
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May become less  communicative when under stress
'

exec	@return_value = [dbo].[CreatePracticeReportData] 86,2,1003,N'Polite and considerate
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May become less  communicative when under stress
'

exec	@return_value = [dbo].[CreatePracticeReportData] 86,2,1004,N'May be unwilling to place strong demands on staff
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May communicate poorly in times of stress
'

exec	@return_value = [dbo].[CreatePracticeReportData] 86,3,1002,N'May be slow to perceive  morale issues
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be somewhat tough in interacting with people
v'

exec	@return_value = [dbo].[CreatePracticeReportData] 86,3,1003,N'May be slow to perceive  morale issues
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be somewhat tough in interacting with people
'

exec	@return_value = [dbo].[CreatePracticeReportData] 86,3,1004,N'Aloof and indifferent to the feelings of others
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Uncommunicative and uncaring
'
	

exec	@return_value = [dbo].[CreatePracticeReportData] 87,1,1002,N'Positive and responsive to feedback
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Respectful of established policies and practices
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 87,1,1003,N'Positive and responsive to feedback
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Respectful of established policies and practices
'

exec	@return_value = [dbo].[CreatePracticeReportData] 87,1,1004,N'Appear to lack conviction
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Overly cooperative
'

exec	@return_value = [dbo].[CreatePracticeReportData] 87,2,1002,N'Pleasant and cooperative
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Respectful and generally pleasant to work with
'

exec	@return_value = [dbo].[CreatePracticeReportData] 87,2,1003,N'Pleasant and cooperative
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Respectful and generally pleasant to work with
'

exec	@return_value = [dbo].[CreatePracticeReportData] 87,2,1004,N'May procrastinate
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May not follow through on commitments
'

exec	@return_value = [dbo].[CreatePracticeReportData] 87,3,1002,N'May sometimes overestimate thier authority
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May sometimes disregard others requests
'

exec	@return_value = [dbo].[CreatePracticeReportData] 87,3,1003,N'May sometimes overestimate thier authority
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May sometimes disregard others requests
'

exec	@return_value = [dbo].[CreatePracticeReportData] 87,3,1004,N'Publicly cooperative but privately irritable
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Uncooperative and stubborn
'
	
	

exec	@return_value = [dbo].[CreatePracticeReportData] 88,1,1002,N'Is not overly self-confident
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Content and unassuming
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 88,1,1003,N'Is not overly self-confident
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Content and unassuming
'

exec	@return_value = [dbo].[CreatePracticeReportData] 88,1,1004,N'Content with a follower role
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Failure to demonstrate confidence for views
'

exec	@return_value = [dbo].[CreatePracticeReportData] 88,2,1002,N'Assertive and confident
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Demanding and assertive
'

exec	@return_value = [dbo].[CreatePracticeReportData] 88,2,1003,N'Assertive and confident
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Demanding and assertive
'

exec	@return_value = [dbo].[CreatePracticeReportData] 88,2,1004,N'May be excessively demanding
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be willing to overpromise in an arrogant way
'

exec	@return_value = [dbo].[CreatePracticeReportData] 88,3,1002,N'Sometimes overly assertive and confident
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Sometimes overly demanding and assertive
'

exec	@return_value = [dbo].[CreatePracticeReportData] 88,3,1003,N'Sometimes overly assertive and confident
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Sometimes overly demanding and assertive
'

exec	@return_value = [dbo].[CreatePracticeReportData] 88,3,1004,N'Overly self-confident and unwilling to admit mistakes
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Arrogant with inflated feelings of self-worth
'
	


exec	@return_value = [dbo].[CreatePracticeReportData] 89,1,1002,N'Thinks before acting
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Makes good, sound decisions
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 89,1,1003,N'Thinks before acting
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Makes good, sound decisions
'

exec	@return_value = [dbo].[CreatePracticeReportData] 89,1,1004,N'Rigid adherance to procedures
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Unwillingness to try new things
'

exec	@return_value = [dbo].[CreatePracticeReportData] 89,2,1002,N'Charming, assertive and decisive
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Willing to take risks
'

exec	@return_value = [dbo].[CreatePracticeReportData] 89,2,1003,N'Charming, assertive and decisive
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Willing to take risks
'

exec	@return_value = [dbo].[CreatePracticeReportData] 89,2,1004,N'May test limits
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May take unnecessary risks
'

exec	@return_value = [dbo].[CreatePracticeReportData] 89,3,1002,N'Sometimes overly daring, and risk-seeking
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Sometimes overly assertive and decisive
'

exec	@return_value = [dbo].[CreatePracticeReportData] 89,3,1003,N'Sometimes overly daring, and risk-seeking
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Sometimes overly assertive and decisive
'

exec	@return_value = [dbo].[CreatePracticeReportData] 89,3,1004,N'Tests limits and make hasty decisions
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Not learn from experience and avoids discussing failures
'
	
	

exec	@return_value = [dbo].[CreatePracticeReportData] 90,1,1002,N'Modest and cooperative
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Cooperative and self-controlled
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 90,1,1003,N'Modest and cooperative
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Cooperative and self-controlled
'

exec	@return_value = [dbo].[CreatePracticeReportData] 90,1,1004,N'Failure to get noticed
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Take so low a profile that goes unnoticed
'

exec	@return_value = [dbo].[CreatePracticeReportData] 90,2,1002,N'Quick and socially skilled
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Entertaining, charming and interesting
'

exec	@return_value = [dbo].[CreatePracticeReportData] 90,2,1003,N'Quick and socially skilled
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Entertaining, charming and interesting
'

exec	@return_value = [dbo].[CreatePracticeReportData] 90,2,1004,N'Can be impulsive and disorganized
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Can confuse activity with effectiveness
'

exec	@return_value = [dbo].[CreatePracticeReportData] 90,3,1002,N'Entertaining but can be overly impulsive
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Enjoys multitasking but may be disorganized
'

exec	@return_value = [dbo].[CreatePracticeReportData] 90,3,1003,N'Entertaining but can be overly impulsive
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Enjoys multitasking but may be disorganized
'

exec	@return_value = [dbo].[CreatePracticeReportData] 90,3,1004,N'Seek attention and be disruptive
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Draws attention to themself to the point of disruption
'
	
	

exec	@return_value = [dbo].[CreatePracticeReportData] 91,1,1002,N'Grounded and practical
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be reluctant to suggest solutions
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 91,1,1003,N'Grounded and practical
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be reluctant to suggest solutions
'

exec	@return_value = [dbo].[CreatePracticeReportData] 91,1,1004,N'Miss opportunities to lead innovation
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Stifle creativity in others
'

exec	@return_value = [dbo].[CreatePracticeReportData] 91,2,1002,N'Steady and sensible
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Curious and sometimes unconventional
'

exec	@return_value = [dbo].[CreatePracticeReportData] 91,2,1003,N'Steady and sensible
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Curious and sometimes unconventional
'

exec	@return_value = [dbo].[CreatePracticeReportData] 91,2,1004,N'May maintain too low a profile
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May not contribute to problem solving
'

exec	@return_value = [dbo].[CreatePracticeReportData] 91,3,1002,N'May sometimes suggest impractical solutions
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Can be clever and innovative
'

exec	@return_value = [dbo].[CreatePracticeReportData] 91,3,1003,N'May sometimes suggest impractical solutions
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Can be clever and innovative
'

exec	@return_value = [dbo].[CreatePracticeReportData] 91,3,1004,N'Becomes unpredictable when stressed
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Thinks in odd, eccentric ways when stressed
'
	

exec	@return_value = [dbo].[CreatePracticeReportData] 92,1,1002,N'Tolerant and relaxed
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Not overly demanding 
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 92,1,1003,N'Tolerant and relaxed
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Not overly demanding 
'

exec	@return_value = [dbo].[CreatePracticeReportData] 92,1,1004,N'May be unwilling to hold others to standards
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Miss or omit critical details when delegating
'

exec	@return_value = [dbo].[CreatePracticeReportData] 92,2,1002,N'Holds others to agreed standards
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Maintain high standards for others
'

exec	@return_value = [dbo].[CreatePracticeReportData] 92,2,1003,N'Holds others to agreed standards
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Maintain high standards for others
'

exec	@return_value = [dbo].[CreatePracticeReportData] 92,2,1004,N'May supervise others too closely
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be overly demanding
'

exec	@return_value = [dbo].[CreatePracticeReportData] 92,3,1002,N'Sometimes has overly high standards
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Sometimes manage a bit to closely
'

exec	@return_value = [dbo].[CreatePracticeReportData] 92,3,1003,N'Sometimes has overly high standards
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Sometimes manage a bit to closely
'

exec	@return_value = [dbo].[CreatePracticeReportData] 92,3,1004,N'Micromanages
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Can be picky, critical and stubborn
'
	

exec	@return_value = [dbo].[CreatePracticeReportData] 93,1,1002,N'Independent and self-reliant
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Sometimes may challenge others inappropriately
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 93,1,1003,N'Independent and self-reliant
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Sometimes may challenge others inappropriately
'

exec	@return_value = [dbo].[CreatePracticeReportData] 93,1,1004,N'Innappropriately challenges authority
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Act independently to point of losing trust
'

exec	@return_value = [dbo].[CreatePracticeReportData] 93,2,1002,N'Generally outgoing and agreeable
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Comfortable working without close supervision 
'

exec	@return_value = [dbo].[CreatePracticeReportData] 93,2,1003,N'Generally outgoing and agreeable
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Comfortable working without close supervision 
'

exec	@return_value = [dbo].[CreatePracticeReportData] 93,2,1004,N'Uncomfortable working on tasks which will be closely supervised
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May make decisions without approval
'

exec	@return_value = [dbo].[CreatePracticeReportData] 93,3,1002,N'May promise more than can be delivered
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be indecisive
'

exec	@return_value = [dbo].[CreatePracticeReportData] 93,3,1003,N'May promise more than can be delivered
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be indecisive
'

exec	@return_value = [dbo].[CreatePracticeReportData] 93,3,1004,N'Overly eager to please
',N'en-US',1,13
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Reluctant to act independently
'
	
	-------------------------MVPI
	


exec	@return_value = [dbo].[CreatePracticeReportData] 161,1,1002,N'Enjoys working behind the scenes
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Will share credit with others
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 161,1,1003,N'Enjoys working behind the scenes
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Will share credit with others
'

exec	@return_value = [dbo].[CreatePracticeReportData] 161,1,1004,N'Avoids high visibility situations
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May not see a need to recognize the performance of others 
'

exec	@return_value = [dbo].[CreatePracticeReportData] 161,2,1002,N'Enjoys having others notice achievements
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Values praise for a job well done
'

exec	@return_value = [dbo].[CreatePracticeReportData] 161,2,1003,N'Enjoys having others notice achievements
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Values praise for a job well done
'

exec	@return_value = [dbo].[CreatePracticeReportData] 161,2,1004,N'May make decisions based on how they will play publicly
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May demotivate modest staff by overuse of public praise
'

exec	@return_value = [dbo].[CreatePracticeReportData] 161,3,1002,N'Seeks opportunities to highlight individual performance
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Likes to stand out and be recognized for performance
'

exec	@return_value = [dbo].[CreatePracticeReportData] 161,3,1003,N'Seeks opportunities to highlight individual performance
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Likes to stand out and be recognized for performance
'

exec	@return_value = [dbo].[CreatePracticeReportData] 161,3,1004,N'Dislikes environments that emphasize collaboration
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be overly self-promoting
'



exec	@return_value = [dbo].[CreatePracticeReportData] 160,1,1002,N'Values getting along with others
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Enjoys being part of a team
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 160,1,1003,N'Values getting along with others
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Enjoys being part of a team
'

exec	@return_value = [dbo].[CreatePracticeReportData] 160,1,1004,N'May seem to have little interest in leading others
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May shy away from competition
'

exec	@return_value = [dbo].[CreatePracticeReportData] 160,2,1002,N'Establish well defined work routines
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Maintain work-life balance
'

exec	@return_value = [dbo].[CreatePracticeReportData] 160,2,1003,N'Establish well defined work routines
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Maintain work-life balance
'

exec	@return_value = [dbo].[CreatePracticeReportData] 160,2,1004,N'May prefer to maintain the status quo
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May tolerate variations in performance
'

exec	@return_value = [dbo].[CreatePracticeReportData] 160,3,1002,N'Values hard work and achievement
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Values opportunities to make a difference
'

exec	@return_value = [dbo].[CreatePracticeReportData] 160,3,1003,N'Values hard work and achievement
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Values opportunities to make a difference
'

exec	@return_value = [dbo].[CreatePracticeReportData] 160,3,1004,N'May dislike others who lack a winning attitude
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be overly competitive
'



--exec	@return_value = [dbo].[CreatePracticeReportData] 159,1,1002,N'Puts business before pleasure
--',N'en-US',1,14
--select @return_value
--exec dbo.createalternativepracticetext @return_value,N'en-US',N'Values manners, procedures and protocol
--'

--exec	@return_value = 	[dbo].[CreatePracticeReportData] 159,1,1003,N'Puts business before pleasure
--',N'en-US',1,14
--select @return_value
--exec dbo.createalternativepracticetext @return_value,N'en-US',N'Values manners, procedures and protocol
--'

--exec	@return_value = [dbo].[CreatePracticeReportData] 159,1,1004,N'May not be able to enjoy themselves at work
--',N'en-US',1,14
--select @return_value
--exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be overly serious
--'

--exec	@return_value = [dbo].[CreatePracticeReportData] 159,2,1002,N'Maintains a well-planned and organized workplace
--',N'en-US',1,14
--select @return_value
--exec dbo.createalternativepracticetext @return_value,N'en-US',N'Promotes civility and good manners at work
--'

--exec	@return_value = [dbo].[CreatePracticeReportData] 159,2,1003,N'Maintains a well-planned and organized workplace
--',N'en-US',1,14
--select @return_value
--exec dbo.createalternativepracticetext @return_value,N'en-US',N'Promotes civility and good manners at work
--'

--exec	@return_value = [dbo].[CreatePracticeReportData] 159,2,1004,N'May resent those who aren''t sufficiently work focused
--',N'en-US',1,14
--select @return_value
--exec dbo.createalternativepracticetext @return_value,N'en-US',N'May prefer to approach things in a standardized way
--'

--exec	@return_value = [dbo].[CreatePracticeReportData] 159,3,1002,N'Enjoys variety and is interested in innovation 
--',N'en-US',1,14
--select @return_value
--exec dbo.createalternativepracticetext @return_value,N'en-US',N'Encourages expressiveness and spontaniety
--'

--exec	@return_value = [dbo].[CreatePracticeReportData] 159,3,1003,N'Enjoys variety and is interested in innovation 
--',N'en-US',1,14
--select @return_value
--exec dbo.createalternativepracticetext @return_value,N'en-US',N'Encourages expressiveness and spontaniety
--'

--exec	@return_value = [dbo].[CreatePracticeReportData] 159,3,1004,N'May dislike those who are overly serious
--',N'en-US',1,14
--select @return_value
--exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be intolerant of people who aren''t spontaneous
--'
	
	

exec	@return_value = [dbo].[CreatePracticeReportData] 157,1,1002,N'Values personal effectiveness
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Teaches others how to help themselves
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 157,1,1003,N'Values personal effectiveness
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Teaches others how to help themselves
'

exec	@return_value = [dbo].[CreatePracticeReportData] 157,1,1004,N'Focus on productivity over morale
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Focus on results over feelings
'

exec	@return_value = [dbo].[CreatePracticeReportData] 157,2,1002,N'Balances staff development with business performance
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Interested in helping others
'

exec	@return_value = [dbo].[CreatePracticeReportData] 157,2,1003,N'Balances staff development with business performance
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Interested in helping others
'

exec	@return_value = [dbo].[CreatePracticeReportData] 157,2,1004,N'May value business issues over staff problems
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May ignore the social aspects of work
'

exec	@return_value = [dbo].[CreatePracticeReportData] 157,3,1002,N'Desire to contribute to society
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Desire to help others
'

exec	@return_value = [dbo].[CreatePracticeReportData] 157,3,1003,N'Desire to contribute to society
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Desire to help others
'

exec	@return_value = [dbo].[CreatePracticeReportData] 157,3,1004,N'May not understand the need for self-reliance
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May focus on feelings over results
'



exec	@return_value = [dbo].[CreatePracticeReportData] 156,1,1002,N'Independent and self-reliant
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Prefers independence and to work on thier own
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 156,1,1003,N'Independent and self-reliant
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Prefers independence and to work on thier own
'

exec	@return_value = [dbo].[CreatePracticeReportData] 156,1,1004,N'Dislikes interruptions that are not immediately relevant
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Overly self-reliant
'

exec	@return_value = [dbo].[CreatePracticeReportData] 156,2,1002,N'Balances working independently with building relationships
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Can both work independently and as part of a team
'

exec	@return_value = [dbo].[CreatePracticeReportData] 156,2,1003,N'Balances working independently with building relationships
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Can both work independently and as part of a team
'

exec	@return_value = [dbo].[CreatePracticeReportData] 156,2,1004,N'May avoid interacting with strangers
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May avoid involving others in problem solving
'

exec	@return_value = [dbo].[CreatePracticeReportData] 156,3,1002,N'Looks at ways to maximize social interaction
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Enjoys frequent and varied social contact
'

exec	@return_value = [dbo].[CreatePracticeReportData] 156,3,1003,N'Looks at ways to maximize social interaction
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Enjoys frequent and varied social contact
'

exec	@return_value = [dbo].[CreatePracticeReportData] 156,3,1004,N'May assume group concensus is always appropriate
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May have too many things going on at the same time
'



exec	@return_value = [dbo].[CreatePracticeReportData] 164,1,1002,N'Appreciate work environment that encourages diverse views
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Appreciates an open-minded work environment
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 164,1,1003,N'Appreciate work environment that encourages diverse views
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Appreciates an open-minded work environment
'

exec	@return_value = [dbo].[CreatePracticeReportData] 164,1,1004,N'May want to change things just for the sake of change
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May not be respectful of organization traditions
'

exec	@return_value = [dbo].[CreatePracticeReportData] 164,2,1002,N'Appreciates established procedures
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Appreciates organizational traditions
'

exec	@return_value = [dbo].[CreatePracticeReportData] 164,2,1003,N'Appreciates established procedures
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Appreciates organizational traditions
'

exec	@return_value = [dbo].[CreatePracticeReportData] 164,2,1004,N'May judge others according to a set of personal values
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May not be open to change
'

exec	@return_value = [dbo].[CreatePracticeReportData] 164,3,1002,N'Has a strong belief system about what is right and wrong
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Has strong opinions about rules and procedures
'

exec	@return_value = [dbo].[CreatePracticeReportData] 164,3,1003,N'Has a strong belief system about what is right and wrong
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Has strong opinions about rules and procedures
'

exec	@return_value = [dbo].[CreatePracticeReportData] 164,3,1004,N'May want to work only with others who share values
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be rigid and judging of other people
'
	
	

exec	@return_value = [dbo].[CreatePracticeReportData] 163,1,1002,N'Prefer risk-taking and innovative work environment
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Views mistakes as opportunities
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 163,1,1003,N'Prefer risk-taking and innovative work environment
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Views mistakes as opportunities
'

exec	@return_value = [dbo].[CreatePracticeReportData] 163,1,1004,N'May not provide sufficient structure
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be too ready to take risks
'

exec	@return_value = [dbo].[CreatePracticeReportData] 163,2,1002,N'Is a careful and responsible person
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Is cautious in evaluating risks of actions
'

exec	@return_value = [dbo].[CreatePracticeReportData] 163,2,1003,N'Is a careful and responsible person
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Is cautious in evaluating risks of actions
'

exec	@return_value = [dbo].[CreatePracticeReportData] 163,2,1004,N'May not understand people who test the limits
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May need to be encouraged to take reasonable risks
'

exec	@return_value = [dbo].[CreatePracticeReportData] 163,3,1002,N'Prefer to minimize risk and uncertainty
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Dislike for making mistakes
'

exec	@return_value = [dbo].[CreatePracticeReportData] 163,3,1003,N'Prefer to minimize risk and uncertainty
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Dislike for making mistakes
'

exec	@return_value = [dbo].[CreatePracticeReportData] 163,3,1004,N'May require everything to be explicit
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be unwilling to take any risks
'
	
	

exec	@return_value = [dbo].[CreatePracticeReportData] 158,1,1002,N'Not particularly motivated by money
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Not focused only on money issues
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 158,1,1003,N'Not particularly motivated by money
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Not focused only on money issues
'

exec	@return_value = [dbo].[CreatePracticeReportData] 158,1,1004,N'May be indifferent to business strategy 
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be indifferent to profitability
'

exec	@return_value = [dbo].[CreatePracticeReportData] 158,2,1002,N'Pays attention to financial matters as well as other concerns
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Balances financial issues with other concerns
'

exec	@return_value = [dbo].[CreatePracticeReportData] 158,2,1003,N'Pays attention to financial matters as well as other concerns
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Balances financial issues with other concerns
'

exec	@return_value = [dbo].[CreatePracticeReportData] 158,2,1004,N'May under-appreciate the need for financial focus
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May not understand those who are indifferent to money
'

exec	@return_value = [dbo].[CreatePracticeReportData] 158,3,1002,N'Focuses on the business realities
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Focus on the bottom line
'

exec	@return_value = [dbo].[CreatePracticeReportData] 158,3,1003,N'Focuses on the business realities
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Focus on the bottom line
'

exec	@return_value = [dbo].[CreatePracticeReportData] 158,3,1004,N'Focus only on the bottom line
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May assume others are as materialistic as they are
'
	

	

exec	@return_value = [dbo].[CreatePracticeReportData] 155,1,1002,N'Values practicality over style
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Values practicality over creativity or innovation
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 155,1,1003,N'Values practicality over style
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Values practicality over creativity or innovation
'

exec	@return_value = [dbo].[CreatePracticeReportData] 155,1,1004,N'May underestimate the importance of appearance
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May equate quality with functionality alone
'

exec	@return_value = [dbo].[CreatePracticeReportData] 155,2,1002,N'Enjoys opportunity to encourage innovation and creativity
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Values both function and appearance
'

exec	@return_value = [dbo].[CreatePracticeReportData] 155,2,1003,N'Enjoys opportunity to encourage innovation and creativity
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Values both function and appearance
'

exec	@return_value = [dbo].[CreatePracticeReportData] 155,2,1004,N'May not appreciate the value of style
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May focus too much on function
'

exec	@return_value = [dbo].[CreatePracticeReportData] 155,3,1002,N'Encourages staff to challenge traditional approaches
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Enjoys taking the lead on issues of design
'

exec	@return_value = [dbo].[CreatePracticeReportData] 155,3,1003,N'Encourages staff to challenge traditional approaches
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Enjoys taking the lead on issues of design
'

exec	@return_value = [dbo].[CreatePracticeReportData] 155,3,1004,N'May underestimate the importance of function 
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May be impatient with those who don''t appear to value style
'
	


exec	@return_value = [dbo].[CreatePracticeReportData] 162,1,1002,N'Is good at spontaneous problem solving
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Can make quick intuitive decisions
'

exec	@return_value = 	[dbo].[CreatePracticeReportData] 162,1,1003,N'Is good at spontaneous problem solving
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Can make quick intuitive decisions
'

exec	@return_value = [dbo].[CreatePracticeReportData] 162,1,1004,N'May not like work that requires careful attention to data
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May rely only on intuition for decision making
'

exec	@return_value = [dbo].[CreatePracticeReportData] 162,2,1002,N'Balances analysis with action
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Prefers to make decisions on both data and experience
'

exec	@return_value = [dbo].[CreatePracticeReportData] 162,2,1003,N'Balances analysis with action
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Prefers to make decisions on both data and experience
'

exec	@return_value = [dbo].[CreatePracticeReportData] 162,2,1004,N'May struggle with others who require more data than necessary
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May not value the role of intuition
'

exec	@return_value = [dbo].[CreatePracticeReportData] 162,3,1002,N'Enjoys careful data analysis and problem solving
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Enjoys integrating data sources to support decisions
'

exec	@return_value = [dbo].[CreatePracticeReportData] 162,3,1003,N'Enjoys careful data analysis and problem solving
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'Enjoys integrating data sources to support decisions
'

exec	@return_value = [dbo].[CreatePracticeReportData] 162,3,1004,N'May not be able to make decisions quickly
',N'en-US',1,14
select @return_value
exec dbo.createalternativepracticetext @return_value,N'en-US',N'May not be able to rely on intuition  at all as an aspect of making decisions
'
	

----------------------------------------------------------------------------------------
