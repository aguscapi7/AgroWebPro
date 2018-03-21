using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using AgroWebPro.LogicaNegocios.Interfaces;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.LogicaNegocios.Metodos
{
    public class Venta:IVenta
    {
        AccesoDatos.Interfaces.IVenta venta;

        public Venta()
        {
            venta = new AccesoDatos.Metodos.Venta();
        }

        public MantenimientoClienteProveedorResponse MantenimientoClienteProveedor(MantenimientoClienteProveedorRequest request)
        {
            MantenimientoClienteProveedorResponse response = new MantenimientoClienteProveedorResponse();
            try
            {
                response = venta.MantenimientoClienteProveedor(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public CrearVentaResponse CrearVenta(CrearVentaRequest request)
        {
            CrearVentaResponse response = new CrearVentaResponse();
            try
            {
                response = venta.CrearVenta(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarClientesProveedoresEmpresaResponse ConsultarClientesProveedoresEmpresa(ConsultarClientesProveedoresEmpresaRequest request)
        {
            ConsultarClientesProveedoresEmpresaResponse response = new ConsultarClientesProveedoresEmpresaResponse();
            try
            {
                response = venta.ConsultarClientesProveedoresEmpresa(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

    }
}
