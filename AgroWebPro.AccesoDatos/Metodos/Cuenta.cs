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
    public class Cuenta:ICuenta
    {
        
        public MantenimientoMovimientoResponse MantenimientoMovimiento(MantenimientoMovimientoRequest request)
        {
            MantenimientoMovimientoResponse response = new MantenimientoMovimientoResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    modelo.PA_MantenimientoMovimiento(
                                      request.tipoOperacion
                                    , request.idMovimiento
                                    , request.fecha
                                    , request.annio
                                    , request.mes
                                    , request.observaciones
                                    , request.idCategoriaMovimiento
                                    , request.monto
                                    , request.idEmpresa
                                    , request.idUsuario 
                                    , request.ingreso
                                    , request.activo                                    
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

        public ConsultarMovimientosResponse ConsultarMovimientos(ConsultarMovimientosRequest request)
        {
            ConsultarMovimientosResponse response = new ConsultarMovimientosResponse();
            ObjectParameter estado = new ObjectParameter("Estado", Constantes.EstadoCorrecto);
            ObjectParameter mensaje = new ObjectParameter("Mensaje", string.Empty);
            try
            {
                using (AgroWebProEntities modelo = new AgroWebProEntities())
                {
                    response.listaMovimientos = modelo.PA_ConsultarMovimientos(
                                                    request.annio,
                                                    request.mes,
                                                    request.busquedaFechas,
                                                    request.fechaInicio,
                                                    request.fechaFin,
                                                    request.idEmpresa,
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

    }
}
