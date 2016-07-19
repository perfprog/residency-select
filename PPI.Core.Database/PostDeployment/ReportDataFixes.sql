Declare @HoganField int

 set @HoganField = 58

Insert into PracticeScaleReport
Select PS2.Id as PracticeScaleId,7 as PracticeReportId,PSR.[Order],PSR.SubOrder,PSR.PracticeTextId,PSR.InterviewPracticeTextId
	from
	PracticeScaleReport PSR
	Join 
	(Select PS.Id,PS.HoganFieldId,Ps.PracticeLevelId,Ps.PracticeCategoryId,PS.LowerBound,PS.UpperBound,PS.ProgramId
	from PracticeScale PS join PracticeReport PR on PR.Id  = 7 join PracticeCategory PC on PS.PracticeCategoryId = PC.Id and PC.PracticeGroup = PR.PracticeGroup 
	where PS.HoganFieldId =  @HoganField  ) PS on PSR.PracticeScaleId = PS.Id and PSR.PracticeReportId = 7
	Join PracticeCategoryScale PCS on PS.PracticeCategoryId = PCS.PracticeCategoryId and PCS.HoganFieldId=@HoganField and PCS.ProgramId = 1
	Join PracticeCategory PC on PCS.PracticeCategoryId = PC.Id			
	Join PracticeText PT on PSR.PracticeTextId = PT.Id
	Join HoganField HF on PS.HoganFieldId = HF.Id
	join (Select * from PracticeScale where ProgramId = 1 and HoganFieldId = @HoganField and PracticeCategoryId in (1002,1003,1004)) PS2 on PS.HoganFieldId = PS2.HoganFieldId and PS.PracticeCategoryId = PS2.PracticeCategoryId and ps.PracticeLevelId = ps2.PracticeLevelId

set @HoganField = 61

Insert into PracticeScaleReport
Select PS2.Id as PracticeScaleId,7 as PracticeReportId,PSR.[Order],PSR.SubOrder,PSR.PracticeTextId,PSR.InterviewPracticeTextId
	from
	PracticeScaleReport PSR
	Join 
	(Select PS.Id,PS.HoganFieldId,Ps.PracticeLevelId,Ps.PracticeCategoryId,PS.LowerBound,PS.UpperBound,PS.ProgramId
	from PracticeScale PS join PracticeReport PR on PR.Id  = 7 join PracticeCategory PC on PS.PracticeCategoryId = PC.Id and PC.PracticeGroup = PR.PracticeGroup 
	where PS.HoganFieldId =  @HoganField  ) PS on PSR.PracticeScaleId = PS.Id and PSR.PracticeReportId = 7
	Join PracticeCategoryScale PCS on PS.PracticeCategoryId = PCS.PracticeCategoryId and PCS.HoganFieldId=@HoganField and PCS.ProgramId = 1
	Join PracticeCategory PC on PCS.PracticeCategoryId = PC.Id			
	Join PracticeText PT on PSR.PracticeTextId = PT.Id
	Join HoganField HF on PS.HoganFieldId = HF.Id
	join (Select * from PracticeScale where ProgramId = 1 and HoganFieldId = @HoganField and PracticeCategoryId in (1002,1003,1004)) PS2 on PS.HoganFieldId = PS2.HoganFieldId and PS.PracticeCategoryId = PS2.PracticeCategoryId and ps.PracticeLevelId = ps2.PracticeLevelId

	set @HoganField = 62

	Insert into PracticeScaleReport
Select PS2.Id as PracticeScaleId,7 as PracticeReportId,PSR.[Order],PSR.SubOrder,PSR.PracticeTextId,PSR.InterviewPracticeTextId
	from
	PracticeScaleReport PSR
	Join 
	(Select PS.Id,PS.HoganFieldId,Ps.PracticeLevelId,Ps.PracticeCategoryId,PS.LowerBound,PS.UpperBound,PS.ProgramId
	from PracticeScale PS join PracticeReport PR on PR.Id  = 7 join PracticeCategory PC on PS.PracticeCategoryId = PC.Id and PC.PracticeGroup = PR.PracticeGroup 
	where PS.HoganFieldId =  @HoganField  ) PS on PSR.PracticeScaleId = PS.Id and PSR.PracticeReportId = 7
	Join PracticeCategoryScale PCS on PS.PracticeCategoryId = PCS.PracticeCategoryId and PCS.HoganFieldId=@HoganField and PCS.ProgramId = 1
	Join PracticeCategory PC on PCS.PracticeCategoryId = PC.Id			
	Join PracticeText PT on PSR.PracticeTextId = PT.Id
	Join HoganField HF on PS.HoganFieldId = HF.Id
	join (Select * from PracticeScale where ProgramId = 1 and HoganFieldId = @HoganField and PracticeCategoryId in (1002,1003,1004)) PS2 on PS.HoganFieldId = PS2.HoganFieldId and PS.PracticeCategoryId = PS2.PracticeCategoryId and ps.PracticeLevelId = ps2.PracticeLevelId

	set @HoganField = 64

	Insert into PracticeScaleReport
Select PS2.Id as PracticeScaleId,7 as PracticeReportId,PSR.[Order],PSR.SubOrder,PSR.PracticeTextId,PSR.InterviewPracticeTextId
	from
	PracticeScaleReport PSR
	Join 
	(Select PS.Id,PS.HoganFieldId,Ps.PracticeLevelId,Ps.PracticeCategoryId,PS.LowerBound,PS.UpperBound,PS.ProgramId
	from PracticeScale PS join PracticeReport PR on PR.Id  = 7 join PracticeCategory PC on PS.PracticeCategoryId = PC.Id and PC.PracticeGroup = PR.PracticeGroup 
	where PS.HoganFieldId =  @HoganField  ) PS on PSR.PracticeScaleId = PS.Id and PSR.PracticeReportId = 7
	Join PracticeCategoryScale PCS on PS.PracticeCategoryId = PCS.PracticeCategoryId and PCS.HoganFieldId=@HoganField and PCS.ProgramId = 1
	Join PracticeCategory PC on PCS.PracticeCategoryId = PC.Id			
	Join PracticeText PT on PSR.PracticeTextId = PT.Id
	Join HoganField HF on PS.HoganFieldId = HF.Id
	join (Select * from PracticeScale where ProgramId = 1 and HoganFieldId = @HoganField and PracticeCategoryId in (1002,1003,1004)) PS2 on PS.HoganFieldId = PS2.HoganFieldId and PS.PracticeCategoryId = PS2.PracticeCategoryId and ps.PracticeLevelId = ps2.PracticeLevelId

	set @HoganField = 84

	Insert into PracticeScaleReport
Select PS2.Id as PracticeScaleId,7 as PracticeReportId,PSR.[Order],PSR.SubOrder,PSR.PracticeTextId,PSR.InterviewPracticeTextId
	from
	PracticeScaleReport PSR
	Join 
	(Select PS.Id,PS.HoganFieldId,Ps.PracticeLevelId,Ps.PracticeCategoryId,PS.LowerBound,PS.UpperBound,PS.ProgramId
	from PracticeScale PS join PracticeReport PR on PR.Id  = 7 join PracticeCategory PC on PS.PracticeCategoryId = PC.Id and PC.PracticeGroup = PR.PracticeGroup 
	where PS.HoganFieldId =  @HoganField  ) PS on PSR.PracticeScaleId = PS.Id and PSR.PracticeReportId = 7
	Join PracticeCategoryScale PCS on PS.PracticeCategoryId = PCS.PracticeCategoryId and PCS.HoganFieldId=@HoganField and PCS.ProgramId = 1
	Join PracticeCategory PC on PCS.PracticeCategoryId = PC.Id			
	Join PracticeText PT on PSR.PracticeTextId = PT.Id
	Join HoganField HF on PS.HoganFieldId = HF.Id
	join (Select * from PracticeScale where ProgramId = 1 and HoganFieldId = @HoganField and PracticeCategoryId in (1002,1003,1004)) PS2 on PS.HoganFieldId = PS2.HoganFieldId and PS.PracticeCategoryId = PS2.PracticeCategoryId and ps.PracticeLevelId = ps2.PracticeLevelId

	set @HoganField = 91

	Insert into PracticeScaleReport
Select PS2.Id as PracticeScaleId,7 as PracticeReportId,PSR.[Order],PSR.SubOrder,PSR.PracticeTextId,PSR.InterviewPracticeTextId
	from
	PracticeScaleReport PSR
	Join 
	(Select PS.Id,PS.HoganFieldId,Ps.PracticeLevelId,Ps.PracticeCategoryId,PS.LowerBound,PS.UpperBound,PS.ProgramId
	from PracticeScale PS join PracticeReport PR on PR.Id  = 7 join PracticeCategory PC on PS.PracticeCategoryId = PC.Id and PC.PracticeGroup = PR.PracticeGroup 
	where PS.HoganFieldId =  @HoganField  ) PS on PSR.PracticeScaleId = PS.Id and PSR.PracticeReportId = 7
	Join PracticeCategoryScale PCS on PS.PracticeCategoryId = PCS.PracticeCategoryId and PCS.HoganFieldId=@HoganField and PCS.ProgramId = 1
	Join PracticeCategory PC on PCS.PracticeCategoryId = PC.Id			
	Join PracticeText PT on PSR.PracticeTextId = PT.Id
	Join HoganField HF on PS.HoganFieldId = HF.Id
	join (Select * from PracticeScale where ProgramId = 1 and HoganFieldId = @HoganField and PracticeCategoryId in (1002,1003,1004)) PS2 on PS.HoganFieldId = PS2.HoganFieldId and PS.PracticeCategoryId = PS2.PracticeCategoryId and ps.PracticeLevelId = ps2.PracticeLevelId


	
  Delete from PracticeScaleReport where Id in 
  (Select PSR.Id from PracticeScaleReport PSR
  join PracticeScale PS on PSR.PracticeScaleId = PS.Id
   where ProgramId = 5)
  
  Delete from [PracticeScale] where ProgramId = 5

  Exec dbo.ClonePracticeScaleReport 7,1,5,0 --Match

  Exec dbo.ClonePracticeScaleReport 12,1,5,0

  Exec dbo.ClonePracticeScaleReport 13,1,5,0

  Exec dbo.ClonePracticeScaleReport 14,1,5,0

  Exec dbo.ClonePracticeScaleReport 12,1,7,0

  Exec dbo.ClonePracticeScaleReport 13,1,7,0

  Exec dbo.ClonePracticeScaleReport 14,1,7,0

  Exec dbo.ClonePracticeScaleReport 12,1,13,0

  Exec dbo.ClonePracticeScaleReport 13,1,13,0

  Exec dbo.ClonePracticeScaleReport 14,1,13,0

  Exec dbo.ClonePracticeScaleReport 12,1,4,0

  Exec dbo.ClonePracticeScaleReport 13,1,4,0

  Exec dbo.ClonePracticeScaleReport 14,1,4,0

  --Pediatric
  Exec dbo.ClonePracticeScaleReport 12,1,0,0

  Exec dbo.ClonePracticeScaleReport 13,1,0,0

  Exec dbo.ClonePracticeScaleReport 14,1,0,0

  --Autism
  Exec dbo.ClonePracticeScaleReport 12,1,8,0

  Exec dbo.ClonePracticeScaleReport 13,1,8,0

  Exec dbo.ClonePracticeScaleReport 14,1,8,0
 
 --Coll Med
  Exec dbo.ClonePracticeScaleReport 12,1,9,0

  Exec dbo.ClonePracticeScaleReport 13,1,9,0

  Exec dbo.ClonePracticeScaleReport 14,1,9,0

  --PPG
  Exec dbo.ClonePracticeScaleReport 12,1,10,0

  Exec dbo.ClonePracticeScaleReport 13,1,10,0

  Exec dbo.ClonePracticeScaleReport 14,1,10,0

  --LFS
  Exec dbo.ClonePracticeScaleReport 12,1,11,0

  Exec dbo.ClonePracticeScaleReport 13,1,11,0

  Exec dbo.ClonePracticeScaleReport 14,1,11,0

  --coll staff
  Exec dbo.ClonePracticeScaleReport 12,1,14,0

  Exec dbo.ClonePracticeScaleReport 13,1,14,0

  Exec dbo.ClonePracticeScaleReport 14,1,14,0

  --Anesther
  Exec dbo.ClonePracticeScaleReport 12,1,15,0

  Exec dbo.ClonePracticeScaleReport 13,1,15,0

  Exec dbo.ClonePracticeScaleReport 14,1,15,0


 Delete from [Residency_Select].[dbo].[PracticeCategoryScale] where id = 559

 Delete from [Residency_Select].[dbo].[PracticeCategoryScale] where ProgramId in (7,13)


Insert into PracticeCategoryScale
SELECT 7 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 7) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null

  Insert into PracticeCategoryScale
SELECT 13 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 13) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null

Insert into PracticeCategoryScale
SELECT 5 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 5) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null

insert into PracticeCategoryScale
SELECT 4 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 4) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null


 insert into PracticeCategoryScale
SELECT 0 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 0) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null

  insert into PracticeCategoryScale
SELECT 8 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 8) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null

  insert into PracticeCategoryScale
SELECT 9 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 9) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null

  insert into PracticeCategoryScale
SELECT 10 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 10) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null

  insert into PracticeCategoryScale
SELECT 11 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 11) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null

  insert into PracticeCategoryScale
SELECT 14 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 14) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null

  insert into PracticeCategoryScale
SELECT 15 as ProgramId,PCS.PracticeCategoryId,PCS.HoganFieldId,PCS.LowerBound,PCS.UpperBound
  FROM [Residency_Select].[dbo].[PracticeCategoryScale] PCS
  left outer join (Select * from PracticeCategoryScale where ProgramId = 15) PCS2 on PCS.HoganFieldId = PCS2.HoganFieldId and PCS.PracticeCategoryId = PCS2.PracticeCategoryId
  where PCS.ProgramId = 1 and PCS2.Id is null