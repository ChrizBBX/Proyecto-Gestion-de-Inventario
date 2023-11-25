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

----------------------*Inicio Tablas de Esquema de Acceso*--------------------------------

CREATE TABLE acce.tbUsuarios
(
usua_Id INT IDENTITY(1,1),
usua_Usuario NVARCHAR(500) NOT NULL,
usua_Contrasenia NVARCHAR(MAX) NOT NULL,
role_Id INT NOT NULL,
usua_Admin BIT DEFAULT 0 NOT NULL,

usua_UsuarioCreacion INT NOT NULL,
usua_FechaCreacion DATETIME NOT NULL,
usua_UsuarioModificacion INT,
usua_FechaModificacion DATETIME,
usua_Estado BIT DEFAULT 1 NOT NULL

CONSTRAINT PK_acce_tbUsuarios_usua_Id PRIMARY KEY(usua_Id), 
CONSTRAINT UQ_acce_tbUsuarios_usua_Usuario UNIQUE(usua_Usuario),
);

/*Se Inserta un usuario para poder hacer constraint de auditoria usuarios con usuarios*/
INSERT INTO acce.tbUsuarios 
(usua_Usuario, usua_Contrasenia, 
role_Id, usua_UsuarioCreacion, 
usua_FechaCreacion, usua_UsuarioModificacion, 
usua_FechaModificacion)
VALUES('admin','123',
1,1,GETDATE(),
NULL,NULL)

GO

ALTER TABLE acce.tbUsuarios
ADD CONSTRAINT FK_acce_tbUsuarios_usua_UsuarioCreacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioCreacion) REFERENCES acce.tbUsuarios (usua_Id)
ALTER TABLE acce.tbUsuarios
ADD CONSTRAINT FK_acce_tbUsuarios_usua_UsuarioModificacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioModificacion) REFERENCES acce.tbUsuarios (usua_Id)

GO

CREATE TABLE acce.tbRoles
(
role_Id INT IDENTITY(1,1),
role_Descripcion NVARCHAR(500) NOT NULL,
usua_UsuarioCreacion INT NOT NULL,

role_FechaCreacion DATETIME NOT NULL,
usua_UsuarioModificacion INT,
role_FechaModificacion DATETIME,
role_Estado BIT DEFAULT 1 

CONSTRAINT PK_acce_tbRoles_role_Id PRIMARY KEY (role_Id),
CONSTRAINT UQ_acce_tbRoles_role_Descripcion UNIQUE(role_Descripcion),
CONSTRAINT FK_acce_tbRoles_usua_UsuarioCreacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioCreacion) REFERENCES acce.tbUsuarios (usua_Id),
CONSTRAINT FK_acce_tbRoles_usua_UsuarioModificacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioModificacion)  REFERENCES acce.tbUsuarios (usua_Id)
)

GO

CREATE TABLE acce.tbPantallas
(
pant_Id INT IDENTITY(1,1),
pant_Nombre NVARCHAR(250),
pant_Url NVARCHAR(250),
pant_Identificador NVARCHAR(MAX),
pant_Icono NVARCHAR(250),
pant_Categoria NVARCHAR(300),
pant_Esquema NVARCHAR(100),

usua_UsuarioCreacion INT NOT NULL,
pant_FechaCreacion DATETIME NOT NULL,
usua_UsuarioModificacion INT,
pant_FechaModificacion DATETIME,
pant_Estado BIT DEFAULT 1 NOT NULL

CONSTRAINT PK_acce_tbPantllas_pant_Id PRIMARY KEY (pant_Id),
CONSTRAINT FK_acce_tbPantallas_usua_UsuarioCreacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioCreacion) REFERENCES acce.tbUsuarios (usua_Id),
CONSTRAINT FK_acce_tbPantallas_usua_UsuarioModificacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioModificacion) REFERENCES acce.tbUsuarios (usua_Id)
);

GO

CREATE TABLE acce.tbRolesPorPantalla
(
ropa_Id INT IDENTITY(1,1),
role_Id INT NOT NULL,
pant_Id INT NOT NULL,

usua_UsuarioCreacion INT NOT NULL,
ropa_FechaCreacion DATETIME NOT NULL,
usua_UsuarioModificacion INT,
ropa_FechaModificacion DATETIME,
ropa_Estado BIT DEFAULT 1

CONSTRAINT PK_acce_tbRolesPorPantalla_ropa_Id PRIMARY KEY (ropa_Id),
CONSTRAINT FK_acce_tbRolesPorPantalla_usua_UsuarioCreacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioCreacion) REFERENCES acce.tbUsuarios (usua_Id),
CONSTRAINT FK_acce_tbRolesPorPantalla_usua_UsuarioModificacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioModificacion) REFERENCES acce.tbUsuarios (usua_Id)
)
GO
----------------------*Fin Tablas de Esquema de Acceso*--------------------------------

----------------------*Inicio Tablas de Esquema General*--------------------------------



----------------------*Fin Tablas de Esquema General*--------------------------------

----------------------*Inicio Tablas de Esquema de Inventario*--------------------------------

/*Tabla Sucursales*/
CREATE TABLE inve.tbSucursales
(
sucu_Id INT IDENTITY(1,1),
sucu_Descripcion NVARCHAR(500) NOT NULL,

usua_UsuarioCreacion INT NOT NULL,
sucu_FechaCreacion DATETIME NOT NULL,
usua_UsuarioModificacion INT,
sucu_FechaModificacion DATETIME,
sucu_Estado BIT DEFAULT 1

CONSTRAINT PK_inve_tbSucursales_sucu_Id PRIMARY KEY (sucu_Id),
CONSTRAINT FK_inve_tbSucursales_usua_UsuarioCreacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioCreacion) REFERENCES acce.tbUsuarios(usua_Id),
CONSTRAINT FK_inve_tbSucursales_usua_UsuarioModificacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioModificacion) REFERENCES acce.tbUsuarios(usua_Id),
CONSTRAINT UQ_inve_tbSucursales_sucu_Descripcion UNIQUE(sucu_Descripcion)
)

GO

/*Tabla Productos*/
CREATE TABLE inve.tbProductos
(
prod_Id INT IDENTITY(1,1),
prod_Descripcion NVARCHAR(500) NOT NULL,
prod_Precio DECIMAL(18,2) NOT NULL,

usua_UsuarioCreacion INT NOT NULL,
prod_FechaCreacion DATETIME NOT NULL,
usua_UsuarioModificacion INT,
prod_FechaModificacion DATETIME,
prod_Estado BIT DEFAULT 1
 
CONSTRAINT PK_inve_tbProductos_prod_Id PRIMARY KEY(prod_Id),
CONSTRAINT FK_inve_tbProductos_usua_UsuarioCreacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioCreacion) REFERENCES acce.tbUsuarios (usua_Id),
CONSTRAINT FK_inve_tbProductos_usua_UsuarioModificacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioModificacion) REFERENCES acce.tbUsuarios (usua_Id),
CONSTRAINT UQ_inve_tbProductos_prod_Descripcion UNIQUE(prod_Descripcion)
) 

GO

/*Tabla Lotes*/
CREATE TABLE inve.tbLotes
(
lote_Id INT IDENTITY(1,1),
prod_Id INT NOT NULL,
lote_Cantidad INT NOT NULL,
lote_FechaVencimiento DATETIME NOT NULL,

usua_UsuarioCreacion INT NOT NULL,
lote_FechaCreacion DATETIME NOT NULL,
usua_UsuarioModificacion INT,
lote_FechaModificacion DATETIME,
lote_Estado BIT DEFAULT 1

CONSTRAINT PK_inve_tbLotes_lote_Id PRIMARY KEY (lote_Id),
CONSTRAINT FK_inve_tbProductos_prod_Id_inve_tbLotes_prod_Id FOREIGN KEY(prod_Id) REFERENCES inve.tbProductos(prod_Id),
CONSTRAINT FK_inve_tbLotes_usua_UsuarioCreacion_acce_tbUsuarios_usua_Id FOREIGN KEY(usua_UsuarioCreacion) REFERENCES acce.tbUsuarios (usua_Id),
CONSTRAINT FK_inve_tbLotes_usua_UsuarioModificacion_acce_tbUsuarios_usua_Id FOREIGN KEY(usua_UsuarioModificacion) REFERENCES acce.tbUsuarios (usua_Id)
)

GO

/*Tabla Salidas*/
CREATE TABLE inve.tbSalidas
(
sali_Id INT IDENTITY(1,1),
usua_Id INT NOT NULL,
sucu_Id INT NOT NULL,
sucu_SalidaEstado NVARCHAR(200),
 
usua_UsuarioCreacion INT NOT NULL,
sali_FechaCreacion DATETIME NOT NULL,
usua_UsuarioModificacion INT,
saliFechaModificacion DATETIME

CONSTRAINT PK_inve_tbSalidas_sali_Id PRIMARY KEY (sali_Id),
CONSTRAINT FK_inve_tbSalidas_usua_Id_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_Id) REFERENCES acce.tbUsuarios(usua_Id),
CONSTRAINT FK_inve_tbSalidas_sucu_Id_inve_tbSucursales_sucu_Id FOREIGN KEY (sucu_Id) REFERENCES inve.tbSucursales(sucu_Id),
CONSTRAINT FK_inve_tbSalidas_usua_UsuarioCreacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioCreacion) REFERENCES acce.tbUsuarios(usua_Id),
CONSTRAINT FK_inve_tbSalidas_usua_UsuarioModificacion_acce_tbUsuarios_usua_Id FOREIGN KEY (usua_UsuarioModificacion) REFERENCES acce.tbUsuarios(usua_Id)
)

/*Tabla SalidasDetalles*/
CREATE TABLE inve.tbSalidasDetalles
(
sade_Id INT IDENTITY(1,1),
sali_Id INT NOT NULL,
lote_Id INT NOT NULL,
sade_Cantidad INT,

usua_UsuarioCreacion INT NOT NULL,
sade_FechaCreacion DATETIME NOT NULL,
usua_UsuarioModificacion INT,
sade_FechaModificacion DATETIME

CONSTRAINT PK_inve_tbSalidasDetalles_sade_Id PRIMARY KEY(sade_Id),
CONSTRAINT FK_inve_tbSalidasDetalles_sali_Id_inve_tbSalidas_sali_Id FOREIGN KEY (sali_Id) REFERENCES inve.tbSalidas(sali_Id),
CONSTRAINT FK_inve_tbSalidasDetalles_lote_Id_inve_tbLotes_lote_Id FOREIGN KEY (lote_Id) REFERENCES inve.tbLotes(lote_Id),
CONSTRAINT FK_inve_tbSalidasDetalles_usua_UsuarioCreacion_acce_tbUsuarios_usua_Id FOREIGN KEY(usua_UsuarioCreacion) REFERENCES acce.tbUsuarios(usua_Id), 
CONSTRAINT FK_inve_tbSalidasDetalles_usua_UsuarioModificacion_acce_tbUsuarios_usua_Id FOREIGN KEY(usua_UsuarioModificacion) REFERENCES acce.tbUsuarios(usua_Id)
)
----------------------*Fin Tablas de Esquema de Inventario*--------------------------------