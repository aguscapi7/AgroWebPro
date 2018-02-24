using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Runtime.Serialization;

namespace AgroWebPro.Entidades.Generales
{
    public class RespuestaBase
    {

        public RespuestaBase()
        {
            Estado = "00";
            Mensaje = string.Empty;
        }

        public string Estado { get; set; }
        public string Mensaje { get; set; }
    }
}
