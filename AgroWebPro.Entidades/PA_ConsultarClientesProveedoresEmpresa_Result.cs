//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgroWebPro.Entidades
{
    using System;
    
    public partial class PA_ConsultarClientesProveedoresEmpresa_Result
    {
        public System.Guid IdClienteProveedor { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public Nullable<System.Guid> IngresadoPor { get; set; }
        public bool Activo { get; set; }
        public System.Guid IdEmpresa { get; set; }
        public bool EsCliente { get; set; }
    }
}
