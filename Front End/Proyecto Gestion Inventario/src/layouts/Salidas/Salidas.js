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
import { Box, Grid, Card, CardHeader, Container, Typography, Dialog, DialogActions, DialogTitle, DialogContent, Button, TextField, FormLabel, useStepContext } from "@mui/material"
import { CardMedia } from "@mui/material"
import { Add, Edit, Visibility, Delete, Label, Cancel, CollectionsOutlined, ExpandCircleDown, Send } from "@mui/icons-material"
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
import { Input, Select, DatePicker } from "antd"
const { RangePicker } = DatePicker;

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
    const [canDoSalidas, setCanDoSalidas] = useState(true)
    const [viewData, setViewData] = useState([])
    const [date,setDate] = useState(null)
    const [sucu,setSucu] = useState(null)
    const [inicio,setInicio] = useState(null)
    const [fin,setFin] = useState(null)
    const [sucursalEstado,setSucursalEstado] = useState(true)

    async function set_Sucursales() {
        const response = await salidasservice.get_Sucursales()
        const mapeado = response.map((sucursal) => ({
            value: sucursal?.sucu_Id,
            label: sucursal?.sucu_Descripcion
        }))
        setSucursales(mapeado)
    }

    async function set_Salidas() {
        const response = await salidasservice.get_Salidas()
        console.log(response)
        setViewData(response)
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

    const handleOnChangeSucursal = async (sucursal) => {
        const response = await salidasservice.sucursal_Status(sucursal)
             
            if(response?.costo_Total > 5000){
                setSucursalEstado(false)
                ToastWarningPersonalizado('Advertencia. La Sucursal seleccionada tiene muchos pendientes')
                setValue('sucu_Id',undefined)
            }else{
                setValue('sucu_Id', sucursal)
                trigger('sucu_Id')
                setSucursalEstado(true)
            }
        
        console.log(response)
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

    const handleOnChangeDateRange = async (value) => {
        console.log('fecha cambiada valor: ',value)
        if (value != null) {

            const fechasFormateadas = value.map((fecha) => {
                const fechaObj = new Date(fecha);
                const mes = fechaObj.getMonth() + 1;
                const dia = fechaObj.getDate();
                const año = fechaObj.getFullYear();

                const mesStr = mes < 10 ? `0${mes}` : mes;
                const diaStr = dia < 10 ? `0${dia}` : dia;

                // Formatear la fecha como "mes-dia-año"
                return `${mesStr}-${diaStr}-${año}`;
            });

            const fecha1 = fechasFormateadas[0];
            const fecha2 = fechasFormateadas[1];

            console.log('Fecha 1:', fecha1);
            console.log('Fecha 2:', fecha2);
            setInicio(fecha1)
            setFin(fecha2)
            const response = await salidasservice.filtered(fecha1,fecha2,sucu)
            setViewData(response)
        }else{
            setInicio(null)
            setFin(null)
            const response = await salidasservice.filtered(null,null,sucu)
            setViewData(response)
        }
    };

    const handleOnChangeSucursalView = async (sucursal) => {
        if(sucursal != undefined){
            setSucu(sucursal)
            const response = await salidasservice.filtered(inicio,fin,sucursal)
            setViewData(response)
        }else{
            setSucu(null)
            const response = await salidasservice.filtered(inicio,fin,null)
            setViewData(response)
        }
        console.log('Sucursal Seleccionada',sucursal)
    }

    useEffect(() => {
        set_Sucursales()
        set_Productos()
        set_Salidas()
        const usuario = JSON.parse(localStorage.getItem('user_data'))
        if (usuario?.role_Id != 1) {
            setCanDoSalidas(false)
        }
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

    const viewColumns = [
        {
            name: 'ID',
            selector: row => row.sali_Id
        },
        {
            name: 'Fecha',
            selector: row => row.sali_FechaCreacion
        },
        {
            name: 'Unidades Totales',
            selector: row => row.unidades_Totales
        },
        {
            name: 'Costo Total',
            selector: row => row.costo_Total
        },
        {
            name: 'Estado',
            selector: row => row.sucu_SalidaEstado
        },
        {
            name: 'Usuario Creacion',
            selector: row => row.usua_UsuarioCreacion_Nombre
        },
        {
            name: 'Usuario Aceptacion',
            selector: row => row.usua_UsuarioModificacion_Nombre
        },
        {
            name: 'Fecha Recibida',
            selector: row => row.saliFechaModificacion
        },
        {
            name: 'Accion',
            cell: (row) => (
                <>
                    {row.sucu_SalidaEstado == "Enviada a sucursal" ?
                        <Box>
                            <SmallSoftButton
                                variant={'gradient'}
                                color={'primary'}
                                onClick={() => { Actualizar_Salida(row.sali_Id) }}
                            >
                                <ExpandCircleDown />
                            </SmallSoftButton>
                        </Box>
                        :
                        <>
                            <SmallSoftButton
                                variant={'gradient'}
                                color={'primary'}
                                disabled={true}
                            >
                                <Send />
                            </SmallSoftButton>
                        </>
                    }
                </>
            )
        },
    ]

    const update = async (sali) => {
        let quitar = modelo.sade_Cantidad
        if (sali > 0) {
            if (quitar > 0) {
                data.forEach(item => {
                    if (quitar > 0) {
                        if (quitar > item.lote_Cantidad) {
                            console.log(`cantidad a restar: `, quitar)
                            salidasservice.insert_SalidasDetalles(sali, item.lote_Id, item.lote_Cantidad)
                            quitar = quitar - item.lote_Cantidad
                        } else {
                            console.log(`cantidad a restar: `, quitar)
                            salidasservice.insert_SalidasDetalles(sali, item.lote_Id, quitar)
                            quitar = 0
                        }
                    }
                });
            }
            //Limpieza
            reset()
            setAdding(false)
            setStandard(true)
            setStock(0)
            setCosto(0)
            setData([[]])
            setDataOriginal([[]])
            ToastSuccessPersonalizado('Exito. La salida se ha registrado exitosamente')
        }
    }

    async function Finalizar_Salida() {
        try {
            const response = await salidasservice.insert_Salidas(modelo)
            setTimeout(() => { update(response) }, 1000)
        } catch (error) {
            console.log(error)
        }
    }

    async function Actualizar_Salida(Id) {
        try {
            const response = await salidasservice.update_Salidas(Id)
            if (response == 1) {
                set_Salidas()
                ToastSuccess('Exito. Estado de salida actualizado')
            }
        } catch (error) {
            console.log(error)
        }
    }

    function cerrarVista(){
        setStandard(true),
        setView(false)
        setInicio(null)
        setFin(null)
        setSucu(null)
        set_Salidas()
        console.log('cerro vista')
    }

    return (
        <DashboardLayout>
            <DashboardNavbar />
            <Card>
                <Container style={{ display: standard ? 'block' : 'none' }}>
                    <Typography pt={3} variant="h2">Salidas</Typography>

                    <Grid container pt={3} pb={1}>

                        <Grid item xs={2} style={{ display: canDoSalidas ? '' : 'none' }}>
                            <SmallSoftButton onClick={() => { setAdding(true), setStandard(false) }} variant={'gradient'} color={'info'}><Add />Realizar una salida</SmallSoftButton>
                        </Grid>


                        <Grid item xs={2}>
                            <SmallSoftButton onClick={() => { setStandard(false), setView(true) }} variant={'gradient'} color={'primary'}><Visibility style={{ marginRight: 8 }} />Ver Salidas</SmallSoftButton>
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
                            <SmallSoftButton style={{'display': sucursalEstado == true ? '' : 'none' }} onClick={() => { trigger(), set_Data() }} variant={'contained'} color={'info'}>
                                <Add style={{ marginRight: 8 }} />
                                Agregar
                            </SmallSoftButton>
                        </Grid>
                    </Grid>

                    {data.length > 0 ? <Container style={{ paddingBottom: 6, }}>
                        <Typography variant="h3" pb={3}>Lotes disponibles</Typography>
                        <DataTable columns={columns} data={data} pagination={true}></DataTable>
                        <Grid container justifyContent="center" pt={6} pb={2}>
                            <Grid item>
                                <SmallSoftButton onClick={Finalizar_Salida} variant={'gradient'} color={'info'}>Finalizar Salida</SmallSoftButton>
                            </Grid>
                        </Grid>
                    </Container> : ''}

                </Container>

                <Container style={{ display: view ? 'block' : 'none' }}>
                    <Typography variant="h3" pt={3} pb={3}>Listado de Salidas</Typography>
                    <Grid container>

                        <Grid item xs={6} mb={5}>
                            <Typography variant="h6">Filtrar por rango de Fechas</Typography>
                            <RangePicker showTime onChange={handleOnChangeDateRange} />
                        </Grid>

                        <Grid item xs={6}>
                            <Typography variant="h6">Sucursal</Typography>
                            <Select placeholder="Filtrar por sucursal..." style={{ width: '95%' }} size="large" options={sucursales} onChange={handleOnChangeSucursalView} allowClear={true}/>
                        </Grid>

                    </Grid>
                    <Container style={{ paddingBottom: 6, }}>
                        <DataTable columns={viewColumns} data={viewData} pagination={true}></DataTable>
                    </Container>

                    <Grid container justifyContent="flex-end" spacing={2} pt={3} pb={1}>
                        <Grid item>
                            <SmallSoftButton onClick={cerrarVista} variant={'contained'} color={'error'}>
                                <Cancel style={{ marginRight: 8 }} />
                                Cancelar
                            </SmallSoftButton>
                        </Grid>

                    </Grid>
                </Container>

            </Card>
        </DashboardLayout>
    )
}

export default Salidas