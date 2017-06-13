If Exists (Select * From sys.procedures Where Name = 'CommentReply_SelectById')
	Drop Procedure [dbo].[CommentReply_SelectById];
Go

Create Procedure [dbo].[CommentReply_SelectById]
	@ID int 
As
	Select	[ID]
			, [CommentId]
			, [Author]
			, [Title]
			, [Content]
			, [DateCreated]
			, [DateModified]
	From	[dbo].[CommentReply] With (NoLock)
	Where	[ID] = @ID
Return 0
Go


/*
 ## TEST ##
Exec [dbo].[CommentReply_SelectById]
	@ID = 1
Go
*/