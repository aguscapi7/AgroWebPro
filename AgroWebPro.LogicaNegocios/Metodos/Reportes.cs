using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.LogicaNegocios.Interfaces;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.LogicaNegocios.Metodos
{
    public class Reportes:IReportes
    {
        AccesoDatos.Interfaces.IReportes reportes;

        public Reportes()
        {
            reportes = new AccesoDatos.Metodos.Reportes();
        }

        public ConsultarReporteVentasResponse ConsultarReporteVentas(ConsultarReporteVentasRequest request)
        {
            ConsultarReporteVentasResponse response = new ConsultarReporteVentasResponse();
            try
            {
                response = reportes.ConsultarReporteVentas(request);
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
