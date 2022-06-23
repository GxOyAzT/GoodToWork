create table Users(
	Id uniqueidentifier primary key,
	Email varchar(100),
	UserName varchar(30),
	HashPassword varchar(100),
	Created datetime
);

create table [Sessions] (
	Id uniqueidentifier primary key,
	UserId uniqueidentifier foreign key references Users(Id) NOT NULL,
	ExpirationDate datetime NOT NULL
);