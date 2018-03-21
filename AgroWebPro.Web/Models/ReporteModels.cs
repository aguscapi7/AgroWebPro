using AgroWebPro.Entidades.Consultas.Salida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class ReporteModels
    {
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public Guid idCultivo { get; set; }
        public Guid idEmpresa { get; set; }
        public List<VentaModels> listaVentas { get; set; }

        public void CopiarReporteVentas(ConsultarReporteVentasResponse reporteVentasResponse)
        {

        }
    }
    
}