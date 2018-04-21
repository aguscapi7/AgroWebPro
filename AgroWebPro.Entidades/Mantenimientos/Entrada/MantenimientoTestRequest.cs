using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class MantenimientoTestRequest:EntradaBase
    {
        public int tipoOperacion { get; set; }
        public string nombre { get; set; }
        public int id { get; set; }
    }
}
