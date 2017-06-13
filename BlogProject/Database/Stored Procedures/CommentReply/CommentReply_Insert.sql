If Exists (Select * From sys.procedures Where Name = 'CommentReply_Insert')
	Drop Procedure [dbo].[CommentReply_Insert];
Go

Create Procedure [dbo].[CommentReply_Insert] -- All parameters that will be passed in
	@ID int Output
	, @CommentId int
	, @Author nvarchar(100)
	, @Title nvarchar(100)
	, @Content nvarchar(max)
	
As
	Declare @Today datetime = GetUtcDate()

	Insert [dbo].[CommentReply] ( -- adding these values to the table
		[CommentId]
		, [Author]
		, [Title]
		, [Content]
		, [DateCreated]
		, [DateModified]
	)
	Values ( -- setting the values to variables for the table
		@CommentId
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

-- Checking Comment table for CommentId to use
Select *
From dbo.Comment With (NoLock)

-- Inserting Comment Reply
Declare @IDOut int
	, @CommentIdIn int = 1
	, @AuthorIn nvarchar(50) = 'DD'
	, @TitleIn nvarchar(100) = 'Test Comment Reply'
	, @ContentIn nvarchar(max) = 'Comment Reply Content'
	

Exec [dbo].[CommentReply_Insert]
	@ID = @IDOut Output
	, @Author= @AuthorIn 
	, @CommentId = @CommentIdIn
	, @Title = @TitleIn
	, @Content = @ContentIn
	
Print @IDOut


-- Checking comment reply table after insert
Select *
From dbo.CommentReply With (NoLock)
*/