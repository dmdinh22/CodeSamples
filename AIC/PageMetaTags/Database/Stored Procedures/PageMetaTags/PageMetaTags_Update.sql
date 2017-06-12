ALTER proc [dbo].[PageMetaTags_Update]
			@MetaTagID int
			, @Value nchar(10)
			, @OwnerID int
			, @OwnerTypeID int
			, @ID int

as

BEGIN

UPDATE [dbo].[PageMetaTags]
   SET [MetaTagID] = @MetaTagID
	  ,[Value] = @Value
	  ,[OwnerId] = @OwnerID
	  ,[OwnerTypeId] = @OwnerTypeID
 WHERE ID = @ID

END

/* ## TEST ##

Declare @ID int = 1;
	
	Declare @MetaTagID int = 2
	, @Value nchar(10) = 'edit'
	, @OwnerID int = 2
	, @OwnerTypeID = 2

	Select * from dbo.PageMetaTags
	Where ID = @ID

	Execute [dbo].[PageMetaTags_Update]
					@MetaTagID
					, @Value
					, @OwnerID
					, @OwnerTypeID
					, @ID

	Select * from dbo.PageMetaTags
	Where ID = @ID
*/

