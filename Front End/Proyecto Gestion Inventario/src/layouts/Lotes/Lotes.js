//Layout
import DashboardLayout from "examples/LayoutContainers/DashboardLayout"
import DashboardNavbar from "examples/Navbars/DashboardNavbar"

//Datatable
import DataTable from "react-data-table-component"

//Hooks
import { useEffect, useState } from "react"

//Servicios
import LotesService from "./LotesService"
import ProductosService from "layouts/Productos/ProductosService"

//Soft UI
import SoftButton from "components/SoftButton"
import SoftInput from "components/SoftInput"

//Material
import { Add } from "@mui/icons-material"
import { Card, Container, Grid, Typography, Accordion, AccordionSummary, AccordionDetails } from "@mui/material"
import styled from "styled-components"
import { ExpandMore } from "@mui/icons-material"


//Ant Design
import { DatePicker } from "antd"
import { Select, Input } from "antd"


//Validaciones
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { Controller, set, useForm } from 'react-hook-form';
import { ToastWarningCamposVacios } from "assets/Toast/Toast"
import { ToastSuccess } from "assets/Toast/Toast"
import { ToastWarningPersonalizado } from "assets/Toast/Toast"
import { ToastError } from "assets/Toast/Toast"
import { ToastSuccessPersonalizado } from "assets/Toast/Toast"

const SmallSoftButton = styled(SoftButton)`
  font-size: 10px;
  padding: 0px 8px 0px 8px; 
  margin-right: 5px;
`;

function Lotes() {
    const lotesservice = LotesService()
    const productosservice = ProductosService()
    const [desplegarNuevo, setDesplegarNuevo] = useState(false);
    const [productos, setProductos] = useState()
    const [data, setData] = useState()
    const [pending, setPending] = useState(true)
    const [dataOriginal, setDataOriginal] = useState()
    const [action, setAction] = useState('')
    const [open, setOpen] = useState(false);
    const [date, setDate] = useState(null);

    const defaultValues = {
        lote_Id: "",
        prod_Id: "",
        lote_Cantidad: "",
        lote_FechaVencimiento: "",
    };

    const LotesSchema = yup.object().shape({
        lote_Id: yup.string(),
        prod_Id: yup.string().required(),
        lote_Cantidad: yup.string().required(),
        lote_FechaVencimiento: yup.string().required(),
    });

    const { handleSubmit, reset, control, formState, watch, setValue, trigger } = useForm({
        defaultValues,
        mode: "all",
        resolver: yupResolver(LotesSchema),
    });
    const { isValid, errors } = formState;
    const modelo = watch()

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

    const handleOnChangeProductos = (value) => {
        setValue('prod_Id', value)
    }

    async function get_Data() {
        const response = await lotesservice.get_Lotes()
        setData(response)
        setPending(!pending)
    }

    async function get_Productos() {
        const response = await productosservice.get_Productos()
        const mapeado = response.map((producto) => ({
            value: producto.prod_Id,
            label: producto.prod_Descripcion
        }))
        setProductos(mapeado)
    }

    useEffect(() => {
        get_Productos()
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
            <Container>
                <Accordion expanded={!desplegarNuevo} style={{ display: desplegarNuevo ? 'none' : 'block' }}>
                    <AccordionSummary
                        aria-controls="panel1a-content"
                        id="panel1a-header"
                    >
                        <Typography variant="h2" style={{ display: desplegarNuevo ? 'none' : 'block' }}>
                            Lotes
                        </Typography>
                    </AccordionSummary>
                    <AccordionDetails>
                        <Container>
                            <Grid pr={3} pb={1} container spacing={2}>
                                <Grid item xs={5}>
                                    <SoftButton onClick={() => { setDesplegarNuevo(!desplegarNuevo), console.log(modelo) }} variant={'gradient'} size={'medium'} color={'info'}><Add />Añadir</SoftButton>
                                </Grid>
                                <Grid item xs={4}></Grid>
                                <Grid item xs={3}><SoftInput placeholder={'Buscar...'}></SoftInput></Grid>

                            </Grid>

                            <DataTable columns={columnas} data={data} pagination={true} />

                        </Container>
                    </AccordionDetails>
                </Accordion>




                {/* Modal Crear y Editar */}
                <Accordion expanded={desplegarNuevo} style={{ display: desplegarNuevo ? 'block' : 'none' }}>
                    <AccordionSummary
                        aria-controls="panel1a-content"
                        id="panel1a-header"
                    >
                        <Typography variant="h4" style={{ display: desplegarNuevo ? 'block' : 'none' }}>
                            Agregar Nuevo Lote
                        </Typography>
                    </AccordionSummary>
                    <AccordionDetails>
                        <Grid container spacing={2} pt={1}>
                            <Grid item sx={6}>
                                <Controller render={({ field }) => (
                                    <Select {...field} error={!!errors.prod_Id} placeholder="Productos" size="large" onChange={handleOnChangeProductos} options={productos} />
                                )}
                                    control={control}
                                    name="prod_Id"
                                />

                                <Controller render={({ field }) => (
                                    <Input {...field} error={!!errors.lote_Cantidad} placeholder="Cantidad" value={field.value}/>
                                )}
                                    control={control}
                                    name="lote_Cantidad"
                                />
                            </Grid>
                        </Grid>
                        <Grid container spacing={2} pt={2}>
                            <Grid item xs={6}>
                                {/* <DatePicker onChange={handleChangeDate} value={date} /> */}
                                <SoftButton onClick={() => { setDesplegarNuevo(!desplegarNuevo) }} variant={'outlined'} size={'medium'} color={'error'}>Cancelar</SoftButton>
                            </Grid>
                            <Grid item xs={6}>
                                {/* Otro contenido si es necesario */}
                            </Grid>
                        </Grid>
                    </AccordionDetails>
                </Accordion>
            </Container>

            {/* Fin Modal Crear y Editar */}
        </DashboardLayout>
    )
}

export default Lotes