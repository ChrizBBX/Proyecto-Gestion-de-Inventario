import axios from 'axios';

//Alertas
import { ToastError } from 'assets/Toast/Toast';

//User Data
import { getUserData } from 'layouts/authentication/sign-in/userData';

function SalidasService(){
    const API_URL = process.env.REACT_APP_API_URL

    async function get_Sucursales(){
        try{
            const response = await axios.get(API_URL + `Sucursales/Listar`)
            return(response.data.data)
        }catch(error){
            console.log(error)
        }
    }

    async function insert_Salidas(modelo){
        const user_data = await getUserData()
        try{
            let formData = {
                usua_UsuarioCreacion: 1,
                sucu_Id: modelo?.sucu_Id,
                sucu_SalidaEstado: "Enviada a sucursal",
                sali_FechaCreacion: new Date()
            }
            const response = await axios.post(API_URL + `Salidas/Insertar`,formData)
            console.log('response', response)
            return response?.data?.data?.messageStatus
        }catch(error){
            console.log(error)
        }
    }

    async function insert_SalidasDetalles(sali_Id,lote_Id,sade_Cantidad){
        const user_data = await getUserData()
        try{
            let formData = {
                usua_UsuarioCreacion: 1,
                sali_Id: sali_Id,
                sade_Cantidad: sade_Cantidad,
                lote_Id: lote_Id,
                sade_FechaCreacion: new Date()
            }
            const response = await axios.post(API_URL + `SalidasDetalles/Insertar`,formData)
            console.log('response', response)
            return response.data.data.messageStatus
        }catch(error){
            console.log(error)
        }
    }

    return{
        get_Sucursales,
        insert_Salidas,
        insert_SalidasDetalles
    }
}

export default SalidasService