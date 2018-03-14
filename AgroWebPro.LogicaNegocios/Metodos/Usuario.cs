using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using AgroWebPro.LogicaNegocios.Interfaces;

namespace AgroWebPro.LogicaNegocios.Metodos
{
    public class Usuario:IUsuario
    {
        AccesoDatos.Interfaces.IUsuario usuario;

        public Usuario()
        {
            usuario = new AccesoDatos.Metodos.Usuario();
        }

        public ConsultarUsuarioLoginResponse ConsultarUsuarioLogin(ConsultarUsuarioLoginRequest request)
        {            
            ConsultarUsuarioLoginResponse response = new ConsultarUsuarioLoginResponse();
            try
            {
                response = usuario.ConsultarUsuarioLogin(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public MantenimientoUsuarioResponse MantenimientoUsuario(MantenimientoUsuarioRequest request)
        {
            MantenimientoUsuarioResponse response = new MantenimientoUsuarioResponse();
            try
            {
                response = usuario.MantenimientoUsuario(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarUsuarioResponse ConsultarUsuario(ConsultarUsuarioRequest request)
        {
            ConsultarUsuarioResponse response = new ConsultarUsuarioResponse();
            try
            {
                response = usuario.ConsultarUsuario(request);
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
