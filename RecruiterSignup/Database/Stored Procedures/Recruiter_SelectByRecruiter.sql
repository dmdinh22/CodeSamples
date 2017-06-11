CREATE PROC [dbo].[Recruiter_SelectByRecruiter]
	@RecruiterID int

AS 

BEGIN
	SELECT r.[RecruiterID]
			, r.[CompanyID]
			, [Email]
			, [Type]
	FROM [dbo].[Recruiter] r
	LEFT JOIN [dbo].[Company] c on r.[CompanyID] = c.[ID]
	WHERE r.[RecruiterID] = @RecruiterID

END

GO


/* ##TEST##

	Declare @RecruiterID int = 33

	exec dbo.Recruiter_SelectByRecruiter
		@RecruiterID

	Select * From dbo.recruiter

*/


