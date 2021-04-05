  
CREATE OR REPLACE FUNCTION Ballot_Insert(id uuid, electionId uuid, ballotchain text, nonce int)
RETURNS SETOF Ballot AS $func$
DECLARE
	sql text = 'INSERT INTO Ballot (id, ElectionId, Nonce, BallotChain, CreateDate) VALUES 
		($1, $2, $4, $3, current_timestamp) RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING id, electionId, ballotchain, nonce;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Ballot_GetByElection(electionId uuid)
RETURNS SETOF Ballot AS $func$
DECLARE 
	sql text = 'SELECT * FROM ballot WHERE ElectionId = $1 ORDER BY CreateDate DESC
	fetch first 1 rows only';
BEGIN
	RETURN QUERY EXECUTE sql USING electionId;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION BallotRequest_Insert(id uuid, electionid uuid, deviceid text)
RETURNS SETOF BallotRequest AS $func$
DECLARE
	sql text = 'INSERT INTO BallotRequest (id, ElectionId, DeviceId, CreateDate) VALUES 
		($1, $2, $3, current_timestamp) RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING id, electionid, deviceid;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION BallotRequest_GetById(id uuid)
RETURNS SETOF BallotRequest AS $func$
DECLARE 
	sql text = 'SELECT * FROM BallotRequest WHERE Id=$1';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;


CREATE OR REPLACE FUNCTION Category_Insert(id uuid, electionid uuid, categorytypeId int, subcategorytypeid int, heading text,
										  title text, district int, judgeposition int, information text, subtitle text,
										  sequence int, selection uuid)
RETURNS SETOF Category AS $func$
DECLARE
	sql text ='INSERT INTO Category (id, ElectionId, CategoryTypeId, SubcategoryTypeid, Heading, CreateDate, LastUpdated, Title, 
	    District, JudgePosition, Information, SubTitle, Sequence, Selection) VALUES 
	($1, $2, $3, $4, $5, current_timestamp, current_timestamp, $6, $7, $8, $9, $10, $11, $12) RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING id, electionId, categoryTypeId, subcategorytypeid, heading, title, district, judgePosition, information, subtitle, sequence, selection;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Category_Update(id uuid, electionid uuid, categoryTypeid int,subcategorytypeid int,  heading text, title text, district int, judgeposition int,
										  information text, subtitle text, sequence int, selection uuid)
RETURNS SETOF Category AS $func$
DECLARE
	sql text = 'UPDATE Category Set ElectionId=$2, CategoryTypeId=$3, Heading=$4, LastUpdated=current_timestamp, 
	        Title=$5, JudgePosition=$6, Information=$7, SubTitle=$8, Sequence=$9, Selection=$10, subcategorytypeId = $11, district=12
	WHERE Id=$1 RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING id, electionId, categoryTypeId, heading, title, judgePosition, information, subtitle, sequence, selection, subcategorytypeid, district;
END;
$func$ Language plpgsql;


CREATE OR REPLACE FUNCTION Category_Delete(id uuid)
RETURNS SETOF Category AS $func$
DECLARE
	sql text = 'DELETE FROM Category WHERE Id=$1 RETURNING *';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Category_Get(active bool)
RETURNS SETOF Category AS $func$
DECLARE
	sql text = 'SELECT * from Category Where Active=$1 RETURNING *';
BEGIN
	RETURN QUERY EXECUTE sql USING active;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Category_GetById(id uuid)
RETURNS SETOF Category AS $func$
DECLARE
	sql text = 'SELECT * from Category WHERE Id=$1';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Category_GetByElection(electionid uuid)
RETURNS SETOF Category AS $func$
DECLARE
	sql text = 'SELECT * from Category WHERE ElectionId=$1 ORDER BY categorytypeId';
BEGIN
	RETURN QUERY EXECUTE sql USING electionid;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Category_GetByType(electionId uuid, type int)
RETURNS SETOF Category AS $func$
DECLARE
	sql text = 'SELECT * from Category WHERE ElectionId=$1 AND CategoryTypeId=$2';
BEGIN
	RETURN QUERY EXECUTE sql USING electionId, type;
END;
$func$ Language plpgsql;


CREATE OR REPLACE FUNCTION CategoryType_Insert(description text)
RETURNS SETOF CategoryType AS $func$
DECLARE
	sql text = 'INSERT INTO CategoryType (Description, CreateDate, LastUpdated, Active)
		VALUES ($1, current_timestamp, current_timestamp, True )';
BEGIN	
	RETURN QUERY EXECUTE concat(sql, 'RETURNING *') USING description;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION CategoryType_Update(id int, description text, active bool)
RETURNS SETOF CategoryType AS $func$
DECLARE
	SQL TEXT = 'UPDATE CategoryType SET Description=$2, LastUpdated=current_timestamp, Active=$3 WHERE Id=$1 RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING id, description, active;	
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION CategoryType_Delete(id int)
RETURNS SETOF CategoryType AS $func$
DECLARE
	sql text = 'UPDATE CategoryType SET Active = False WHERE Id=$1 RETURNING *';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION CategoryType_Get(active bool)
RETURNS SETOF CategoryType AS $func$
DECLARE 
	sql text = 'SELECT * from CategoryType WHERE Active = $1';
BEGIN
	RETURN QUERY EXECUTE sql USING active;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION CategoryType_GetById(id int)
RETURNS SETOF CategoryType AS $func$
DECLARE 
	sql text = 'SELECT * from CategoryType WHERE Active=True AND Id=$1';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;


CREATE OR REPLACE FUNCTION Election_Insert(id uuid, date date, startdate timestamp, enddate timestamp,
			description text, version text, allowupdates bool)
RETURNS SETOF Election AS $func$
DECLARE 
	sql text = 'INSERT INTO Election (id, Date, StartDateLocal, EndDateLocal, Description, Version, AllowUpdates, CreateDate, LastUpdated) VALUES 
	($1, $2, $3, $4, $5, $6, $7, current_timestamp, current_timestamp) RETURNING *';
BEGIN
	RETURN QUERY EXECUTE sql USING id, date, startdate, enddate, description, version, allowupdates;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Election_Update(id uuid, date date, startdate timestamp, enddate timestamp,
		description text, version text, allowupdates bool)
RETURNS SETOF Election AS $func$
DECLARE 
	sql text = 'UPDATE Election SET Date=$2, Description=$3, StartDateLocal=$4, EndDateLocal=$5, 
		Version=$6, AllowUpdates=$7, LastUpdated=current_timestamp WHERE Id=$1 RETURNING *';
BEGIN
	RETURN QUERY EXECUTE sql USING id, date, description, startdate, enddate, version, allowupdates;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Election_Delete(id uuid)
RETURNS SETOF Election AS $func$
DECLARE
	sql text = 'DELETE FROM Election WHERE Id=$1 RETURNING *';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Election_Get(date date)
RETURNS SETOF Election AS $func$
DECLARE
	sql text = 'SELECT * from Election WHERE Date > $1 order by Date desc';
BEGIN
	RETURN QUERY EXECUTE sql USING date;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Election_GetById(id uuid)
RETURNS SETOF Election AS $func$
DECLARE
	sql text = 'SELECT * from Election Where Id=$1';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Party_Insert(description text)
RETURNS SETOF party AS $func$
DECLARE
	sql text = 'INSERT INTO Party (Description, Active, CreateDate, LastUpdated) VALUES 
	($1, True, current_timestamp, current_timestamp) RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING description;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Party_Update(id int, description text, active bool)
RETURNS SETOF party AS $func$
DECLARE
	sql text = 'UPDATE Party SET Description=$2, Active=$3, LastUpdated=current_timestamp where Id=$1 RETURNING *';
BEGIN	
	RETURN QUERY EXECUTE sql USING id, description, active;	
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Party_Delete(id int)
RETURNS SETOF party AS $func$
DECLARE
	sql text = 'UPDATE Party SET Active = False WHERE Id=$1 RETURNING *';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Party_Get(active bool)
RETURNS SETOF party AS $func$
DECLARE 
	sql text = 'SELECT * from Party WHERE Active = $1';
BEGIN
	RETURN QUERY EXECUTE sql USING active;
END;
$func$ Language plpgsql;


CREATE OR REPLACE FUNCTION Party_GetById(id int)
RETURNS SETOF party AS $func$
DECLARE
	sql text = 'SELECT * from Party WHERE id=$1';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;


CREATE OR REPLACE FUNCTION Ticket_Insert(id uuid, electionid uuid, categoryid uuid, description text,
		partyid int, tickettype int, information text, sequence int)
RETURNS SETOF Ticket AS $func$
DECLARE
	sql text = 'INSERT INTO Ticket (id, ElectionId, CategoryId, Description, PartyId, TicketType, Information, Sequence, CreateDate, LastUpdated) VALUES 
	($1, $2, $3, $4, $5, $6, $7, $8, current_timestamp, current_timestamp) RETURNING *';
BEGIN
	RETURN QUERY EXECUTE sql USING id, electionid, categoryid, description, partyid, tickettype, information, sequence;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Ticket_Update(id uuid, electionid uuid, categoryid uuid, description text,
		partyid int, tickettype int, information text, sequence int)
RETURNS SETOF Ticket AS $func$
DECLARE
	sql text = 'UPDATE Ticket SET ElectionId=$2, CategoryId=$3, Description=$4, PartyId=$5,
	TicketType=$6, Information=$7, Sequence=$8, LastUpdated=current_timestamp 
	WHERE Id=$1 RETURNING *';
BEGIN
	RETURN QUERY EXECUTE sql USING id, electionid, categoryid, description, partyid, tickettype, information, sequence;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Ticket_Delete(id uuid)
RETURNS SETOF Ticket AS $func$
DECLARE
	sql text = 'DELETE FROM Ticket WHERE Id=$1 RETURNING *';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Ticket_GetById(id uuid)
RETURNS SETOF Ticket AS $func$
DECLARE
	sql text = 'SELECT * from Ticket WHERE Id = $1';
BEGIN
	RETURN QUERY EXECUTE sql USING id;
END;
$func$ Language plpgsql;

CREATE OR REPLACE FUNCTION Ticket_GetByElection(electionid uuid)
RETURNS SETOF Ticket AS $func$
DECLARE
	sql text = 'SELECT * from Ticket WHERE ElectionId = $1';
BEGIN
	RETURN QUERY EXECUTE sql USING electionid;
END;
$func$ Language plpgsql;

