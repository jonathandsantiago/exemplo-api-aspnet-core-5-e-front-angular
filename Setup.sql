CREATE DATABASE [FavoDeMelDb]
GO

USE  [FavoDeMelDb]

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF (OBJECT_ID(N'[Produto]') IS NULL)
BEGIN

	CREATE TABLE [Produto] (
		[Id] int NOT NULL IDENTITY,
		[Nome] nvarchar(max) NULL,
		[Preco] decimal(18,2) NOT NULL,
		CONSTRAINT [PK_Produto] PRIMARY KEY ([Id])
	);


	CREATE TABLE [Usuario] (
		[Id] int NOT NULL IDENTITY,
		[Nome] nvarchar(max) NULL,
		[Login] nvarchar(max) NULL,
		[Password] nvarchar(max) NULL,
		[Setor] int NOT NULL,
		CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id])
	);

	CREATE TABLE [Comanda] (
		[Id] int NOT NULL IDENTITY,
		[GarcomId] int NULL,
		CONSTRAINT [PK_Comanda] PRIMARY KEY ([Id]),
		CONSTRAINT [FK_Comanda_Usuario_GarcomId] FOREIGN KEY ([GarcomId]) REFERENCES [Usuario] ([Id]) ON DELETE NO ACTION
	);

	CREATE TABLE [ComandaPedido] (
		[Id] int NOT NULL IDENTITY,
		[ComandaId] int NULL,
		[ProdutoId] int NULL,
		[Quantidade] int NOT NULL,
		[Situacao] int NOT NULL,
		CONSTRAINT [PK_ComandaPedido] PRIMARY KEY ([Id]),
		CONSTRAINT [FK_ComandaPedido_Comanda_ComandaId] FOREIGN KEY ([ComandaId]) REFERENCES [Comanda] ([Id]) ON DELETE NO ACTION,
		CONSTRAINT [FK_ComandaPedido_Produto_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produto] ([Id]) ON DELETE NO ACTION
	);
	
	CREATE INDEX [IX_Comanda_GarcomId] ON [Comanda] ([GarcomId]);
	CREATE INDEX [IX_ComandaPedido_ComandaId] ON [ComandaPedido] ([ComandaId]);
	CREATE INDEX [IX_ComandaPedido_ProdutoId] ON [ComandaPedido] ([ProdutoId]);

	INSERT INTO Usuario(Nome, Login, Password, Setor)
	VALUES('Garçom','garcom','4E1378E14A5EC5D25A44DA12C959D5F6', 1)
	INSERT INTO Usuario(Nome, Login, Password, Setor)
	VALUES('Cozinha','cozinha','82EE206B2AD6BD2FE12B5D785E96953C',2)

	INSERT INTO PRODUTO(Nome, Preco)
	VALUES('Rodizio Almoço', 90.00)
	INSERT INTO PRODUTO(Nome, Preco)
	VALUES('Rodizio Jantar', 45.00)
	INSERT INTO PRODUTO(Nome, Preco)
	VALUES('Refrigerante lata 290ml', 5.00)
	INSERT INTO PRODUTO(Nome, Preco)
	VALUES('Cerveja lata 290ml', 5.00)
	INSERT INTO PRODUTO(Nome, Preco)
	VALUES('Suco Jarra', 15.00)
END

	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
	VALUES (N'20200226032106_EstruturacaoBd', N'3.1.2');


