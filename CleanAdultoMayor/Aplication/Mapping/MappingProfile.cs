using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Aplication.DTOs;
using AutoMapper;

namespace Aplication.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Adulto, AdultoDTOs>().ReverseMap();
            CreateMap<FichaEnfermeria, FichaEnfermeriaDTOs>()
            .ForMember(dest => dest.Adulto, opt => opt.MapFrom(src => src.Adulto)).ReverseMap();
            CreateMap<FichaOrientacion, FichaOrientacionDTOs>()
            .ForMember(dest => dest.Adulto, opt => opt.MapFrom(src => src.Adulto)).ReverseMap();
            CreateMap<FichaProteccion, FichaProteccionDTOs>()
            .ForMember(dest => dest.Adulto, opt => opt.MapFrom(src => src.Adulto)).ReverseMap();
            CreateMap<FichaFisioterapia, FichaFisioterapiaDTOs>()
            .ForMember(dest => dest.Adulto, opt => opt.MapFrom(src => src.Adulto)).ReverseMap();
        }
    }
}
