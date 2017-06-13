If Exists (Select * From sys.procedures Where Name = 'CommentAdvanced_SelectByBlogId')
	Drop Procedure [dbo].[CommentAdvanced_SelectByBlogId];
Go

Create Procedure [dbo].[CommentAdvanced_SelectByBlogId]
	@BlogPostId int 
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
	Where	[BlogPostId] = @BlogPostId
	And		[ParentCommentId] is null
Return 0
Go


/*
 ## TEST ##
Exec [dbo].[CommentAdvanced_SelectByBlogId]
	@BlogPostId = 5
Go
*/