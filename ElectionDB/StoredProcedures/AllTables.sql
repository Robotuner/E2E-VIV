GO

CREATE PROCEDURE [dbo].[Category_Insert]
	@id UNIQUEIDENTIFIER, 
	@categoryTypeId int, 
	@minor nvarchar(256),
	@active bit,
	@title nvarchar(100),
	@includesSenator bit,
	@judgePosition int,
	@information nvarchar(max),
	@subtitle nvarchar(200)
AS
	INSERT INTO [dbo].[Category] (id, CategoryTypeId, Minor, CreateDate, LastUpdated, Active, Title, IncludesSenator, JudgePosition, Information, SubTitle) VALUES 
	(@id, @categoryTypeId, @minor, GetUTCDate(), GetUTCDate(),@active, @title, @includesSenator, @judgePosition, @information, @subtitle)

	SELECT * FROM [dbo].[Category] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Category_Update]
	@id UNIQUEIDENTIFIER, 
	@categoryTypeId int, 
	@minor nvarchar(256),
	@active bit,
	@title nvarchar(100),
	@includesSenator bit,
	@judgePosition int,
	@information nvarchar(max),
	@subtitle nvarchar(200)
AS
	UPDATE [dbo].[Category] Set Id=@id, CategoryTypeId=@categoryTypeId, Minor=@minor, LastUpdated=GetUTCDate(), Active=@active, Title=@title, IncludesSenator=@includesSenator, 
				JudgePosition=@judgePosition, Information=@information, SubTitle=@subtitle

	SELECT * FROM [dbo].[Category] WHERE Id = @id
RETURN 0
GO


CREATE PROCEDURE [dbo].[Category_Delete]
	@id UNIQUEIDENTIFIER
AS
	UPDATE [dbo].[Category] SET Active = 0 WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Category_Get]
	@active bit = 1
AS
	SELECT * from [dbo].[Category] WHERE Active = @active
RETURN 0
GO


CREATE PROCEDURE [dbo].[CategoryType_Insert]
	@id int, 
	@description nvarchar(50),
	@active bit
AS
	INSERT INTO [dbo].[CategoryType] (Id, Description, CreateDate, LastUpdated, Active)
		VALUES (@id, @description, GetUTCDate(), GetUTCDate(), @active )
	
	SELECT * FROM [dbo].[CategoryType] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[CategoryType_Update]
	@id int, 
	@description nvarchar(50),
	@active bit
AS
	UPDATE [dbo].[CategoryType] SET Id=@id, Description=@description, LastUpdated=GetUTCDate(), Active=@active
	
	SELECT * FROM [dbo].[CategoryType] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[CategoryType_Delete]
	@id int
AS
	UPDATE [dbo].[CategoryType] SET Active = 0 WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[CatalogType_Get]
	@active bit = 1
AS
	SELECT * from [dbo].[CatalogType] WHERE Active = @active
RETURN 0
GO

CREATE PROCEDURE [dbo].[Election_Insert]
	@id UNIQUEIDENTIFIER, 
	@date datetime, 
	@description nvarchar(max),
	@slateId UNIQUEIDENTIFIER,
	@active bit
AS
	INSERT INTO [dbo].[Election] (id, Date, Description, SlateId, Active, CreateDate, LastUpdated) VALUES 
	(@id, @date, @description, @slateId, @active, GetUTCDate(), GetUTCDate())

	SELECT * FROM [dbo].[Election] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Election_Update]
	@id UNIQUEIDENTIFIER, 
	@date datetime, 
	@description nvarchar(max),
	@slateId UNIQUEIDENTIFIER,
	@active bit

AS
	UPDATE[dbo].[Election] SET id=@id, Date=@date, Description=@description, SlateId=@slateId, Active=@active, LastUpdated=GetUTCDate()

	SELECT * FROM [dbo].[Election] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Election_Delete]
	@id UNIQUEIDENTIFIER
AS
	UPDATE [dbo].[Election] SET Active = 0 WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Election_Get]
	@active bit = 1
AS
	SELECT * from [dbo].[Election] WHERE Active = @active
RETURN 0
GO


CREATE PROCEDURE [dbo].[Party_Insert]
	@id UNIQUEIDENTIFIER, 
	@description nvarchar(200),
	@active bit = 1
AS
	INSERT INTO [dbo].[Party] (id, Description, Active, CreateDate, LastUpdated) VALUES 
	(@id, @description, @active, GetUTCDate(), GetUTCDate())

	SELECT * FROM [dbo].[Party] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Party_Update]
	@id UNIQUEIDENTIFIER, 
	@description nvarchar(200),
	@active bit
AS
	UPDATE [dbo].[Party] SET id=@id, Description=@description, Active=@active, LastUpdated=GetUTCDate()

	SELECT * FROM [dbo].[Party] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Party_Delete]
	@id UNIQUEIDENTIFIER
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

CREATE PROCEDURE [dbo].[Slate_Insert]
	@id UNIQUEIDENTIFIER, 
	@electionId UNIQUEIDENTIFIER,
	@categoryTypeId int,
	@location nvarchar(50),
	@active bit
AS
	INSERT INTO [dbo].[Slate] (id, ElectionId, CategoryTypeId, Location, Active, CreateDate, LastUpdated) VALUES 
	(@id, @electionId, @categoryTypeId, @location, @active, GetUTCDate(), GetUTCDate())

	SELECT * FROM [dbo].[Slate] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Slate_Update]
	@id UNIQUEIDENTIFIER, 
	@electionId UNIQUEIDENTIFIER,
	@categoryTypeId int,
	@location nvarchar(50),
	@active bit
AS
	UPDATE [dbo].[Slate] SET id=@id, ElectionId=@electionId, CategoryTypeId=@categoryTypeId, Location=@location, Active=@active, LastUpdated=GetUTCDate()
	
	SELECT * FROM [dbo].[Slate] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Slate_Delete]
	@id UNIQUEIDENTIFIER
AS
	UPDATE [dbo].[Slate] SET Active = 0 WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Slate_GetByElection]
	@active bit = 1,
	@electionId UNIQUEIDENTIFIER
AS
	SELECT * from [dbo].[Slate] WHERE Active = @active AND ElectionId = @electionId
RETURN 0
GO

CREATE PROCEDURE [dbo].[Slate_GetByElection_CategoryType]
	@active bit = 1,
	@electionId UNIQUEIDENTIFIER,
	@categoryType int
AS
	SELECT * from [dbo].[Slate] WHERE Active = @active AND ElectionId = @electionId AND CategoryTypeId = @categoryType
RETURN 0
GO

CREATE PROCEDURE [dbo].[Ticket_Insert]
	@id UNIQUEIDENTIFIER, 
	@description nvarchar(256), 
	@partyId int,
	@active bit,
	@information nvarchar(max)
AS
	INSERT INTO [dbo].[Ticket] (id, Description, PartyId, Active, Information, CreateDate, LastUpdated) VALUES 
	(@id, @description, @partyId, @active, @information, GetUTCDate(), GetUTCDate())

	SELECT * FROM [dbo].[Ticket] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Ticket_Update]
	@id UNIQUEIDENTIFIER, 
	@description nvarchar(256), 
	@partyId int,
	@active bit,
	@information nvarchar(max)
AS
	UPDATE [dbo].[Ticket] SET id=@id, Description=@description, PartyId=@partyId, Active=@active, Information=@information, LastUpdated=GetUTCDate()

	SELECT * FROM [dbo].[Ticket] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Ticket_Delete]
	@id UNIQUEIDENTIFIER
AS
	UPDATE [dbo].[Ticket] SET Active = 0 WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Ticket_GetById]
	@id UNIQUEIDENTIFIER
AS
	SELECT * from [dbo].[Slate] WHERE Id = @id
RETURN 0
GO

