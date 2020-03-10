CREATE PROCEDURE CustomerGetId
        @Id UNIQUEIDENTIFIER 
AS
	
SELECT [Id], [FirstName], [LastName], [BirthDate], [Document],
        [UserName], [Password], [Active]

FROM [Customer]
WHERE [Id] = @Id
