using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class ClienteProveedorModels
    {
        public Guid idClienteProveedor { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string nombre { get; set; }
        [Display(Name = "Apellidos")]
        public string apellidos { get; set; }
        [Display(Name = "Correo")]
        public string correo { get; set; }
        [Display(Name = "Dirección")]
        public string direccion { get; set; }
        public string fechaIngreso { get; set; }
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }
        public Guid idEmpresa { get; set; }
        public string nombreEmpresa { get; set; }

        [Display(Name = "Tipo")]
        public bool esCliente { get; set; }
        public List<ClienteProveedorModels> listaClienteProveedor { get; set; }
        public bool errorValidacion { get; set; }
        
        public void CopiarClientesProveedores(ConsultarClientesProveedoresEmpresaResponse clientesProveedores)
        {
            try
            {
                listaClienteProveedor = new List<ClienteProveedorModels>();
                if (clientesProveedores != null && clientesProveedores.estado.Equals(Constantes.EstadoCorrecto) && clientesProveedores.listaClientesProveedores.Count >= 0)
                {
                    ClienteProveedorModels clienteProveedor = null;
                    foreach (var clienteProveedorItem in clientesProveedores.listaClientesProveedores)
                    {
                        clienteProveedor = new ClienteProveedorModels();
                        clienteProveedor.idClienteProveedor = clienteProveedorItem.IdClienteProveedor;
                        clienteProveedor.nombre = clienteProveedorItem.Nombre;
                        clienteProveedor.apellidos = clienteProveedorItem.Apellidos;
                        clienteProveedor.correo = clienteProveedorItem.Correo;
                        clienteProveedor.esCliente = clienteProveedorItem.EsCliente;
                        clienteProveedor.telefono = clienteProveedorItem.Telefono;
                        clienteProveedor.direccion = clienteProveedorItem.Direccion == null ? "" : clienteProveedorItem.Direccion;
                        listaClienteProveedor.Add(clienteProveedor);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}