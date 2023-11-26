/**
=========================================================
* Soft UI Dashboard React - v4.0.1
=========================================================

* Product Page: https://www.creative-tim.com/product/soft-ui-dashboard-react
* Copyright 2023 Creative Tim (https://www.creative-tim.com)

Coded by www.creative-tim.com

 =========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*/

import { useState } from "react";

// react-router-dom components
import { Link } from "react-router-dom";

// @mui material components
import Switch from "@mui/material/Switch";

// Soft UI Dashboard React components
import SoftBox from "components/SoftBox";
import SoftTypography from "components/SoftTypography";
import SoftInput from "components/SoftInput";
import SoftButton from "components/SoftButton";

// Authentication layout components
import CoverLayout from "layouts/authentication/components/CoverLayout";

// Images
import curved9 from "assets/images/curved-images/curved-6.jpg";

//Material
import { FormControl } from "@mui/material";

// Validaciones 
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { Controller, useForm } from 'react-hook-form';

//Alertas
import { ToastError, ToastSuccessPersonalizado, ToastWarningCamposVacios, ToastWarningPersonalizado } from "../../../assets/Toast/Toast"

//Servicios
import LoginService from "../LoginService/LoginService";

//Navegacion
import { useNavigate } from "react-router-dom";

//Hooks
import { useEffect } from "react";
import routes from "routes";

//Custom Icons
import { Inventory2 } from "@mui/icons-material";
import { Category } from "@mui/icons-material";

function SignIn() {
  const [rememberMe, setRememberMe] = useState(true);

  useEffect(() => {
    localStorage.clear()
  }, [])

  const loginservice = LoginService()
  const Navegate = useNavigate()

  const handleSetRememberMe = () => setRememberMe(!rememberMe);

  const validacion = async () => {
    try {
      handleSubmit()
      trigger()
      if (isValid) {
        const response = await loginservice.login(modelo.username, modelo.password)
        if (response?.usua_Id > 0) {
          Navegate('/dashboard')
          ToastSuccessPersonalizado(`Exito. Bienvenido ${response.usua_Usuario}.`)
          localStorage.setItem('user_data', response);
        } else {
          ToastWarningPersonalizado('Advertencia. Usuario o Contraseña incorrecto.')
        }
        console.log(response)
      } else {
        ToastWarningCamposVacios()
      }
    } catch (error) {
      console.log('algo trono en validacion login', error)
    }
  }

  const defaultValues = {
    username: "",
    password: ""
  };

  const LoginSchema = yup.object().shape({
    username: yup.string().required(),
    password: yup.string().required(),
  });

  //Tab 1 useform
  const { handleSubmit, reset, control, formState, watch, setValue, trigger } = useForm({
    defaultValues,
    mode: "all",
    resolver: yupResolver(LoginSchema),
  });
  const { isValid, errors } = formState;
  const modelo = watch()

  return (
    <form onSubmit={handleSubmit((_data) => { })}>
      <CoverLayout
        title="InventoNext"
        description="La mejor solucion en Gestion de Inventario."
        image={curved9}
        top={15}
      >
        <SoftBox component="form" role="form">
          <SoftBox mb={2}>
            <SoftBox mb={1} ml={0.5}>
              <SoftTypography component="label" variant="caption" fontWeight="bold">
                Usuario
              </SoftTypography>
            </SoftBox>
            <Controller render={({ field }) => (
              <SoftInput {...field} error={!!errors.username} type="text" placeholder="Usuario" />
            )}
              control={control}
              name="username"
            />
          </SoftBox>
          <SoftBox mb={2}>
            <SoftBox mb={1} ml={0.5}>
              <SoftTypography component="label" variant="caption" fontWeight="bold">
                Contraseña
              </SoftTypography>
            </SoftBox>
            <Controller render={({ field }) => (
              <SoftInput {...field} error={!!errors.password} type="password" placeholder="Contraseña" />
            )}
              control={control}
              name="password"
            />
          </SoftBox>
          <SoftBox display="flex" alignItems="center">
            <Switch checked={rememberMe} onChange={handleSetRememberMe} />
            <SoftTypography
              variant="button"
              fontWeight="regular"
              onClick={handleSetRememberMe}
              sx={{ cursor: "pointer", userSelect: "none" }}
            >
              &nbsp;&nbsp;Recuerdame
            </SoftTypography>
          </SoftBox>
          <SoftBox mt={4} mb={1}>
            <SoftButton onClick={() => {
              validacion()
            }} variant="gradient" color="info" fullWidth>
              Iniciar Sesion
            </SoftButton>
          </SoftBox>
          <SoftBox mt={3} textAlign="center">
          </SoftBox>
        </SoftBox>
      </CoverLayout>
    </form>
  );
}

export default SignIn;
