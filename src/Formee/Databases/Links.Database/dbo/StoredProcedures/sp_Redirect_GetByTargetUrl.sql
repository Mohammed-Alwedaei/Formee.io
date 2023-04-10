CREATE PROCEDURE [dbo].[sp_Redirect_GetByTargetUrl]
	@TargetUrl NVARCHAR(100)
AS

BEGIN
	SELECT [Id], [OriginalUrl], [TargetUrl] FROM dbo.[Link]
	WHERE TargetUrl = @TargetUrl 
		AND IsDeleted != 1;
END
