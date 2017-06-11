create proc [dbo].[OwnerType_SelectAll]

as

BEGIN

SELECT [ID]
      ,[Name]
      ,[Description]
  FROM [dbo].[OwnerType]

END


GO
/*
	execute dbo.OwnerType_SelectAll

	select * from dbo.ownertype
*/

