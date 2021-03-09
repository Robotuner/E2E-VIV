CREATE Trigger [CategoryType_LastUpdate_Trigger]
ON CategoryType 
AFTER UPDATE
AS
BEGIN
	IF NOT UPDATE(LastUpdated)
	BEGIN
		UPDATE CategoryType
			SET CategoryType.LastUpdated = GETUTCDATE()
			FROM CategoryType as t
			INNER JOIN inserted as i
			ON t.Id = i.Id;
	END
END
