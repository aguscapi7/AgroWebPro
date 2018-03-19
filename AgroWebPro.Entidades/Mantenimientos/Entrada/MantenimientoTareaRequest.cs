using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class MantenimientoTareaRequest:EntradaBase
    {        
        public int tipoOperacion { get; set; }
        public Guid idTarea { get; set; }
        public Guid idUsuario { get; set; }
        public Guid idTerreno { get; set; }
        public DateTime fechaAsignacion { get; set; }
        public Guid asignadaPor { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion { get; set; }
        public decimal horasEstimadas { get; set; }
        public string resumen { get; set; }
        public string observaciones { get; set; }
        public int idTipoTarea { get; set; }
        public bool activa { get; set; }
    }
}
