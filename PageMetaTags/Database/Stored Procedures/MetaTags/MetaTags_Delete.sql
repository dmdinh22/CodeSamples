CREATE proc [dbo].[MetaTags_Delete]
	@ID int
as


DELETE FROM [dbo].[MetaTags]
      WHERE ID = @ID;

GO

/* ## TEST ##
	
	Declare @ID int = 5

	Select * from dbo.MetaTags 
	Where ID = @ ID

	Execute dbo.MetaTags_Delete @ID

	Select * from dbo.MetaTags 
	Where ID = @ ID

*/

