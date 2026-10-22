create database ExamenFinal
go

use ExamenFinal
go

create table tb_paises(
idpais char(3) Primary key,
nompais varchar(40) not null
)
go

Create table tb_Consumidor (
dnicont char(8) Primary key,
nomcont varchar(40) not null,
apecont varchar(40) not null,
emailcont varchar(250) Unique,
idpais char(3) not null,
Constraint FK_Pais Foreign Key (idpais)
references tb_paises (idpais)
)
go

insert into tb_paises values ('PER','Peru'),
('USA','Estados Unidos'),
('ESP','España')
go

insert into tb_Consumidor (dnicont, nomcont, apecont, emailcont, idpais)
values
('12345678','Juan','Pérez','juan.perez@mail.com','PER'),
('87654321','María','Gómez','maria.gomez@mail.com','PER'),
('45678912','Carlos','Ramírez','carlos.ramirez@mail.com','USA'),
('74185296','Lucía','Torres','lucia.torres@mail.com','ESP'),
('36925814','Andrés','Flores','andres.flores@mail.com','PER');
go

Create or Alter Procedure usp_agrega_consumidor
@dnicont char(8),
@nomcont varchar(40),
@apecont varchar(40),
@emailcont varchar(250),
@idpais char(3),
@resultado varchar(150) = null OUTPUT
as
Begin
	if @dnicont is null or
	LEN(TRIM(@dnicont)) <> 8 or
	TRIM(@dnicont) Like '%[^0-9]%'
	begin
		set @resultado = 'El DNI debe contener 8 digitos y ser numerico'
		return
	end

	if (@nomcont is null or TRIM(@nomcont) = '') or
	(@apecont is null or TRIM(@apecont) = '')
	begin
		set @resultado = 'El Nombre o Apellido no deben estar Vacios'
		return
	end

	if @idpais is null or
	TRIM(@idpais) = '' or
	@idpais Like '%[^a-zA-ZáéíóúÁÉÍÓÚñÑ]%'
	begin
		set @resultado = 'El Pais solo debe contener Letras y no estar vacio'
		return
	end

	begin try
		insert into tb_Consumidor(dnicont,nomcont,apecont,emailcont,idpais) 
		values (@dnicont,@nomcont,@apecont,@emailcont,@idpais)

		set @resultado = 'Consumidor Registrado Correctamente'
	end try
	begin catch
		set @resultado = 'Error al Registrar: '+ ERROR_MESSAGE()
	end catch
end
go

Create or Alter Procedure usp_eliminar_consumidor
@dnicont char(8),
@resultado varchar(150) = null output
as
Begin
	if not exists (select 1 from tb_Consumidor where dnicont = @dnicont)
	begin
		set @resultado = 'El DNI no esta registrado'
		return
	end

	begin try
		Delete tb_Consumidor where dnicont = @dnicont
		set @resultado = 'El Consumidor se elimino correctamente'
	end try
	begin catch
		set @resultado = 'Error al Eliminiar: '+ ERROR_MESSAGE()
	end catch
end
go

Create or Alter Procedure usp_listar_consumidor
as
Begin
	select * from tb_Consumidor
end
go

Create or Alter Procedure usp_actualizar_consumidor
@dnicont char(8),
@nomcont varchar(40),
@apecont varchar(40),
@emailcont varchar(250),
@idpais char(3),
@resultado varchar(150) = null OUTPUT
as
Begin
	if @dnicont is null or
	LEN(TRIM(@dnicont)) <> 8 or
	TRIM(@dnicont) Like '%[^0-9]%'
	begin
		set @resultado = 'El DNI debe contener 8 digitos y ser numerico'
		return
	end

	if not exists (select 1 from tb_Consumidor where dnicont = @dnicont)
	begin
		set @resultado = 'El DNI no esta registrado'
		return
	end

	if (@nomcont is null or TRIM(@nomcont) = '') or
	(@apecont is null or TRIM(@apecont) = '')
	begin
		set @resultado = 'El Nombre o Apellido no deben estar Vacios'
		return
	end

	if @idpais is null or
	TRIM(@idpais) = '' or
	@idpais Like '%[^a-zA-ZáéíóúÁÉÍÓÚñÑ]%'
	begin
		set @resultado = 'El Pais solo debe contener Letras y no estar vacio'
		return
	end

	begin try
		update tb_Consumidor set
		nomcont = @nomcont, apecont = @apecont,
		emailcont = @emailcont, idpais = @idpais
		where dnicont = @dnicont

		set @resultado = 'Consumidor Actualizado Correctamente'
	end try
	begin catch
		set @resultado = 'Error al Actualizar: '+ ERROR_MESSAGE()
	end catch
end
go

Create or Alter Procedure usp_obtener_consumidor
@dnicont char(8)
as
Begin
	select * from tb_Consumidor
	where dnicont = @dnicont
end
go

