using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class MantenimientoEmpresaRequest:EntradaBase
    {
        public int tipoOperacion { get; set; }
        public Guid idEmpresa { get; set; }
        public string nombreEmpresa { get; set; }
        public string descripcionEmpresa { get; set; }
        public string telefono { get; set; }
        public string cedulaJuridica { get; set; }
        public decimal diferenciaUtc { get; set; }
        public string direccion { get; set; }
        public bool activa { get; set; }
        
    }
}
