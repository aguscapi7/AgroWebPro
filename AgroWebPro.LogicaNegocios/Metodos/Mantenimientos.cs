using AgroWebPro.AccesoDatos.Metodos;
using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.LogicaNegocios.Metodos
{
    public class Mantenimientos
    {
        public MantenimientoEmpresaResponse MantenimientoEmpresa(MantenimientoEmpresaRequest request)
        {
            AccesoDatos.Metodos.Mantenimientos mantenimiento = new AccesoDatos.Metodos.Mantenimientos();
            MantenimientoEmpresaResponse response = new MantenimientoEmpresaResponse();
            try
            {
                response = mantenimiento.MantenimientoEmpresa(request);
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
            AccesoDatos.Metodos.Mantenimientos mantenimiento = new AccesoDatos.Metodos.Mantenimientos();
            MantenimientoUsuarioResponse response = new MantenimientoUsuarioResponse();
            try
            {
                response = mantenimiento.MantenimientoUsuario(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public MantenimientoCultivoResponse MantenimientoCultivo(MantenimientoCultivoRequest request)
        {
            AccesoDatos.Metodos.Mantenimientos mantenimiento = new AccesoDatos.Metodos.Mantenimientos();
            MantenimientoCultivoResponse response = new MantenimientoCultivoResponse();
            try
            {
                response = mantenimiento.MantenimientoCultivo(request);
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
