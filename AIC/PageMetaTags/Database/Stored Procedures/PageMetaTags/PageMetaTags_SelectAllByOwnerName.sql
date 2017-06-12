CREATE proc [dbo].[PageMetaTags_SelectAllByOwnerName]
	@OwnerName nvarchar(255) 
as


BEGIN

SELECT pmt.ID
      
	  ,mt.Name as MetaTagName
      ,pmt.Value as MetaTagValue
	  ,pmt.MetaTagID
	  ,ot.Name as OwnerName
	  ,pmt.OwnerTypeId
	  --,p.FirstName
	  --,p.LastName
	  --,pmt.OwnerId


	  from PageMetaTags pmt 
	  join MetaTags mt on pmt.MetaTagID = mt.ID 
	  join OwnerType ot on pmt.OwnerTypeId = ot.ID
	  --join People p on pmt.OwnerID = p.ID

where ot.Name = '/'

UNION
SELECT pmt.ID
      
	  ,mt.Name as MetaTagName
      ,pmt.Value as MetaTagValue
	  ,pmt.MetaTagID
	  ,ot.Name as OwnerName
	  ,pmt.OwnerTypeId

	  from PageMetaTags pmt 
	  join MetaTags mt on pmt.MetaTagID = mt.ID 
	  join OwnerType ot on pmt.OwnerTypeId = ot.ID

where ot.Name = @OwnerName
END


GO

/*
declare @ownername nvarchar(255) = 'resume'
	execute dbo.[PageMetaTags_SelectAllByOwnerName] @ownername
*/


