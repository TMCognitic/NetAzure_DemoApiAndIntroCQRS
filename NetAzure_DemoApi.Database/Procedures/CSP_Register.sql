﻿CREATE PROCEDURE [dbo].[CSP_Register]
	@Email NVARCHAR(384),
	@Passwd NVARCHAR(20)
AS
BEGIN
	INSERT INTO [User] (Email, Passwd) VALUES (@Email, HASHBYTES('SHA2_512', @Passwd));
	RETURN 0
END
