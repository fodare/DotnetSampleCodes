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

CREATE TABLE TutorialAppSchema.Auth
(
    [Email] [nvarchar](50) NULL,
    [PasswordHash] [varbinary](max) NULL,
    [PasswordSalt] [varbinary](max) NULL
)
GO

CREATE TABLE TutorialAppSchema.AuthTable
(
    [Email] [nvarchar](50) NULL,
    [Password] [nvarchar](255) NULL,
    [PasswordConformation] [nvarchar](255) NULL
)
GO

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

CREATE OR ALTER PROCEDURE TutorialAppSchema.spUser_Add
    /* EXEC TutorialAppSchema.spUser_Add @email = 'likeman@test.com', @password = 'afadkfh', 
    @passwordConfirmation = 'sfsdfshfs', @firstName = 'fiyre', 
    @lastName = 'sdknfsd', @gender = 'Female'*/
    @email nvarchar(50),
    @password nvarchar(50),
    @passwordConfirmation nvarchar(50),
    @firstName nvarchar(50),
    @lastName nvarchar(50),
    @gender nvarchar(50)
AS
BEGIN
    INSERT INTO TutorialAppSchema.AuthTable
        ([Email],[Password],[PasswordConformation] )
    VALUES
        (@email, @password, @passwordConfirmation)

    INSERT INTO TutorialAppSchema.Users
        (
        [FirstName], [LastName], [Email],[Gender], [Active])
    VALUES
        (@firstName, @lastName, @email, @gender, 1)
END

CREATE OR ALTER PROCEDURE TutorialAppSchema.spUserEmail_Get
    /* EXEC TutorialAppSchema.spUserEmail_Get @useremail = boris */
    @useremail VARCHAR(50)
AS
BEGIN
    SELECT
        [Email], [Password], [PasswordConformation]
    FROM TutorialAppSchema.AuthTable
    WHERE [Email] = @useremail
END

CREATE OR ALTER PROCEDURE TutorialAppSchema.spPost_Get
    /* EXEC TutorialAppSchema.spPost_Get @postId = 3, @userId = 2 */
    /* EXEC TutorialAppSchema.spPost_Get @userId = 3 */
    @postId  INT = NULL,
    @userId INT = NULL
AS
BEGIN
    SELECT [PostId],
        [UserId],
        [PostTitle],
        [PostContent],
        [PostCreationDate],
        [LastUpdateddate]
    FROM TutorialAppSchema.Posts
    WHERE [PostId] = ISNULL(@postId, PostId)
        AND [UserId] = ISNULL(@userId, UserId)
END

CREATE OR ALTER PROCEDURE TutorialAppSchema.spPost_Delete
    /* EXEC TutorialAppSchema.spPost_Delete @postId = 10, @userId = 1002 */
    @postId INT,
    @userId INT
AS
BEGIN
    DELETE FROM TutorialAppSchema.Posts
    WHERE [PostId] = @postId AND [UserId] = @userId
END