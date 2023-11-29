//Layout
import DashboardLayout from "examples/LayoutContainers/DashboardLayout"
import DashboardNavbar from "examples/Navbars/DashboardNavbar"

//Datatable
import DataTable from "react-data-table-component"

//Hooks
import { useState, useEffect } from "react"

//Servicios
import SalidasService from "./SalidasService"
import ProductosService from "layouts/Productos/ProductosService"
import LotesService from "layouts/Lotes/LotesService"

//Soft UI
import SoftInput from "components/SoftInput"
import SoftButton from "components/SoftButton"

//Material
import { Box, Grid, Card, CardHeader, Container, Typography, Dialog, DialogActions, DialogTitle, DialogContent, Button, TextField, FormLabel } from "@mui/material"
import { CardMedia } from "@mui/material"
import { Add, Edit, Visibility, Delete, Label, Cancel, CollectionsOutlined } from "@mui/icons-material"
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

//AntDesign
import { Input, Select } from "antd"

const SmallSoftButton = styled(SoftButton)`
  font-size: 12px;
  padding: 0px 8px 0px 8px; 
  margin-right: 5px;
`;

function Salidas() {
    const salidasservice = SalidasService()
    const productosservice = ProductosService()
    const lotesservice = LotesService()
    const [standard, setStandard] = useState(true)
    const [adding, setAdding] = useState(false)
    const [view, setView] = useState(false)
    const [sucursales, setSucursales] = useState([{}])
    const [productos, setProductos] = useState([{}])
    const [stock, setStock] = useState(0)
    const [costo, setCosto] = useState(0)
    const [dataOriginal, setDataOriginal] = useState([])
    const [data, setData] = useState([])
    const [cantidadRestar, setCantidadRestar] = useState(0)

    async function set_Sucursales() {
        const response = await salidasservice.get_Sucursales()
        const mapeado = response.map((sucursal) => ({
            value: sucursal?.sucu_Id,
            label: sucursal?.sucu_Descripcion
        }))
        setSucursales(mapeado)
    }

    async function set_Productos() {
        const response = await productosservice.get_Productos()
        const mapeado = response.map((sucursal) => ({
            value: sucursal?.prod_Id,
            label: sucursal?.prod_Descripcion
        }))
        setProductos(mapeado)
    }

    async function set_Lotes(producto) {
        const response = await lotesservice.get_Lotes_ByProducto(producto)
        console.log('lotes', response)
        if (response[0]?.cantidad_Total != undefined && response[0]?.cantidad_Total > 0) {
            setCosto(costo)
            setStock(response[0]?.cantidad_Total)
            setDataOriginal(response)
            setData([])
        } else {
            ToastWarningPersonalizado('Advertencia. No hay Stock para este producto')
            setStock(0)
            setDataOriginal([])
            setData([])
        }
    }

    function set_Data() {
        trigger()
        if (isValid) {
            setData(dataOriginal)
        } else {
            ToastWarningCamposVacios()
        }
    }

    const defaultValues = {
        sucu_Id: undefined,
        sucu_Estado: "",
        prod_Id: undefined,
        sade_Cantidad: "",
    };

    const SalidasSchema = yup.object().shape({
        sucu_Id: yup.string().required(),
        prod_Id: yup.string().required(),
        sade_Cantidad: yup.string().required(),
        sucu_Estado: yup.string(),
    });

    //Tab 1 useform
    const { handleSubmit, reset, control, formState, watch, setValue, trigger } = useForm({
        defaultValues,
        mode: "all",
        resolver: yupResolver(SalidasSchema),
    });
    const { isValid, errors } = formState;
    const modelo = watch()

    const handleOnChangeSucursal = (sucursal) => {
        setValue('sucu_Id', sucursal)
        trigger('sucu_Id')
    }

    const handleOnChangeProducto = (producto) => {

        setValue('prod_Id', producto)
        set_Lotes(producto)
        trigger('prod_Id')
        setValue('sade_Cantidad', "")
    }

    const handleOnChangeCantidad = (cantidad) => {
        const inputValue = cantidad.target.value
        if (/^[0-9]*$/.test(inputValue) || inputValue === '') {
            if (inputValue > -1 && inputValue <= stock) {
                setValue('sade_Cantidad', inputValue)
                console.log('se seteo', inputValue)
                trigger('sade_Cantidad')
            } else {
                ToastWarningPersonalizado('Advertencia. Limite de stock')
                trigger('sade_Cantidad')
            }
        }
    }

    useEffect(() => {
        set_Sucursales()
        set_Productos()
        setTimeout(() => { console.log('lo que queres ver', sucursales) }, 500)
    }, [])

    useEffect(() => {
        if (dataOriginal[0]?.prod_Precio) {
            const costo = dataOriginal[0]?.prod_Precio * modelo.sade_Cantidad
            setCosto(costo)
        }
    }, [modelo.sade_Cantidad, modelo.prod_Id])

    const columns = [
        {
            name: 'ID',
            selector: row => row.prod_Id
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
            name: 'Lote',
            selector: row => row.lote_Id
        },
        {
            name: 'Costo',
            selector: row => row.lote_Costo
        },
        {
            name: 'Fecha Vencimiento',
            selector: row => row.lote_FechaVencimiento
        }
    ]

    const update = async(sali) =>{
        if(sali > 0){
            if (cantidadRestar > 0) {
                data.forEach(item => {
                if (cantidadRestar > item) {
                    console.log('Se le resta a un lote y continua')
                    salidasservice.insert_SalidasDetalles(sali, item.lote_Id, cantidadRestar)
                    setCantidadRestar(cantidadRestar - item.lote_Cantidad)
                } else {
                    console.log('Se le resta a un lote y para')
                    salidasservice.insert_SalidasDetalles(sali, item.lote_Id, cantidadRestar)
                    setCantidadRestar(0)
                }
                });
            }
    }
    }

    async function Finalizar_Salida() {
        setCantidadRestar(modelo.sade_Cantidad)
       try{
        const response = await salidasservice.insert_Salidas(modelo)
        setTimeout(()=>{update(response)},1000)
       }catch(error){
        console.log(error)
       }
    }

    return (
        <DashboardLayout>
            <DashboardNavbar />
            <Card>
                <Container style={{ display: standard ? 'block' : 'none' }}>
                    <Typography pt={3} variant="h2">Salidas</Typography>

                    <Grid container pt={3} pb={1}>

                        <Grid item xs={2}>
                            <SmallSoftButton onClick={() => { setAdding(true), setStandard(false) }} variant={'gradient'} color={'info'}><Add />Realizar una salida</SmallSoftButton>
                        </Grid>


                        <Grid item xs={2}>
                            <SmallSoftButton onClick={() => { }} variant={'gradient'} color={'primary'}><Visibility style={{ marginRight: 8 }} />Ver Salidas</SmallSoftButton>
                        </Grid>

                    </Grid>

                </Container>

                <Container style={{ display: adding ? 'block' : 'none' }}>
                    <Typography pt={3} variant="h3">Realizar una Salida</Typography>

                    <Grid container>

                        <Grid item xs={6} pt={3}>
                            <Typography>Sucursal</Typography>
                            <Select status={!!errors.sucu_Id ? 'error' : ''} value={modelo.sucu_Id} placeholder="Seleccione una sucursal..." onChange={handleOnChangeSucursal} style={{ width: '95%' }} size="large" options={sucursales} />
                        </Grid>

                        <Grid item xs={6} pt={3}>
                            <Typography>Producto</Typography>
                            <Select status={!!errors.prod_Id ? 'error' : ''} value={modelo.prod_Id} placeholder="Seleccione un producto..." onChange={handleOnChangeProducto} style={{ width: '100%' }} size="large" options={productos} />
                        </Grid>

                        <Grid item xs={6} pt={3}>
                            <Typography>Cantidad</Typography>
                            <Input status={!!errors.sade_Cantidad ? 'error' : ''} addonAfter={`${modelo.sade_Cantidad} ${modelo.sade_Cantidad != "" ? "/" : ""} ${stock}`} disabled={stock == 0 ? true : false} value={modelo.sade_Cantidad} placeholder="Cantidad..." onChange={handleOnChangeCantidad} style={{ width: '95%' }} size="large" />
                        </Grid>

                        <Grid item xs={6} pt={3}>
                            <Typography>Costo total: {costo}</Typography>
                        </Grid>

                    </Grid>

                    <Grid container justifyContent="flex-end" spacing={2} pt={3} pb={1}>
                        <Grid item>
                            <SmallSoftButton onClick={() => { setAdding(false), setStandard(true), reset(), setStock(0), setData([]) }} variant={'contained'} color={'error'}>
                                <Cancel style={{ marginRight: 8 }} />
                                Cancelar
                            </SmallSoftButton>
                        </Grid>
                        <Grid item>
                            <SmallSoftButton onClick={() => { setData([]) }} variant={'contained'} color={'primary'}>
                                <Cancel style={{ marginRight: 8 }} />
                                Quitar
                            </SmallSoftButton>
                        </Grid>
                        <Grid item>
                            <SmallSoftButton onClick={() => { trigger(), set_Data() }} variant={'contained'} color={'info'}>
                                <Add style={{ marginRight: 8 }} />
                                Agregar
                            </SmallSoftButton>
                        </Grid>
                    </Grid>

                    {data.length > 0 ? <Container style={{ paddingBottom: 6, }}>
                        <Typography variant="h3" pb={3}>Lotes disponibles</Typography>
                        <DataTable columns={columns} data={data}></DataTable>
                        <Grid container justifyContent="center" pt={6} pb={2}>
                            <Grid item>
                                <SmallSoftButton onClick={Finalizar_Salida} variant={'gradient'} color={'info'}>Finalizar Salida</SmallSoftButton>
                            </Grid>
                        </Grid>
                    </Container> : ''}

                </Container>

            </Card>
        </DashboardLayout>
    )
}

export default Salidas