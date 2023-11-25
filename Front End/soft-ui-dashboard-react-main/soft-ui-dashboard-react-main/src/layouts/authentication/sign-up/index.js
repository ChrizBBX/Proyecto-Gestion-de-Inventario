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
import Card from "@mui/material/Card";
import Checkbox from "@mui/material/Checkbox";

// Soft UI Dashboard React components
import SoftBox from "components/SoftBox";
import SoftTypography from "components/SoftTypography";
import SoftInput from "components/SoftInput";
import SoftButton from "components/SoftButton";

// Authentication layout components
import BasicLayout from "layouts/authentication/components/BasicLayout";
import Socials from "layouts/authentication/components/Socials";
import Separator from "layouts/authentication/components/Separator";

// Images
import curved6 from "assets/images/curved-images/curved14.jpg";

// Validaciones 
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { Controller, useForm } from 'react-hook-form';

//Alertas
import  {ToastError,ToastSuccessPersonalizado,ToastWarningCamposVacios,ToastWarningPersonalizado} from "../../../assets/Toast/Toast"

//Servicios
import LoginService from "../LoginService/LoginService";

//Navegacion
import { useNavigate } from "react-router-dom";

//Hooks
import { useEffect } from "react";



function SignUp() {
  useEffect(() => {
    localStorage.clear()
  },[])

  const loginservice = LoginService()
  const Navegate = useNavigate()
  const [agreement, setAgremment] = useState(true);

  const handleSetAgremment = () => setAgremment(!agreement);

  const validacion = async () => {
    if (isValid) {
     const response = await loginservice.login(modelo.username,modelo.password)
     if(response?.usua_Id > 0){
      Navegate('/dashboard')
      ToastSuccessPersonalizado('Exito. Bienvenido.')
      localStorage.setItem('user_data', response);
     }else{
      ToastWarningPersonalizado('Advertencia. Usuario o Contraseña incorrecto.')
     }
     console.log(response)
    } else {
      ToastWarningCamposVacios()
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
  const { handleSubmit, reset, control, formState, watch, setValue } = useForm({
    defaultValues,
    mode: "all",
    resolver: yupResolver(LoginSchema),
  });
  const { isValid, errors } = formState;
  const modelo = watch()

  return (
<div>
<BasicLayout
      title="InventoNext"
      description="La mejor solucion en Administracion de Inventario."
      image={curved6}
    >
      <form onSubmit={handleSubmit((_data) => { })}>
        <Card>
          <SoftBox p={3} mb={1} textAlign="center">
            <SoftTypography variant="h5" fontWeight="medium">
              Iniciar Sesion
            </SoftTypography>
          </SoftBox>
          <SoftBox mb={2}>
          </SoftBox>
          <SoftBox pt={2} pb={3} px={3}>
            <SoftBox component="form" role="form">
              <SoftBox mb={2}>
                <Controller render={({ field }) => (
                  <SoftInput {...field} error={!!errors.username} type="text" placeholder="Usuario" />
                )}
                  control={control}
                  name="username"
                />
              </SoftBox>
              <SoftBox mb={2}>
                <Controller render={({ field }) => (
                  <SoftInput {...field} error={!!errors.password} type="password" placeholder="Contraseña" />
                )}
                  control={control}
                  name="password"
                />
              </SoftBox>
              <SoftBox display="flex" alignItems="center">
              </SoftBox>
              <SoftBox mt={4} mb={1}>
                <SoftButton onClick={() => {
                  validacion()
                }} variant="gradient" color="dark" fullWidth>
                  sign up
                </SoftButton>
              </SoftBox>
              <SoftBox mt={3} textAlign="center">
              </SoftBox>
            </SoftBox>
          </SoftBox>
        </Card>
      </form>
    </BasicLayout>
</div>
  );
}

export default SignUp;
