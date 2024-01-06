namespace ProyectoGestionInventarioCAAG.Utility
{
    public class OutputMessage
    {
        public static string Fault = "Ha ocurrido un error";
        public static string FaultCreatorUser = "El usuario Creador no existe";
        public static string FaultEditorUser = "El usuario Modificacion no existe";
        public static string FaultUserNotExists = "El usuario seleccionado no existe";

        #region Generales
        public static string FaultUserCreationNotExists = "El Usuario creacion no existe";
        #endregion

        #region Empleado
        public static string SuccessInsertEmpleado = "El empleado se a Agregado exitosamente";
        public static string FaultInsertEmpleado = "La entidad no es valida o hay campos vacios";
        public static string FaultEmpleadoNotExists = "El empleado seleccionado no existe";
        public static string FaultEmpleadoExists = "Ya existe un empleado con ese numero de Identidad";
        #endregion

        #region Usuarios
        public static string SuccessInsertUser = "El usuario se ha agregado exitosamente";
        public static string SuccessEditUser = "El usuario se ha editado exitosamente";
        public static string SuccessEnableUser = "El usuario se ha activado exitosamente";
        public static string SuccessDisableUser = "El usuario se ha desactivado exitosamente";
        public static string FaultUserLogin = "Usuario o contraseña incorrecto";
        public static string FaultUserEntity = "Entidad de usuario no valida o hay campos vacios";
        public static string FaultUserExists = "Ya existe un nombre de usuario igual al asignado";
        public static string FaultuserNotExists = "El usuario no existe";
        public static string FaultListUser = "Error al listar los usuarios";
        public static string FaultInsertUser = "Error al agregar el usuario";
        public static string FaultEditUser = "Error al editar el usuario";
        public static string FaultEnableUser = "Error al activar el usuario";
        public static string FaultDisableUser = "Error al desactivar el usuario";
        #endregion

        #region Perfiles
        public static string FaultPerfilNotExists = "El perfil seleccionado no existe";
        #endregion

        #region Lotes
        public static string SuccessInsertLote = "El lote se ha agregado exitosamente";
        public static string FaultLotesProductNotStock = "No hay lotes disponibles para el producto seleccionado";
        #endregion

        #region Productos
        public static string FaultProductoNotExists = "El producto seleccionado no existe";
        #endregion

        #region Salidas
        public static string SuccessInsertSalida = "La salida de inventario de ha realizado exitosamente";
        public static string SuccessReceiveSalida = "La sucursal ha sido recibida exitosamente";
        public static string FaultInsertSalida = "Ha ocurrido un error al realizar la salida";
        public static string FaultSalidaEntity = "La entidad no es valida o hay campos vacios";
        public static string FaultNotAdmin = "El usuario no tiene permiso de realizar una salida";
        public static string FaultSucursalMaxPendingValue = "La sucursal ha excedido el valor maximo de salidas en estado pendiendte";
        public static string FaultSalidaNotExists = "La salida seleccionada no existe";
        public static string FaultSalidaAlreadyReceived = "La salida seleccionada ya esta recibida";
        public static string FaultNotEnoughStock = "No hay stock suficiente para la cantidad de producto solicitada";
        #endregion

        #region Sucursales
        public static string FaultSucursalNotExists = "La sucursal Seleccionada no existe";
        #endregion

        #region Estados Salida
        public static string FaultEstadoSalidaNotExists = "El estado de salida seleccionado no existe";
        #endregion
    }
}
