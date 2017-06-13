If Exists (Select * From sys.procedures Where Name = 'CommentAdvanced_Insert')
	Drop Procedure [dbo].[CommentAdvanced_Insert];
Go

Create Procedure [dbo].[CommentAdvanced_Insert] -- All parameters that will be passed in
	@ID int Output
	, @BlogPostId int
	, @ParentCommentId int Null
	, @Author nvarchar(100)
	, @Title nvarchar(100)
	, @Content nvarchar(max)
	
	
As
	Declare @Today datetime = GetUtcDate()

	Insert [dbo].[CommentAdvanced] ( -- adding these values to the table
		[BlogPostId]
		, [ParentCommentId]
		, [Author]
		, [Title]
		, [Content]
		, [DateCreated]
		, [DateModified]
	)
	Values ( -- setting the values to variables for the table
		@BlogPostId
		, @ParentCommentId
		, @Author
		, @Title
		, @Content
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

-- Inserting CommentAdvanced
Declare @IDOut int
	, @BlogPostIdIn int = 6
	, @ParentCommentIdIn int = null
	, @AuthorIn nvarchar(100) = 'DD'
	, @TitleIn nvarchar(100) = 'Test CommentAdvancedTitle'
	, @ContentIn nvarchar(max) = 'CommentAdvanced blah blah blah'
	

Exec [dbo].[CommentAdvanced_Insert]
	@ID = @IDOut Output
	, @BlogPostId = @BlogPostIdIn
	, @ParentCommentId = @ParentCommentIdIn
	, @Author = @AuthorIn 
	, @Title = @TitleIn
	, @Content = @ContentIn
	
Print @IDOut


-- Checking comment table after insert
Select *
From dbo.Comment With (NoLock)
*/