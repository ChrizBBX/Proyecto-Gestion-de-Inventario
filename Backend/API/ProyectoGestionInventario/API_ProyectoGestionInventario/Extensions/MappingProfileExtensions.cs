using API_ProyectoGestionInventario.Models.ModelsInventario;
using AutoMapper;
using ProyectoGestionInventario.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoGestionInventario.API.Extentions
{
    public class MappingProfileExtensions : Profile
    {
        public MappingProfileExtensions()
        {
            #region
            CreateMap<ProductosViewModel, tbProducto>().ReverseMap();
            #endregion
        }
    }
}
