GO

CREATE PROCEDURE [dbo].[Signature_Insert]
	@id UNIQUEIDENTIFIER,
	@ballotid UNIQUEIDENTIFIER,
	@electionid UNIQUEIDENTIFIER,
	@name nvarchar(256),
	@birthyear int,
	@confirmed bit,
	@deviceid nvarchar(40),
	@imagearray Image,
	@longitude decimal(9,6),
	@latitude decimal(9,6),
	@platform int,
	@previoussignature UNIQUEIDENTIFIER,
	@signaturestatus int,
	@submitdate DateTime
AS
	INSERT INTO [dbo].[Signature] (id, BallotId, ElectionId, Name, BirthYear,Confirmed, DeviceId, ImageArray, Longitude, Latitude, Platform, PreviousSignature, SignatureStatus, SubmitDate, CreateDate, LastUpdatedDate) VALUES 
	(@id, @ballotid, @electionid, @name, @birthyear, @confirmed, @deviceid, @imagearray, @longitude, @latitude, @platform, @previoussignature, @signaturestatus, @submitdate, GetUTCDate(), GetUTCDate())

	SELECT * FROM [dbo].[Signature] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Signature_UpdateStatus]
	@id UNIQUEIDENTIFIER,
	@signaturestatus int
AS
	UPDATE [dbo].[Signature] set SignatureStatus=@signaturestatus, LastUpdatedDate=GetUTCDate()
	WHERE Id=@id

	SELECT * FROM [dbo].[Signature] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[Signature_GetAll]
	@electionid UNIQUEIDENTIFIER,
	@oset int,
	@take int,
	@confirmed bit
AS
	SELECT s.* FROM [dbo].[Signature] s
	WHERE s.Confirmed=@confirmed AND s.ElectionId=@electionid
	ORDER BY s.CreateDate desc
	OFFSET @oset ROWS
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
	@ballotid UNIQUEIDENTIFIER,
	@nonce int,
	@ballotrequestid UNIQUEIDENTIFIER
AS
	INSERT INTO [dbo].[SignatureNotice] (id, BallotId, Nonce, BallotRequestId, CreateDate) VALUES 
	(@id, @ballotid, @nonce, @ballotrequestid, GetUTCDate())

	SELECT * FROM [dbo].[SignatureNotice] WHERE Id = @id
RETURN 0
GO

CREATE PROCEDURE [dbo].[SignatureNotice_GetExpectedNonce]
	@ballotid UNIQUEIDENTIFIER
AS
	SELECT TOP 1 * FROM [dbo].[SignatureNotice] WHERE BallotId = @ballotid ORDER BY CreateDate DESC

RETURN 0
GO

CREATE PROCEDURE [dbo].[Vote_Insert]
	@id UNIQUEIDENTIFIER,
	@electionid UNIQUEIDENTIFIER,
	@ballotid UNIQUEIDENTIFIER,
	@categoryid UNIQUEIDENTIFIER,
	@categorytypeid int,
	@selectionid UNIQUEIDENTIFIER,
	@votestatus int,
	@approvaldate DateTime
AS
	INSERT INTO [dbo].[Vote] (id, ElectionId, BallotId, CategoryId, CategoryTypeId, SelectionId, VoteStatus, ApprovalDate, CreateDate, LastUpdatedDate) VALUES 
	(@id, @electionid, @ballotid, @categoryid, @categorytypeid, @selectionid, @votestatus, @approvaldate, GetUTCDate(), GetUTCDate())

	SELECT Id FROM [dbo].[Vote] WHERE id=@id
RETURN 0
GO


CREATE PROCEDURE [dbo].[Vote_Update]
	@id UNIQUEIDENTIFIER,
	@electionid UNIQUEIDENTIFIER,
	@ballotid UNIQUEIDENTIFIER,
	@categoryid UNIQUEIDENTIFIER,
	@categorytypeid int,
	@selectionid UNIQUEIDENTIFIER,
	@votestatus int
AS
	UPDATE [dbo].[Vote] SET VoteStatus=@votestatus, LastUpdatedDate=GETUTCDATE() 
	WHERE Id=@id AND  ElectionId=@electionid AND BallotId=@ballotid AND  CategoryId=@categoryid AND CategoryTypeId=@categorytypeid AND SelectionId=@selectionid

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
	@ballotid UNIQUEIDENTIFIER
AS
    -- VoteStatus is an enum 0 == isValidVote!
	SELECT * FROM [dbo].[Vote] WHERE ballotId=@ballotid AND VoteStatus = 0;
RETURN 0
GO

CREATE PROCEDURE [dbo].[Vote_GetAllByElectionId]
	@electionid UNIQUEIDENTIFIER,
	@oset int,
	@take int,
	@confirmed bit
AS
	SELECT DISTINCT v.* FROM [dbo].[Vote] v
	LEFT OUTER JOIN [dbo].[Signature] s ON s.BallotId=v.BallotId
	WHERE s.Confirmed=@confirmed AND v.ElectionId=@electionid 
	ORDER BY v.CreateDate
	OFFSET @oset ROWS
	FETCH NEXT @take ROWS ONLY
RETURN 0
GO

CREATE PROCEDURE [dbo].[Vote_GetAllByCategoryType]
	@electionid UNIQUEIDENTIFIER,
	@categorytype int,
	@oset int,
	@take int,
	@confirmed bit
AS
	SELECT DISTINCT v.* FROM [dbo].[Vote] v
	LEFT OUTER JOIN [dbo].[Signature] s ON s.BallotId=v.BallotId
	WHERE s.Confirmed=@confirmed AND v.ElectionId=@electionid AND v.CategoryTypeId=@categorytype
	ORDER BY v.CreateDate
	OFFSET @oset ROWS
	FETCH NEXT @take ROWS ONLY
RETURN 0
GO

CREATE PROCEDURE [dbo].[Vote_GetAll]
	@electionid UNIQUEIDENTIFIER,
	@categoryid UNIQUEIDENTIFIER,
	@oset int,
	@take int,
	@confirmed bit
AS
	SELECT DISTINCT v.* FROM [dbo].[Vote] v
	LEFT OUTER JOIN [dbo].[Signature] s ON s.BallotId=v.BallotId
	WHERE s.Confirmed=@confirmed AND v.ElectionId=@electionid AND v.CategoryId=@categoryid
	ORDER BY v.CreateDate
	OFFSET @oset ROWS
	FETCH NEXT @take ROWS ONLY
RETURN 0
GO

-- this is really in-efficient only for debugging purposes
CREATE PROCEDURE [dbo].[Vote_GetVotes]
	@electionid UNIQUEIDENTIFIER,
	@categoryid UNIQUEIDENTIFIER
AS
	SELECT Count(*) FROM [dbo].[Vote] v
	LEFT OUTER JOIN [dbo].[Signature] s ON s.BallotId=v.BallotId
	WHERE s.Confirmed=1 AND v.ElectionId=@electionid AND v.CategoryId=@categoryid
RETURN 0
GO

CREATE PROCEDURE [dbo].[VoteResult_GetByElectionId]
	@electionid UNIQUEIDENTIFIER
AS
	SELECT * FROM [dbo].[VoteResult] WHERE Electionid=@electionid;
RETURN 0
GO


CREATE PROCEDURE [dbo].[Vote_GetVRecordBallotId]
	@ballotid UNIQUEIDENTIFIER
AS
	SELECT c.heading, c.title, c.judgeposition, c.categorytypeid, t.description, p.description from vote v
	JOIN category c ON c.id = v.categoryid
	JOIN ticket t ON t.id=v.selectionid
	JOIN party p ON p.id=t.partyid
	WHERE v.ballotid = @ballotid AND v.VoteStatus=0
RETURN 0
GO
