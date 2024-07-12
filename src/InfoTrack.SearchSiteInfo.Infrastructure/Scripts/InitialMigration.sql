IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [SearchRequests] (
    [Id] int NOT NULL IDENTITY,
    [Keywords] nvarchar(max) NOT NULL,
    [Url] nvarchar(max) NOT NULL,
    [Engine] nvarchar(max) NOT NULL,
    [Rank] int NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    CONSTRAINT [PK_SearchRequests] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240710111802_InitialMigration', N'8.0.6');
GO

COMMIT;
GO

