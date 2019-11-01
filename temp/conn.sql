

CREATE DATABASE FutbalMng

USE FutbalMng

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


CREATE USER futadmin for login futadmin


CREATE LOGIN futadmin WITH PASSWORD ='Fut@dm1npass'
CREATE USER futadmin for login futadmin

EXEC sp_addsrvrolemember 
    @loginame = N'futadmin', 
    @rolename = N'sysadmin';

USE FutbalMng
GRANT INSERT ON OBJECT::users TO futadmin
USE FutbalMng
GRANT SELECT ON OBJECT::users TO futadmin

USE FutbalMng
select * from users


USE FutbalMng
select * from games

select * from UserGame
USE FutbalMng

select * from address

SELECT * FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE='BASE TABLE'

select
* from sys.database_permissions
