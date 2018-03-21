using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using AgroWebPro.LogicaNegocios.Interfaces;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.LogicaNegocios.Metodos
{
    public class Tarea:ITarea
    {
        AccesoDatos.Interfaces.ITarea tarea;

        public Tarea()
        {
            tarea = new AccesoDatos.Metodos.Tarea();
        }

        public MantenimientoAvanceTareaUsuarioResponse MantenimientoAvanceTareaUsuario(MantenimientoAvanceTareaUsuarioRequest request)
        {
            MantenimientoAvanceTareaUsuarioResponse response = new MantenimientoAvanceTareaUsuarioResponse();
            try
            {
                response = tarea.MantenimientoAvanceTareaUsuario(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public MantenimientoTareaResponse MantenimientoTarea(MantenimientoTareaRequest request)
        {
            MantenimientoTareaResponse response = new MantenimientoTareaResponse();
            try
            {
                response = tarea.MantenimientoTarea(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarTareasEmpresaResponse ConsultarTareasEmpresa(ConsultarTareasEmpresaRequest request)
        {
            ConsultarTareasEmpresaResponse response = new ConsultarTareasEmpresaResponse();
            try
            {
                response = tarea.ConsultarTareasEmpresa(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public ConsultarTareasUsuarioResponse ConsultarTareasUsuario(ConsultarTareasUsuarioRequest request)
        {
            ConsultarTareasUsuarioResponse response = new ConsultarTareasUsuarioResponse();
            try
            {
                response = tarea.ConsultarTareasUsuario(request);
            }
            catch (Exception ex)
            {
                response.estado = Constantes.EstadoError;
                response.mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        }

        public CrearRecoleccionCultivoResponse CrearRecoleccionCultivo(CrearRecoleccionCultivoRequest request)
        {
            CrearRecoleccionCultivoResponse response = new CrearRecoleccionCultivoResponse();
            try
            {
                response = tarea.CrearRecoleccionCultivo(request);
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
