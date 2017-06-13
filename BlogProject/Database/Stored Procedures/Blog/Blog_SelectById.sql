If Exists (Select * From sys.procedures Where Name = 'Blog_SelectById')
	Drop Procedure [dbo].[Blog_SelectById];
Go

Create Procedure [dbo].[Blog_SelectById]
	@ID int 
As
	Select	[ID]
			, [Title]
			, [Content]
			, [AuthorId]
			, [DateCreated]
			, [DateModified]
	From	[dbo].[Blog] With (NoLock)
	Where	[ID] = @ID
Return 0
Go


/*
 ## TEST ##
Exec [dbo].[Blog_SelectById]
	@ID = 4
Go
*/