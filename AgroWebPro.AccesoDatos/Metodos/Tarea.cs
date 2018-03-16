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

namespace AgroWebPro.AccesoDatos.Metodos
{
    public class Tarea:ITarea
    {
        public MantenimientoAvanceTareaUsuarioResponse MantenimientoAvanceTareaUsuario(MantenimientoAvanceTareaUsuarioRequest request)
        {
            MantenimientoUsuarioResponse response = new MantenimientoUsuarioResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    modelo.PA_MantenimientoAvanceTareaUsuario(request.tipoOperacion
                                                    , request.idTarea
                                                    , request.idEstadoTarea
                                                    , request.observaciones
                                                    , request.horasEstimadas
                                                    , estado
                                                    , mensaje);
                    if (estado.Value.ToString().Equals(Constantes.EstadoError))
                    {
                        response.estado = Constantes.EstadoError;
                        response.mensaje = Constantes.MensajeErrorAccesoDatos + mensaje.Value.ToString();
                    }
                    else if (estado.Value.ToString().Equals(Constantes.EstadoErrorCustom))
                    {
                        response.estado = Constantes.EstadoErrorCustom;
                        response.mensaje = mensaje.Value.ToString();
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
