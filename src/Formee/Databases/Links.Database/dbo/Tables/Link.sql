﻿CREATE TABLE [dbo].[Link]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
	[ContainerId] NVARCHAR(24) NOT NULL,
	[LinksDetailsId] INT NOT NULL REFERENCES [dbo].[LinkDetails](Id),
	[OriginalUrl] NVARCHAR(1024) NOT NULL,
	[TargetUrl] NVARCHAR(100) NULL,
	[IsDeleted] BIT NULL,
	[DeletedDate] DATETIME2(7) NULL,
	[CreatedDate] DATETIME2(7) NOT NULL
);
