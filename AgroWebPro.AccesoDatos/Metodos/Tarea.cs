﻿using AgroWebPro.Entidades.Mantenimientos.Entrada;
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
    public class Tarea:ITarea
    {
        public MantenimientoAvanceTareaUsuarioResponse MantenimientoAvanceTareaUsuario(MantenimientoAvanceTareaUsuarioRequest request)
        {
            MantenimientoAvanceTareaUsuarioResponse response = new MantenimientoAvanceTareaUsuarioResponse();
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

        public MantenimientoTareaResponse MantenimientoTarea(MantenimientoTareaRequest request)
        {
            MantenimientoTareaResponse response = new MantenimientoTareaResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    modelo.PA_MantenimientoTarea(request.tipoOperacion
                                                    , request.idTarea
                                                    , request.idUsuario
                                                    , request.idTerreno
                                                    , request.asignadaPor
                                                    , request.fechaInicio
                                                    , request.fechaFinalizacion
                                                    , request.horasEstimadas
                                                    , request.resumen
                                                    , request.observaciones
                                                    , request.idTipoTarea
                                                    , request.activa
                                                    , request.actualizarCoordenadas
                                                    , request.coordenadas
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

        public ConsultarTareasEmpresaResponse ConsultarTareasEmpresa(ConsultarTareasEmpresaRequest request)
        {
            ConsultarTareasEmpresaResponse response = new ConsultarTareasEmpresaResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaTareasEmpresa = modelo.PA_ConsultarTareasEmpresa(
                                                    request.idEmpresa
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

        public ConsultarTareasUsuarioResponse ConsultarTareasUsuario(ConsultarTareasUsuarioRequest request)
        {
            ConsultarTareasUsuarioResponse response = new ConsultarTareasUsuarioResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaTareasUsuario = modelo.PA_ConsultarTareasUsuario(
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

        public CrearRecoleccionCultivoResponse CrearRecoleccionCultivo(CrearRecoleccionCultivoRequest request)
        {
            CrearRecoleccionCultivoResponse response = new CrearRecoleccionCultivoResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    modelo.PA_CrearRecoleccionCultivo(request.idTarea, request.idUsuarioRecolecta, request.kilogramosPrimera, request.kilogramosSegunda, request.kilogramosRechazo, request.causaRechazo, request.idUsuarioSupervisor, estado, mensaje);
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
