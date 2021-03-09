CREATE TABLE [dbo].[Vote]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ElectionId] UNIQUEIDENTIFIER NOT NULL, 
    [BallotId] UNIQUEIDENTIFIER NOT NULL, 
    [CategoryId] UNIQUEIDENTIFIER NOT NULL,
    [CategoryTypeId] INT NOT NULL DEFAULT 0, 
    [SelectionId] UNIQUEIDENTIFIER NOT NULL, 
    [VoteStatus] INT NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [LastUpdatedDate] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [ApprovalDate] DATETIME NOT NULL
)

