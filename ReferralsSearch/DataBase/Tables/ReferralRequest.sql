USE [C30]
GO

/****** Object:  Table [dbo].[ReferralRequestInternal]    Script Date: 5/27/2017 11:24:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReferralRequestInternal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReferrerId] [int] NULL,
	[CandidateId] [int] NOT NULL,
	[JobId] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[Accepted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReferralRequestInternal] ADD  CONSTRAINT [DF_Pending]  DEFAULT ((0)) FOR [Accepted]
GO


