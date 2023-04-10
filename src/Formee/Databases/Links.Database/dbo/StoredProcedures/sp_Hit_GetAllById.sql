CREATE PROCEDURE [dbo].[sp_Hit_GetAllById]
	@LinkId int
AS

BEGIN
	SELECT [Id], [LinkId], [CreatedDate] FROM dbo.[LinkHits]
	WHERE LinkId = @LinkId;
END