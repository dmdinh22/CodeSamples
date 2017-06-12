CREATE TABLE [dbo].[Recruiter](
	[RecruiterID] [int] NOT NULL,
	[CompanyID] [int] NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Type] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Recruiter]  WITH CHECK ADD  CONSTRAINT [FK_Recruiter_Company] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([ID])
GO

CREATE TABLE [dbo].[Recruiter] CHECK CONSTRAINT [FK_Recruiter_Company]
GO

CREATE TABLE [dbo].[Recruiter]  WITH CHECK ADD  CONSTRAINT [FK_Recruiter_People] FOREIGN KEY([RecruiterID])
REFERENCES [dbo].[People] ([ID])
GO

CREATE TABLE [dbo].[Recruiter] CHECK CONSTRAINT [FK_Recruiter_People]
GO


