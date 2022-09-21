CREATE PROCEDURE [dbo].[CSP_Login]
	@Email NVARCHAR(384),
	@Passwd NVARCHAR(20)
AS
BEGIN
	SELECT Id, Email 
	FROM [User]
	WHERE Email = @Email and Passwd = HASHBYTES('SHA2_512', @Passwd);
	RETURN 0
END