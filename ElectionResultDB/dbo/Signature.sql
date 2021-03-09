CREATE TABLE [dbo].[Signature]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [BallotId] UNIQUEIDENTIFIER NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Dob] DATETIME NOT NULL,
    [Confirmed] BIT NOT NULL DEFAULT 0, 
    [ImageArray] IMAGE NULL, 
    [CreateDate] DATETIME NOT NULL DEFAULT sysutcdatetime() 
)
