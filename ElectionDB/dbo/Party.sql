CREATE TABLE [dbo].[Party]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Description] NVARCHAR(200) NOT NULL, 
    [CreateDate] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [LastUpdated] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [Active] BIT NOT NULL DEFAULT 1
)
