If Exists (Select * From sys.procedures Where Name = 'CommentAdvanced_Update')
	Drop Procedure [dbo].[CommentAdvanced_Update];
Go

Create Procedure [dbo].[CommentAdvanced_Update]
	@ID int
	, @Title nvarchar(100)
	, @Content nvarchar(max)
	--, @DateModified datetime -> not a variable we pass a parameter into when executing.
As
	Declare @Today datetime = GetUtcDate()
	Update	[dbo].[CommentAdvanced]
	Set		[Title] = @Title
			, [Content] = @Content
			, [DateModified] = @Today
	Where	[ID] = @ID

Return 0
Go


/*
 ## TEST ##
Exec [dbo].[CommentAdvanced_Select]

Exec [dbo].[CommentAdvanced_Update]
	@ID = 8
	, @Title = 'some new updated title'
	, @Content = 'some new new updated content'

	Exec [dbo].[CommentAdvanced_Select]
Go
*/

