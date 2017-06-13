If Exists (Select * From sys.procedures Where Name = 'CommentAdvanced_SelectById')
	Drop Procedure [dbo].[CommentAdvanced_SelectById];
Go

Create Procedure [dbo].[CommentAdvanced_SelectById]
	@ID int 
As
	Select  [ID]
			, [BlogPostId]
			, [ParentCommentId]
			, [Author]
			, [Title]
			, [Content]
			, [DateCreated]
			, [DateModified]
	From	[dbo].[CommentAdvanced] With (NoLock)
	Where	[ID] = @ID
Return 0
Go


/*
 ## TEST ##
Exec [dbo].[CommentAdvanced_Select]

Exec [dbo].[CommentAdvanced_SelectById]
	@ID = 1
Go
*/