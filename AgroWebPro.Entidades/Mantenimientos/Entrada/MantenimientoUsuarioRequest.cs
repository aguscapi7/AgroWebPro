using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class MantenimientoUsuarioRequest:EntradaBase
    {
        public int tipoOperacion { get; set; }
        public Guid idUsuario { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public Nullable<Guid> ingresadoPor { get; set; }
        public Guid rol { get; set; }
        public bool activo { get; set; }
    }
}
