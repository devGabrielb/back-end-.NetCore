CREATE TABLE [Customer]
(
	
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[FirstName] VARCHAR(40) NOT NULL,
	[LastName] VARCHAR(40) NOT NULL,
    [BirthDate] DATETIME DEFAULT(GETDATE()),
	[Email] VARCHAR(40) NOT NULL,
	[Document] CHAR(11) NOT NULL,
	[UserName] VARCHAR(40) NOT NULL,
    [Password] VARCHAR(12) NOT NULL,
    [Active] BIT NOT NULL,

)

CREATE TABLE [Product]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[Title] VARCHAR(255) NOT NULL,
	[Image] VARCHAR(1024) NOT NULL,
	[Price] MONEY NOT NULL,
	[quantityOnHand] DECIMAL(10, 2) NOT NULL,
)

CREATE TABLE [Order]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[CustomerId] UNIQUEIDENTIFIER NOT NULL,
    [Number] VARCHAR(60) NOT NULL,
	[CreateDate] DATETIME NOT NULL DEFAULT(GETDATE()),
	[Status] INT NOT NULL DEFAULT(1),
    [DeliveryFee] MONEY NOT NULL,
    [Discount] MONEY NOT NULL,
	FOREIGN KEY([CustomerId]) REFERENCES [Customer]([Id])
)

CREATE TABLE [OrderItem] (
	[Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[OrderId] UNIQUEIDENTIFIER NOT NULL,
	[ProductId] UNIQUEIDENTIFIER NOT NULL,
	[Quantity] DECIMAL(10, 2) NOT NULL,
	[Price] MONEY NOT NULL,
	FOREIGN KEY([OrderId]) REFERENCES [Order]([Id]),
	FOREIGN KEY([ProductId]) REFERENCES [Product]([Id])
)
