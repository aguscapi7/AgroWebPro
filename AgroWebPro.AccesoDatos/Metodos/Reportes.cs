using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroWebPro.Entidades;
using AgroWebPro.AccesoDatos.Interfaces;

namespace AgroWebPro.AccesoDatos.Metodos
{
    public class Reportes:IReportes
    {
        public ConsultarReporteVentasResponse ConsultarReporteVentas(ConsultarReporteVentasRequest request)
        {
            ConsultarReporteVentasResponse response = new ConsultarReporteVentasResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaReporteVentas = modelo.PA_ConsultarReporteVentas(
                                                     request.fechaInicio
                                                    ,request.fechaFin
                                                    ,request.idEmpresa
                                                    ,estado
                                                    , mensaje).ToList();
                    if (estado.Value.ToString().Equals(Constantes.EstadoError))
                    {
                        response.estado = Constantes.EstadoError;
                        response.mensaje = Constantes.MensajeErrorAccesoDatos + mensaje.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorAccesoDatos + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

    }
}
