CREATE TABLE [dbo].[Signature]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [BallotId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ElectionId] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [Confirmed] BIT NOT NULL DEFAULT 0, 
    [BirthYear] INT NOT NULL,
    [ImageArray] IMAGE NULL, 
    [DeviceId] NVARCHAR(40) NOT NULL, 
    [Longitude] DECIMAL(9, 6) NULL , 
    [Latitude] DECIMAL(9, 6) NULL , 
    [Platform] INT NOT NULL,
    [PreviousSignature] UNIQUEIDENTIFIER NULL,
    [CreateDate] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [LastUpdatedDate] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [SignatureStatus] INT NOT NULL DEFAULT 0, 
    [SubmitDate] DATETIME NOT NULL, 
)
