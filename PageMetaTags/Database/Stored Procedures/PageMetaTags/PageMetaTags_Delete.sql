
ALTER proc [dbo].[PageMetaTags_Delete]
	@ID int
as

BEGIN
	DELETE FROM [dbo].[PageMetaTags]
		  WHERE ID = @ID;
	  
END

	/* ## TEST ##
	
	Declare @ID int = 9

	Select * from dbo.PageMetaTags 
	Where ID = @ID

	Execute dbo.PageMetaTags_Delete @ID

	Select * from dbo.PageMetaTags 
	Where ID = @ID

*/