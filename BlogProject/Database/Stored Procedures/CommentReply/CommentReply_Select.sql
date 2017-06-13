If Exists (Select * From sys.procedures Where Name = 'CommentReply_Select')
	Drop Procedure [dbo].[CommentReply_Select];
Go

Create Procedure [dbo].[CommentReply_Select]
As
	Select	[ID]
			, [CommentId]
			, [Author]
			, [Title]
			, [Content]
			, [DateCreated]
			, [DateModified]
	From	[dbo].[CommentReply] With (NoLock)

Return 0
Go


/*
 ## TEST ##
Exec [dbo].[CommentReply_Select]
Go
*/