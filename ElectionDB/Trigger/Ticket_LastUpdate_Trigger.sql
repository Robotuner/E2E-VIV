CREATE Trigger [Ticket_LastUpdate_Trigger]
ON Ticket 
AFTER UPDATE
AS
BEGIN
	IF NOT UPDATE(LastUpdated)
	BEGIN
		UPDATE Ticket
			SET Ticket.LastUpdated = GETUTCDATE()
			FROM Ticket as t
			INNER JOIN inserted as i
			ON t.Id = i.Id;
	END
END
