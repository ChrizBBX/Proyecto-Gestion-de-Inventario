-------------------------------*Usuarios*----------------------------------

/*INSERT Usuarios*/
CREATE OR ALTER PROCEDURE acce.UDP_tbUsuarios_Insert 
(
@usua_Usuario NVARCHAR(500),
@usua_Contrasenia NVARCHAR(MAX),
@role_Id INT,
@usua_Admin INT,
@usua_UsuarioCreacion INT,
@usua_FechaCreacion DATETIME
)
AS
BEGIN
	BEGIN TRY
		DECLARE @password NVARCHAR(MAX) = (HASHBYTES('sha2_512',@usua_Contrasenia));

		INSERT INTO acce.tbUsuarios
		(usua_Usuario, usua_Contrasenia, role_Id, usua_Admin, usua_UsuarioCreacion, usua_FechaCreacion, usua_UsuarioModificacion, usua_FechaModificacion, usua_Estado)
		VALUES
		(@usua_Usuario, @password, @role_Id, @usua_Admin, @usua_UsuarioCreacion, @usua_FechaCreacion, NULL, NULL, 1)
		SELECT 1
	END TRY
	BEGIN CATCH
		SELECT 'Resultado' + ERROR_MESSAGE()
	END CATCH
END


GO

/*Login*/
CREATE OR ALTER PROCEDURE acce.UDP_Login 
(
@usua_Usuario NVARCHAR(200),
@usua_Contrasenia NVARCHAR(MAX)
)
AS
BEGIN
	DECLARE @password NVARCHAR(MAX) = (HASHBYTES('sha2_512',@usua_Contrasenia));
	IF EXISTS (SELECT * FROM acce.tbUsuarios WHERE usua_Usuario = @usua_Usuario AND usua_Contrasenia = @password)
		BEGIN
			SELECT * FROM acce.tbUsuarios WHERE usua_Usuario = @usua_Usuario AND usua_Contrasenia = @password
		END
	ELSE IF NOT EXISTS (SELECT * FROM acce.tbUsuarios WHERE usua_Usuario = @usua_Contrasenia AND usua_Contrasenia = @password)
		BEGIN
			SELECT 2
		END
	ELSE
	SELECT 'Resultado' + ERROR_MESSAGE() 
	END

GO
-------------------------------* Fin Usuarios*----------------------------------

-------------------------------*Pantallas*----------------------------------
/*Pantallas Listar*/
CREATE OR ALTER PROCEDURE acce.UDP_tbPantallas_Select
AS
BEGIN
	SELECT 
	pant_Id, pant_Nombre, pant_Url, pant_Identificador, pant_Icono, pant_Componente, pant_PropiedadExtra, pant_PropiedadExtra_1, pant_PropiedadExtra_2, usua_UsuarioCreacion, pant_FechaCreacion, usua_UsuarioModificacion, pant_FechaModificacion, pant_Estado
	FROM [acce].[tbPantallas]
END
-------------------------------*Fin Pantallas*----------------------------------

-------------------------------*RolesPorPantalla*----------------------------------
GO
/*Vista para Roles por pantalla*/
CREATE VIEW acce.VW_tbRolesPorPantalla
AS
SELECT
ropa_Id, ropa.role_Id,role.role_Descripcion, 
ropa.pant_Id,pant.pant_Nombre,
pant.pant_Url,pant.pant_Identificador,
pant.pant_Icono,pant.pant_Componente,
pant.pant_PropiedadExtra,pant.pant_PropiedadExtra_1,
pant.pant_PropiedadExtra_2,
ropa.usua_UsuarioCreacion, ropa_FechaCreacion, 
ropa.usua_UsuarioModificacion, 
ropa_FechaModificacion, ropa_Estado

FROM acce.tbRolesPorPantalla ropa 
INNER JOIN acce.tbRoles role
ON ropa.role_Id = role.role_Id 
INNER JOIN acce.tbPantallas pant
ON ropa.pant_Id = pant.pant_Id

/*Filtrar PantallasPorRoles*/
GO
CREATE OR ALTER PROCEDURE acce.UDP_tbRolesPorPantalla_Filtrado
@role_Id INT
AS
BEGIN
	BEGIN TRY
		IF @role_Id = 1
		BEGIN
			SELECT * FROM acce.tbPantallas
		END
		ELSE
		BEGIN
			SELECT * FROM acce.VW_tbRolesPorPantalla WHERE role_Id = @role_Id
		END
	END TRY
	BEGIN CATCH
		SELECT 'Resultado' + ERROR_MESSAGE()
	END CATCH
END
-------------------------------*Fin RolesPorPantalla*----------------------------------

-------------------------------*Productos*----------------------------------
GO
/*Productos SELECT*/
CREATE OR ALTER PROCEDURE inve.UDP_tbProductos_Select
AS
BEGIN
	SELECT 
	prod_Id, prod_Descripcion, prod_Precio, usua_UsuarioCreacion, prod_FechaCreacion, usua_UsuarioModificacion, prod_FechaModificacion, prod_Estado
	FROM inve.tbProductos
END

GO
/*Productos CREATE*/
CREATE OR ALTER PROCEDURE inve.UDP_tbProductos_Insert
@prod_Descripcion NVARCHAR(500),
@prod_Precio DECIMAL(18,2),
@usua_UsuarioCreacion INT,
@prod_FechaCreacion DATETIME
AS
BEGIN
	BEGIN TRY
		INSERT INTO inve.tbProductos
		(prod_Descripcion, prod_Precio, usua_UsuarioCreacion, prod_FechaCreacion, usua_UsuarioModificacion, prod_FechaModificacion)
		VALUES
		(@prod_Descripcion,@prod_Precio,@usua_UsuarioCreacion,@prod_FechaCreacion,NULL,NULL)
		
		SELECT 1
	END TRY
	BEGIN CATCH
		SELECT 'Resultado' + ERROR_MESSAGE()
	END CATCH
END
-------------------------------*Fin Productos*----------------------------------