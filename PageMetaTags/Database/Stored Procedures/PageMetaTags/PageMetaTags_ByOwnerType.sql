CREATE proc [dbo].[PageMetaTags_ByOwnerType]
	@Name nvarchar(255)
as


BEGIN
select pmt.Value

from pagemetatags pmt join ownertype ot

on pmt.OwnerTypeId = ot.id

where name = @Name

END
GO

/*
declare @name nvarchar(255) = 'contactus'
	execute dbo.[PageMetaTags_ByOwnerType] @name
*/


