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
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarUsuarioLoginResponse ConsultarUsuarioLogin(ConsultarUsuarioLoginRequest request)
        {
            AccesoDatos.Metodos.Consultas mantenimiento = new AccesoDatos.Metodos.Consultas();
            ConsultarUsuarioLoginResponse response = new ConsultarUsuarioLoginResponse();
            try
            {
                response = mantenimiento.ConsultarUsuarioLogin(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarFamiliasResponse ConsultarFamilias(ConsultarFamiliasRequest request)
        {
            AccesoDatos.Metodos.Consultas mantenimiento = new AccesoDatos.Metodos.Consultas();
            ConsultarFamiliasResponse response = new ConsultarFamiliasResponse();
            try
            {
                response = mantenimiento.ConsultarFamilias(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarCultivosEmpresaResponse ConsultarCultivosEmpresa(ConsultarCultivosEmpresaRequest request)
        {
            AccesoDatos.Metodos.Consultas mantenimiento = new AccesoDatos.Metodos.Consultas();
            ConsultarCultivosEmpresaResponse response = new ConsultarCultivosEmpresaResponse();
            try
            {
                response = mantenimiento.ConsultarCultivosEmpresa(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarEmpleadosEmpresaResponse ConsultarEmpleadosEmpresa(ConsultarEmpleadosEmpresaRequest request)
        {
            AccesoDatos.Metodos.Consultas mantenimiento = new AccesoDatos.Metodos.Consultas();
            ConsultarEmpleadosEmpresaResponse response = new ConsultarEmpleadosEmpresaResponse();
            try
            {
                response = mantenimiento.ConsultarEmpleadosEmpresa(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarRolesResponse ConsultarRoles(ConsultarRolesRequest request)
        {
            AccesoDatos.Metodos.Consultas mantenimiento = new AccesoDatos.Metodos.Consultas();
            ConsultarRolesResponse response = new ConsultarRolesResponse();
            try
            {
                response = mantenimiento.ConsultarRoles(request);
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
