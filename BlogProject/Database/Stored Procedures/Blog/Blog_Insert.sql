If Exists (Select * From sys.procedures Where Name = 'Blog_Insert')
	Drop Procedure [dbo].[Blog_Insert];
Go

Create Procedure [dbo].[Blog_Insert] -- All parameters that will be passed in
	@ID int Output
	, @Title nvarchar(100)
	, @Content nvarchar(max)
	, @AuthorId int
As
	Declare @Today datetime = GetUtcDate()

	Insert	[dbo].[Blog] ( -- adding these values to the table
			[Title]
			, [Content]
			, [AuthorId]
			, [DateCreated]
			, [DateModified]
	)
	Values ( -- setting the values to variables for the table
			@Title
			, @Content
			, @AuthorId
			, @Today
			, @Today
	);

	Set @ID = Scope_Identity()

Return 0
Go

/* ##TEST ##

-- Checking Author table for AuthorId to use
Select *
From dbo.Author With (NoLock)

-- Inserting Blog
Declare @IDOut int
	, @TitleIn nvarchar(100) = 'Test Title'
	, @ContentIn nvarchar(max) = 'blah blah blah'
	, @AuthorIdIn int = 2;

Exec [dbo].[Blog_Insert]
	@ID = @IDOut Output
	, @Title = @TitleIn
	, @Content = @ContentIn
	, @AuthorId = @AuthorIdIn 


Print @IDOut

-- Checking Author table for AuthorId to use
Select *
From dbo.Author With (NoLock)

-- Checking blog table after insert
Select *
From dbo.Blog With (NoLock)
*/