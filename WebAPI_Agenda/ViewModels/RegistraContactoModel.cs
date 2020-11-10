using System.ComponentModel.DataAnnotations;

namespace WebAPI_Agenda.ViewModels
{
    /// <summary>
    /// Modelo para recuperar la info del contacto 
    /// </summary>
    public class RegistraContactoModel
    { 
        /// <summary>
        ///  NOMBRE DEL CONTACTO	
        /// </summary>
        [Required(ErrorMessage = "Campo requerido")]
        public string Nombre { get; set; }
        /// <summary>
        /// APELLIDOS DEL CONTACTO
        /// </summary>
        public string Apellidos { get; set; }
        /// <summary>
        /// NUMERO TELEFONICO 
        /// </summary>
        [Required(ErrorMessage = "Campo requerido")]
        public string Telefono { get; set; }
        /// <summary>
        /// CORREO ELECTRONICO 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// DIRECCION DEL CONTACTO
        /// </summary>
        public string Direccion { get; set; }
        /// <summary>
        /// TIPO DE TELEFONO 	
        /// </summary>
        public string CategoriaTel { get; set; }
    }
}
