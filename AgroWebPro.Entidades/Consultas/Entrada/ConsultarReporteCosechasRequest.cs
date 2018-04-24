using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Consultas.Entrada
{
    public class ConsultarReporteCosechasRequest : EntradaBase
    {
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion { get; set; }
        public Guid idEmpresa { get; set; }
        public Nullable<Guid> idCultivo { get; set; }
    }
}
