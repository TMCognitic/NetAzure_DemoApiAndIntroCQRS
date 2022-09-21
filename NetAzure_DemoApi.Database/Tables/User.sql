CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL IDENTITY, 
    [Email] NVARCHAR(384) NOT NULL, 
    [Passwd] BINARY(64) NOT NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]), 
    CONSTRAINT [UK_User_Email] UNIQUE ([Email]) 
)
