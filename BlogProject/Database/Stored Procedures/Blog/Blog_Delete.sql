If Exists (Select * From sys.procedures Where Name = 'Blog_Delete')
	Drop Procedure [dbo].[Blog_Delete];
Go

Create Procedure [dbo].[Blog_Delete]
	@ID int
As
	Delete	[dbo].[Blog]
	Where	[ID] = @ID

Return 0
Go


/*
 ## TEST ##

Exec [dbo].[Blog_SelectById]
	@ID = 1

Exec [dbo].[Blog_Delete]
	@ID = 1

Exec [dbo].[Blog_SelectById]
	@ID = 1
Go
*/

