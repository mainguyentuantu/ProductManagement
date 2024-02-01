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

CREATE TABLE [Products] (
    [MaSP] int NOT NULL IDENTITY,
    [TenSP] nvarchar(max) NULL,
    [KichThuoc] nvarchar(max) NULL,
    [ChatLieu] nvarchar(max) NULL,
    [MauSac] nvarchar(max) NULL,
    [KieuDang] nvarchar(max) NULL,
    [ThuongHieu] nvarchar(max) NULL,
    [GiaCa] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([MaSP])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240131101743_InitialDBProductManagement', N'8.0.1');
GO

COMMIT;
GO

