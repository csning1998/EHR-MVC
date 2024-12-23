CREATE TRIGGER trg_UpdateUserRoleOnPractitionerInsert
ON [dbo].[Practitioner]
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE [dbo].[Users]
    SET [Role] = 'Practitioner'
    FROM [dbo].[Users] u
    INNER JOIN INSERTED i ON u.[UserId] = i.[UserId];
END;
