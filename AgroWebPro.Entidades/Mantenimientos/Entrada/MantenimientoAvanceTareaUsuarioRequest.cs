using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class MantenimientoAvanceTareaUsuarioRequest:EntradaBase
    {
        public int tipoOperacion { get; set; }
        public Guid idTarea { get; set; }
        public int idEstadoTarea { get; set; }
        public string observaciones { get; set; }
        public DateTime fechaAvance { get; set; }
        public decimal horasEstimadas { get; set; }
    }
}
