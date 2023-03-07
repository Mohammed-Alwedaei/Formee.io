CREATE PROCEDURE [dbo].[sp_Link_DeleteAllByContainerId]
	@ContainerId NVARCHAR(24),
	@DeletedDate DATETIME2(7)
AS

BEGIN
	UPDATE dbo.[Link]
	SET IsDeleted = 1, DeletedDate = @DeletedDate
	WHERE ContainerId = @ContainerId AND IsDeleted != 1 AND DeletedDate IS NULL;

	SELECT [Id], [ContainerId], [OriginalUrl], [TargetUrl], [DeletedDate] 
	FROM dbo.[Link]
	WHERE ContainerId = @ContainerId 
			AND IsDeleted != 1
			AND DeletedDate != @DeletedDate;
END