If Exists (Select * From sys.procedures Where Name = 'Comment_Insert')
	Drop Procedure [dbo].[Comment_Insert];
Go

Create Procedure [dbo].[Comment_Insert] -- All parameters that will be passed in
	@ID int Output
	, @BlogPostId int
	, @Title nvarchar(100)
	, @Content nvarchar(max)
	, @UserName nvarchar(50)
	
As
	Declare @Today datetime = GetUtcDate()

	Insert [dbo].[Comment] ( -- adding these values to the table
		[BlogPostId]
		, [Title]
		, [Content]
		, [UserName]
		, [DateCreated]
		, [DateModified]
	)
	Values ( -- setting the values to variables for the table
		@BlogPostId
		, @Title
		, @Content
		, @UserName
		, @Today
		, @Today
	);

	Set @ID = Scope_Identity()

Return 0
Go

/* ##TEST ##

-- Checking Blog table for BlogPostId to use
Select *
From dbo.Blog With (NoLock)

-- Inserting Comment
Declare @IDOut int
	, @BlogPostIdIn int = 5
	, @TitleIn nvarchar(100) = 'Test Title'
	, @ContentIn nvarchar(max) = 'blah blah blah'
	, @UserNameIn nvarchar(50) = 'DD';

Exec [dbo].[Comment_Insert]
	@ID = @IDOut Output
	, @BlogPostId = @BlogPostIdIn
	, @Title = @TitleIn
	, @Content = @ContentIn
	, @UserName = @UserNameIn 


Print @IDOut


-- Checking comment table after insert
Select *
From dbo.Comment With (NoLock)
*/