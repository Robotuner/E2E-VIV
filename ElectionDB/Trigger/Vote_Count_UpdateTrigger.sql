CREATE Trigger [Vote_Count_UpdateTrigger]
ON [dbo].[Vote]
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @voteStatus int
	DECLARE @candidateId UNIQUEIDENTIFIER
	SELECT	@candidateId = i.SelectionId FROM INSERTED i;
	SELECT	@voteStatus = i.VoteStatus FROM INSERTED i;

	if EXISTS (SELECT 1 FROM [dbo].[VoteResult] WHERE Id=@candidateId)
	BEGIN
		IF (UPDATE(VoteStatus) AND @voteStatus > 0)
		BEGIN
			UPDATE [dbo].[VoteResult] SET [Count]=([Count] - 1) WHERE Id=@candidateId;
		END
	END
END
