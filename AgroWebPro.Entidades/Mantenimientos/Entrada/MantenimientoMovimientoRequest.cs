using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class MantenimientoMovimientoRequest:EntradaBase
    {        
        public int tipoOperacion { get; set; }
        public Guid idMovimiento { get; set; }
        public DateTime fecha { get; set; }
        public int annio { get; set; }
        public int mes { get; set; }
        public string observaciones { get; set; }
        public Guid idCategoriaMovimiento { get; set; }
        public decimal monto { get; set; }
        public Guid idEmpresa { get; set; }
        public Guid idUsuario { get; set; }
        public bool ingreso { get; set; }
        public bool activo { get; set; }
    }
}
