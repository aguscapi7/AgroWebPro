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
    
    public partial class PA_ConsultarTareasUsuario_Result
    {
        public System.Guid IdTarea { get; set; }
        public System.Guid IdUsuario { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidosEmpleado { get; set; }
        public System.Guid IdTerreno { get; set; }
        public string NombreTerreno { get; set; }
        public System.DateTime FechaAsignacion { get; set; }
        public System.Guid AsignadaPor { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFinalizacion { get; set; }
        public decimal HorasEstimadas { get; set; }
        public string Resumen { get; set; }
        public string Observaciones { get; set; }
        public bool Activa { get; set; }
        public int IdTipoTarea { get; set; }
        public string NombreTarea { get; set; }
        public bool Recoleccion { get; set; }
    }
}
