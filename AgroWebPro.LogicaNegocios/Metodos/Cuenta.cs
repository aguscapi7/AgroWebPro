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
    public class Cuenta:ICuenta
    {
        AccesoDatos.Interfaces.ICuenta venta;

        public Cuenta()
        {
            venta = new AccesoDatos.Metodos.Cuenta();
        }

        public MantenimientoMovimientoResponse MantenimientoMovimiento(MantenimientoMovimientoRequest request)
        {
            MantenimientoMovimientoResponse response = new MantenimientoMovimientoResponse();
            try
            {
                response = venta.MantenimientoMovimiento(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarMovimientosResponse ConsultarMovimientos(ConsultarMovimientosRequest request)
        {
            ConsultarMovimientosResponse response = new ConsultarMovimientosResponse();
            try
            {
                response = venta.ConsultarMovimientos(request);
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
