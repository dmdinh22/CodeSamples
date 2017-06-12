USE [C30]
GO

/****** Object:  UserDefinedTableType [dbo].[MetaTagTableType]    Script Date: 6/11/2017 2:18:19 PM ******/
CREATE TYPE [dbo].[MetaTagTableType] AS TABLE(
	[ID] [int] NULL,
	[MetaTagID] [int] NULL,
	[Value] [nchar](150) NULL,
	[OwnerID] [int] NULL,
	[OwnerTypeID] [int] NULL
)
GO


