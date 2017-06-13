Create Table [dbo].[Blog] (
	[ID] int Not Null Identity(1,1)
	, [Title] nvarchar(100) Not Null
	, [Content] nvarchar(max) Not Null
	, [AuthorId] int Not Null
	, [DateCreated] datetime Not Null
	, [DateModified] datetime Not Null
Constraint [PK_Blog] Primary Key (
	ID
	)
);

-- Foreign Keys
Alter Table [dbo].[Blog]
Add Constraint [FK_Blog_AuthorId]
Foreign Key ([AuthorId])
References [dbo].[Author] ([ID])

-- Indexes
Create NonClustered Index [IX_Blog_AuthorId] on [dbo].[Blog] (
	[AuthorId]
)