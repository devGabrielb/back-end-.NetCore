CREATE PROCEDURE CustomerGetId
        @Id UNIQUEIDENTIFIER 
AS
	
SELECT [Id], [Title], [Image], [Price], [quantityOnHand]

FROM [Product]
WHERE [Id] = @Id