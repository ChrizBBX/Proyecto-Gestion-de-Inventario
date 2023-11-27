import axios from 'axios';

//Alertas
import  {ToastError,ToastSuccessPersonalizado,ToastWarningCamposVacios,ToastWarningPersonalizado} from "../../../assets/Toast/Toast"
function LoginService () {
    const API_URL = process.env.REACT_APP_API_URL

    async function login(usuario,contrasenia){
        try{
            const response = await axios.post(API_URL + `Usuarios/Login?usuario=${usuario}&contrasenia=${contrasenia}`)
            return response.data
        }catch(error){
            ToastError()
            console.log(error)
        }
    }

    async function getPantallas(rol){
        try{
            const response = await axios.get(API_URL + `Pantallas/ListarPorRol?id=${rol}`)
            return response?.data?.data
        }catch(error){
            ToastError()
            console.log(error)
        }
    }

    return{
        login,
        getPantallas
    }
}

export default LoginService