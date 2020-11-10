using API.BUSISNESS;
using AutoMapper;
using DTO.ENTITIES;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Agenda.ViewModels;

namespace WebAPI_Agenda.Controllers
{
    /// <summary>
    /// Controller para administrar la agenda
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly ContactoBusisness contactoBusisness;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contactoBusisness"></param>
        public AgendaController(ContactoBusisness contactoBusisness, IMapper mapper)
        {
            this.contactoBusisness = contactoBusisness;
            this.mapper = mapper;
        }

        /// <summary>
        /// Api para consultar las categorias de telefonos 
        /// </summary> 
        /// <returns>Retorna lista de categorias</returns>
        /// <remarks>
        /// Sample Request 
        /// 
        ///     GET
        ///     El servicio no requiere parametros 
        ///     
        /// Sample Response 
        /// 
        ///     {
        ///         "listCategorias": [
        ///             {
        ///                 "categoriaTelId": 1,
        ///                 "descripcion": "Celular"
        ///             },
        ///             {
        ///                 "categoriaTelId": 2,
        ///                 "descripcion": "Casa"
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        [HttpGet]
        [Route("ConsultaCategoriasTel")]
        public async Task<ActionResult<IList<CategoriaTelDto>>> ConsultaCAtegoriasTel()
        {
            try
            {
                IList<CategoriaTelDto> listCategorias = new List<CategoriaTelDto>();
                listCategorias = await contactoBusisness.ConsultaCAtegoriasTel();
                return Ok(new { listCategorias });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Api para consultar las categorias de telefonos 
        /// </summary> 
        /// <returns>Retorna lista de categorias</returns>
        /// <remarks>
        /// Sample Request 
        /// 
        ///     GET
        ///     El servicio no requiere parametros 
        ///     
        /// Sample Response 
        /// 
        ///     {
        ///     "listContacto": [
        ///             {
        ///               "contactoId": 1,
        ///               "nombre": "David Israel",
        ///               "apellidos": "David Israel",
        ///               "telefono": "David Israel",
        ///               "email": "David Israel",
        ///               "direccion": "David Israel",
        ///               "categoriaTel": "David Israel",
        ///               "validations": [],
        ///               "isValid": true
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        [HttpGet]
        [Route("ConsultaContactos")]
        public async Task<ActionResult<IList<ContactoDto>>> ConsultaContactos()
        {
            try
            {
                IList<ContactoDto> listContacto = new List<ContactoDto>();
                listContacto = await contactoBusisness.ConsultaContactos();
                return Ok(new { listContacto });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// APi para registrar un contacto 
        /// </summary>
        /// <param name="contactoModel">Modelo con la información del contacot a registrar</param>
        /// <returns>Mensaje con el estatus de la acción </returns>
        /// <remarks>
        /// Sample Request
        /// 
        ///     POST
        ///     {
        ///         "nombre": "David",
        ///         "apellidos": "Montes Rodriguez",
        ///         "telefono": "5534443491",
        ///         "email": "davidmr_1312@hotmail.com",
        ///         "direccion": "Rocio #5 San Juan Bosco Atizapan de Zaragoza",
        ///         "categoriaTel": "Celular"
        ///     }
        ///     
        /// Sample Response
        /// 
        ///     {
        ///         "status": true,
        ///         "message": "El contacto se registro con éxito"
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("RegistraContacto")]
        public async Task<IActionResult> RegistraContacto(RegistraContactoModel contactoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactoDto contacto = null;
                    ///Registra usuario en BD y valida si ocurrio un error
                    contacto = await contactoBusisness.RegistraContactoBusisness(mapper.Map<RegistraContactoModel, ContactoDto>(contactoModel));
                    if (contacto.Validations.Count > 0)
                        return Ok(new { status = false, message = contacto.Validations[0].ErrorMessage });

                    return Ok(new { status = true,  message  = "El contacto se registro con éxito" });
                }
                else
                    return BadRequest(ModelState);


            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// APi para actualizar un contacto 
        /// </summary>
        /// <param name="contactoModel">Modelo con la información del contacot a actualizar</param>
        /// <returns>Mensaje con el estatus de la acción </returns>
        /// <remarks>
        /// Sample Request
        /// 
        ///     PUT
        ///     {
        ///         "contactoId": 1,
        ///         "nombre": "David Israel",
        ///         "apellidos": "Montes Rodriguez",
        ///         "telefono": "5534443491",
        ///         "email": "davidmr_1312@hotmail.com",
        ///         "direccion": "Rocio #5 San Juan Bosco Atizapan de Zaragoza",
        ///         "categoriaTel": "Celular"
        ///     }
        ///     
        /// Sample Response
        ///     
        ///     {
        ///         "status": false,
        ///         "message": "El contacto se actualizo con éxito"
        ///     }
    /// </remarks>
    [HttpPut]
        [Route("ActualizaContacto")]
        public async Task<IActionResult> ActualizaContacto(ActualizaContactoModel contactoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactoDto contacto = null;
                    ///Registra usuario en BD y valida si ocurrio un error
                    contacto = await contactoBusisness.ActualizaContactoBusisness(mapper.Map<ActualizaContactoModel, ContactoDto>(contactoModel));
                    if (contacto.Validations.Count > 0)
                        return Ok(new { status = false, message = contacto.Validations[0].ErrorMessage });

                    return Ok(new { status = true, message = "El contacto se actualizo con éxito" });
                }
                else
                    return BadRequest(ModelState);


            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// APi para actualizar un contacto 
        /// </summary>
        /// <param name="IdContacto"> Identificador unico del contacto</param>
        /// <returns>Mensaje con el estatus de la acción </returns>
        /// <remarks>
        /// Sample Request
        /// 
        ///     DELETE
        ///     IdContacto = 1
        ///     
        /// Sample Response
        /// 
        ///     {
        ///         "status": true,
        ///         "message": "El contacto se elimino con éxito"
        ///     }
        /// </remarks>
        [HttpDelete]
        [Route("EliminaContacto")]
        public async Task<IActionResult> EliminaContacto(int IdContacto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactoDto contacto = null;
                    ///Registra usuario en BD y valida si ocurrio un error
                    contacto = await contactoBusisness.EliminaontactoBusisness(IdContacto);
                    if (contacto.Validations.Count > 0)
                        return Ok(new { status = false, message = contacto.Validations[0].ErrorMessage });

                    return Ok(new { status = true, message = "El contacto se elimino con éxito" });
                }
                else
                    return BadRequest(ModelState);


            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
