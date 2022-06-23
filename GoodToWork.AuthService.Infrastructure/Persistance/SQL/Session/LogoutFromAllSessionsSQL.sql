create procedure LogoutFromAllSessions @userId uniqueidentifier
as
update [Sessions] set ExpirationDate = GETDATE() where UserId = @userId and ExpirationDate > GETDATE()