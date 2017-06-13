If Exists (Select * From sys.procedures Where Name = 'Author_Update')
	Drop Procedure [dbo].[Author_Update];
Go

Create Procedure [dbo].[Author_Update]
			@ID int
			, @Email nvarchar(100)
As
	Update	[dbo].[Author]
	Set		[Email] = @Email
	Where	[ID] = @ID
Return 0
Go


/*
 ## TEST ##
Exec [dbo].[Author_SelectById]
	@ID = 1

Exec [dbo].[Author_Update]
	@ID = 1
	, @Email = 'blahblah@blah.net'

Exec [dbo].[Author_SelectById]
	@ID = 1
Go
*/

