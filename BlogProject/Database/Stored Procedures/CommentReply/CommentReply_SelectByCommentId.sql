If Exists (Select * From sys.procedures Where Name = 'CommentReply_SelectByCommentId')
	Drop Procedure [dbo].[CommentReply_SelectByCommentId];
Go

Create Procedure [dbo].[CommentReply_SelectByCommentId]
	@CommentId int 
As
	Select  [CommentId]
			, [Author]
			, [Title]
			, [Content]
			, [DateCreated]
			, [DateModified]
	From	[dbo].[CommentReply] With (NoLock)
	Where	[CommentId] = @CommentId
Return 0
Go


/*
 ## TEST ##
Exec [dbo].[CommentReply_SelectByCommentId]
	@CommentId = 4
Go
*/