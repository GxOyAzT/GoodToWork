create procedure FindUserByPassword @user varchar(100), @password varchar(100)
as
select * from Users u
where (u.UserName = @user or u.Email = @user) and u.HashPassword = @password