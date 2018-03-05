using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class MantenimientoCultivoRequest:EntradaBase
    {
        public int tipoOperacion { get; set; }
        public Guid idCultivo { get; set; }
        public string nombreCultivo { get; set; }
        public string descripcionCultivo { get; set; }
        public Guid idFamilia { get; set; }
        public Guid ingresadoPor { get; set; }
        public bool activo { get; set; }
        public Guid idEmpresa { get; set; }
        
    }
}
