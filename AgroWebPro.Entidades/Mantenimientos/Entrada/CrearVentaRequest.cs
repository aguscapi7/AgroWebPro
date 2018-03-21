using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Mantenimientos.Entrada
{
    public class CrearVentaRequest:EntradaBase
    {
        public Guid idCliente { get; set; }
        public string detalleVenta { get; set; }
        public Guid idVendedor { get; set; }
        public decimal totalVenta { get; set; }
        public decimal totalDescuentos { get; set; }
        public decimal totalImpuestos { get; set; }
        public DateTime fechaVenta { get; set; }
        public decimal plazoCredito { get; set; }
    }
}
