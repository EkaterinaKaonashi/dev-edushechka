﻿CREATE PROCEDURE [dbo].[User_SelectAll]
AS
	SELECT * 
	FROM [User]
	where IsDeleted = 0