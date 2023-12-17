USE DotNetCourseDatabase;
Go

CREATE TABLE TutorialAppSchema.Users
(
    [UserId] [int] IDENTITY(1,1) NOT NULL,
    [FirstName] [nvarchar](50) NULL,
    [LastName] [nvarchar](50) NULL,
    [Email] [nvarchar](50) NULL,
    [Gender] [nvarchar](50) NULL,
    [Active] [bit] NULL
)
GO

SELECT *
FROM TutorialAppSchema.Users;
GO

CREATE TABLE TutorialAppSchema.Auth
(
    [Email] [nvarchar](50) NULL,
    [PasswordHash] [varbinary](max) NULL,
    [PasswordSalt] [varbinary](max) NULL
)
GO

SELECT *
FROM TutorialAppSchema.Auth;
GO

CREATE TABLE TutorialAppSchema.AuthTable
(
    [Email] [nvarchar](50) NULL,
    [Password] [nvarchar](255) NULL,
    [PasswordConformation] [nvarchar](255) NULL
)
GO

SELECT *
FROM TutorialAppSchema.AuthTable;
Go


CREATE TABLE TutorialAppSchema.Posts
(
    PostId INT IDENTITY(1,1),
    UserId INT NOT NULL,
    PostTitle VARCHAR(255),
    PostContent VARCHAR(MAX),
    PostCreationDate DATETIME,
    LastUpdateddate DATETIME
);
GO

CREATE CLUSTERED INDEX cix_Posts_UserId_PostId ON TutorialAppSchema.Posts(UserId, PostId);



