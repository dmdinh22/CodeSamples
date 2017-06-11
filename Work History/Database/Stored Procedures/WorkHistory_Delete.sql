CREATE proc [dbo].[WorkHistory_Delete]
	@ID int
as


BEGIN

	  DELETE FROM [dbo].[WorkHistory]
	  WHERE ID = @ID;

END

/* ## TEST ##

		Declare @ID int = 25
		
		Select * 
		From dbo.WorkHistory
		WHERE ID = @ID;

		Execute dbo.WorkHistory_Delete @ID

		Select * 
		From dbo.WorkHistory
		WHERE ID = @ID;

*/