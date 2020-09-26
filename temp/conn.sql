
--Server=tcp:tomzredb-server1.database.windows.net,1433;Initial Catalog=tomzredb;Persist Security Info=False;User ID=futadmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
CREATE DATABASE FutbalMng

CREATE DATABASE FutbalMngAuth

USE FutbalMngAuth

 --user 
CREATE TABLE users (
	Id uniqueidentifier primary key not null,
	Email nvarchar(100) not null,
    Password nvarchar(200) not null,
    Username nvarchar(100) not null,
	CreatedOn datetime not null,
    UpdatedOn datetime null
)

USE [master];

DECLARE @kill varchar(8000) = '';  
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
FROM sys.dm_exec_sessions
WHERE database_id  = db_id('FutbalMng')

EXEC(@kill);




CREATE LOGIN futadmin WITH PASSWORD ='Fut@dm1npass'
CREATE USER futadmin for login futadmin

EXEC sp_addsrvrolemember 
    @loginame = N'futadmin', 
    @rolename = N'sysadmin';

USE FutbalMngAuth
GRANT INSERT ON OBJECT::users TO futadmin
USE FutbalMngAuth
GRANT SELECT ON OBJECT::users TO futadmin

USE FutbalMng
select * from users


USE FutbalMng
select * from games

select * from UserGame
USE FutbalMng

select * from address

SELECT * FROM INFORMATION_SCHEMA.TABLES 
SELECT * FROM ClientCorsOrigins
select * from Clients
WHERE TABLE_TYPE='BASE TABLE'

update Clients set ClientUri = 'http://localhost:3000' where id =1
insert into ClientCorsOrigins(Origin, ClientId)
VALUES('http://localhost:3000', 1)

SELECT name, database_id, create_date  
FROM sys.databases ;  

select
* from sys.database_permissions

select * from AspNetUsers

select * from dbo.DeviceCodes


