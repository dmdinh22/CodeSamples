If Exists (Select * From sys.procedures Where Name = 'Author_SelectByBlogId')
	Drop Procedure [dbo].[Author_SelectByBlogId];
Go

Create Procedure [dbo].[Author_SelectByBlogId]
	@BlogID int 
As
	Select	a.[ID]
			, a.[Email]
			, a.[DateCreated]
	From	[dbo].[Blog] b With (NoLock) Join
			[dbo].[Author] a With (NoLock) On b.AuthorId = a.ID
	Where	b.[ID] = @BlogID
Return 0
Go


/*
 ## TEST ##
 Select * From dbo.Blog

Exec [dbo].[Author_SelectByBlogId]
	@BlogID = 1
Go
*/

