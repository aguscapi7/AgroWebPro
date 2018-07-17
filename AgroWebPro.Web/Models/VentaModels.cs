using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class VentaModels
    {
        public List<ClienteProveedorModels> listaClienteProveedor { get; set; }
        public List<CultivoModels> listaCultivos { get; set; }
        MaestroVentaModels maestroVentas { get; set; }

        public VentaModels()
        {
            maestroVentas = new MaestroVentaModels();
        }

    }

    public class DetalleVentaModels
    {
        public Guid idDetalleVenta { get; set; }
        public Guid idMaestroVenta { get; set; }
        public Guid idCultivo { get; set; }
        public decimal cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal precioTotal { get; set; }
    }

    public class MaestroVentaModels
    {
        public Guid idMaestroVenta { get; set; }
        public Guid idCliente { get; set; }
        public Guid idVendedor { get; set; }
        public decimal montoDescuentos { get; set; }
        public decimal montoImpuestos { get; set; }
        public decimal montoTotal { get; set; }
        public bool contado { get; set; }
        public decimal plazoCredito { get; set; }
        public Guid idEmpresa { get; set; }
        public int idMoneda { get; set; }
        public List<DetalleVentaModels> listaDetalleVenta { get; set; }
    }
}