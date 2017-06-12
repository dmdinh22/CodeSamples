CREATE proc [dbo].[WorkHistory_SelectAll]

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


END

/*
	
	Execute dbo.WorkHistory_SelectAll

*/
