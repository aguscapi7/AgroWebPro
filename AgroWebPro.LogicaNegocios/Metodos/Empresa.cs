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
    public class Empresa:IEmpresa
    {
        AccesoDatos.Interfaces.IEmpresa empresa;
        public Empresa()
        {
            empresa = new AccesoDatos.Metodos.Empresa();
        }

        public MantenimientoEmpresaResponse MantenimientoEmpresa(MantenimientoEmpresaRequest request)
        {
            MantenimientoEmpresaResponse response = new MantenimientoEmpresaResponse();
            try
            {
                response = empresa.MantenimientoEmpresa(request);
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
            ConsultarCultivosEmpresaResponse response = new ConsultarCultivosEmpresaResponse();
            try
            {
                response = empresa.ConsultarCultivosEmpresa(request);
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
            ConsultarEmpleadosEmpresaResponse response = new ConsultarEmpleadosEmpresaResponse();
            try
            {
                response = empresa.ConsultarEmpleadosEmpresa(request);
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
            MantenimientoCultivoResponse response = new MantenimientoCultivoResponse();
            try
            {
                response = empresa.MantenimientoCultivo(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public MantenimientoTerrenoResponse MantenimientoTerreno(MantenimientoTerrenoRequest request)
        {
            MantenimientoTerrenoResponse response = new MantenimientoTerrenoResponse();
            try
            {
                response = empresa.MantenimientoTerreno(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarEmpresaResponse ConsultarEmpresa(ConsultarEmpresaRequest request)
        {
            ConsultarEmpresaResponse response = new ConsultarEmpresaResponse();
            try
            {
                response = empresa.ConsultarEmpresa(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarTerrenoResponse ConsultarTerreno(ConsultarTerrenoRequest request)
        {
            ConsultarTerrenoResponse response = new ConsultarTerrenoResponse();
            try
            {
                response = empresa.ConsultarTerreno(request);
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
                response = empresa.ConsultarClientesProveedoresEmpresa(request);
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
