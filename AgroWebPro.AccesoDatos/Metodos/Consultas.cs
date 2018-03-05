using AgroWebPro.Entidades;
using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.AccesoDatos.Metodos
{
    public class Consultas
    {
        public ConsultarZonasHorariasResponse ConsultarZonasHorarias(ConsultarZonasHorariasRequest request)
        {
            ConsultarZonasHorariasResponse response = new ConsultarZonasHorariasResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaZonasHorarias = modelo.PA_ConsultarZonasHorarias(
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

        public ConsultarFamiliasResponse ConsultarFamilias(ConsultarFamiliasRequest request)
        {
            ConsultarFamiliasResponse response = new ConsultarFamiliasResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaFamilias = modelo.PA_ConsultarFamilias(
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

        public ConsultarCultivosEmpresaResponse ConsultarCultivosEmpresa(ConsultarCultivosEmpresaRequest request)
        {
            ConsultarCultivosEmpresaResponse response = new ConsultarCultivosEmpresaResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaCultivosEmpresa = modelo.PA_ConsultarCultivosEmpresa(
                                                    request.idEmpresa
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
