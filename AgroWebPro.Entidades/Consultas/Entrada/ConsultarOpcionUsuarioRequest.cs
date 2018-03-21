using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Consultas.Entrada
{
    public class ConsultarOpcionUsuarioRequest:EntradaBase
    {
        public Guid idUsuario { get; set; }
        public string controladdor { get; set; }
        public string accion { get; set; }
    }
}
