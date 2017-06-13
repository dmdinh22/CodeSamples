If Exists (Select * From sys.procedures Where Name = 'Author_SelectById')
	Drop Procedure [dbo].[Author_SelectById];
Go

Create Procedure [dbo].[Author_SelectById]
	@ID int 
As
	Select	[ID]
			, [Email]
			, [DateCreated]
	From	[dbo].[Author] With (NoLock)
	Where	[ID] = @ID
Return 0
Go


/*
 ## TEST ##
Exec [dbo].[Author_SelectById]
	@ID = 1
Go
*/