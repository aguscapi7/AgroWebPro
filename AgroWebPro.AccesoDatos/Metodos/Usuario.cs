using AgroWebPro.AccesoDatos.Interfaces;
using AgroWebPro.Entidades;
using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
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
    public class Usuario:IUsuario
    {
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

        public ConsultarUsuarioLoginResponse ConsultarUsuarioLogin(ConsultarUsuarioLoginRequest request)
        {
            ConsultarUsuarioLoginResponse response = new ConsultarUsuarioLoginResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaUsuarioLogin = modelo.PA_ConsultarUsuarioLogin(
                                                     request.correo
                                                    , request.password
                                                    , request.olvido
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

        public ConsultarUsuarioResponse ConsultarUsuario(ConsultarUsuarioRequest request)
        {
            ConsultarUsuarioResponse response = new ConsultarUsuarioResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaUsuario = modelo.PA_ConsultarUsuario(
                                                     request.idUsuario
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
        public ConsultarTestResponse ConsultarTest(ConsultarTestRequest request)
        {
            ConsultarTestResponse response = new ConsultarTestResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaNombres = modelo.PA_ConsultarTest(
                                                     estado
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

        public MantenimientoTestResponse MantenimientoTest(MantenimientoTestRequest request)
        {
            MantenimientoTestResponse response = new MantenimientoTestResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    modelo.PA_MantenimientoTest(request.tipoOperacion
                                                    , request.id
                                                    , request.nombre
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
