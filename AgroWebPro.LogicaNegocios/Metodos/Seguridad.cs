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
    public class Seguridad:ISeguridad
    {
        AccesoDatos.Interfaces.ISeguridad seguridad;

        public Seguridad()
        {
            seguridad = new AccesoDatos.Metodos.Seguridad();
        }

        public ConsultarOpcionesRolResponse ConsultarOpcionesRol(ConsultarOpcionesRolRequest request)
        {
            ConsultarOpcionesRolResponse response = new ConsultarOpcionesRolResponse();
            try
            {
                response = seguridad.ConsultarOpcionesRol(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarOpcionUsuarioResponse ConsultarOpcionUsuario(ConsultarOpcionUsuarioRequest request)
        {
            ConsultarOpcionUsuarioResponse response = new ConsultarOpcionUsuarioResponse();
            try
            {
                response = seguridad.ConsultarOpcionUsuario(request);
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
