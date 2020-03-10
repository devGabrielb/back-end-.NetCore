CREATE PROCEDURE getByUserName
        @UserName VARCHAR(40) 
AS
	
SELECT [FirstName], [LastName], [BirthDate], [Email], [Document],
        [UserName], [Password]

FROM [Customer]
WHERE [UserName] = @UserName