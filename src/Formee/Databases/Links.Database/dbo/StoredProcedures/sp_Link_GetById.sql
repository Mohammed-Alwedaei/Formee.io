CREATE PROCEDURE [dbo].[sp_Link_GetById]
	@Id INT
AS

BEGIN 
	SELECT * FROM dbo.[Link]
	WHERE Id = @Id AND IsDeleted != 0;
END