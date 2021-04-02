
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

CREATE TRIGGER Vote_Count_Update_Trigger
AFTER UPDATE ON Vote
	FOR EACH ROW	
		EXECUTE PROCEDURE Vote_Count_Update_Function();
		
CREATE TRIGGER Vote_Count_Insert_Trigger
AFTER INSERT ON Vote
	FOR EACH ROW	
		EXECUTE PROCEDURE Vote_Count_Insert_Function();

