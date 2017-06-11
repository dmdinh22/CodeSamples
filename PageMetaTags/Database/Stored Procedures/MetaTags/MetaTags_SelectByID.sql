create proc [dbo].[MetaTags_SelectByID]
	@ID int

as


BEGIN

SELECT [ID]
	  ,[Name]
	  ,[Description]
  FROM [dbo].[MetaTags]
  	WHERE ID = @ID

END


GO

/*
	Declare @ID int = 1;
	execute dbo.MetaTags_SelectByID

*/


