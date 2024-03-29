﻿CREATE PROCEDURE [dbo].[sp_Hit_GetAllByContainerId]
	@ContainerId NVARCHAR(24),
	@StartDate DATETIME2(7),
	@EndDate DATETIME2(7)
AS

BEGIN
	SELECT DISTINCT [L].[Id], [L].[CreatedDate], [H].[Id], [H].[LinkId], [H].[CreatedDate]
	FROM dbo.[Link] AS L
	INNER JOIN dbo.[LinkHits] AS H
	ON (L.Id = H.LinkId)
	WHERE L.ContainerId = @ContainerId
	AND IsDeleted != 1 
	AND H.CreatedDate >= @StartDate
	AND H.CreatedDate <= @EndDate;
END
