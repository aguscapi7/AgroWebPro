using AgroWebPro.Entidades.Consultas.Salida;
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
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string fechaIngreso { get; set; }
        public string telefono { get; set; }
        public Guid idEmpresa { get; set; }
        public string nombreEmpresa { get; set; }
        public bool esCliente { get; set; }
        public List<ClienteProveedorModels> listaClienteProveedor { get; set; }

        public void CopiarClientesProveedores(ConsultarClientesProveedoresEmpresaResponse clientesProveedores)
        {

        }
    }
}