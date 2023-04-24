CREATE PROCEDURE [dbo].[sp_Link_GetById]
	@Id INT
AS

BEGIN 
	SELECT [L].[Id], [L].[ContainerId], [L].[LinksDetailsId], 
	[L].[OriginalUrl], [L].[TargetUrl], [L].[IsDeleted], [L].[DeletedDate], 
	[L].[CreatedDate], [D].[Id], [D].[Name], [D].[Domain] FROM dbo.[Link] AS L 
	INNER JOIN dbo.[LinkDetails] AS D
	ON (L.LinksDetailsId = D.Id)
	WHERE L.Id = @Id AND IsDeleted != 1;
END