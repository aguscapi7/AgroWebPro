
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
    public class Catalogos:ICatalogos
    {
        AccesoDatos.Interfaces.ICatalogos catalogos;
        public Catalogos()
        {
            catalogos = new AccesoDatos.Metodos.Catalogos();
        }
        public ConsultarUnidadesVentaResponse ConsultarUnidadesVenta(ConsultarUnidadesVentaRequest request)
        {
            ConsultarUnidadesVentaResponse response = new ConsultarUnidadesVentaResponse();
            try
            {
                response = catalogos.ConsultarUnidadesVenta(request);
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
            ConsultarRolesResponse response = new ConsultarRolesResponse();
            try
            {
                response = catalogos.ConsultarRoles(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarMonedasResponse ConsultarMonedas(ConsultarMonedasRequest request)
        {
            ConsultarMonedasResponse response = new ConsultarMonedasResponse();
            try
            {
                response = catalogos.ConsultarMonedas(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;

        }
        public ConsultarZonasHorariasResponse ConsultarZonasHorarias(ConsultarZonasHorariasRequest request)
        {
            ConsultarZonasHorariasResponse response = new ConsultarZonasHorariasResponse();
            try
            {
                response = catalogos.ConsultarZonasHorarias(request);
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
            ConsultarFamiliasResponse response = new ConsultarFamiliasResponse();
            try
            {
                response = catalogos.ConsultarFamilias(request);
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
