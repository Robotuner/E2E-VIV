CREATE Trigger [Election_LastUpdate_Trigger]
ON Election 
AFTER UPDATE
AS
BEGIN
	IF NOT UPDATE(LastUpdated)
	BEGIN
		UPDATE Election
			SET Election.LastUpdated = GETUTCDATE()
			FROM Election as t
			INNER JOIN inserted as i
			ON t.Id = i.Id;
	END
END
