CREATE proc [dbo].[WorkHistory_SelectByID]
	@ID int
as


BEGIN

	Select [ID]
	,[PersonID]
	,[CompanyID]
	,[Company]
	,[Title]
	,[Contract]
	,[DateStarted]
	,[DateEnded]
	,[Description]
	,[ModifiedBy]
	,[ModifiedDate]

	From [dbo].[WorkHistory] 
	WHERE ID = @ID


END

/*
	Declare @ID int = 2;

	Execute dbo.WorkHistory_SelectByID @ID

*/