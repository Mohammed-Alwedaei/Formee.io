CREATE PROCEDURE [dbo].[sp_Hit_Create]
	@LinkId INT,
	@CreatedDate DATETIME2(7)
AS

BEGIN
	INSERT INTO dbo.[LinkHits](LinkId, CreatedDate)
	VALUES(@LinkId, @CreatedDate);
END