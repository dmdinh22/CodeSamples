ALTER proc [dbo].[PageMetaTags_SelectAll]

as


BEGIN

SELECT [ID]
      ,[MetaTagID]
      ,[Value]
      ,[OwnerId]
      ,[OwnerTypeId]
  FROM [dbo].[PageMetaTags]

END

/*
	execute dbo.PageMetaTags_SelectAll
*/
