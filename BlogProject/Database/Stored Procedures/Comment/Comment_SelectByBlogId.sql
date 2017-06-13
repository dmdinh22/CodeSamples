If Exists (Select * From sys.procedures Where Name = 'Comment_SelectByBlogId')
	Drop Procedure [dbo].[Comment_SelectByBlogId];
Go

Create Procedure [dbo].[Comment_SelectByBlogId]
	@BlogPostId int 
As
	Select  [BlogPostId]
			, [Title]
			, [Content]
			, [UserName]
			, [DateCreated]
			, [DateModified]
	From	[dbo].[Comment] With (NoLock)
	Where	[BlogPostId] = @BlogPostId
Return 0
Go


/*
 ## TEST ##
Exec [dbo].[Comment_SelectByBlogId]
	@BlogPostId = 4
Go
*/