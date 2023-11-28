//Layout
import DashboardLayout from "examples/LayoutContainers/DashboardLayout"
import DashboardNavbar from "examples/Navbars/DashboardNavbar"

//Datatable
import DataTable from "react-data-table-component"

//Hooks
import { useState, useEffect } from "react"

//Servicios
import SalidasService from "./SalidasService"

//Soft UI
import SoftInput from "components/SoftInput"
import SoftButton from "components/SoftButton"

//Material
import { Box, Grid, Card, CardHeader, Container, Typography, Dialog, DialogActions, DialogTitle, DialogContent, Button, TextField } from "@mui/material"
import { CardMedia } from "@mui/material"
import { Add, Edit, Visibility, Delete, Label } from "@mui/icons-material"
import styled from "styled-components"
import { grey } from "@mui/material/colors"

// Validaciones 
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { Controller, set, useForm } from 'react-hook-form';
import { ToastWarningCamposVacios } from "assets/Toast/Toast"
import { ToastSuccess } from "assets/Toast/Toast"
import { ToastWarningPersonalizado } from "assets/Toast/Toast"
import { ToastError } from "assets/Toast/Toast"
import { ToastSuccessPersonalizado } from "assets/Toast/Toast"
import LoginService from "layouts/authentication/LoginService/LoginService"

const SmallSoftButton = styled(SoftButton)`
  font-size: 10px;
  padding: 0px 8px 0px 8px; 
  margin-right: 5px;
`;

function Salidas() {
    const salidasservice = SalidasService()

    return (
        <DashboardLayout>
            <DashboardNavbar />
        <Card>
        <Container>
               <Typography pt={3} variant="h2">Salidas</Typography>
               <Grid pr={3} pt={3} pb={1} container spacing={2}>
                  <Grid item xs={5}>
                     <SoftButton onClick={() => {}} variant={'gradient'} size={'medium'} color={'info'}><Add />Añadir</SoftButton>
                  </Grid>
                  <Grid item xs={4}></Grid>
                  <Grid item xs={3}><SoftInput placeholder={'Buscar...'}></SoftInput></Grid>

               </Grid>

               <DataTable pagination={true} />
            </Container>

        </Card>
     </DashboardLayout>
    )
}

export default Salidas