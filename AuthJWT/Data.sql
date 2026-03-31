INSERT INTO Users (Username, PasswordHash, RefreshToken, RefreshTokenExpiryTime)
VALUES ('alice', 'password123', CONVERT(VARCHAR(36), NEWID()), DATEADD(day, 7, GETUTCDATE()));

INSERT INTO Users (Username, PasswordHash, RefreshToken, RefreshTokenExpiryTime)
VALUES ('bob', 'Passw0rd!', CONVERT(VARCHAR(36), NEWID()), DATEADD(day, 7, GETUTCDATE()));

INSERT INTO Users (Username, PasswordHash, RefreshToken, RefreshTokenExpiryTime)
VALUES ('carol', '123456', CONVERT(VARCHAR(36), NEWID()), DATEADD(day, 7, GETUTCDATE()));

INSERT INTO Users (Username, PasswordHash, RefreshToken, RefreshTokenExpiryTime)
VALUES ('dave', 'secret', CONVERT(VARCHAR(36), NEWID()), DATEADD(day, 7, GETUTCDATE()));

-- Example user with expired refresh token
INSERT INTO Users (Username, PasswordHash, RefreshToken, RefreshTokenExpiryTime)
VALUES ('eve', 'passwordExpired', CONVERT(VARCHAR(36), NEWID()), DATEADD(day, -1, GETUTCDATE()));

-- Verify inserted rows
SELECT UserId, Username,PasswordHash, RefreshToken, RefreshTokenExpiryTime FROM Users;