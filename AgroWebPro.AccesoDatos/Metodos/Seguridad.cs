using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroWebPro.Utilitarios;
using System.Data.Entity.Core.Objects;
using AgroWebPro.Entidades;
using AgroWebPro.AccesoDatos.Interfaces;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Entidades.Consultas.Entrada;

namespace AgroWebPro.AccesoDatos.Metodos
{
    public class Seguridad:ISeguridad
    {
        public ConsultarOpcionesRolResponse ConsultarOpcionesRol(ConsultarOpcionesRolRequest request)
        {
            ConsultarOpcionesRolResponse response = new ConsultarOpcionesRolResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaOpcionesRol = modelo.PA_ConsultarOpcionesRol(
                                                     request.idRol
                                                    , estado
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
