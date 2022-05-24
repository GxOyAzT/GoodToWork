create procedure GetUserBySessionId @tokenId uniqueidentifier
as
select u.* from [Sessions] s
inner join [Users] u on s.UserId = u.Id
where s.Id = @tokenId and ExpirationDate > GETDATE()