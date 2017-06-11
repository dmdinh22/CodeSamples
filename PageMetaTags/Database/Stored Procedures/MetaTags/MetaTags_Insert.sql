create proc [dbo].[MetaTags_Insert]
	@ID int OUTPUT
	, @Name nvarchar(20)
	, @Description nvarchar(150)

as

BEGIN

INSERT INTO [dbo].[MetaTags]
           ([Name]
           ,[Description])
     VALUES
           (@Name
           , @Description)

	SET @ID = SCOPE_IDENTITY()
END



GO

/* ## TEST CODE ##
Declare @ID int = 0

Declare @Name = 'card'
		, @Description = 'summary, photo or player'

Execute dbo.MetaTags_Insert
		@ID OUTPUT
		, @Name
		, @Description

Select @ID

Select * from dbo.MetaTags

*/
