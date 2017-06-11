CREATE proc [dbo].[WorkHistory_Insert]
		@Id int OUTPUT
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

Declare @ModifiedBy uniqueIdentifier = NEWID()

INSERT INTO [dbo].[WorkHistory]
		   ([PersonID]
		   ,[CompanyID]
		   ,[Company]
		   ,[Title]
		   ,[Contract]
		   ,[DateStarted]
		   ,[DateEnded]
		   ,[Description]
		   ,[ModifiedBy]
		   ,[ModifiedDate])
	 VALUES
		   (@PersonID  
			, @CompanyID 
			, @Company
			, @Title 
			, @Contract 
			, @DateStarted 
			, @DateEnded 
			, @Description 
			, @ModifiedBy 
			, @Today)
			
	SET @ID = SCOPE_IDENTITY()
	
END 
/* ## TEST CODE ##
	Declare @Id int = 1;

	Declare @Today datetime = GetUtcDate();

	Declare @PersonID int = 0
		, @CompanyID int = 0
		, @Company nvarchar(20) = 'Sabio'
		, @Title nvarchar(20) = 'Developer'
		, @Contract bit = 1
		, @DateStarted date = '2017-04-03'
		, @DateEnded date = null
		, @Description nvarchar(200) = 'Web Development'


	Execute dbo.WorkHistory_Insert 
			@Id OUTPUT
			, @PersonID 
			, @CompanyID 
			, @Company 
			, @Title 
			, @Contract 
			, @DateStarted 
			, @DateEnded 
			, @Description 


	Select @ID

	Select *
	From dbo.WorkHistory
	Where ID = @ID
*/

