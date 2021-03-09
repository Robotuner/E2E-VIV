CREATE Trigger [Category_LastUpdate_Trigger]
ON Category
AFTER UPDATE
AS
BEGIN
	IF NOT UPDATE(LastUpdated)
	BEGIN
		UPDATE Category
			SET Category.LastUpdated = GETUTCDATE()
			FROM Category as t
			INNER JOIN inserted as i
			ON t.Id = i.Id;
	END
END