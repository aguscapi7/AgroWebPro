using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class MantenimientoRecoleccionRequest:EntradaBase
    {

        public Guid idTarea { get; set; }
        public Guid idUsuarioRecolecta { get; set; }
        public decimal kilogramosPrimera { get; set; }
        public decimal kilogramosSegunda { get; set; }
        public decimal kilogramosRechazo { get; set; }
        public string causaRechazo { get; set; }
        public Nullable<Guid> idUsuarioSupervisor { get; set; }

    }
}
