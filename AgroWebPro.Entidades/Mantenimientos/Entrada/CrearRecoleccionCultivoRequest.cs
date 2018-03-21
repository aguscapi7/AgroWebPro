using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class CrearRecoleccionCultivoRequest:EntradaBase
    {
        public Guid idCultivo { get; set; }
        public Guid idUsuarioRecolecta { get; set; }
        public decimal kilogramosPrimera { get; set; }
        public decimal kilogramosSegunda { get; set; }
        public decimal kilogramosRechazo { get; set; }
        public string causaRechazo { get; set; }
        public string codigoVerificadorSupervisor { get; set; }
        public Guid IdUsuarioSupervisor { get; set; }
    }
}
