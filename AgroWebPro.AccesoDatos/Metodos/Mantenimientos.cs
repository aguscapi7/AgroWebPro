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
                                                    , request.idZonaHoraria
                                                    , request.direccion
                                                    , request.activa
                                                    , estado
                                                    , mensaje);
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

        public MantenimientoUsuarioResponse MantenimientoUsuario(MantenimientoUsuarioRequest request)
        {
            MantenimientoUsuarioResponse response = new MantenimientoUsuarioResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    modelo.PA_MantenimientoUsuario(request.tipoOperacion
                                                    , request.idUsuario
                                                    , request.nombre
                                                    , request.apellidos
                                                    , request.correo
                                                    , request.password
                                                    , request.direccion
                                                    , request.telefono
                                                    , request.ingresadoPor
                                                    , request.rol
                                                    , request.activo
                                                    , request.idEmpresa
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

        public MantenimientoCultivoResponse MantenimientoCultivo(MantenimientoCultivoRequest request)
        {
            MantenimientoCultivoResponse response = new MantenimientoCultivoResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    modelo.PA_MantenimientoCultivo(request.tipoOperacion
                                                    , request.idCultivo 
                                                    , request.nombreCultivo
                                                    , request.descripcionCultivo
                                                    , request.idFamilia
                                                    , request.ingresadoPor
                                                    , request.activo
                                                    , request.idEmpresa
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
