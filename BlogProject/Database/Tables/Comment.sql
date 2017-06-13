Create Table [dbo].[Comment] (
	[ID] int Not Null Identity(1,1)
	, [BlogPostId] int Not Null
	, [Title] nvarchar(100) Not Null
	, [Content] nvarchar(max) Not Null
	, [UserName] nvarchar(50) Not Null
	, [DateCreated] datetime Not Null
	, [DateModified] datetime Not Null
Constraint [PK_Comment] Primary Key (
	ID
	)
);

-- Foreign Keys
Alter Table [dbo].[Comment]
Add Constraint [FK_Comment_BlogPostId]
Foreign Key ([BlogPostId])
References [dbo].[Blog] ([ID])

-- Indexes
Create NonClustered Index [IX_Comment_BlogPostId] on [dbo].[Comment] (
	[BlogPostId]
)