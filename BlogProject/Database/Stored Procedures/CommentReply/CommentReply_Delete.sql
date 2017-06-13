If Exists (Select * From sys.procedures Where Name = 'CommentReply_Delete')
	Drop Procedure [dbo].[CommentReply_Delete];
Go

Create Procedure [dbo].[CommentReply_Delete]
	@ID int
As
	Delete [dbo].[CommentReply]
	Where [ID] = @ID

Return 0
Go


/*
 ## TEST ##

Exec [dbo].[CommentReply_SelectById]
	@ID = 1

Exec [dbo].[CommentReply_Delete]
	@ID = 1

Exec [dbo].[Comment_Select]
Go
*/

