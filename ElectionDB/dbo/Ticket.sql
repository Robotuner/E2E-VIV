CREATE TABLE [dbo].[Ticket]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Description] NVARCHAR(256) NOT NULL, 
    [PartyId] INT NULL , 
    [CreateDate] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [LastUpdated] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [Information] NVARCHAR(MAX) NULL, 
    [CategoryId] UNIQUEIDENTIFIER NOT NULL, 
    [Sequence] INT NOT NULL DEFAULT 0, 
    [ElectionId] UNIQUEIDENTIFIER NOT NULL,
    [TicketType] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Election] FOREIGN KEY ([ElectionId]) REFERENCES [Election](Id), 
    CONSTRAINT [FK_Category] FOREIGN KEY (CategoryId) REFERENCES [Category](Id) ON DELETE CASCADE,
    CONSTRAINT [FK_Party] FOREIGN KEY (PartyId) REFERENCES [Party](Id),
)
