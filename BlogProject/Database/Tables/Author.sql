Create Table [dbo].[Author](
	[ID] int Not Null Identity(1,1)
	, [Email] nvarchar(100) Not Null
	, [DateCreated] datetime Not Null
Constraint [PK_Author] Primary Key (
[ID]
)
);

--- Foreign Keys

--- Indexes