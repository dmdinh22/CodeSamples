USE [C30]
GO

/****** Object:  Table [dbo].[PageMetaTags]    Script Date: 5/19/2017 9:43:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PageMetaTags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MetaTagID] [int] NOT NULL,
	[Value] [nchar](150) NULL,
	[OwnerId] [int] NULL,
	[OwnerTypeId] [int] NULL,
 CONSTRAINT [PK_PageMetaTags] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PageMetaTags]  WITH CHECK ADD  CONSTRAINT [FK_PageMetaTags_MetaTags] FOREIGN KEY([MetaTagID])
REFERENCES [dbo].[MetaTags] ([ID])
GO

ALTER TABLE [dbo].[PageMetaTags] CHECK CONSTRAINT [FK_PageMetaTags_MetaTags]
GO

ALTER TABLE [dbo].[PageMetaTags]  WITH CHECK ADD  CONSTRAINT [FK_PageMetaTags_OwnerType] FOREIGN KEY([OwnerTypeId])
REFERENCES [dbo].[OwnerType] ([ID])
GO

ALTER TABLE [dbo].[PageMetaTags] CHECK CONSTRAINT [FK_PageMetaTags_OwnerType]
GO

ALTER TABLE [dbo].[PageMetaTags]  WITH CHECK ADD  CONSTRAINT [FK_PageMetaTags_People] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[People] ([ID])
GO

ALTER TABLE [dbo].[PageMetaTags] CHECK CONSTRAINT [FK_PageMetaTags_People]
GO


