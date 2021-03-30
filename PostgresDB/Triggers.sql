/*DROP FUNCTION Category_LastUpdate_Function();

CREATE OR REPLACE function Category_LastUpdate_Function()
	RETURNS TRIGGER
	LANGUAGE PLPGSQL
	AS 
$$
BEGIN
	UPDATE Category
		SET LastUpdated = current_timestamp
	WHERE Category.Id = new.Id;
	RETURN NEW;
END;
$$

CREATE OR REPLACE function CategoryType_LastUpdate_Function()
	RETURNS TRIGGER
	LANGUAGE PLPGSQL
	AS 
$$
BEGIN
	if New.lastupdated != Old.lastupdated THEN
		UPDATE CategoryType
			SET LastUpdated = current_timestamp
		WHERE CategoryType.Id = NEW.Id;
	END IF;
	RETURN NEW;
END;
$$

CREATE OR REPLACE function Election_LastUpdate_Function()
	RETURNS TRIGGER
	LANGUAGE PLPGSQL
	AS 
$$
BEGIN
	UPDATE Election
		SET LastUpdated = current_timestamp
	WHERE Election.Id = NEW.Id;
	
	RETURN NEW;
END;
$$
		
CREATE OR REPLACE function Ticket_LastUpdate_Function()
	RETURNS TRIGGER
	LANGUAGE PLPGSQL
	AS 
$$
BEGIN
	UPDATE Ticket
		SET LastUpdated = current_timestamp
	WHERE Ticket.Id = NEW.Id;
	
	RETURN NEW;
END;
$$
*/

CREATE OR REPLACE function Vote_Count_Update_Function()
	RETURNS TRIGGER
	LANGUAGE PLPGSQL
	AS 
$$
BEGIN
	DECLARE 
		totalCount int;
	BEGIN	
		IF EXISTS (SELECT 1 FROM VoteResult WHERE Id=NEW.SelectionId) THEN
			IF NEW.voteStatus > 0 THEN	
				SELECT COUNT from VoteResult into totalCount where Id = NEW.SelectionId;
				UPDATE VoteResult SET Count=totalCount-1 WHERE Id=NEW.SelectionId;
			END IF;
		END IF;
	END;

	RETURN NEW;
END
$$
		
CREATE OR REPLACE function Vote_Count_Insert_Function()
	RETURNS trigger
	LANGUAGE PLPGSQL
	AS 
$$
BEGIN
	DECLARE 
		totalCount int;
	BEGIN	
		IF NOT EXISTS (SELECT 1 FROM VoteResult WHERE Id=NEW.SelectionId) THEN						
			INSERT INTO VoteResult (Id, ElectionId, CategoryId, Count) VALUES
			(NEW.Selectionid, NEW.electionId, NEW.categoryId, 1);
		ELSE
			SELECT Count from VoteResult INTO totalCount WHERE Id=NEW.SelectionId;		
			UPDATE VoteResult set Count = (totalCount+1) WHERE Id=NEW.SelectionId;
		END IF;
	END;

	RETURN NEW;
END
$$
/*
CREATE TRIGGER Category_LastUpdate_Trigger
AFTER UPDATE ON Category
	FOR EACH ROW	
		EXECUTE PROCEDURE Category_LastUpdate_Function();
		
CREATE TRIGGER CategoryType_LastUpdate_Trigger
AFTER UPDATE ON CategoryType
	FOR EACH ROW	
		EXECUTE PROCEDURE CategoryType_LastUpdate_Function();
		
CREATE TRIGGER Election_LastUpdate_Trigger
AFTER UPDATE ON Election
	FOR EACH ROW	
		EXECUTE PROCEDURE Election_LastUpdate_Function();
		
CREATE TRIGGER Ticket_LastUpdate_Trigger
AFTER UPDATE ON Ticket
	FOR EACH ROW	
		EXECUTE PROCEDURE Ticket_LastUpdate_Function();		
*/

CREATE TRIGGER Vote_Count_Update_Trigger
AFTER UPDATE ON Vote
	FOR EACH ROW	
		EXECUTE PROCEDURE Vote_Count_Update_Function();
		
CREATE TRIGGER Vote_Count_Insert_Trigger
AFTER INSERT ON Vote
	FOR EACH ROW	
		EXECUTE PROCEDURE Vote_Count_Insert_Function();

