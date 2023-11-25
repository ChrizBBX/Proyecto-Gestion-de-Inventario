import { func } from "prop-types";
import { toast } from "react-toastify"

export const ToastSuccess = () => {
    toast.success('Exito. Registro agregado exitosamente.', {
        autoClose: 1500,
        style: {
            fontSize: '14px'
        },
        closeOnClick: true,
    });
}

export const ToastSuccessPersonalizado = (texto) => {
    toast.success(texto, {
        autoClose: 1500,
        style: {
            fontSize: '14px'
        },
        closeOnClick: true,
    });
}

export const ToastError = () => {
    toast.error('Error. Ha ocurrido un error.', {
        autoClose: 1500,
        style: {
            fontSize: '14px'
        },
        closeOnClick: true,
    });
}

export const ToastWarningCamposVacios = () => {
    toast.warn('Advertencia. Hay campos vacios.', {
        autoClose: 1500,
        style: {
            fontSize: '14px'
        },
        closeOnClick: true,
    });
}

export const ToastWarningPersonalizado = (texto) => {
    toast.warn(texto, {
        autoClose: 1500,
        style: {
            fontSize: '14px'
        },
        closeOnClick: true,
    });
}




