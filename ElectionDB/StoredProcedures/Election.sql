
CREATE PROCEDURE [dbo].[Ballot_Insert]
	@id UNIQUEIDENTIFIER, 
	@electionid UNIQUEIDENTIFIER,
	@ballotchain nvarchar(max), 
	@nonce int
AS
	INSERT INTO [dbo].[Ballot] (id, ElectionId, Nonce, BallotChain, CreateDate) VALUES 
	(@id, @electionid, @nonce, @ballotchain, GetUTCDate())

	SELECT * FROM [dbo].[Ballot] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Ballot_GetByElection]
	@electionid UNIQUEIDENTIFIER
AS
	SELECT TOP 1 * FROM [dbo].[ballot] WHERE ElectionId = @electionid ORDER BY CreateDate DESC

RETURN 0
GO


CREATE PROCEDURE [dbo].[BallotRequest_Insert]
	@id UNIQUEIDENTIFIER, 
	@electionid UNIQUEIDENTIFIER,
	@deviceid nvarchar(50)
AS
	INSERT INTO [dbo].[BallotRequest] (id, ElectionId, DeviceId, CreateDate) VALUES 
	(@id, @electionid, @deviceid, GetUTCDate())

	SELECT * FROM [dbo].[BallotRequest] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[BallotRequest_GetById]
	@id UNIQUEIDENTIFIER
AS
	SELECT TOP 1 * FROM [dbo].[ballotRequest] WHERE Id = @id ORDER BY CreateDate DESC

RETURN 0
GO








CREATE PROCEDURE [dbo].[Category_Insert]
	@id UNIQUEIDENTIFIER, 
	@electionid UNIQUEIDENTIFIER,
	@categorytypeid int, 
	@heading nvarchar(256),
	@title nvarchar(100),
	@judgeposition int,
	@information nvarchar(max),
	@subtitle nvarchar(200),
	@sequence int,
	@selection UNIQUEIDENTIFIER
AS
	INSERT INTO [dbo].[Category] (id, ElectionId, CategoryTypeId, Heading, CreateDate, LastUpdated, Title, JudgePosition, Information, SubTitle, Sequence, Selection) VALUES 
	(@id, @electionid, @categorytypeid, @heading, GetUTCDate(), GetUTCDate(), @title, @judgeposition, @information, @subtitle,@sequence, @selection)

	SELECT * FROM [dbo].[Category] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Category_Update]
	@id UNIQUEIDENTIFIER, 
	@electionid UNIQUEIDENTIFIER,
	@categorytypeid int, 
	@heading nvarchar(256),
	@title nvarchar(100),
	@judgeposition int,
	@information nvarchar(max),
	@subtitle nvarchar(200),
	@sequence int,
	@selection UNIQUEIDENTIFIER
AS
	UPDATE [dbo].[Category] Set ElectionId=@electionid, CategoryTypeId=@categorytypeid, Heading=@heading, LastUpdated=GetUTCDate(), 
	        Title=@title, Judgeposition=@judgePosition, Information=@information, SubTitle=@subtitle, Sequence=@sequence, Selection=@selection
	WHERE Id=@id

	SELECT * FROM [dbo].[Category] WHERE Id = @id
RETURN 0
GO


CREATE PROCEDURE [dbo].[Category_Delete]
	@id UNIQUEIDENTIFIER
AS
	DELETE FROM [dbo].[Category] WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Category_Get]
AS
	SELECT * from [dbo].[Category]
RETURN 0
GO

CREATE PROCEDURE [dbo].[Category_GetById]
	@id UNIQUEIDENTIFIER
AS
	SELECT * from [dbo].[Category] WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Category_GetByElection]
	@electionid UNIQUEIDENTIFIER
AS
	SELECT * from [dbo].[Category] WHERE ElectionId=@electionid ORDER BY categoryTypeId
RETURN 0
GO

CREATE PROCEDURE [dbo].[Category_GetByType]
	@electionid UNIQUEIDENTIFIER,
	@type int
AS
	SELECT * from [dbo].[Category] WHERE ElectionId=@electionid AND CategoryTypeId=@type
RETURN 0
GO


CREATE PROCEDURE [dbo].[CategoryType_Insert]
	@description nvarchar(50)
AS
	INSERT INTO [dbo].[CategoryType] (Description, CreateDate, LastUpdated, Active)
		VALUES (@description, GetUTCDate(), GetUTCDate(), 1 )
	
	SELECT * FROM [dbo].[CategoryType] WHERE Id = (Select SCOPE_IDENTITY())
RETURN 0
GO

CREATE PROCEDURE [dbo].[CategoryType_Update]
	@id int, 
	@description nvarchar(50),
	@active bit
AS
	UPDATE [dbo].[CategoryType] SET Description=@description, LastUpdated=GetUTCDate(), Active=@active
	WHERE Id=@id
	
	SELECT * FROM [dbo].[CategoryType] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[CategoryType_Delete]
	@id int
AS
	UPDATE [dbo].[CategoryType] SET Active = 0 WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[CategoryType_Get]
	@active bit = 1
AS
	SELECT * from [dbo].[CategoryType] WHERE Active = @active
RETURN 0
GO

CREATE PROCEDURE [dbo].[CategoryType_GetById]
	@active bit = 1,
	@id int
AS
	SELECT * from [dbo].[CategoryType] WHERE Active = @active AND id=@id
RETURN 0
GO


CREATE PROCEDURE [dbo].[Election_Insert]
	@id UNIQUEIDENTIFIER, 
	@date datetime, 
	@startdate datetime,
	@enddate datetime,
	@description nvarchar(max),
	@version nvarchar(10),
	@allowUpdates bit
AS
	INSERT INTO [dbo].[Election] (id, Date, StartDateLocal, EndDateLocal, Description, Version, AllowUpdates, CreateDate, LastUpdated) VALUES 
	(@id, @date, @startdate, @enddate, @description,  @version, @allowUpdates, GetUTCDate(), GetUTCDate())

	SELECT * FROM [dbo].[Election] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Election_Update]
	@id UNIQUEIDENTIFIER, 
	@date datetime, 
	@startdate datetime,
	@enddate datetime,
	@description nvarchar(max),
	@version nvarchar(10),
	@allowUpdates bit

AS
	UPDATE[dbo].[Election] SET Date=@date, Description=@description, StartDateLocal=@startdate, EndDateLocal=@enddate, 
	Version=@version, AllowUpdates=@allowUpdates, LastUpdated=GetUTCDate() WHERE Id=@id

	SELECT * FROM [dbo].[Election] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Election_Delete]
	@id UNIQUEIDENTIFIER
AS
	DELETE FROM [dbo].[Election] WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Election_Get]
	@date datetime
AS
	SELECT * from [dbo].[Election] WHERE [Date] > @date order by [Date] desc
RETURN 0
GO

CREATE PROCEDURE [dbo].[Election_GetById]
	@id UNIQUEIDENTIFIER
AS
	SELECT * from [dbo].[Election] Where Id=@id;
RETURN 0
GO

CREATE PROCEDURE [dbo].[Party_Insert]
	@description nvarchar(200),
	@active bit = 1
AS
	INSERT INTO [dbo].[Party] (Description, Active, CreateDate, LastUpdated) VALUES 
	(@description, @active, GetUTCDate(), GetUTCDate())

	SELECT * FROM [dbo].[Party] WHERE Id = (Select SCOPE_IDENTITY())
RETURN 0
GO

CREATE PROCEDURE [dbo].[Party_Update]
	@id int, 
	@description nvarchar(200),
	@active bit
AS
	UPDATE [dbo].[Party] SET Description=@description, Active=@active, LastUpdated=GetUTCDate() where Id=@id

	SELECT * FROM [dbo].[Party] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Party_Delete]
	@id int
AS
	UPDATE [dbo].[Party] SET Active = 0 WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Party_Get]
	@active bit = 1
AS
	SELECT * from [dbo].[Party] WHERE Active = @active
RETURN 0
GO

CREATE PROCEDURE [dbo].[Party_GetById]
	@id int
AS
	SELECT * from [dbo].[Party] WHERE id=@id
RETURN 0
GO


CREATE PROCEDURE [dbo].[Ticket_Insert]
	@id UNIQUEIDENTIFIER, 
	@electionid UNIQUEIDENTIFIER, 
	@categoryid UNIQUEIDENTIFIER, 
	@description nvarchar(256), 
	@partyid int,
	@tickettype int,
	@information nvarchar(max),
	@sequence int
AS
	INSERT INTO [dbo].[Ticket] (id, ElectionId, CategoryId, Description, PartyId, TicketType, Information, Sequence, CreateDate, LastUpdated) VALUES 
	(@id, @electionid, @categoryid, @description, @partyid, @tickettype, @information, @sequence, GetUTCDate(), GetUTCDate())

	SELECT * FROM [dbo].[Ticket] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Ticket_Update]
	@id UNIQUEIDENTIFIER, 
	@electionid UNIQUEIDENTIFIER, 
	@categoryid UNIQUEIDENTIFIER, 
	@description nvarchar(256), 
	@partyid int,
	@tickettype int,
	@information nvarchar(max),
	@sequence int
AS
	UPDATE [dbo].[Ticket] SET ElectionId=@electionid, CategoryId=@categoryid, Description=@description, PartyId=@partyid,
	TicketType=@tickettype, Information=@information, Sequence=@sequence, LastUpdated=GetUTCDate() 
	WHERE Id=@id

	SELECT * FROM [dbo].[Ticket] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Ticket_Delete]
	@id UNIQUEIDENTIFIER
AS
	DELETE FROM [dbo].[Ticket] WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Ticket_GetById]
	@id UNIQUEIDENTIFIER
AS
	SELECT * from [dbo].[Ticket] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Ticket_GetByElection]
	@electionid UNIQUEIDENTIFIER
AS
	SELECT * from [dbo].[Ticket] WHERE ElectionId = @electionid
RETURN 0
GO

