IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Funds] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(30) NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Funds] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ValueFund] (
    [Id] bigint NOT NULL IDENTITY,
    [DateFund] datetime2 NOT NULL,
    [Value] int NOT NULL,
    [FundId] bigint NOT NULL,
    CONSTRAINT [PK_ValueFund] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ValueFund_Funds_FundId] FOREIGN KEY ([FundId]) REFERENCES [Funds] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_ValueFund_FundId] ON [ValueFund] ([FundId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190708153419_InitialMigration', N'2.2.4-servicing-10062');

GO

