namespace DTO.ENTITIES
{
    /// <summary>
    /// DTO para manipular la informacion del contacto 
    /// </summary>
    public class ContactoDto : BaseValidateBulkOperationDto
    {
       /// <summary>
       /// Identificador unico del usuario 
       /// </summary>
        public int ContactoId { get; set; }
        /// <summary>
        ///  NOMBRE DEL CONTACTO	
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// APELLIDOS DEL CONTACTO
        /// </summary>
        public string Apellidos { get; set; }
        /// <summary>
        /// NUMERO TELEFONICO 
        /// </summary>
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

