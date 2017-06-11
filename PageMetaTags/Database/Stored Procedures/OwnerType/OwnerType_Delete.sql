CREATE proc [dbo].[OwnerType_Delete]
	@ID int
as

BEGIN
	BEGIN TRANSACTION;
	SAVE TRANSACTION MTDeleteTransaction;

	BEGIN TRY
	Delete FROM dbo.pagemetatags
		where ownertypeid = @id
	DELETE FROM [dbo].[OwnerType]
		  WHERE ID = @ID;
	END TRY

		BEGIN CATCH 
			IF @@TRANCOUNT > 0 
			BEGIN
				ROLLBACK TRANSACTION MTDeleteTransaction; -- ROLLBACK to MTTransaction
			END
		END CATCH
		COMMIT TRANSACTION 
END
GO

/* ## TEST ##
	
	Declare @ID int = 30

	Select * from dbo.ownertype 

	Execute dbo.ownertype_delete @ID

	Select * from dbo.ownertype 


*/   