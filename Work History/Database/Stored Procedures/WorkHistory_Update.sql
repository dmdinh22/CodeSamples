CREATE proc [dbo].[WorkHistory_Update]
		@Id int 
		, @PersonID int 
		, @CompanyID int
		, @Company nvarchar(20)
		, @Title nvarchar(20)
		, @Contract bit
		, @DateStarted date
		, @DateEnded date
		, @Description nvarchar(200)
as


BEGIN
Declare @Today datetime = GetUtcDate();

	UPDATE [dbo].[WorkHistory]
	   SET [PersonID] = @PersonID  
		   ,[CompanyID] = @CompanyID 
		   ,[Company] = @Company
		   ,[Title] = @Title 
		   ,[Contract] = @Contract 
		   ,[DateStarted] = @DateStarted 
		   ,[DateEnded] = @DateEnded 
		   ,[Description] = @Description 
		   ,[ModifiedDate] = @Today
	 WHERE ID = @ID

END

/* ## TEST ##

Declare @Id int = 3;

	Declare @PersonID int = 1
		, @CompanyID int = 0
		, @Company nvarchar(20) = 'Sabio'
		, @Title nvarchar(20) = 'Sabio GRAD!'
		, @Contract bit = 0
		, @DateStarted date = '2017-04-03'
		, @DateEnded date = null
		, @Description nvarchar(200) = 'WebDev'
		, @ModifiedDate datetime = GetUtcDate();

	
	Select *
	From dbo.WorkHistory 
	Where ID = @ID

	Execute dbo.WorkHistory_Update
				@Id
				, @PersonID  
				, @CompanyID 
				, @Company
				, @Title 
				, @Contract 
				, @DateStarted 
				, @DateEnded 
				, @Description 
			

	Select *
	From dbo.WorkHistory
	Where ID = @ID

*/
