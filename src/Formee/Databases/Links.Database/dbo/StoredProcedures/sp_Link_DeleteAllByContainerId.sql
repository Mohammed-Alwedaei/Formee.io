CREATE PROCEDURE [dbo].[sp_Link_DeleteAllByContainerId]
	@ContainerId NVARCHAR(24),
	@DeletedDate DATETIME2(7)
AS

BEGIN
	UPDATE dbo.[Link]
	SET IsDeleted = 1, DeletedDate = @DeletedDate
	WHERE ContainerId = @ContainerId AND IsDeleted != 1;

	SELECT [L].[Id], [L].[ContainerId], [L].[UserId], [L].[LinksDetailsId], 
	[L].[OriginalUrl], [L].[TargetUrl], [L].[IsDeleted], [L].[DeletedDate], [L].[CreatedDate], 
	[D].[Id], [D].[Name], [D].[Domain] 
	FROM dbo.[Link] as L
	INNER JOIN dbo.[LinkDetails] as D
	ON (L.LinksDetailsId = D.Id)
	WHERE ContainerId = @ContainerId 
	AND DeletedDate = @DeletedDate;
END