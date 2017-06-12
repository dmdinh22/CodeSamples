USE [C30]
GO

/****** Object:  StoredProcedure [dbo].[Recruiter_InsertTableType]    Script Date: 5/19/2017 11:08:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Recruiter_InsertTableType]
	--Parameters to pass in
	@CreatedBy nvarchar(128)
	, @PersonID int
	, @addressdt AddressTableType READONLY
	, @companydt CompanyTableType READONLY
	, @phonedt PhoneTableType READONLY
	, @recruiterdt RecruiterTableType READONLY

AS


BEGIN 
	BEGIN TRANSACTION;
	SAVE TRANSACTION RITransaction;

	BEGIN TRY
	
		Declare @Today datetime = GetUtcDate()
		Declare @PhoneID int
		Declare @AddressID int
		Declare @CompanyID int

		INSERT INTO [dbo].[Phone](
			[PersonId]
			, [PhoneTypeId]
			, [CountryCode]
			, [Number]
			, [Extension]
			, [CreatedBy]
			, [ModifiedDate])
		SELECT
			@PersonID
			, [PhoneTypeId]
			, [CountryCode]
			, [Number]
			, [Extension]
			, @CreatedBy
			, @Today
		FROM 
			@phonedt

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
		SELECT	
			@PersonID
			, [Line1]
			, [Line2]
			, [City]
			, [State]
			, [PostalCode]
			, [Active]
			, @CreatedBy
			, @CreatedBy
			, @Today
		FROM 
			@addressdt

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

		SELECT
			[Name]
			, [Description]
			, @AddressID
			, @PhoneID
			, [WebsiteURL]
			, [LogoURL]
			, [DisplayOrder]
			, @CreatedBy
			, @CreatedBy
			, @Today
		FROM
			@companydt

		SET @CompanyID = SCOPE_IDENTITY()

		INSERT INTO [dbo].[Recruiter](
			[RecruiterID]
			, [CompanyID]
			, [Email]
			, [Type])
		SELECT 
			@PersonID
			, @CompanyID
			, [Email]
			, [Type]
		FROM
			@recruiterdt

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

select * from dbo.people

select * from dbo.phone order by ID DESC
select * from dbo.address order by ID DESC
select * from dbo.company order by ID DESC
select * from dbo.recruiter


--- Declare parameters & Table Types as params ---

DECLARE
	@CreatedBy nvarchar(128) = NEWID()
	, @PersonID int = 49

DECLARE @phonedt [dbo].[PhoneTableType]

INSERT INTO @phonedt(PhoneTypeID, CountryCode, Number, Extension)
VALUES(1, '321', '1234567890', '12345')

DECLARE @addressdt [dbo].[AddressTableType]

INSERT INTO @addressdt(Line1, Line2, City, State, PostalCode, Active)
VALUES('123 PCH', null, 'Irvine', 'CA', '92660', 1)

DECLARE @companydt [dbo].[CompanyTableType]

INSERT INTO @companydt(Name, Description, WebsiteURL, LogoURL, DisplayOrder)
VALUES('Irvine Company', 'The Irvine Company', 'irvineco.com', 'irvineco.com/logo.jpg', 1);

DECLARE @recruiterdt [dbo].[RecruiterTableType]

INSERT INTO @recruiterdt(Email, Type)
VALUES('recruiter@irvineco.com', 'In-House')

EXEC [dbo].[Recruiter_InsertTableType]
	@CreatedBy
	, @PersonID
	, @addressdt 
	, @companydt 
	, @phonedt 
	, @recruiterdt 

select * from @phonedt
select * from @addressdt
select * from @companydt
select * from @recruiterdt
*/


