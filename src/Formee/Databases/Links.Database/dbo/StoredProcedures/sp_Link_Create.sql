CREATE PROCEDURE [dbo].[sp_Link_Create]
	 @ContainerId NVARCHAR(24),
	 @OriginalUrl NVARCHAR(1024),
	 @IsDeleted BIT,
	 @CreatedDate DATETIME2(7)
AS

BEGIN
	INSERT INTO dbo.[Link](ContainerId, OriginalUrl, IsDeleted, CreatedDate)
	OUTPUT inserted.Id
	VALUES(@ContainerId, @OriginalUrl, @IsDeleted, @CreatedDate);
END