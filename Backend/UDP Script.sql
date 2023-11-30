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
	WHERE prod_Estado = 1	
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
		IF NOT EXISTS (SELECT prod_Descripcion FROM inve.tbProductos WHERE prod_Descripcion = @prod_Descripcion AND prod_Estado = 1)
			BEGIN
				INSERT INTO inve.tbProductos
				(prod_Descripcion, prod_Precio, usua_UsuarioCreacion, prod_FechaCreacion, usua_UsuarioModificacion, prod_FechaModificacion)
				VALUES
				(@prod_Descripcion,@prod_Precio,@usua_UsuarioCreacion,@prod_FechaCreacion,NULL,NULL)
				
				SELECT 1
			END
		ELSE IF EXISTS (SELECT prod_Descripcion FROM inve.tbProductos WHERE prod_Descripcion = @prod_Descripcion AND prod_Estado = 0)
			BEGIN
				UPDATE inve.tbProductos
				SET prod_Descripcion = @prod_Descripcion,
					prod_Precio = @prod_Precio,
					usua_UsuarioModificacion = @usua_UsuarioCreacion,
					prod_FechaModificacion = @prod_FechaCreacion,
					prod_Estado = 1
				WHERE prod_Descripcion = @prod_Descripcion
				SELECT 1
			END
		ELSE 
			SELECT 2
	END TRY
	BEGIN CATCH
		SELECT 'Resultado' + ERROR_MESSAGE()
	END CATCH
END

GO

/*Productos UPDATE*/
CREATE OR ALTER PROCEDURE inve.UDP_tbProductos_Update
@prod_Id INT,
@prod_Descripcion NVARCHAR(500),
@prod_Precio DECIMAL(18,2),
@usua_UsuarioModificacion INT,
@prod_FechaModificacion DATETIME
AS
BEGIN
	BEGIN TRY
		IF EXISTS (SELECT prod_Id FROM inve.tbProductos WHERE prod_Descripcion = @prod_Descripcion AND prod_Estado IN (0, 1) AND prod_Id != @prod_Id)
			BEGIN
				SELECT 2
			END	
		ELSE
			BEGIN
				UPDATE inve.tbProductos
				SET prod_Descripcion = @prod_Descripcion,
					prod_Precio = @prod_Precio,
					usua_UsuarioModificacion = @usua_UsuarioModificacion,
					prod_FechaModificacion = @prod_FechaModificacion,
					prod_Estado = 1
				WHERE prod_Id = @prod_Id 
				SELECT 1
			END
	END TRY
	BEGIN CATCH	
		SELECT 'Resultado: ' + ERROR_MESSAGE()
	END CATCH
END


GO
/*Productos Delete*/
CREATE OR ALTER PROCEDURE inve.UDP_tbProductos_Delete
@prod_Id INT
AS
BEGIN
	BEGIN TRY
		UPDATE inve.tbProductos
		SET prod_Estado = 0
		WHERE prod_Id = @prod_Id
		SELECT 1
	END TRY
	BEGIN CATCH
		SELECT 'Resultado' + ERROR_MESSAGE()
	END CATCH
END
GO
-------------------------------*Fin Productos*----------------------------------

-------------------------------*Lotes*----------------------------------

/*Lotes View*/	
CREATE VIEW inve.VW_tbLotes
AS
SELECT 
lote_Id, lote.prod_Id,prod_Descripcion,prod_Precio, 
lote_Cantidad,CAST(lote.lote_Cantidad * prod.prod_Precio AS DECIMAL(18,2)) AS lote_Costo, lote_FechaVencimiento, 
lote.usua_UsuarioCreacion,creador.usua_Usuario AS usua_UsuarioCrecion_Usuario, 
lote_FechaCreacion, lote.usua_UsuarioModificacion, modificador.usua_Usuario AS usua_UsuarioModificacion_Usuario,  
lote_FechaModificacion, lote_Estado,NULL AS Cantidad_Total,NULL AS Costo_Total
FROM inve.tbLotes lote INNER JOIN inve.tbProductos prod
ON lote.prod_Id = prod.prod_Id INNER JOIN acce.tbUsuarios creador
ON lote.usua_UsuarioCreacion = creador.usua_Id LEFT JOIN acce.tbUsuarios modificador
ON lote.usua_UsuarioModificacion = modificador.usua_Id

GO

/*Lotes Select*/
CREATE OR ALTER PROCEDURE inve.UDP_tbLotes_Select
AS
BEGIN
	SELECT  * FROM inve.VW_tbLotes WHERE lote_Estado = 1 AND lote_Cantidad > 0
END


GO

/*Lotes SelectPorProducto*/
CREATE OR AlTER PROCEDURE inve.UDP_tbLotes_SelectPorProducto
@prod_Id INT
AS
BEGIN
	BEGIN TRY
		SELECT 
			lote_Id, 
			prod_Id, 
			prod_Descripcion, 
			prod_Precio, 
			lote_Cantidad, 
			lote_FechaVencimiento, 
			usua_UsuarioCreacion, 
			usua_UsuarioCrecion_Usuario, 
			lote_FechaCreacion, 
			usua_UsuarioModificacion, 
			usua_UsuarioModificacion_Usuario, 
			lote_FechaModificacion, 
			lote_Estado,
			lote_Costo,
			(SELECT SUM(lote_Cantidad) FROM inve.VW_tbLotes WHERE prod_Id = @prod_Id) AS Cantidad_Total,
			(SELECT SUM(lote_Costo) FROM inve.VW_tbLotes WHERE prod_Id = @prod_Id) AS Costo_Total

		FROM 
			inve.VW_tbLotes 
		WHERE 
			prod_Id = @prod_Id AND lote_Cantidad > 0
ORDER BY 
	lote_FechaVencimiento
	END TRY
	BEGIN CATCH
		SELECT 'Resultado' + ERROR_MESSAGE()
	END CATCH
END

GO
-------------------------------*Fin Lotes*----------------------------------

-------------------------------*Sucursales*----------------------------------
/*Sucursales View*/
CREATE VIEW inve.VW_tbSucursales
AS
SELECT 
sucu_Id, sucu_Descripcion, 
sucu.usua_UsuarioCreacion,creador.usua_Usuario AS usua_UsuarioCreacion_Nombre, sucu_FechaCreacion, 
sucu.usua_UsuarioModificacion,modificador.usua_Usuario AS usua_UsuarioModificacion_Nombre, sucu_FechaModificacion, 
sucu_Estado
FROM inve.tbSucursales sucu INNER JOIN acce.tbUsuarios creador
ON sucu.usua_UsuarioCreacion = creador.usua_Id LEFT JOIN acce.tbUsuarios modificador
ON sucu.usua_UsuarioModificacion = modificador.usua_Id

GO

/*Sucursales Select*/
CREATE OR ALTER PROCEDURE inve.UDP_tbSucursales_Select
AS
BEGIN
	SELECT * FROM inve.VW_tbSucursales WHERE sucu_Estado = 1
END
-------------------------------*Fin Sucursales*----------------------------------
GO
-------------------------------*Salidas*----------------------------------
/*Salidas Insert*/
CREATE OR ALTER PROCEDURE inve.UDP_tbSalidas_Insert
@usua_Id INT,
@sucu_Id INT,
@sucu_SalidaEstado NVARCHAR(200),
@sali_FechaCreacion DATETIME
AS
BEGIN
	BEGIN TRY
		INSERT INTO inve.tbSalidas
		(usua_Id, sucu_Id, sucu_SalidaEstado, usua_UsuarioCreacion, sali_FechaCreacion, usua_UsuarioModificacion, saliFechaModificacion)
		VALUES(@usua_Id,@sucu_Id,@sucu_SalidaEstado,@usua_Id,@sali_FechaCreacion,NULL,NULL)
		SELECT SCOPE_IDENTITY()
	END TRY
	BEGIN CATCH
		SELECT 'Resultado' + ERROR_MESSAGE()
	END CATCH
END

GO

/*Salidas Update*/
CREATE OR ALTER PROCEDURE inve.UDP_tbSalidas_Update
@sali_Id INT,
@usua_UsuarioModificacion INT,
@sali_FechaModificacion DATETIME
AS
BEGIN
	BEGIN TRY
		UPDATE inve.tbSalidas
			SET sucu_SalidaEstado = 'Recibido',
				usua_UsuarioModificacion = @usua_UsuarioModificacion,
				saliFechaModificacion = @sali_FechaModificacion
		WHERE sali_Id = @sali_Id
	SELECT 1
	END TRY

	BEGIN CATCH
		SELECT 'Resultado: ' + ERROR_MESSAGE()
	END CATCH
END

-------------------------------*Fin Salidas*----------------------------------

GO
-------------------------------*Salidas Detalle*----------------------------------
CREATE OR ALTER PROCEDURE inve.UDP_tbSalidasDetetalles_Insert
@sali_Id INT,
@lote_Id INT,
@sade_Cantidad INT,
@usua_UsuarioCreacion INT,
@sade_FechaCreacion DATETIME
AS
BEGIN
	BEGIN TRY
		INSERT INTO inve.tbSalidasDetalles
		(sali_Id, lote_Id, sade_Cantidad, usua_UsuarioCreacion, sade_FechaCreacion, usua_UsuarioModificacion, sade_FechaModificacion)
		VALUES(@sali_Id,@lote_Id,@sade_Cantidad,@usua_UsuarioCreacion,@sade_FechaCreacion,NULL,NULL)

		UPDATE inve.tbLotes
		SET lote_Cantidad = lote_Cantidad - @sade_Cantidad
		WHERE lote_Id = @lote_Id
	SELECT 1
	END TRY
	BEGIN CATCH
		SELECT 'Resultado: ' + ERROR_MESSAGE()
	END CATCH
END
-------------------------------*Fin Salidas Detalle*----------------------------------
GO
-------------------Salidas View------------------------------------
CREATE VIEW inve.VW_tbSalidas
AS
SELECT 
    sali.sali_Id, 
    sali.sucu_Id,
    sali_FechaCreacion,
    sali.saliFechaModificacion,
    sali.sucu_SalidaEstado,
    sali.usua_UsuarioCreacion,
    creador.usua_Usuario AS usua_UsuarioCreacion_Nombre,
    sali.usua_UsuarioModificacion,
    modificador.usua_Usuario AS usua_UsuarioModificacion_Nombre,
    SUM(sade.sade_Cantidad) AS Unidades_Totales,
    SUM(prod.prod_Precio * sade.sade_Cantidad) AS costo_Total
FROM inve.tbSalidas sali 
    INNER JOIN inve.tbSalidasDetalles sade ON sali.sali_Id = sade.sali_Id 
    INNER JOIN inve.tbLotes lote ON sade.lote_Id = lote.lote_Id 
    INNER JOIN inve.tbProductos prod ON lote.prod_Id = prod.prod_Id 
    INNER JOIN acce.tbUsuarios creador ON sali.usua_UsuarioCreacion = creador.usua_Id 
    LEFT JOIN acce.tbUsuarios modificador ON sali.usua_UsuarioModificacion = modificador.usua_Id
GROUP BY 
    sali.sali_Id, 
    sali.sucu_Id,
    sali_FechaCreacion,
    sali.saliFechaModificacion,
    sali.sucu_SalidaEstado,
    sali.usua_UsuarioCreacion,
    creador.usua_Usuario,
    sali.usua_UsuarioModificacion,
    modificador.usua_Usuario;


GO

/*Salidas View Select*/
CREATE OR ALTER PROCEDURE inve.UDP_VW_tbSalidas_Select
AS
BEGIN
	SELECT * FROM inve.VW_tbSalidas
END

GO

/*Salidas View Filtrado*/
CREATE OR ALTER PROCEDURE inve.UDP_VW_tbSalidas_Select_Filtered (
    @sucu_Id INT = NULL,
    @fechaInicio DATETIME = NULL,
    @fechaFin DATETIME = NULL
)
AS
BEGIN
    SELECT 
        sali.sali_Id, 
        sali.sucu_Id,
        sali_FechaCreacion,
        sali.saliFechaModificacion,
        sali.sucu_SalidaEstado,
        sali.usua_UsuarioCreacion,
        creador.usua_Usuario AS usua_UsuarioCreacion_Nombre,
        sali.usua_UsuarioModificacion,
        modificador.usua_Usuario AS usua_UsuarioModificacion_Nombre,
        SUM(sade.sade_Cantidad) AS Unidades_Totales,
        SUM(prod.prod_Precio * sade.sade_Cantidad) AS costo_Total
    FROM inve.tbSalidas sali 
        INNER JOIN inve.tbSalidasDetalles sade ON sali.sali_Id = sade.sali_Id 
        INNER JOIN inve.tbLotes lote ON sade.lote_Id = lote.lote_Id 
        INNER JOIN inve.tbProductos prod ON lote.prod_Id = prod.prod_Id 
        INNER JOIN acce.tbUsuarios creador ON sali.usua_UsuarioCreacion = creador.usua_Id 
        LEFT JOIN acce.tbUsuarios modificador ON sali.usua_UsuarioModificacion = modificador.usua_Id
    WHERE
        (@sucu_Id IS NULL OR sali.sucu_Id = @sucu_Id)
        AND (@fechaInicio IS NULL OR sali.sali_FechaCreacion >= @fechaInicio)
        AND (@fechaFin IS NULL OR sali.sali_FechaCreacion <= @fechaFin)
    GROUP BY 
        sali.sali_Id, 
        sali.sucu_Id,
        sali_FechaCreacion,
        sali.saliFechaModificacion,
        sali.sucu_SalidaEstado,
        sali.usua_UsuarioCreacion,
        creador.usua_Usuario,
        sali.usua_UsuarioModificacion,
        modificador.usua_Usuario;
END;

GO
/*Verificar estado sucursal*/
CREATE OR ALTER PROCEDURE inve.UDP_VW_tbSalidas_Select_Sucursal_Estado 2
@sucu_Id INT
AS
BEGIN
    SELECT sucu_Id, SUM(costo_Total) AS costo_Total
    FROM inve.VW_tbSalidas 
    WHERE sucu_SalidaEstado = 'Enviada a sucursal' AND sucu_Id = @sucu_Id
    GROUP BY sucu_Id;
END


-------------------Fin Salidas View--------------------------------
