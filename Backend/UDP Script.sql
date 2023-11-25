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