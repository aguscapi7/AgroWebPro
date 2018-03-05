using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Consultas.Entrada
{
    public class ConsultarFamiliasRequest:EntradaBase
    {
        public string correo { get; set; }
        public string password { get; set; }
    }
}
