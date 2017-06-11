ALTER proc [dbo].[PageMetaTags_SelectById]
	@ID int

as


BEGIN

SELECT [ID]
	  ,[MetaTagID]
	  ,[Value]
	  ,[OwnerId]
	  ,[OwnerTypeId]
  FROM [dbo].[PageMetaTags]
  Where ID = @ID

END

/*
	Declare @ID int = 1;
	execute dbo.PageMetaTags_SelectById @ID
*/

