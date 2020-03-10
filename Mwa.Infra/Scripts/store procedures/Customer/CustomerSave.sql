CREATE PROCEDURE CustomerSave
    @Id UNIQUEIDENTIFIER,
    @FirstName VARCHAR(40),
    @LastName VARCHAR(40),
	@Email VARCHAR(40),
    @Document CHAR(11),
    @UserName VARCHAR(40),
    @Password VARCHAR(12),
    @Active BIT
AS
    INSERT INTO [Customer] (
        [Id], 
        [FirstName], 
        [LastName],
		[Email], 
        [Document], 
        [UserName], 
        [Password],
        [Active]
    ) VALUES (
        @Id,
        @FirstName,
        @LastName,
		@Email,
        @Document,
        @UserName,
        @Password,
        @Active
    )