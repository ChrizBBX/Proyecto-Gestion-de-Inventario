//Default Imports
import React from "react"

//Layout
import DashboardLayout from "examples/LayoutContainers/DashboardLayout"
import DashboardNavbar from "examples/Navbars/DashboardNavbar"

//Datatable
import DataTable from "react-data-table-component"

//Hooks
import { useState, useEffect } from "react"

//Servicios
import ProductosService from "./ProductosService"

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
import { Controller, useForm } from 'react-hook-form';
import { ToastWarningCamposVacios } from "assets/Toast/Toast"
import { ToastSuccess } from "assets/Toast/Toast"

//Alertas


const SmallSoftButton = styled(SoftButton)`
  font-size: 10px;
  padding: 0px 8px 0px 8px; 
  margin-right: 5px;
`;



function Productos() {
   const [pending, setPending] = useState(true)
   const [open, setOpen] = useState(false);
   const productosservice = ProductosService()

   const handleOpen = () => {
      setOpen(true);
   };

   const handleClose = () => {
      setOpen(false);
      reset()
   };

   const columnas = [
      {
         key: 'prod_Id',
         name: 'ID',
         selector: row => row.prod_Id
      },
      {
         key: 'prod_Descripcion',
         name: 'Producto',
         selector: row => row.prod_Descripcion
      },
      {
         key: 'prod_Precio',
         name: 'Precio',
         selector: row => row.prod_Precio
      },
      {
         key: 'acciones',
         name: 'Acciones',
         cell: (row) => (
            <Box>
               <SmallSoftButton
                  variant={'gradient'}
                  color={'primary'}
                  onClick={() => { console.log(row) }}
               >
                  <Edit />
               </SmallSoftButton>
               <SmallSoftButton
                  variant={'gradient'}
                  color={'secondary'}
                  onClick={() => { console.log(row) }}
               >
                  <Visibility />
               </SmallSoftButton>
               <SmallSoftButton
                  variant={'gradient'}
                  color={'error'}
                  onClick={() => { console.log(row) }}
               >
                  <Delete />
               </SmallSoftButton>
            </Box>
         ),
      },
   ];


   const [data, setData] = useState([])

   async function get_Data() {
      const response = await productosservice.get_Productos()
      setData(response)
      setPending(!pending)
   }

   //Carga de Data
   useEffect(() => {
      get_Data()
      setTimeout(() => { console.log(data) }, 2000)
   }, [])

   const defaultValues = {
      Nombre: "",
      Precio: ""
   };

   const ProductosSchema = yup.object().shape({
      Nombre: yup.string().required(),
      Precio: yup.string().required(),
   });

   //Tab 1 useform
   const { handleSubmit, reset, control, formState, watch, setValue, trigger } = useForm({
      defaultValues,
      mode: "all",
      resolver: yupResolver(ProductosSchema),
   });
   const { isValid, errors } = formState;
   const modelo = watch()

   const Agregar_Producto = async () =>{
      try{
         trigger()
         if(isValid){
            await productosservice.insert_Productos(modelo)
            ToastSuccess()
            await get_Data()
            handleClose()
         }else{
            ToastWarningCamposVacios()
         }
      }catch(error){
         console.log(error)
      }
   }

   return (
      <DashboardLayout>
         <DashboardNavbar />
         <Card>
            <Container>
               <Typography pt={3} variant="h2">Productos</Typography>
               <Grid pr={3} pt={3} pb={1} container spacing={2}>
                  <Grid item xs={5}>
                     <SoftButton onClick={handleOpen} variant={'gradient'} size={'medium'} color={'info'}><Add />Añadir</SoftButton>
                  </Grid>
                  <Grid item xs={4}></Grid>
                  <Grid item xs={3}><SoftInput placeholder={'Buscar...'}></SoftInput></Grid>

               </Grid>

               <DataTable columns={columnas} data={data} pagination={true} />
            </Container>


            <Dialog open={open} onClose={handleClose} fullWidth='md'>
               <Card>
                  <DialogTitle><Typography variant="h5">Agregar un Producto</Typography></DialogTitle>
                  <DialogContent>
                     <Grid container spacing={2} pt={3}>
                        <Grid item xs={6}>
                           <Controller render={({ field }) => (
                              <SoftInput {...field} error={!!errors.Nombre} placeholder="Nombre del producto" sx={{ width: "100%" }} />
                           )}
                              control={control}
                              name="Nombre"
                           />
                        </Grid>
                        <Grid item xs={6}>
                        <Controller render={({ field }) => (
                              <SoftInput {...field} error={!!errors.Precio} placeholder="Nombre del producto" sx={{ width: "100%" }} />
                           )}
                              control={control}
                              name="Precio"
                           />
                        </Grid>
                     </Grid>
                  </DialogContent>
                  <DialogActions>
                     <SoftButton onClick={handleClose} variant={'text'} color={'error'}>Cerrar</SoftButton>
                     <SoftButton onClick={Agregar_Producto} variant={'text'} color={'info'}>Agregar</SoftButton>
                  </DialogActions>
               </Card>
            </Dialog>

         </Card>
      </DashboardLayout>
   )
}

export default Productos