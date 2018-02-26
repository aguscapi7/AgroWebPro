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
            estado = "00";
            mensaje = string.Empty;
        }

        public string estado { get; set; }
        public string mensaje { get; set; }
    }
}
