ALTER proc [dbo].[PageMetaTags_Insert]
	@ID int OUTPUT
	, @MetaTagID int
	, @Value nchar(150)
	, @OwnerID int
	, @OwnerTypeID int

as


BEGIN


INSERT INTO [dbo].[PageMetaTags]
		   ([MetaTagID]
		   ,[Value]
		   ,[OwnerId]
		   ,[OwnerTypeId])
	 VALUES
		(@MetaTagID 
		, @Value 
		, @OwnerID 
		, @OwnerTypeID)

	SET @ID = SCOPE_IDENTITY()
END

/* ## TEST CODE ##

Declare @MetaTagID int = 10
	, @Value nchar(150) = 'An Inside Connection - Diversity'
	, @OwnerID int = 15
	, @OwnerTypeID int = 9

Execute dbo.MetaTags_Insert
		@MetaTagID 
		, @Value 
		, @OwnerID 
		, @OwnerTypeID

Select * from dbo.PageMetaTags

*/

