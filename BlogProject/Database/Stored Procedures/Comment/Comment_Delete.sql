If Exists (Select * From sys.procedures Where Name = 'Comment_Delete')
	Drop Procedure [dbo].[Comment_Delete];
Go

Create Procedure [dbo].[Comment_Delete]
	@ID int
As
	Delete	[dbo].[Comment]
	Where	[ID] = @ID

Return 0
Go


/*
 ## TEST ##

Exec [dbo].[Comment_SelectById]
	@ID = 1

Exec [dbo].[Comment_Delete]
	@ID = 1

Exec [dbo].[Comment_SelectById]
	@ID = 1
Go
*/

