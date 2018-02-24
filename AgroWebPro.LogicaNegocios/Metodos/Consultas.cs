using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.LogicaNegocios.Metodos
{
    public class Consultas
    {
        public ConsultarZonasHorariasResponse ConsultarZonasHorarias(ConsultarZonasHorariasRequest request)
        {
            AccesoDatos.Metodos.Consultas mantenimiento = new AccesoDatos.Metodos.Consultas();
            ConsultarZonasHorariasResponse response = new ConsultarZonasHorariasResponse();
            try
            {
                response = mantenimiento.ConsultarZonasHorarias(request);
            }
            catch (Exception ex)
            {
                response.Estado = Constantes.EstadoError;
                response.Mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }
    }
}
