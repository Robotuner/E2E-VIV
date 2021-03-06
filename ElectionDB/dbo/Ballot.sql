CREATE TABLE [dbo].[Ballot]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [BallotChain] NVARCHAR(MAX) NOT NULL, 
    [Nonce] INT NOT NULL, 
    [ElectionId] UNIQUEIDENTIFIER NOT NULL, 
    [CreateDate] DATETIME NOT NULL DEFAULT sysutcdatetime()
)
