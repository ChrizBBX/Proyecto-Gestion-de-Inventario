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

    async function get_Salidas(){
        try{
            const response = await axios.get(API_URL + `SalidasView/Listar`)
            console.log('set Salidas', response.data)
            return response?.data
        }catch(error){
            console.log(error)
        }
    }

    async function insert_Salidas(modelo){
        const usuario = JSON.parse(localStorage.getItem('user_data'))
        try{
            let formData = {
                usua_UsuarioCreacion: usuario.usua_Id,
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

    async function update_Salidas(Id){
        const usuario = JSON.parse(localStorage.getItem('user_data'))
        try{
            let formData = {
                usua_UsuarioModificacion: usuario.usua_Id,
                sali_Id: Id,
                saliFechaModificacion: new Date(),
                sucu_SalidaEstado: 'Nada Porque en la base se actualiza solo'
            }
            const response = await axios.post(API_URL + `Salidas/Actualizar`,formData)
            console.log('response', response)
            return response?.data?.data?.messageStatus
        }catch(error){
            console.log(error)
        }
    }

    async function insert_SalidasDetalles(sali_Id,lote_Id,sade_Cantidad){
        const usuario = JSON.parse(localStorage.getItem('user_data'))
        try{
            let formData = {
                usua_UsuarioCreacion: usuario.usua_Id,
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

    async function filtered(inicio,fin,sucu){
        const usuario = JSON.parse(localStorage.getItem('user_data'))
        try{
            let sucursal = 0
            let f1 = 0
            let f2 = 0

            if(sucu != null){
                sucursal = sucu
            }

            if(f1 != null){
                f1 = inicio
            }

            if(f2 != null){
                f2 = fin
            }

            const response = await axios.get(API_URL + `SalidasView/Listar_Filtrado?sucu=${sucursal}&inicio=${f1}&fin=${f2}`)
            console.log('response',response)
            console.log('response bien', response.data.data)
            return response?.data?.data
        }catch(error){
            console.log(error)
        }
    }

    async function sucursal_Status(sucu){
        const usuario = JSON.parse(localStorage.getItem('user_data'))
        try{
  
            const response = await axios.get(API_URL + `SalidasView/Sucursal_Status?Id=${sucu}`)
            console.log('response',response)
            console.log('response bien', response.data.data)
            return response?.data?.data
        }catch(error){
            console.log(error)
        }
    }

    return{
        get_Sucursales,
        insert_Salidas,
        update_Salidas,
        insert_SalidasDetalles,
        get_Salidas,
        filtered,
        sucursal_Status
    }
}

export default SalidasService