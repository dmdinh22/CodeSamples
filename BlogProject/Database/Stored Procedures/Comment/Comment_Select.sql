If Exists (Select * From sys.procedures Where Name = 'Comment_Select')
	Drop Procedure [dbo].[Comment_Select];
Go

Create Procedure [dbo].[Comment_Select]
As
	Select	[ID]
			, [BlogPostId]
			, [Title]
			, [Content]
			, [UserName]
			, [DateCreated]
			, [DateModified]
	From	[dbo].[Comment] With (NoLock)

Return 0
Go


/*
 ## TEST ##
Exec [dbo].[Comment_Select]
Go
*/