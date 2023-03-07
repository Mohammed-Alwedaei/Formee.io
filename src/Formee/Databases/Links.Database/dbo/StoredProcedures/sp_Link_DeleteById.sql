CREATE PROCEDURE [dbo].[sp_Link_DeleteById]
	@Id INT,
	@DeletedDate DATETIME2(7)
AS

BEGIN
	UPDATE dbo.[Link]
	SET IsDeleted = 1, DeletedDate = @DeletedDate
	WHERE Id = @Id;

	SELECT [Id], [ContainerId], [OriginalUrl], [TargetUrl], [DeletedDate] 
	FROM dbo.[Link]
	WHERE Id = @Id;
END
