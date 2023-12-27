
CREATE DATABASE ProyectoGestionInventarioCAAG

GO
USE ProyectoGestionInventarioCAAG
GO

CREATE TABLE Usuarios(
  usuarioId								INT IDENTITY(1,1),
  usuarioNombreUsuario					VARCHAR(100)	NOT NULL,
  usuarioContrasena						VARCHAR(MAX)	NOT NULL,
  perfilId								INT				NOT NULL,
  empleadoId							INT				NOT NULL,

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

  CONSTRAINT PK_Usuarios_usuarioId PRIMARY KEY (usuarioId)
)

GO
INSERT INTO usuarios(usuarioNombreUsuario,usuarioContrasena,perfilId,empleadoId,usuarioCreacion,FechaCreacion,usuarioModificacion,FechaModificacion,Activo)
VALUES('admin', '123',1,1,1,'10-03-2004',NULL,NULL,1);

GO
ALTER TABLE Usuarios
ADD CONSTRAINT FK_Usuarios_usuarioCreacion_Usuarios_usuarioId FOREIGN KEY(usuarioCreacion) REFERENCES Usuarios(usuarioId)
ALTER TABLE Usuarios
ADD CONSTRAINT FK_Usuarios_usuarioModificacion_Usuarios_usuarioId FOREIGN KEY(usuarioModificacion) REFERENCES Usuarios(usuarioId)

GO

CREATE TABLE Empleados
(
empleadoId INT IDENTITY(1,1),
empleadoNombre VARCHAR(200),
empleadoApellido VARCHAR(200),
empleadoIdentidad VARCHAR(13),
empleadoFechaNacimiento DATE,
empleadoSexo VARCHAR,
empleadoTelefono VARCHAR(14),

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,	
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

CONSTRAINT PK_Empleados_empleadoId PRIMARY KEY (empleadoId),
CONSTRAINT UQ_Empleados_empleadoIdentidad UNIQUE (empleadoIdentidad),
CONSTRAINT CK_Empleados_empleadoSexo CHECK(empleadoSexo IN('M', 'F')),
CONSTRAINT FK_Empleados_usuarioCreacion FOREIGN KEY (usuarioCreacion) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_Empleados_usuarioModificacion FOREIGN KEY (usuarioModificacion) REFERENCES Usuarios (usuarioId)
)

INSERT INTO Empleados VALUES('Christopher','Aguilar','0501200414817','10-03-2004','M','99122654',1,GETDATE(),NULL,NULL,1)

ALTER TABLE Usuarios
ADD CONSTRAINT FK_Usuarios_empleadoId_Empleados_empleadoId FOREIGN KEY (empleadoId) REFERENCES Empleados (empleadoId)

GO

CREATE TABLE Perfiles
(
perfilId INT IDENTITY (1,1),
perfilNombre VARCHAR(150) NOT NULL,

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

CONSTRAINT PK_Perfiles_perfilId PRIMARY KEY(perfilId),
CONSTRAINT UQ_Perfiles_perfilNombre UNIQUE(perfilNombre),
CONSTRAINT FK_Perfiles_usuarioCreacion_Usuarios_usuarioId FOREIGN KEY (usuarioCreacion) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_Perfiles_usuarioModificacion_Usuarios_usuarioId FOREIGN KEY (usuarioModificacion) REFERENCES Usuarios (usuarioId)
)

INSERT INTO Perfiles VALUES ('Perfil Prueba',1,GETDATE(),NULL,NULL,1)

ALTER TABLE Usuarios
ADD CONSTRAINT FK_Usuarios_perfilId_Perfiles_perfilId FOREIGN KEY (perfilId) REFERENCES Perfiles (perfilId)

GO

CREATE TABLE Sucursales
(
sucursalId INT IDENTITY(1,1),
sucursalNombre VARCHAR(150) NOT NULL,

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

CONSTRAINT PK_Sucursales_sucursalId PRIMARY KEY (sucursalId),
CONSTRAINT UQ_Sucursales_sucursalNombre UNIQUE (sucursalNombre),
CONSTRAINT FK_Sucursales_usuarioCreacion_Usuarios_usuarioId FOREIGN KEY (usuarioCreacion) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_Sucursales_usuarioModificacion_Usuarios_usuarioId FOREIGN KEY (usuarioModificacion) REFERENCES Usuarios (usuarioId)
)

GO

CREATE TABLE EstadosSalidas
(
estadoSalidaId INT IDENTITY (1,1),
estadoSalidaNombre VARCHAR (100) NOT NULL,

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

CONSTRAINT PK_EstadosSalidas_estadoSalidaId PRIMARY KEY (estadoSalidaId),
CONSTRAINT UQ_EstadosSalidas_estadoSalidaNombre UNIQUE (estadoSalidaNombre),
CONSTRAINT FK_EstadosSalidas_usuarioCreacion_Usuarios_usuarioId FOREIGN KEY (usuarioCreacion) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_EstadosSalidas_usuarioModificacion_Usuarios_usuarioId FOREIGN KEY (usuarioModificacion) REFERENCES Usuarios (usuarioId)
)

CREATE TABLE Permisos
(
permisoId INT IDENTITY(1,1),
permisoNombre VARCHAR(100),

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

CONSTRAINT PK_Permisos_permisoId PRIMARY KEY (permisoId),
CONSTRAINT FK_Permisos_usuarioCreacion_Usuarios_usuarioId FOREIGN KEY (usuarioCreacion) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_Permisos_usuarioModificacion_Usuarios_usuarioId FOREIGN KEY (usuarioModificacion) REFERENCES Usuarios (usuarioId)
)

GO

CREATE TABLE PefilesPorPermisos
(
perfilPorPermisoId INT,
perfilId INT NOT NULL,
permisoId INT NOT NULL,

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

CONSTRAINT PK_PerfilesPorPermisos_perfilPorPermisoId PRIMARY KEY (perfilPorPermisoId),
CONSTRAINT FK_PerfilesPorPermisos_perfilId_Perfiles_perfilId FOREIGN KEY (perfilId) REFERENCES Perfiles (perfilId),
CONSTRAINT FK_PerfilesPorPermisos_permisoId_Permisos_permisoId FOREIGN KEY (permisoId) REFERENCES Permisos (permisoId),
CONSTRAINT FK_PerfilesPorPermisos_usuarioCreacion_Usuarios_usuarioId FOREIGN KEY (usuarioCreacion) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_PerfilesPorPermisos_usuarioModificacion_Usuarios_usuarioId FOREIGN KEY (usuarioModificacion) REFERENCES Usuarios (usuarioId)
)

GO

CREATE TABLE Productos
(
productoId INT IDENTITY (1,1),
productoNombre VARCHAR(200) NOT NULL,

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

CONSTRAINT PK_Productos_productoId PRIMARY KEY (productoId),
CONSTRAINT FK_Productos_usuarioCreacion_Usuarios_usuarioId FOREIGN KEY (usuarioCreacion) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_Productos_usuarioModificacion_Usuarios_usuarioId FOREIGN KEY (usuarioModificacion) REFERENCES Usuarios (usuarioId),
)

CREATE TABLE Lotes
(
loteId INT IDENTITY(1,1),
productoId INT,
loteCantidadInicial INT,
loteCostoCantidad DECIMAL(18,2),
loteFechaVencimiento DATE,
loteCantidad INT,

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

CONSTRAINT PK_Lotes_loteId PRIMARY KEY (loteId),
CONSTRAINT FK_Lotes_productoId_Productos_productoId FOREIGN KEY (productoId) REFERENCES  Productos (productoId),
CONSTRAINT FK_Lotes_usurioCreacion_Usuarios_usuarioId FOREIGN KEY (usuarioCreacion) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_Lotes_usuarioModificacion_Usuarios_usuarioId FOREIGN KEY (usuarioModificacion) REFERENCES Usuarios (usuarioId)
)


GO

CREATE TABLE Salidas
(
salidaId INT IDENTITY (1,1),
sucursalId INT NOT NULL,
usuarioId INT NOT NULL,
salidaFecha DATETIME NOT NULL,
salidaFechaRecibido DATETIME NOT NULL,
salidaTotal DECIMAL(18,2) NOT NULL,
estadoId INT NOT NULL,

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

CONSTRAINT PK_Salidas_salidaId PRIMARY KEY (salidaId),
CONSTRAINT FK_Salidas_sucursalId_Sucursales_sucursalId FOREIGN KEY (sucursalId) REFERENCES Sucursales (sucursalId),
CONSTRAINT FK_Salidas_UsuarioId_Usuarios_usuarioId FOREIGN KEY (usuarioId) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_Salidas_usuarioCreacion_Usuarios_usuarioId FOREIGN KEY (usuarioCreacion) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_Salidas_usuarioModificacion_Usuarios_usuarioId FOREIGN KEY (usuarioModificacion) REFERENCES Usuarios (usuarioId)
)

CREATE TABLE SalidasDetalle
(
salidaDetalle INT IDENTITY (1,1),
salidaId INT NOT NULL,
loteId INT NOT NULL,
salidaDetalleCantidad INT,

usuarioCreacion INT NOT NULL,
FechaCreacion DATETIME NOT NULL,
usuarioModificacion INT,
FechaModificacion DATETIME,
Activo BIT DEFAULT 1

CONSTRAINT PK_SalidasDetalle_salidaDetalle PRIMARY KEY (salidaDetalle),
CONSTRAINT FK_SalidasDetalle_salidaId_Salidas_salidaId FOREIGN KEY (salidaId) REFERENCES Salidas (salidaId),
CONSTRAINT FK_SalidasDetalle_loteId_Lotes_loteId FOREIGN KEY (loteId) REFERENCES Lotes (loteId),
CONSTRAINT FK_salidasDetalle_usuarioCreacion_Usuarios_usuarioId FOREIGN KEY (usuarioCreacion) REFERENCES Usuarios (usuarioId),
CONSTRAINT FK_salidasDetalle_usuarioModificacion_Usuarios_usuarioId FOREIGN KEY (usuarioModificacion) REFERENCES Usuarios (usuarioId)
)