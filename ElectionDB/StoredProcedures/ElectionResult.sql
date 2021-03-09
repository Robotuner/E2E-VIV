GO

CREATE PROCEDURE [dbo].[Signature_Insert]
	@id UNIQUEIDENTIFIER,
	@ballotId UNIQUEIDENTIFIER,
	@electionId UNIQUEIDENTIFIER,
	@name nvarchar(256),
	@birthYear int,
	@confirmed bit,
	@deviceId nvarchar(40),
	@imageArray Image,
	@longitude decimal(9,6),
	@latitude decimal(9,6),
	@platform int,
	@previousSignature UNIQUEIDENTIFIER,
	@signatureStatus int,
	@submitDate DateTime
AS
	INSERT INTO [dbo].[Signature] (id, BallotId, ElectionId, Name, BirthYear,Confirmed, DeviceId, ImageArray, Longitude, Latitude, Platform, PreviousSignature, SignatureStatus, SubmitDate, CreateDate, LastUpdatedDate) VALUES 
	(@id, @ballotId, @electionId, @name, @birthYear, @confirmed, @deviceid, @imageArray, @longitude, @latitude, @platform, @previousSignature, @signatureStatus, @submitDate, GetUTCDate(), GetUTCDate())

	SELECT * FROM [dbo].[Signature] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Signature_UpdateStatus]
	@id UNIQUEIDENTIFIER,
	@signatureStatus int
AS
	UPDATE [dbo].[Signature] set SignatureStatus=@signatureStatus, LastUpdatedDate=GetUTCDate()
	WHERE Id=@id

	SELECT * FROM [dbo].[Signature] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Signature_GetAll]
	@electionId UNIQUEIDENTIFIER,
	@offset int,
	@take int,
	@confirmed bit
AS
	SELECT s.* FROM [dbo].[Signature] s
	WHERE s.Confirmed=@confirmed AND s.ElectionId=@electionId
	ORDER BY s.CreateDate desc
	OFFSET @offset ROWS
	FETCH NEXT @take ROWS ONLY
RETURN 0
GO

CREATE PROCEDURE [dbo].[Signature_GetById]
	@id UNIQUEIDENTIFIER
AS
	SELECT * FROM [dbo].[Signature] WHERE Id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Signature_GetByBallotId]
	@id UNIQUEIDENTIFIER
AS
	SELECT TOP 1 * FROM [dbo].[Signature] WHERE BallotId=@id ORDER BY CreateDate DESC
RETURN 0
GO

CREATE PROCEDURE [dbo].[SignatureNotice_Insert]
	@id UNIQUEIDENTIFIER, 
	@ballotId UNIQUEIDENTIFIER,
	@nonce int
AS
	INSERT INTO [dbo].[SignatureNotice] (id, BallotId, Nonce, CreateDate) VALUES 
	(@id, @ballotId, @nonce, GetUTCDate())

	SELECT * FROM [dbo].[SignatureNotice] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[SignatureNotice_GetExpectedNonce]
	@ballotId UNIQUEIDENTIFIER
AS
	SELECT TOP 1 * FROM [dbo].[SignatureNotice] WHERE BallotId = @ballotId ORDER BY CreateDate DESC

RETURN 0
GO



CREATE PROCEDURE [dbo].[Vote_Insert]
	@id UNIQUEIDENTIFIER,
	@electionId UNIQUEIDENTIFIER,
	@ballotId UNIQUEIDENTIFIER,
	@categoryId UNIQUEIDENTIFIER,
	@categoryTypeId int,
	@selectionId UNIQUEIDENTIFIER,
	@voteStatus int,
	@approvalDate DateTime
AS
	INSERT INTO [dbo].[Vote] (id, ElectionId, BallotId, CategoryId, CategoryTypeId, SelectionId, VoteStatus, ApprovalDate, CreateDate, LastUpdatedDate) VALUES 
	(@id, @electionId, @ballotId, @categoryId, @categoryTypeId, @selectionId, @voteStatus, @approvalDate, GetUTCDate(), GetUTCDate())

	SELECT Id FROM [dbo].[Vote] WHERE id=@id
RETURN 0
GO


CREATE PROCEDURE [dbo].[Vote_Update]
	@id UNIQUEIDENTIFIER,
	@electionId UNIQUEIDENTIFIER,
	@ballotId UNIQUEIDENTIFIER,
	@categoryId UNIQUEIDENTIFIER,
	@categoryTypeId int,
	@selectionId UNIQUEIDENTIFIER,
	@voteStatus int
AS
	UPDATE [dbo].[Vote] SET VoteStatus=@voteStatus, LastUpdatedDate=GETUTCDATE() 
	WHERE Id=@id AND  ElectionId=@electionId AND BallotId=@ballotId AND  CategoryId=@categoryId AND CategoryTypeId=@categoryTypeId AND SelectionId=@selectionId

	SELECT Id FROM [dbo].[Vote] WHERE id=@id
RETURN 0
GO


create PROCEDURE [dbo].[Vote_GetById]
	@id UNIQUEIDENTIFIER
AS
	SELECT * FROM [dbo].[Vote] WHERE id=@id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Vote_GetByBallotId]
	@ballotId UNIQUEIDENTIFIER
AS
    -- VoteStatus is an enum 0 == isValidVote!
	SELECT * FROM [dbo].[Vote] WHERE ballotId=@ballotId AND VoteStatus = 0;
RETURN 0
GO

CREATE PROCEDURE [dbo].[Vote_GetAll]
	@electionId UNIQUEIDENTIFIER,
	@categoryId UNIQUEIDENTIFIER,
	@offset int,
	@take int,
	@confirmed bit
AS
	SELECT DISTINCT v.* FROM [dbo].[Vote] v
	LEFT OUTER JOIN [dbo].[Signature] s ON s.BallotId=v.BallotId
	WHERE s.Confirmed=@confirmed AND v.ElectionId=@electionId AND v.CategoryId=@categoryId
	ORDER BY v.CreateDate
	OFFSET @offset ROWS
	FETCH NEXT @take ROWS ONLY
RETURN 0
GO

-- this is really in-efficient only for debugging purposes
CREATE PROCEDURE [dbo].[Vote_GetVotes]
	@electionId UNIQUEIDENTIFIER,
	@categoryId UNIQUEIDENTIFIER
AS
	SELECT Count(*) FROM [dbo].[Vote] v
	LEFT OUTER JOIN [dbo].[Signature] s ON s.BallotId=v.BallotId
	WHERE s.Confirmed=1 AND v.ElectionId=@electionId AND v.CategoryId=@categoryId
RETURN 0
GO

CREATE PROCEDURE [dbo].[VoteResult_GetByElectionId]
	@electionId UNIQUEIDENTIFIER
AS
	SELECT * FROM [dbo].[VoteResult] WHERE Electionid=@electionId;
RETURN 0
GO
