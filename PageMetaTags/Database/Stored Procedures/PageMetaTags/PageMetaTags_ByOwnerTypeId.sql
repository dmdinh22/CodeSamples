CREATE proc [dbo].[PageMetaTags_ByOwnerTypeId]
	@OwnerTypeId int
as



BEGIN
select ot.Name
		,pmt.Value

from pagemetatags pmt join ownertype ot

on pmt.OwnerTypeId = ot.id

where OwnerTypeId = @OwnerTypeId

END
GO

/*
declare @ownertypeid int = 10
	execute dbo.[PageMetaTags_ByOwnerTypeId] @ownertypeid
*/


