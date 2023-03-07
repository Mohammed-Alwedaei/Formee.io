CREATE PROCEDURE [dbo].[sp_Link_UpdateTargetUrlById]
	@Id INT,
	@TargetUrl NVARCHAR(100)
AS

BEGIN
	UPDATE dbo.[Link]
	SET TargetUrl = @TargetUrl
	WHERE Id = @Id;

	SELECT * FROM dbo.[Link]
	WHERE Id = @Id;
END
