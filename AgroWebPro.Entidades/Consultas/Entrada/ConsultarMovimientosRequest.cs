using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Consultas.Entrada
{
    public class ConsultarMovimientosRequest : EntradaBase
    {
        public int annio { get; set; }
        public int mes { get; set; }
        public bool busquedaFechas { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin{ get; set; }
        public Guid idEmpresa { get; set; }
    }
}
