CREATE TABLE [dbo].[Election]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Date] DATETIME NOT NULL, 
    [StartDateLocal] DATETIME NOT NULL, 
    [EndDateLocal] DATETIME NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL, 
    [CreateDate] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [LastUpdated] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [Version] NCHAR(10) NULL, 
    [AllowUpdates] BIT NOT NULL DEFAULT 0, 
)
