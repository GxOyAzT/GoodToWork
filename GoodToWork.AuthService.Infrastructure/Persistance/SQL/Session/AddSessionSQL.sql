create procedure AddSession @id uniqueidentifier, @userId uniqueidentifier, @expirationDate date
as
insert into [Sessions] (Id, UserId, ExpirationDate) values (@id, @userId, @expirationDate)