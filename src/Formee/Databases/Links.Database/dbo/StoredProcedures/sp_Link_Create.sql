CREATE PROCEDURE [dbo].[sp_Link_Create]
	 @ContainerId NVARCHAR(24),
	 @Name NVARCHAR(30),
	 @OriginalUrl NVARCHAR(1024),
	 @IsDeleted BIT,
	 @CreatedDate DATETIME2(7)
AS

BEGIN
	INSERT INTO dbo.[Link](ContainerId, Name, OriginalUrl, IsDeleted, CreatedDate)
	OUTPUT inserted.Id
	VALUES(@ContainerId, @Name, @OriginalUrl, @IsDeleted, @CreatedDate);
END