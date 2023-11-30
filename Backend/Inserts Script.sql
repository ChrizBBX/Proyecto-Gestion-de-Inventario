 /*Insert de Roles*/
 INSERT INTO acce.tbRoles
 (role_Descripcion, usua_UsuarioCreacion, role_FechaCreacion, usua_UsuarioModificacion, role_FechaModificacion)
 VALUES('Logistica', 1, GETDATE(), NULL, NULL)
 
 /*Insert de Pantallas*/
 INSERT INTO acce.tbPantallas
 (pant_Nombre, pant_Url, pant_Identificador, pant_Icono, pant_Componente, pant_PropiedadExtra, pant_PropiedadExtra_1, pant_PropiedadExtra_2, usua_UsuarioCreacion, pant_FechaCreacion, usua_UsuarioModificacion, pant_FechaModificacion)
 VALUES
  ('Lotes','/lotes','lotes','<Inventory2 />','<Lotes />',NULL,NULL,NULL,1,GETDATE(),NULL,NULL),
 ('Productos','/productos','productos','<Category />','<Productos />',NULL,NULL,NULL,1,GETDATE(),NULL,NULL),
 ('Salidas','/salidas','salidas',NULl,'<Salidas />',NULL,NULL,NULL,1,GETDATE(),NULL,NULL)

 /*Insert de Lotes*/
 INSERT INTO inve.tbLotes
 (prod_Id, lote_Cantidad, lote_FechaVencimiento, usua_UsuarioCreacion, lote_FechaCreacion, usua_UsuarioModificacion, lote_FechaModificacion)
 VALUES
    (3,200,'12-12-2023',1,GETDATE(),NULL,NULL),
 (24,50,'01-11-2024',1,GETDATE(),NULL,NULL),
  (24,600,'01-11-2025',1,GETDATE(),NULL,NULL),
    (24,100,'12-24-2023',1,GETDATE(),NULL,NULL)

 /*Insert de Productos*/
 INSERT INTO inve.tbProductos
 (prod_Descripcion, prod_Precio, usua_UsuarioCreacion, prod_FechaCreacion, usua_UsuarioModificacion, prod_FechaModificacion)
 VALUES
 ('Smart Watch',200,1,GETDATE(),NULL,NULL)

 /*Insert de Sucursales*/
 INSERT INTO inve.tbSucursales
 (sucu_Descripcion, usua_UsuarioCreacion, sucu_FechaCreacion, usua_UsuarioModificacion, sucu_FechaModificacion)
 VALUES
 ('Sucursal San Pedro Sula',1,GETDATE(),NULL,NULL),
 ('Sucursal Tegucigalpa',1,GETDATE(),NULL,NULL),
  ('Sucursal La Ceiba',1,GETDATE(),NULL,NULL)