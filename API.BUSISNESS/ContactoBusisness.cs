using API.DATA;
using DTO.ENTITIES;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.BUSISNESS
{
    /// <summary>
    /// Calse para manejar la capa de negocio del controller agenda
    /// </summary>
    public class ContactoBusisness
    {
        private readonly AgendaData agendaData;

        /// <summary>
        /// Constructor
        /// </summary> 
        public ContactoBusisness(AgendaData agendaData)
        {
            this.agendaData = agendaData;
        }

        /// <summary>
        /// Metodo para registrar un contacto de la agenda 
        /// </summary>
        /// <param name="contacto">Modelo con la informacion del contacto a registrar </param>
        /// <returns>Retorna estatus de la accion </returns>
        public async Task<ContactoDto> RegistraContactoBusisness(ContactoDto contacto)
            => await agendaData.RegistraContactoData(contacto);

        /// <summary>
        /// Metodo para actualizar un contacto de la agenda 
        /// </summary>
        /// <param name="contacto">Modelo con la informacion del contacto a actualizar </param>
        /// <returns>Retorna estatus de la accion </returns>
        public async Task<ContactoDto> ActualizaContactoBusisness(ContactoDto contacto)
            => await agendaData.ActualizaContactoData(contacto);

        /// <summary>
        /// Metodo para eliminar un contacto de la agenda 
        /// </summary>
        /// <param name="IdContacto">Identificador unico del contacto </param>
        /// <returns>Retorna estatus de la accion </returns>
        public async Task<ContactoDto> EliminaontactoBusisness(int IdContacto)
            => await agendaData.EliminaontactoData(IdContacto);

        /// <summary>
        /// Metodo para conultar todas las categorias 
        /// </summary>
        /// <returns>Retorna lista de categorias</returns>
        public async Task<IList<CategoriaTelDto>> ConsultaCAtegoriasTel()
            => await agendaData.ConsultaCAtegoriasTel();

        /// <summary>
        /// Metodo para consiltar todos los contactos
        /// </summary>
        /// <returns>Lista de contactos</returns>
        public async Task<IList<ContactoDto>> ConsultaContactos()
            => await agendaData.ConsultaContactos();
    }
}
