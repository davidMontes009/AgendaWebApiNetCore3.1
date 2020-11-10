using AutoMapper;
using DTO.ENTITIES;
using WebAPI_Agenda.ViewModels;

namespace WebAPI_Agenda.Services
{
    /// <summary>
    /// Clase para manejar el AAutomapper
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MappingProfile()
        {
            CreateMap<RegistraContactoModel, ContactoDto>().ReverseMap();
            CreateMap<ActualizaContactoModel, ContactoDto>().ReverseMap();
        }
    }
}
