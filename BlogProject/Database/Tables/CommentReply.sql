Create Table [dbo].[CommentReply] (
	[ID] int Not Null Identity(1,1)
	, [CommentId] int Not Null
	, [Author] nvarchar(100) Not Null
	, [Title] nvarchar(100) Not Null
	, [Content] nvarchar(max) Not Null
	, [DateCreated] datetime Not Null
	, [DateModified] datetime Not Null
Constraint [PK_CommentReply] Primary Key (
	ID
	)
);

-- Foreign Keys
Alter Table [dbo].[CommentReply]
Add Constraint [FK_CommentReply_CommentId]
Foreign Key ([CommentId])
References [dbo].[Comment] ([ID])

-- Indexes
Create NonClustered Index [IX_CommentReply_CommentId] on [dbo].[CommentReply] (
	[CommentId]
)