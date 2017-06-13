Create Table [dbo].[CommentAdvanced] (
	[ID] int Not Null Identity(1,1)
	, [BlogPostId] int Not Null
	, [ParentCommentId] int Null
	, [Author] nvarchar(100) Not Null
	, [Title] nvarchar(100) Not Null
	, [Content] nvarchar(max) Not Null
	, [DateCreated] datetime Not Null
	, [DateModified] datetime Not Null
Constraint [PK_CommentAdvanced] Primary Key (
	ID
	)
);

-- Foreign Keys
Alter Table [dbo].[CommentAdvanced]
Add Constraint [FK_CommentAdvanced_BlogPostId]
Foreign Key ([BlogPostId])
References [dbo].[Blog] ([ID])

Alter Table [dbo].[CommentAdvanced]
Add Constraint [FK_CommentAdvanced_ParentCommentId]
Foreign Key ([ParentCommentId])
References [dbo].[CommentAdvanced] ([ID])

-- Indexes
Create NonClustered Index [IX_CommentAdvanced_BlogPostId] on [dbo].[CommentAdvanced] (
	[BlogPostId]
)

Create NonClustered Index [IX_CommentAdvanced_ParentCommentId] on [dbo].[CommentAdvanced] (
	[ParentCommentId]
)