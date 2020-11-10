using API.SHARED;
using DTO.ENTITIES;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace API.DATA
{
    public class AgendaData
    {
        /// Inyección de dependencias  
        private readonly IConfiguration _configuration;
        private string ConnectionString = string.Empty;
        ErrorViewModel errorView ;

        public AgendaData(IConfiguration configuration) {
            _configuration = configuration;
            ConnectionString = _configuration["ConnectionStrings:connectionAgenda"];
        }

        /// <summary>
        /// Metodo para registrar un contacto de la agenda 
        /// </summary>
        /// <param name="contacto">Modelo con la informacion del contacto a registrar </param>
        /// <returns>Retorna estatus de la accion </returns>
        public async Task<ContactoDto> RegistraContactoData(ContactoDto contacto)
        {
            ContactoDto objContacto = new ContactoDto();

            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var command = connection.CreateSpCommand(_configuration["StoresProcedures:REGISTRA_CONTACTO"]))
                    {
                        command.AddSqlParameter("@Nombre"       , contacto.Nombre);
                        command.AddSqlParameter("@Apellidos"    , contacto.Apellidos);
                        command.AddSqlParameter("@Telefono"     , contacto.Telefono);
                        command.AddSqlParameter("@Email"        , contacto.Email);
                        command.AddSqlParameter("@Direccion"    , contacto.Direccion);
                        command.AddSqlParameter("@CategoriaTel" , contacto.CategoriaTel); 

                        ///Valida que la conexion este abierta 
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        ///Inicializa nueva instancia de la clase SqlDataAdapter con el objeto SqlCommand como parametro 
                        var _adapter = new SqlDataAdapter(command);
                        var _dataSet = new DataSet();
                        _adapter.Fill(_dataSet);

                        foreach (DataRow row in _dataSet.Tables[0].Rows)
                        {
                            errorView = new ErrorViewModel()
                            {
                                ErrorMessage = row.ItemArray[0].ToString()
                            };

                            objContacto.Validations.Add(errorView);
                            connection.Close();
                            return await Task.FromResult(objContacto);
                        }

                        //foreach (DataRow row in _dataSet.Tables[1].Rows)
                        //{
                        //    _
                        //}
                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(objContacto);
        }

        /// <summary>
        /// Metodo para actualizar un contacto de la agenda 
        /// </summary>
        /// <param name="contacto">Modelo con la informacion del contacto a actualizar </param>
        /// <returns>Retorna estatus de la accion </returns>
        public async Task<ContactoDto> ActualizaContactoData(ContactoDto contacto)
        {
            ContactoDto objContacto = new ContactoDto();

            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var command = connection.CreateSpCommand(_configuration["StoresProcedures:SP_ACTUALIZA_CONTACTO"]))
                    {
                        command.AddSqlParameter("@ContactoId"       , contacto.ContactoId);
                        command.AddSqlParameter("@Nombre"           , contacto.Nombre);
                        command.AddSqlParameter("@Apellidos"        , contacto.Apellidos);
                        command.AddSqlParameter("@Telefono"         , contacto.Telefono);
                        command.AddSqlParameter("@Email"            , contacto.Email);
                        command.AddSqlParameter("@Direccion"        , contacto.Direccion);
                        command.AddSqlParameter("@CategoriaTel"     , contacto.CategoriaTel);

                        ///Valida que la conexion este abierta 
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        ///Inicializa nueva instancia de la clase SqlDataAdapter con el objeto SqlCommand como parametro 
                        var _adapter = new SqlDataAdapter(command);
                        var _dataSet = new DataSet();
                        _adapter.Fill(_dataSet);

                        foreach (DataRow row in _dataSet.Tables[0].Rows)
                        {
                            errorView = new ErrorViewModel()
                            {
                                ErrorMessage = row.ItemArray[0].ToString()
                            };

                            objContacto.Validations.Add(errorView);
                            connection.Close();
                            return await Task.FromResult(objContacto);
                        } 
                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(objContacto);
        }

        /// <summary>
        /// Metodo para eliminar un contacto de la agenda 
        /// </summary>
        /// <param name="IdContacto">Identificador unico del contacto </param>
        /// <returns>Retorna estatus de la accion </returns>
        public async Task<ContactoDto> EliminaontactoData(int IdContacto)
        {
            ContactoDto objContacto = new ContactoDto();

            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var command = connection.CreateSpCommand(_configuration["StoresProcedures:ELIMINA_CONTACTO"]))
                    {
                        command.AddSqlParameter("@ContactoId", IdContacto);

                        ///Valida que la conexion este abierta 
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        ///Inicializa nueva instancia de la clase SqlDataAdapter con el objeto SqlCommand como parametro 
                        var _adapter = new SqlDataAdapter(command);
                        var _dataSet = new DataSet();
                        _adapter.Fill(_dataSet);

                        foreach (DataRow row in _dataSet.Tables[0].Rows)
                        {
                            errorView = new ErrorViewModel()
                            {
                                ErrorMessage = row.ItemArray[0].ToString()
                            };

                            objContacto.Validations.Add(errorView);
                            connection.Close();
                            return await Task.FromResult(objContacto);
                        } 
                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(objContacto);
        }

        /// <summary>
        /// Metodo para conultar todas las categorias 
        /// </summary>
        /// <returns>Retorna lista de categorias</returns>
        public async Task<IList<CategoriaTelDto>> ConsultaCAtegoriasTel()
        {
            IList<CategoriaTelDto> listCategorias = new List<CategoriaTelDto>();
            CategoriaTelDto categoriaTel = null;
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = @"SELECT [CategoriaTelId],[Descripcion] FROM [dbo].[TBL_CATEGORIA]";
                    command.CommandTimeout = 15;
                    command.CommandType = CommandType.Text;

                    ///Inicializa nueva instancia de la clase SqlDataAdapter con el objeto SqlCommand como parametro 
                    var _adapter = new SqlDataAdapter(command);
                    var _dataSet = new DataSet();
                    _adapter.Fill(_dataSet);

                    ///Valida si existio un error al actualizar domicilio
                    foreach (DataRow row in _dataSet.Tables[0].Rows)
                    {
                         categoriaTel = new CategoriaTelDto()
                        {
                            CategoriaTelId  = int.Parse(row.ItemArray[0].ToString()),
                            Descripcion     = row.ItemArray[1].ToString()
                        };
                        listCategorias.Add(categoriaTel);
                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(listCategorias);
        }

        /// <summary>
        /// Metodo para consiltar todos los contactos
        /// </summary>
        /// <returns>Lista de contactos</returns>
        public async Task<IList<ContactoDto>> ConsultaContactos()
        {
            IList<ContactoDto> listContactos = new List<ContactoDto>();
            ContactoDto contacto = null;
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(15), TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = @"SELECT [ContactoId],c.[Nombre],ISNULL(c.[Apellidos],''),c.[Telefono],ISNULL(c.[Email],''),ISNULL(c.[Direccion],''),cat.[Descripcion]
	                                         FROM [dbo].[TBL_CONTACTO]  c
		                                        INNER JOIN [dbo].[TBL_CATEGORIA] cat ON	cat.CategoriaTelId = c.CategoriaTelId";
                    command.CommandTimeout = 15;
                    command.CommandType = CommandType.Text;

                    ///Inicializa nueva instancia de la clase SqlDataAdapter con el objeto SqlCommand como parametro 
                    var _adapter = new SqlDataAdapter(command);
                    var _dataSet = new DataSet();
                    _adapter.Fill(_dataSet);

                    ///Valida si existio un error al actualizar domicilio
                    foreach (DataRow row in _dataSet.Tables[0].Rows)
                    {
                        contacto = new ContactoDto()
                        {
                            ContactoId      = int.Parse(row.ItemArray[0].ToString()),
                            Nombre          = row.ItemArray[1].ToString(),
                            Apellidos       = row.ItemArray[1].ToString(),
                            Telefono        = row.ItemArray[1].ToString(),
                            Email           = row.ItemArray[1].ToString(),
                            Direccion       = row.ItemArray[1].ToString(),
                            CategoriaTel    = row.ItemArray[1].ToString()
                        };
                        listContactos.Add(contacto);
                    }
                    connection.Close();
                }
                transactionScope.Complete();
            }
            return await Task.FromResult(listContactos);
        }

    }
}
