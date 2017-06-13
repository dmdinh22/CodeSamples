If Exists (Select * From sys.procedures Where Name = 'CommentAdvanced_SelectByParentCommentId')
	Drop Procedure [dbo].[CommentAdvanced_SelectByParentCommentId];
Go

Create Procedure [dbo].[CommentAdvanced_SelectByParentCommentId]
	@ParentCommentId int Null
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
	Where	[ParentCommentId] = @ParentCommentId
Return 0
Go


/*
 ## TEST ##
Exec [dbo].[CommentAdvanced_Select]

Exec [dbo].[CommentAdvanced_SelectByParentCommentId]
	@ParentCommentId = 4
Go
*/