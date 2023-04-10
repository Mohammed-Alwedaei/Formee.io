CREATE PROCEDURE [dbo].[sp_Link_GetAllByContainerId]
	@ContainerId NVARCHAR(24)
AS

BEGIN
	SELECT [L].[Id], [L].[ContainerId], [L].[LinksDetailsId], [L].[OriginalUrl], 
	[L].[TargetUrl], [L].[IsDeleted], [L].[DeletedDate], [L].[CreatedDate], 
	[D].[Id], [D].[Name], [D].[Domain] 
	FROM dbo.[Link] AS L 
	INNER JOIN dbo.[LinkDetails] AS D
	ON (L.LinksDetailsId = D.Id)
	WHERE L.ContainerId = @ContainerId AND IsDeleted != 1;
END