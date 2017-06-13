If Exists (Select * From sys.procedures Where Name = 'Comment_Update')
	Drop Procedure [dbo].[Comment_Update];
Go

Create Procedure [dbo].[Comment_Update]
	@ID int
	, @Title nvarchar(100)
	, @Content nvarchar(max)
	--, @DateModified datetime -> not a variable we pass a parameter into when executing.

As
	Declare @Today datetime = GetUtcDate()
	Update	[dbo].[Comment]
	Set		[Title] = @Title
			, [Content] = @Content
			, [DateModified] = @Today
	Where	[ID] = @ID

Return 0
Go


/*
 ## TEST ##
Exec [dbo].[Comment_SelectById]
	@ID= 2

Exec [dbo].[Comment_Update]
	@ID= 2
	, @Title = 'updated comment title'
	, @Content = 'edited content'

Exec [dbo].[Comment_SelectById]
	@ID= 2
Go
*/

