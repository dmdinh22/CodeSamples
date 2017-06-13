If Exists (Select * From sys.procedures Where Name = 'CommentAdvanced_Delete')
	Drop Procedure [dbo].[CommentAdvanced_Delete];
Go

Create Procedure [dbo].[CommentAdvanced_Delete]
	@ID int
As
	Delete [dbo].[CommentAdvanced]
	Where [ID] = @ID

Return 0
Go


/*
 ## TEST ##

Exec [dbo].[CommentAdvanced_SelectById]
	@ID = 1

Exec [dbo].[CommentAdvanced_Delete]
	@ID = 1

Exec [dbo].[Comment_Select]
Go
*/

