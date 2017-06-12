create proc [dbo].[OwnerType_Insert]
	@ID int OUTPUT
	, @Name nvarchar(255)
	, @Description nvarchar(150)

as

BEGIN

INSERT INTO [dbo].[OwnerType]
           ([Name]
           ,[Description])
     VALUES
           (@Name
           , @Description)

	SET @ID = SCOPE_IDENTITY()
END



GO


