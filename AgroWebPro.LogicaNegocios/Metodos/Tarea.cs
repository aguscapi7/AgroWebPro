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
    }
}
