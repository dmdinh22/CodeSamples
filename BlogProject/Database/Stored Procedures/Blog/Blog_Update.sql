If Exists (Select * From sys.procedures Where Name = 'Blog_Update')
	Drop Procedure [dbo].[Blog_Update];
Go

Create Procedure [dbo].[Blog_Update]
	@ID int
	, @Title nvarchar(100)
	, @Content nvarchar(max)
	--, @DateModified datetime -> not a variable we pass a parameter into when executing.
As
	Declare @Today datetime = GetUtcDate()
	Update	[dbo].[Blog]
	Set		[Title] = @Title
			, [Content] = @Content
			, [DateModified] = @Today
	Where	[ID] = @ID

Return 0
Go


/*
 ## TEST ##
Exec [dbo].[Blog_SelectById]
	@ID = 6

Exec [dbo].[Blog_Update]
	@ID = 6
	, @Title = 'some new updated title'
	, @Content = 'some new new updated content'

	Exec [dbo].[Blog_SelectById]
	@ID = 6
Go
*/

