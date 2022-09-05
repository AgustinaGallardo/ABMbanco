create database AMBbancoProgII

use AMBbancoProgII

create table CLIENTES(
id_cliente int IDENTITY (1,1) not null,
nombre varchar (20) not null,
apellido varchar (30) not null,
dni int not null,
constraint pk_id_cliente PRIMARY KEY (id_cliente)
)

create table TIPOCUENTAS(
id_tipoCuenta int,
nombre varchar (40)
constraint pk_tipoCuenta PRIMARY KEY(id_tipoCuenta))

create table CUENTAS
(id_cuenta int IDENTITY (1,1),
cbu int not null,
saldo decimal (24,4),
ultimoMovimiento datetime,
id_cliente int,
id_tipoCuenta int,
constraint pk_cuenta PRIMARY KEY (id_cuenta),
constraint fk_id_cliente FOREIGN KEY (id_cliente)
References CLIENTES (id_cliente),
constraint fk_id_tipoCuenta FOREIGN KEY (id_tipoCuenta)
References TIPOCUENTAS (id_tipoCuenta)
)

INSERT INTO TIPOCUENTAS (id_tipoCuenta, nombre) VALUES (1, 'caja de ahorro en pesos')
INSERT INTO TIPOCUENTAS (id_tipoCuenta, nombre) VALUES (2, 'caja de ahorro en dolares')
INSERT INTO TIPOCUENTAS (id_tipoCuenta, nombre) VALUES (3, 'cuenta corriente en pesos')
INSERT INTO TIPOCUENTAS (id_tipoCuenta, nombre) VALUES (4, 'cuenta corriente en dolares')
INSERT INTO TIPOCUENTAS (id_tipoCuenta, nombre) VALUES (5, 'cuenta sueldo')
INSERT INTO TIPOCUENTAS (id_tipoCuenta, nombre) VALUES (6, 'caja de ahorro recaudadora')

insert into clientes (nombre,apellido,dni) values ('Laura','Lopez',34566778)
insert into clientes (nombre,apellido,dni) values ('Pilar','Barro',34234578)
insert into clientes (nombre,apellido,dni) values ('Rosario','Kleiner',2345778)
insert into clientes (nombre,apellido,dni) values ('Laura','Mastreta',31235778)
insert into clientes (nombre,apellido,dni) values ('Geronimo','Kleiner',31324578)

insert into cuentas (cbu,saldo,ultimoMovimiento,id_cliente) values (123134,1500,'5/5/2018',1)
insert into cuentas (cbu,saldo,ultimoMovimiento,id_cliente) values (215234,3000,'15/5/2022',2)
insert into cuentas (cbu,saldo,ultimoMovimiento,id_cliente) values (12434,1200,'15/9/2022',3)
insert into cuentas (cbu,saldo,ultimoMovimiento,id_cliente) values (6765,30000,'15/11/2022',4)
insert into cuentas (cbu,saldo,ultimoMovimiento,id_cliente) values (6554,5000,'1/8/2022',5)
 
create procedure cboTiposCuentas
as
begin
select * from TIPOCUENTAS	
end

create procedure ProximoCiente
@next int output
as
begin
set @next = (select max(id_cliente) from clientes);
end

create procedure insertCliente
@apellido varchar(50),
@nombre varchar(50),
@dni int,
@cod_cliente int output
as
begin
insert into CLIENTES (apellido,nombre,dni)
values (@apellido,@nombre,@dni)
set @cod_cliente = SCOPE_IDENTITY();
end

 create procedure insertCuenta
 @cod_cliente int,
 @cbu int,
 @id_tipoCuenta int,
 @saldo decimal(10,2),
@ultimoMovimiento datetime
as
begin
insert into cuentas (cbu,id_tipoCuenta,saldo,ultimoMovimiento,id_cliente)
	values  (@cbu,@id_tipoCuenta, @saldo,@ultimoMovimiento,@cod_cliente)
end

select * from CLIENTES