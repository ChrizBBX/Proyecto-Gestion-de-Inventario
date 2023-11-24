CREATE DATABASE ProyectoGestionInventario_BD

GO
USE ProyectoGestionInventario_BD
GO

CREATE SCHEMA acce
GO
CREATE SCHEMA gral
GO
CREATE SCHEMA inve
GO

CREATE TABLE inve.tbProductos
(
 prod_Id INT IDENTITY(1,1),
 prod_Descripcion NVARCHAR(500),
 prov_Id INT,
 cate_Id INT,


 CONSTRAINT PK_inve_tbProductos_prod_Id PRIMARY KEY(prod_Id)
) 

INSERT INTO inve.tbProductos VALUES('Funciona',1,1)

GO

CREATE PROCEDURE inve.UDP_tbProductos_List
	AS
	BEGIN
		SELECT * FROM inve.tbProductos
	END