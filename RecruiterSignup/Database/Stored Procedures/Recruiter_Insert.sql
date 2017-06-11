CREATE PROC [dbo].[Recruiter_Insert]
	@RecruiterID int OUTPUT
	, @CompanyID int OUTPUT
	, @Email nvarchar(256)
	, @Type nvarchar(50)
	, @PhoneTypeID int OUTPUT
	, @Title nvarchar(50)
	, @PhoneTypeDescription nvarchar(50)
	, @PhoneID int OUTPUT
	, @PersonID int
	, @AreaCode nchar(3)
	, @Prefix nchar(3)
	, @Number nchar(4)
	, @Extension nchar(5)
	, @CreatedBy uniqueidentifier
	, @AddressID int OUTPUT
	, @Line1 nvarchar(100)
	, @Line2 nvarchar(100)
	, @City nvarchar(100)
	, @State nchar(2)
	, @PostalCode nvarchar(10)
	, @Active bit
	, @CreateBy uniqueidentifier
	, @Name nvarchar(50)
	, @CompanyDescription nvarchar(100)
	, @WebsiteURL nvarchar(100)
	, @LogoURL nchar(10)
	, @DisplayOrder int
	
AS

BEGIN 
	BEGIN TRANSACTION;
	SAVE TRANSACTION RITransaction;

	BEGIN TRY
	
	Declare @Today datetime = GetUtcDate()

	INSERT INTO [dbo].[PhoneType](
		[Title]
		, [Description]
		, [createby]
		, [modifiedDate])
	VALUES (
		@Title
		, @PhoneTypeDescription
		, @createby
		, @Today);

	SET @PhoneTypeID = SCOPE_IDENTITY()

	INSERT INTO [dbo].[Phone](
		[PersonId]
		, [PhoneTypeId]
		, [AreaCode]
		, [Prefix]
		, [Number]
		, [Extension]
		, [CreatedBy]
		, [ModifiedDate])
	Values (
		@PersonId
		, @PhoneTypeID
		, @AreaCode
		, @Prefix
		, @Number
		, @Extension
		, @CreatedBy
		, @Today);

	SET @PhoneID = SCOPE_IDENTITY()

	INSERT INTO [dbo].[Address](
		[PersonID]
		, [Line1]
		, [Line2]
		, [City]
		, [State]
		, [PostalCode]
		, [Active]
		, [CreatedBy]
		, [ModifiedBy]
		, [ModifiedDate])
	Values (	
		@RecruiterID
		, @Line1
		, @Line2
		, @City
		, @State
		, @PostalCode
		, @Active
		, @CreatedBy
		, @CreatedBy
		, @Today);

	SET @AddressID = SCOPE_IDENTITY()
		
	INSERT INTO [dbo].[Company] (
		[Name]
		, [Description]
		, [AddressID]
		, [PhoneID]
		, [WebsiteURL]
		, [LogoURL]
		, [DisplayOrder]
		, [CreatedBy]
		, [ModifiedBy]
		, [ModifiedDate])

	Values (
		@Name
		, @CompanyDescription
		, @AddressID
		, @PhoneID
		, @WebsiteURL
		, @LogoURL
		, @DisplayOrder
		, @CreatedBy
		, @CreatedBy
		, @Today
	);

	SET @CompanyID = SCOPE_IDENTITY()

	INSERT INTO [dbo].[Recruiter](
	[RecruiterID]
	, [CompanyID]
	, [Email]
	, [Type])

	VALUES (
		@RecruiterID
		, @CompanyID
		, @Email
		, @Type);

	END TRY

		BEGIN CATCH 
			IF @@TRANCOUNT > 0
			BEGIN
				ROLLBACK TRANSACTION RITransaction;
			END
		END CATCH
		COMMIT TRANSACTION
END

GO

/* -------------------TEST CODE-------------------

select * from dbo.recruiter
select * from dbo.phone
select * from dbo.phonetype
select * from dbo.company
select * from dbo.address


DECLARE
	@RecruiterID int = 33
	, @CompanyID int
	, @Email nvarchar(256) = 'garret@mailinator.com'
	, @Type nvarchar(50) = 'Staffing'
	, @PhoneTypeID int = 1
	, @Title nvarchar(50) = 'Cell'
	, @PhoneTypeDescription nvarchar(50) = 'Cell Number'
	, @PhoneID int 
	, @PersonID int = 1
	, @AreaCode nchar(3) = '714'
	, @Prefix nchar(3) = '321'
	, @Number nchar(4) = '5432'
	, @Extension nchar(5) = '00000'
	, @CreatedBy uniqueidentifier = '1A599F4C-0698-4DE9-8B80-563B099CD299'
	, @AddressID int 
	, @Line1 nvarchar(100) = '120 Newport Center Dr.'
	, @Line2 nvarchar(100) = null
	, @City nvarchar(100) = 'Newport Beach'
	, @State nchar(2) = 'CA'
	, @PostalCode nvarchar(10) = '92660'
	, @Active bit = 1
	, @CreateBy uniqueidentifier = '1A599F4C-0698-4DE9-8B80-563B099CD299'
	, @Name nvarchar(50) = 'An Inside Connection'
	, @CompanyDescription nvarchar(100) = 'A better way to recruit'
	, @WebsiteURL nvarchar(100) = 'aic.dev'
	, @LogoURL nchar(10) = 'aic.jpg'
	, @DisplayOrder int = 1

EXEC [dbo].[Recruiter_Insert]
	@RecruiterID
	, @CompanyID 
	, @Email
	, @Type 
	, @PhoneTypeID 
	, @Title 
	, @PhoneTypeDescription 
	, @PhoneID 
	, @PersonID 
	, @AreaCode
	, @Prefix 
	, @Number 
	, @Extension
	, @CreatedBy 
	, @AddressID 
	, @Line1 
	, @Line2 
	, @City 
	, @State
	, @PostalCode 
	, @Active
	, @CreateBy
	, @Name 
	, @CompanyDescription
	, @WebsiteURL 
	, @LogoURL
	, @DisplayOrder
*/




