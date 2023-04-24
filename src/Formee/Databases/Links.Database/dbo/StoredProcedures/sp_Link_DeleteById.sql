CREATE PROCEDURE [dbo].[sp_Link_DeleteById]
	@Id INT,
	@DeletedDate DATETIME2(7)
AS

BEGIN
	UPDATE dbo.[Link]
	SET IsDeleted = 1, DeletedDate = @DeletedDate
	WHERE Id = @Id;

	SELECT [L].[Id], [L].[ContainerId], [L].[UserId], [L].[LinksDetailsId], 
	[L].[OriginalUrl], [L].[TargetUrl], [L].[IsDeleted], [L].[DeletedDate], [L].[CreatedDate], 
	[D].[Id], [D].[Name], [D].[Domain] 
	FROM dbo.[Link] as L
	INNER JOIN dbo.[LinkDetails] as D
	ON (L.LinksDetailsId = D.Id)
	WHERE L.Id = @Id;
END
