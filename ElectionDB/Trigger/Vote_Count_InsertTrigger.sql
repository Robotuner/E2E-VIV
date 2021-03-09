CREATE Trigger [Vote_Count_InsertTrigger]
ON [dbo].[Vote]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @candidateId UNIQUEIDENTIFIER
	DECLARE @electionId UNIQUEIDENTIFIER
	DECLARE @categoryId UNIQUEIDENTIFIER
	SELECT @candidateId = INSERTED.SelectionId FROM INSERTED
	SELECT @electionId = INSERTED.ElectionId FROM INSERTED
	SELECT @categoryId = INSERTED.CategoryId FROM INSERTED

	if NOT EXISTS (SELECT 1 FROM [dbo].[VoteResult] WHERE Id=@candidateId)
	BEGIN
		INSERT INTO [dbo].[VoteResult] (Id, ElectionId, CategoryId, [Count]) VALUES (@candidateid, @electionId, @categoryId, 1)
	END
	ELSE
	BEGIN
		UPDATE [dbo].[VoteResult] SET [Count]= (ISNULL([Count],0) + 1) WHERE Id=@candidateId;
	END

END