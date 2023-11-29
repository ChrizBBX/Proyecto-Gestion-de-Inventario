import axios from 'axios';

//Alertas
import { ToastError } from 'assets/Toast/Toast';

//User Data
import { getUserData } from 'layouts/authentication/sign-in/userData';


function LotesService(){
    const API_URL = process.env.REACT_APP_API_URL

    async function get_Lotes() {
        try {
            const response = await axios.get(API_URL + `Lotes/Listar`)
            console.log(response.data.data)
            return response.data.data
        } catch (error) {
            console.log(error)
        }
    }
    
    async function get_Lotes_ByProducto(id){
        try{
            const response = await axios.get(API_URL + `Lotes/ListarPorProducto?Id=${id}`)
            return response.data.data
        }catch(error){
            console.log(error)
        }
    }

    return{
        get_Lotes,
        get_Lotes_ByProducto
    }
}

export default LotesService