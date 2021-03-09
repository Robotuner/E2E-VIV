CREATE TABLE [dbo].[CategoryType]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Description] NVARCHAR(50) NOT NULL, 
    [CreateDate] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [LastUpdated] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [Active] BIT NOT NULL DEFAULT 1
)
