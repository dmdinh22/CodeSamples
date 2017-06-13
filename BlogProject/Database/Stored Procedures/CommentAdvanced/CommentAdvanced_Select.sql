If Exists (Select * From sys.procedures Where Name = 'CommentAdvanced_Select')
	Drop Procedure [dbo].[CommentAdvanced_Select];
Go

Create Procedure [dbo].[CommentAdvanced_Select]
As
	Select [ID]
			, [BlogPostId]
			, [ParentCommentId]
			, [Author]
			, [Title]
			, [Content]
			, [DateCreated]
			, [DateModified]
	From	[dbo].[CommentAdvanced] With (NoLock)


Return 0
Go


/*
 ## TEST ##
Exec [dbo].[CommentAdvanced_Select]
Go
*/