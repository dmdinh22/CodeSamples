CREATE proc [dbo].[MetaTags_SelectAll]

as

BEGIN

SELECT [ID]
      ,[Name]
      ,[Description]
	  ,[Enum]
  FROM [dbo].[MetaTags]

END


GO

/*
	execute dbo.MetaTags_SelectAll
*/


