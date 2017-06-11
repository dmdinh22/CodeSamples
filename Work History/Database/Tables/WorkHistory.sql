USE [C30]
GO

/****** Object:  Table [dbo].[WorkHistory]    Script Date: 4/15/2017 3:48:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WorkHistory](
	[ID] [int] Identity (1,1),
	[PersonID] [int] NOT NULL,
	[CompanyID] [int] NULL,
	[Title] [nvarchar](20) NOT NULL,
	[Contract] [bit] NULL,
	[DateStarted] [date] NOT NULL,
	[DateEnded] [date] NULL,
	[Description] [nvarchar](200) NOT NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NOT NULL
) ON [PRIMARY]

GO


