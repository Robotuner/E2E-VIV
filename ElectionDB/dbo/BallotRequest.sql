﻿CREATE TABLE [dbo].[BallotRequest]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ElectionId] UNIQUEIDENTIFIER NOT NULL, 
    [DeviceId] NVARCHAR(50) NOT NULL, 
    [CreateDate] DATETIME NOT NULL DEFAULT sysutcdatetime()
)
