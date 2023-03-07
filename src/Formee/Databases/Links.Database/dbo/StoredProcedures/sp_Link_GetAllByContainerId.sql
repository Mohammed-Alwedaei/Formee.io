CREATE PROCEDURE [dbo].[sp_Link_GetAllByContainerId]
	@ContainerId NVARCHAR(24)
AS

BEGIN
	SELECT [Id], [ContainerId], [OriginalUrl], [TargetUrl], [IsDeleted], 
	[CreatedDate] FROM dbo.[Link]
	WHERE ContainerId = @ContainerId AND IsDeleted != 1;
END