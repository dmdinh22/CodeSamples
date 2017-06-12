CREATE PROC [dbo].[Profile_ReferralPaging]
	@PersonID int,
	@PageNum int,
	@PageSize int

AS

BEGIN

	WITH results AS
	(select wh.PersonID, 
			   p.FirstName,
			   p.LastName,
			   wh.companyID,
			   wh.Company, 
			   e.School,
			   f.FileUrl,
			   p.ComplexUserId
			
		from dbo.workhistory wh 
		inner join dbo.people p on p.ID = wh.PersonID
		inner join dbo.[file] f on p.ID = f.PersonID
		inner join dbo.education e on p.ID = e.PersonID
	
		where (wh.company in (select company from dbo.WorkHistory where personid = @PersonID)
		 and wh.personID != @personID -- where the personid isn't your id
		 and f.FileTitle = 'ProfilePicture') 
		 OR (e.school in (select school from dbo.education where personid = @PersonID) 
		 and e.personID != @personID 
		 and f.FileTitle = 'ProfilePicture')
		 ),
		 
		 Results_Count
		 AS
		 (
			SELECT COUNT(*) AS TotalRows FROM results
		 )


		 select * 
		 from results
		 cross join Results_Count
		 order by [PersonID]
		 OFFSET (@PageNum - 1) * @PageSize ROWS
		 FETCH NEXT @PageSize ROWS ONLY;
END

/* ## TEST CODE ##
DECLARE @PersonID int = 10, @PageNum int = 1, @PageSize int = 5
EXEC [dbo].[Profile_ReferralPaging]
		@PersonID,
		@PageNum,
		@PageSize

*/