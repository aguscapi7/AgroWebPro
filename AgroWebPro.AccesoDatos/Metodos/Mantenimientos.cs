using AgroWebPro.Entidades;
using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.AccesoDatos.Metodos
{
    public class Mantenimientos
    {
        public MantenimientoEmpresaResponse MantenimientoEmpresa(MantenimientoEmpresaRequest request)
        {
            MantenimientoEmpresaResponse response = new MantenimientoEmpresaResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    modelo.PA_MantenimientoEmpresa(request.tipoOperacion
                                                    , request.idEmpresa
                                                    , request.nombreEmpresa
                                                    , request.descripcionEmpresa
                                                    , request.telefono
                                                    , request.cedulaJuridica
                                                    , request.diferenciaUtc
                                                    , request.direccion
                                                    , request.activa
                                                    , estado
                                                    , mensaje);
                    if (estado.Value.ToString().Equals(Constantes.EstadoError))
                    {
                        response.Estado = Constantes.EstadoError;
                        response.Mensaje = Constantes.MensajeErrorAccesoDatos + mensaje.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Estado = Constantes.EstadoError;
                response.Mensaje = Constantes.MensajeErrorAccesoDatos + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }
    }
}
