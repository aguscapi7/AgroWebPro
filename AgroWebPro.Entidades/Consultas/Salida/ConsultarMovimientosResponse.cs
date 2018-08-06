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
    public class ConsultarMovimientosResponse:RespuestaBase
    {
        public List<PA_ConsultarMovimientos_Result> listaMovimientos { get; set; }
    }
}
