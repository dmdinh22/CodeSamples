CREATE proc [dbo].[PageMetaTags_SelectAllPlusName]
	@Owner_Name nvarchar(255) 
as

BEGIN

SELECT pmt.ID
      ,pmt.MetaTagID
	  ,mt.Name as MetaTag
      ,pmt.Value as MetaTagName
      ,pmt.OwnerId
      ,pmt.OwnerTypeId
	  
	  ,ot.Name as Owner_Name

	  from PageMetaTags pmt 
	  join MetaTags mt on pmt.MetaTagID = mt.ID 
	  join OwnerType ot on pmt.OwnerTypeId = ot.ID

where ot.Name = @Owner_Name
END


GO

/*
	execute dbo.[PageMetaTags_SelectAllPlusName]
*/



