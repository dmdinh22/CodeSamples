If Exists (Select * From sys.procedures Where Name = 'Blog_Select')
	Drop Procedure [dbo].[Blog_Select];
Go

Create Procedure [dbo].[Blog_Select]
As
	Select	[ID]
			, [Title]
			, [Content]
			, [AuthorId]
			, [DateCreated]
			, [DateModified]
	From	[dbo].[Blog] With (NoLock)

Return 0
Go


/*
 ## TEST ##
Exec [dbo].[Blog_Select]
Go
*/