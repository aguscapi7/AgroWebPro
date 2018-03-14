using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class MantenimientoTerrenoRequest:EntradaBase
    {
        public int tipoOperacion { get; set; }
        public Guid idTerreno { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string coordenadas { get; set; }
        public Guid idCultivo { get; set; }
        public Guid ingresadoPor { get; set; }
        public bool activo { get; set; }
        public bool actualizarCoordenadas { get; set; }
        public Guid idEmpresa { get; set; }
    }
}
