import axios from 'axios';

//Alertas
import { ToastError } from 'assets/Toast/Toast';

//User Data
import { getUserData } from 'layouts/authentication/sign-in/userData';

function ProductosService() {
    const API_URL = process.env.REACT_APP_API_URL

    const formatFechaHora = (date) => {
        const year = date.getFullYear();
        const mes = String(date.getMonth() + 1).padStart(2, '0');
        const dia = String(date.getDate()).padStart(2, '0');
        const hora = String(date.getHours()).padStart(2, '0');
        const minutos = String(date.getMinutes()).padStart(2, '0');
        const segundos = String(date.getSeconds()).padStart(2, '0');

        return `${year}-${mes}-${dia}T${hora}:${minutos}:${segundos}`;
    };

    async function get_Productos() {
        try {
            const response = await axios.get(API_URL + `Productos/Listar`)
            return response.data
        } catch (error) {
            ToastError()
            console.log(error)
        }
    }

    async function insert_Productos(modelo) {
        try {
            const user_data = await getUserData()
            let formData = {
                prod_Descripcion: modelo?.Nombre,
                prod_Precio: modelo?.Precio,
                usua_UsuarioCreacion: user_data?.usua_Id,
                prod_FechaCreacion: new Date()
            }
            console.log('data a ser enviada',user_data)
            const response = await axios.post(API_URL + "Productos/Insertar",formData)
            return response
        } catch (error) {
            ToastError()
            console.log(error)
        }
    }

    async function update_Productos(modelo) {
        try {
            const user_data = await getUserData()
            let formData = {
                prod_Id: modelo?.Id,
                prod_Descripcion: modelo?.Nombre,
                prod_Precio: modelo?.Precio,
                usua_UsuarioModificacion: user_data?.usua_Id,
                prod_FechaModificacion: new Date()
            }
            console.log('data a ser enviada',formData)
            const response = await axios.post(API_URL + "Productos/Actualizar",formData)
            return response
        } catch (error) {
            ToastError()
            console.log(error)
        }
    }

    async function delete_Productos(modelo) {
        try {
            console.log(modelo)
            const user_data = await getUserData()
            let formData = {
                prod_Id: modelo?.Id,
                prod_Descripcion: "none",
                prod_Precio: "10",
                usua_UsuarioModificacion: user_data?.usua_Id,
                prod_FechaModificacion: new Date()
            }
            console.log('data a ser enviada',formData)
            const response = await axios.post(API_URL + "Productos/Eliminar",formData)
            return response
        } catch (error) {
            ToastError()
            console.log(error)
        }
    }

    return {
        get_Productos,
        insert_Productos,
        update_Productos,
        delete_Productos
    }
}

export default ProductosService