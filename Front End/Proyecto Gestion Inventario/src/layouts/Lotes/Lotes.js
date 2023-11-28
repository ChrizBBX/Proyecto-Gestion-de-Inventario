//Layout
import DashboardLayout from "examples/LayoutContainers/DashboardLayout"
import DashboardNavbar from "examples/Navbars/DashboardNavbar"

//Datatable
import DataTable from "react-data-table-component"

//Hooks
import { useState, useEffect } from "react"

//Servicios
import LotesService from "./LotesService"

//Soft UI
import SoftInput from "components/SoftInput"
import SoftButton from "components/SoftButton"

//Material
import { Box, Grid, Card, CardHeader, Container, Typography, Dialog, DialogActions, DialogTitle, DialogContent, Button, TextField } from "@mui/material"
import { CardMedia } from "@mui/material"
import { Add, Edit, Visibility, Delete, Label } from "@mui/icons-material"
import styled from "styled-components"
import { grey } from "@mui/material/colors"
import DateTimePicker from "react-datetime-picker"

// Validaciones 
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { Controller, set, useForm } from 'react-hook-form';
import { ToastWarningCamposVacios } from "assets/Toast/Toast"
import { ToastSuccess } from "assets/Toast/Toast"
import { ToastWarningPersonalizado } from "assets/Toast/Toast"
import { ToastError } from "assets/Toast/Toast"
import { ToastSuccessPersonalizado } from "assets/Toast/Toast"
import { column } from "stylis"
import 'react-datetime-picker/dist/DateTimePicker.css';

const SmallSoftButton = styled(SoftButton)`
  font-size: 10px;
  padding: 0px 8px 0px 8px; 
  margin-right: 5px;
`;



function Lotes() {
    const lotesservice = LotesService()
    const [data, setData] = useState()
    const [pending, setPending] = useState(true)
    const [dataOriginal, setDataOriginal] = useState()
    const [action, setAction] = useState('')
    const [open, setOpen] = useState(false);
    const [date, setDate] = useState(new Date());


    const handleOpen = (accion, row) => {
        //reset()
        switch (accion) {
            case "Crear":
            setOpen(true)
                break;
        }
        setTimeout(() => { setAction(accion) }, 200)
    };

    const handleClose = (accion) => {
        switch (accion) {
           case "Basic":
              setOpen(false)
              break;
        }
        //reset()
     };

     const handleChangeDate = (newDate) => {
        setDate(newDate);
      };

    async function get_Data() {
        const response = await lotesservice.get_Lotes()
        setData(response)
        setPending(!pending)
    }

    useEffect(() => {
        get_Data()
        setDataOriginal(data)
        setTimeout(() => { console.log(data) }, 500)
    }, [])

    const columnas = [
        {
            key: 'lote_Id',
            name: 'ID',
            selector: row => row.lote_Id
        },
        {
            name: 'Producto',
            selector: row => row.prod_Descripcion
        },
        {
            name: 'Cantidad',
            selector: row => row.lote_Cantidad
        },
        {
            name: 'Fecha de Vencimiento',
            selector: row => row.lote_FechaVencimiento
        },
    ]

    return (
        <DashboardLayout>
            <DashboardNavbar />
            <Card>
                <Container>
                    <Typography pt={3} variant="h2">Lotes</Typography>
                    <Grid pr={3} pt={3} pb={1} container spacing={2}>
                        <Grid item xs={5}>
                            <SoftButton onClick={() =>{handleOpen('Crear')}} variant={'gradient'} size={'medium'} color={'info'}><Add />Añadir</SoftButton>
                        </Grid>
                        <Grid item xs={4}></Grid>
                        <Grid item xs={3}><SoftInput placeholder={'Buscar...'}></SoftInput></Grid>

                    </Grid>

                    <DataTable columns={columnas} data={data} pagination={true} />
                </Container>

                        {/* Modal Crear y Editar */}
                        <Dialog open={open} onClose={() => { handleClose("Basic") }} fullWidth='md'>
               <Card>
                  <DialogTitle><Typography variant="h5">{action == 'Crear' ? 'Agregar un Lote' : 'Editar Lote'}</Typography></DialogTitle>
                  <DialogContent>
                     <Grid container spacing={2} pt={3}>
                        <Grid item xs={6}>
                      <DateTimePicker onChange={handleChangeDate} value={date} />
                        </Grid>
                        <Grid item xs={6}>
                
                        </Grid>
                     </Grid>
                  </DialogContent>
                  <DialogActions>
                     <SoftButton onClick={() => { handleClose("Basic") }} variant={'text'} color={'error'}>Cerrar</SoftButton>
                     <SoftButton onClick={() => {}} variant={'text'} color={'info'}>{action == 'Crear' ? 'Agregar' : 'Editar'}</SoftButton>
                  </DialogActions>
               </Card>
            </Dialog>

            {/* Fin Modal Crear y Editar */}

            </Card>
        </DashboardLayout>
    )
}

export default Lotes