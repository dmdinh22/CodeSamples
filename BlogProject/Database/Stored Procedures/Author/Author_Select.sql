If Exists (Select * From sys.procedures Where Name = 'Author_Select')
	Drop Procedure [dbo].[Author_Select];
Go

Create Procedure [dbo].[Author_Select]
As
	Select	[ID]
			, [Email]
			, [DateCreated]
	From	[dbo].[Author] With (NoLock)

Return 0
Go


/*
 ## TEST ##
Exec [dbo].[Author_Select]
Go
*/