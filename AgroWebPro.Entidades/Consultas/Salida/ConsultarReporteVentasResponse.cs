using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Consultas.Salida
{
    [DataContract]
    public class ConsultarReporteVentasResponse:RespuestaBase
    {
        public List<PA_ConsultarReporteVentas_Result> listaReporteVentas { get; set; }
    }
}
