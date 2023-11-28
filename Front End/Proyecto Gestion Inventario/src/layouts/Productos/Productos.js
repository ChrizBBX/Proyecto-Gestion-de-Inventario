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

function Productos() {
   const [pending, setPending] = useState(true)
   const [action, setAction] = useState('')
   const [open, setOpen] = useState(false);
   const [openDetails, setOpenDetails] = useState(false);
   const [openDelete, setOpenDelete] = useState(false);
   const [details, setDetails] = useState([])
   const productosservice = ProductosService()

   const handleOpen = (accion, row) => {
      reset()
      setTimeout(()=>{
         if (accion == "Editar") {
            setValue('Id', row.prod_Id)
            setValue('Nombre', row.prod_Descripcion)
            setValue('Precio', row.prod_Precio)
            setOpen(true)
         } else if (accion == "Crear") {
            setOpen(true)
         } else if (accion == "Detalles") {
            setDetails(row)
            setOpenDetails(true)
         } 
   
         if(accion == "Eliminar"){
            setOpenDelete(true)
            console.log('se abrio eliminar')
            setValue('Id',row.prod_Id)
         }
      },500)
      setAction(accion)
   };

   const handleClose = (accion) => {
      switch (accion) {
         case "Basic":
            setOpen(false)
            break;
         case "Detalles":
            setOpenDetails(false)
            break;
         case "Eliminar":
            setOpenDelete(false)
            break;
      }
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
                  onClick={() => { handleOpen('Editar', row) }}
               >
                  <Edit />
               </SmallSoftButton>
               <SmallSoftButton
                  variant={'gradient'}
                  color={'secondary'}
                  onClick={() => { handleOpen("Detalles", row) }}
               >
                  <Visibility />
               </SmallSoftButton>
               <SmallSoftButton
                  variant={'gradient'}
                  color={'error'}
                  onClick={() => { handleOpen("Eliminar", row) }}
               >
                  <Delete />
               </SmallSoftButton>
            </Box>
         ),
      },
   ];


   const [data, setData] = useState([])
   const [dataOriginal, setDataOriginal] = useState([])

   function handleFilter(event) {

      const inputValue = event.target.value.toLowerCase();

      if (!inputValue) {
         get_Data()
         return;
      }

      const filtrado = dataOriginal.filter((filas) => {
         return filas.prod_Descripcion.toLowerCase().includes(inputValue.toLowerCase())
      });
      setData(filtrado);
   }


   async function get_Data() {
      const response = await productosservice.get_Productos()
      setData(response)
      setPending(!pending)
   }

   //Carga de Data
   useEffect(() => {
      get_Data()
      setDataOriginal(data)
      setTimeout(() => { console.log(data) }, 2000)
   }, [])

   const defaultValues = {
      Id: "",
      Nombre: "",
      Precio: ""
   };

   const ProductosSchema = yup.object().shape({
      Id: yup.string(),
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

   const Agregar_Producto = async () => {
      try {
         trigger()
         if (isValid) {
            const response = await productosservice.insert_Productos(modelo)
            if (response.data.data.messageStatus == "1") {
               ToastSuccessPersonalizado('Exito. Registro editado exitosamente')
               await get_Data()
               handleClose("Basic")
            } else if (response.data.data.messageStatus == "2") {
               ToastWarningPersonalizado('Advertencia. Ese Producto ya existe')
            }
         } else {
            ToastWarningCamposVacios()
         }
      } catch (error) {
         console.log(error)
         ToastError()
      }
   }

   const Editar_Producto = async () => {
      console.log('Entro en editar producto')
      try {
         trigger()
         if (isValid) {
            const response = await productosservice.update_Productos(modelo)
            if (response.data.data.messageStatus == "1") {
               ToastSuccess()
               await get_Data()
               handleClose("Basic")
            } else if (response.data.data.messageStatus == "2") {
               ToastWarningPersonalizado('Advertencia. Ese Producto ya existe')
            }
         } else {
            ToastWarningCamposVacios()
         }
      } catch (error) {
         console.log(error)
         ToastError()
      }
   }

   const Eliminar_Producto = async () => {
      console.log(modelo)
      try {
         const response = await productosservice.delete_Productos(modelo)
         console.log(response)
         if (response.data.data.messageStatus == 1) {
            ToastSuccess()
            get_Data()
            handleClose("Eliminar")
         } else if (response.data.data.messageStatus == 2) {
            ToastWarningPersonalizado('Advertencia. Este elemento esta siendo utilizado')
         }
      } catch (error) {
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
                     <SoftButton onClick={() => { handleOpen('Crear') }} variant={'gradient'} size={'medium'} color={'info'}><Add />Añadir</SoftButton>
                  </Grid>
                  <Grid item xs={4}></Grid>
                  <Grid item xs={3}><SoftInput onChange={handleFilter} placeholder={'Buscar...'}></SoftInput></Grid>

               </Grid>

               <DataTable columns={columnas} data={data} pagination={true} />
            </Container>


            {/* Modal Crear y Editar */}
            <Dialog open={open} onClose={() => { handleClose("Basic") }} fullWidth='md'>
               <Card>
                  <DialogTitle><Typography variant="h5">{action == 'Crear' ? 'Agregar un producto' : 'Editar producto'}</Typography></DialogTitle>
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
                              <SoftInput {...field} error={!!errors.Precio} placeholder="Precio del producto" sx={{ width: "100%" }} />
                           )}
                              control={control}
                              name="Precio"
                           />
                        </Grid>
                     </Grid>
                  </DialogContent>
                  <DialogActions>
                     <SoftButton onClick={() => { handleClose("Basic") }} variant={'text'} color={'error'}>Cerrar</SoftButton>
                     <SoftButton onClick={() => {
                        if (action == "Crear")
                           Agregar_Producto()
                        else
                           Editar_Producto()
                     }} variant={'text'} color={'info'}>{action == 'Crear' ? 'Agregar' : 'Editar'}</SoftButton>
                  </DialogActions>
               </Card>
            </Dialog>

            {/* Fin Modal Crear y Editar */}

            {/*Modal Mostar Data */}
            <Dialog open={openDetails} onClose={() => { handleClose("Detalles") }} fullWidth='md'>
               <Card>
                  <DialogTitle><Typography variant="h5">Detalles del Producto</Typography></DialogTitle>
                  <DialogContent>
                     <Grid container spacing={2} pt={3}>

                        <Grid item xs={6}>
                           <Typography fontSize={20}>ID</Typography>
                           <Typography fontSize={17}>{details?.prod_Id}</Typography>
                        </Grid>

                        <Grid item xs={6}>
                           <Typography fontSize={20}>Producto</Typography>
                           <Typography fontSize={17}>{details?.prod_Descripcion}</Typography>
                        </Grid>

                        <Grid item xs={6}>
                           <Typography fontSize={20}>Precio</Typography>
                           <Typography fontSize={17}>{details?.prod_Precio}</Typography>
                        </Grid>

                        <Grid item xs={6}>
                           <Typography fontSize={20}>Usuario Creacion</Typography>
                           <Typography fontSize={17}>{details?.usua_UsuarioCreacion}</Typography>
                        </Grid>

                        <Grid item xs={6}>
                           <Typography fontSize={20}>Fecha Creacion</Typography>
                           <Typography fontSize={17}>{details?.prod_FechaCreacion}</Typography>
                        </Grid>

                        <Grid item xs={6}>
                           <Typography fontSize={20}>Usuario Modificador</Typography>
                           <Typography fontSize={17}>{details?.usua_UsuarioModificacion}</Typography>
                        </Grid>

                        <Grid item xs={6}>
                           <Typography fontSize={20}>Fecha Modificacion</Typography>
                           <Typography fontSize={17}>{details?.prod_FechaModificacion}</Typography>
                        </Grid>

                     </Grid>
                  </DialogContent>
                  <DialogActions>
                     <SoftButton onClick={() => { handleClose("Detalles") }} variant={'text'} color={'error'}>Cerrar</SoftButton>
                  </DialogActions>
               </Card>
            </Dialog>
            {/* Fin Modal Mostar Data */}

            {/* Modal Eliminar*/}
            <Dialog open={openDelete} onClose={() => { handleClose("Eliminar") }} fullWidth='md'>
               <Card>
                  <DialogTitle><Typography variant="h5">¿Desea eliminar este registro?</Typography></DialogTitle>
                  <DialogContent>
                  </DialogContent>
                  <DialogActions>
                     <SoftButton onClick={() => { handleClose("Eliminar") }} variant={'text'} color={'error'}>Cerrar</SoftButton>
                     <SoftButton onClick={Eliminar_Producto} variant={'text'} color={'primary'}>Eliminar</SoftButton>
                  </DialogActions>
               </Card>
            </Dialog>

            {/* Fin Modal Eliminar */}
         </Card>
      </DashboardLayout>
   )
}

export default Productos