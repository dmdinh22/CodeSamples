CREATE proc [dbo].[MetaTags_Update]
		@Name nvarchar(20)
		, @Description nvarchar(150)
		, @ID int
		

as

BEGIN

UPDATE [dbo].[MetaTags]
   SET [Name] = @Name
      ,[Description] = @Description
 WHERE ID = @ID
END


GO

/* ## TEST ##

Declare @ID int = 1;
	
	Declare @Name nvarchar(20) = 'edited'
	, @Description nvarchar(150) = 'something'

	Select * from dbo.MetaTags
	Where ID = @ID

	Execute [dbo].[MetaTags_Update]
					@Name
					, @Description

	Select * from dbo.MetaTags
	Where ID = @ID
*/

