using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class AvanceTareaModels
    {
        public decimal kilogramosPrimera { get; set; }
        public decimal kilogramosSegunda { get; set; }
        public decimal kilogramosRechazo { get; set; }
        public string causaRechazo { get; set; }
        public Guid idUsuarioSupervisor { get; set; }
        public string codigoVerificadorSupervisor { get; set; }
    }
}