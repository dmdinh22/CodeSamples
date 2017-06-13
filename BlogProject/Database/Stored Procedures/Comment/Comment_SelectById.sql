If Exists (Select * From sys.procedures Where Name = 'Comment_SelectById')
	Drop Procedure [dbo].[Comment_SelectById];
Go

Create Procedure [dbo].[Comment_SelectById]
	@ID int 
As
	Select	[ID]
			, [BlogPostId]
			, [Title]
			, [Content]
			, [UserName]
			, [DateCreated]
			, [DateModified]
	From	[dbo].[Comment] With (NoLock)
	Where	[ID] = @ID
Return 0
Go


/*
 ## TEST ##
Exec [dbo].[Comment_SelectById]
	@ID = 4
Go
*/