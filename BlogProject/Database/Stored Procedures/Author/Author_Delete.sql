If Exists (Select * From sys.procedures Where Name = 'Author_Delete')
	Drop Procedure [dbo].[Author_Delete];
Go

Create Procedure [dbo].[Author_Delete]
	@ID int
As
	Delete	[dbo].[Author]
	Where	[ID] = @ID

Return 0
Go


/*
 ## TEST ##

Exec [dbo].[Author_SelectById]
	@ID = 1

Exec [dbo].[Author_Delete]
	@ID = 1

Exec [dbo].[Author_SelectById]
	@ID = 1
Go
*/

