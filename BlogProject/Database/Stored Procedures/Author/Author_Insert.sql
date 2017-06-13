If Exists (Select * From sys.procedures Where Name = 'Author_Insert')
	Drop Procedure [dbo].[Author_Insert];
Go

Create Procedure [dbo].[Author_Insert]
	@ID int Output
	, @Email nvarchar(100)
As
	Declare @Today datetime = GetUtcDate()

	Insert	[dbo].[Author] (
			[Email]
			, [DateCreated]
	)
	Values (
			@Email
			, @Today
	);

	Set @ID = Scope_Identity()

Return 0
Go

/*
 ## TEST ##
Declare @IDOut int
	, @EmailIn nvarchar(100) = 'iamgetting@yourmom.com';

Exec [dbo].[Author_Insert]
	@ID = @IDOut Output
	, @Email = @EmailIn

Print @IDOut

Select *
From dbo.Author With (NoLock)
*/