If Exists (Select * From sys.procedures Where Name = 'CommentReply_Update')
	Drop Procedure [dbo].[CommentReply_Update];
Go

Create Procedure [dbo].[CommentReply_Update]
	@ID int
	, @Title nvarchar(100)
	, @Content nvarchar(max)
	--, @DateModified datetime -> not a variable we pass a parameter into when executing.

As
	Declare @Today datetime = GetUtcDate()
	Update	[dbo].[CommentReply]
	Set		[Title] = @Title
			, [Content] = @Content
			, [DateModified] = @Today
	Where	[ID] = @ID

Return 0
Go


/*
 ## TEST ##
Exec [dbo].[CommentReply_SelectById]
	@ID= 2

Exec [dbo].[CommentReply_Update]
	@ID= 2
	, @Title = 'updated commentreply title'
	, @Content = 'edited reply content'

Exec [dbo].[CommentReply_SelectById]
	@ID= 2
Go
*/

