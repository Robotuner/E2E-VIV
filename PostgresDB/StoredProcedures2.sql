
CREATE OR REPLACE FUNCTION Signature_Insert(id uuid, ballotid uuid, electionid uuid, name text, birthyear int,
    confirmed bool, deviceid text, imagearray bytea, longitude double precision, latitude double precision,
	platform int, previoussignature uuid, signaturestatus int, submitdate timestamp)
RETURNS SETOF Signature AS $func$
DECLARE
	sql text = 'INSERT INTO Signature (id, BallotId, ElectionId, Name, BirthYear,Confirmed, DeviceId, ImageArray, Longitude, Latitude, Platform, 
		PreviousSignature, SignatureStatus, SubmitDate, CreateDate, LastUpdatedDate) VALUES 
		($1, $2, $3, $4, $5, $6, $7, $8, $9, $10, $11, $12, $13, $14, current_timestamp, current_timestamp) RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING id, ballotid, electionid, name, birthyear,confirmed, deviceid, imagearray, longitude, latitude, platform, previoussignature, signaturestatus, submitdate;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Signature_UpdateStatus(id uuid, signaturestatus int)
RETURNS SETOF Signature AS $func$
DECLARE
	sql text = 'UPDATE Signature set SignatureStatus=$2, LastUpdatedDate=current_timestamp WHERE Id=$1 RETURNING *';	
BEGIN	
	RETURN QUERY EXECUTE sql USING id, signaturestatus;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Signature_GetAll(electionid uuid, oset int, take int, confirmed boolean)
RETURNS SETOF Signature AS $func$
DECLARE
	sql text = 'SELECT s.* FROM Signature s 
		WHERE s.Confirmed=$4 AND s.ElectionId=$1
		ORDER BY s.CreateDate desc
		OFFSET $2 ROWS
		FETCH NEXT $3 ROWS ONLY';
BEGIN	
	RETURN QUERY EXECUTE sql USING electionid, oset, take, confirmed;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Signature_GetById(id uuid)
RETURNS SETOF Signature AS $func$
DECLARE
	sql text = 'SELECT * FROM Signature WHERE Id=$1';
BEGIN	
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Signature_GetByBallotId(id uuid)
RETURNS SETOF Signature AS $func$
DECLARE
	sql text = 'SELECT * FROM Signature WHERE BallotId=$1 ORDER BY CreateDate DESC fetch first 1 rows only';
BEGIN	
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;


CREATE OR REPLACE FUNCTION SignatureNotice_Insert(id uuid, ballotid uuid, nonce int, ballotRequestId uuid)
RETURNS SETOF SignatureNotice AS $func$
DECLARE
	sql text = 'INSERT INTO SignatureNotice (id, ballotid, nonce, ballotrequestid, createdate) VALUES 
	($1, $2, $3, $4, current_timestamp) RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING id, ballotid, nonce, ballotRequestId;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION SignatureNotice_GetExpectedNonce(ballotid uuid)
RETURNS SETOF SignatureNotice AS $func$
DECLARE
	sql text = 'SELECT * FROM SignatureNotice WHERE BallotId = $1 ORDER BY CreateDate DESC fetch first 1 rows only';
BEGIN	
	RETURN QUERY EXECUTE sql USING ballotid;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Vote_Insert(id uuid, electionid uuid, ballotid uuid, categoryid uuid,
	categorytypeid int, selectionid uuid, votestatus int, approvaldate timestamp)
RETURNS SETOF Vote AS $func$
DECLARE
	sql text = 'INSERT INTO Vote (id, ElectionId, BallotId, CategoryId, CategoryTypeId, SelectionId, VoteStatus, ApprovalDate, CreateDate, LastUpdatedDate) VALUES 
	($1, $2, $3, $4, $5, $6, $7, $8, current_timestamp, current_timestamp)  RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING id, electionid, ballotid, categoryid, categorytypeid, selectionid, votestatus, approvaldate;
END;
$func$ Language plpgsql;


CREATE OR REPLACE FUNCTION Vote_Update(id uuid, electionid uuid, ballotid uuid, categoryid uuid,
	categorytypeid int, selectionid uuid, votestatus int)
RETURNS SETOF Vote AS $func$
DECLARE
	sql text = 'UPDATE Vote SET VoteStatus=$7, SelectionId=$6, LastUpdatedDate=current_timestamp 
	WHERE Id=$1 AND ElectionId=$2 AND BallotId=$3 AND CategoryId=$4 AND CategoryTypeId=$5  RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING id, electionid, ballotid, categoryid, categorytypeid, selectionid, votestatus;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Vote_GetById(id uuid)
RETURNS SETOF Vote AS $func$
DECLARE
	sql text = 'SELECT * FROM Vote WHERE id=$1';
BEGIN	
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Vote_GetByBallotId(ballotid uuid)
RETURNS SETOF Vote AS $func$
DECLARE
	sql text = 'SELECT * FROM Vote WHERE ballotId=$1 AND VoteStatus = 0';
BEGIN	
	RETURN QUERY EXECUTE sql USING ballotid;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Vote_GetAllByElectionId(electionid uuid, oset int, take int, confirmed bool)
RETURNS SETOF Vote AS $func$
DECLARE
	sql text = 'SELECT DISTINCT v.* FROM Vote v
	LEFT OUTER JOIN Signature s ON s.BallotId=v.BallotId
	WHERE s.Confirmed=$4 AND v.ElectionId=$1 
	ORDER BY v.CreateDate
	OFFSET $2 ROWS
	FETCH NEXT $3 ROWS ONLY';
BEGIN	
	RETURN QUERY EXECUTE sql USING electionid, oset, take, confirmed;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Vote_GetAllByCategoryType(electionid uuid, categorytype int, oset int, take int, confirmed bool)
RETURNS SETOF Vote AS $func$
DECLARE
	sql text = 'SELECT DISTINCT v.* FROM Vote v
	LEFT OUTER JOIN Signature s ON s.BallotId=v.BallotId
	WHERE s.Confirmed=$5 AND v.ElectionId=$1 AND v.CategoryTypeId=$2 
	ORDER BY v.CreateDate
	OFFSET $3 ROWS
	FETCH NEXT $4 ROWS ONLY';
BEGIN	
	RETURN QUERY EXECUTE sql USING electionid, categorytype, oset, take, confirmed;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Vote_GetAll(electionid uuid, categoryid uuid, oset int, take int, confirmed bool)
RETURNS SETOF Vote AS $func$
DECLARE
	sql text = 'SELECT DISTINCT v.* FROM Vote v
	LEFT OUTER JOIN Signature s ON s.BallotId=v.BallotId
	WHERE s.Confirmed=$5 AND v.ElectionId=$1 AND v.CategoryId=$2 
	ORDER BY v.CreateDate
	OFFSET $3 ROWS
	FETCH NEXT $4 ROWS ONLY';
BEGIN	
	RETURN QUERY EXECUTE sql USING electionid, categoryid, oset, take, confirmed;
END;
$func$ Language plpgsql;

-- this is really in-efficient only for debugging purposes
CREATE OR REPLACE FUNCTION Vote_GetVotes(electionid uuid, categoryid uuid)
RETURNS SETOF Vote AS $func$
DECLARE
	sql text = 'SELECT Count(*) FROM Vote v
	LEFT OUTER JOIN Signature s ON s.BallotId=v.BallotId
	WHERE s.Confirmed=1 AND v.ElectionId=$1 AND v.CategoryId=$2';
BEGIN	
	RETURN QUERY EXECUTE sql USING electionid, categoryid;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION VoteResult_GetByElectionId(electionid uuid)
RETURNS SETOF VoteResult AS $func$
DECLARE
	sql text = 'SELECT * FROM VoteResult WHERE Electionid=$1';
BEGIN	
	RETURN QUERY EXECUTE sql USING electionid;
END;
$func$ Language plpgsql;


