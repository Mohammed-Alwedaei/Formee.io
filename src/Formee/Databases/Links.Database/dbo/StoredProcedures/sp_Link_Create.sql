CREATE PROCEDURE [dbo].[sp_Link_Create]
	 @ContainerId NVARCHAR(24),
	 @UserId UNIQUEIDENTIFIER,
	 @Name NVARCHAR(30),
	 @Domain NVARCHAR(255),
	 @OriginalUrl NVARCHAR(1024),
	 @IsDeleted BIT,
	 @CreatedDate DATETIME2(7)
AS

BEGIN
	-- Insert the link details and set the id of the record into the temp varibale (@LinkDetailTable)
	INSERT INTO dbo.LinkDetails(Name, Domain)
	VALUES(@Name, @Domain);
	
	-- Create the link and attach the link details id
	INSERT INTO dbo.[Link](ContainerId, UserId, LinksDetailsId, OriginalUrl, IsDeleted, CreatedDate)
	OUTPUT inserted.Id
	VALUES(@ContainerId, @UserId, SCOPE_IDENTITY(), @OriginalUrl, @IsDeleted, @CreatedDate);
END