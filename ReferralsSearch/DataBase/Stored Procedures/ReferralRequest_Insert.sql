
CREATE PROC [dbo].[ReferralRequest_Insert]
				@Token nvarchar(128)
				,@Info nvarchar(max)
				,@ReferralType nchar(20)

AS 

BEGIN

	INSERT INTO [dbo].[ReferralRequest]
				([Token]
				,[Info]
				,[ReferralType])

		VALUES	(@Token
				,@Info			
				,@ReferralType)

END

/*

Declare @Token nvarchar(128) = 'ff84b062-e405-4c8f-ab9b-9664db63b50b';

Declare	@Info nvarchar(max) = '{JobId: 1, PersonId: 4, RefererId: 10}'
		,@ReferralType nchar(20) = 'Request'

Execute dbo.ReferralRequest_Insert
			@Token
			,@Info
			,@ReferralType

select * from dbo.ReferralRequest

*/
