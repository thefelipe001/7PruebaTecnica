create database FelipePrueba
go 
use FelipePrueba
create table Persona
(
ID int identity(1,1)primary key,
Nombre varchar(30),
FechaDeNacimiento Datetime
)

select * from Persona
insert into Persona values('Felipe Mejia',GETDATE())
insert into Persona values('Hansel Cabrera',2001-09-31 )