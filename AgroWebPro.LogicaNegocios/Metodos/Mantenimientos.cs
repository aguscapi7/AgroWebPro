using AgroWebPro.AccesoDatos.Metodos;
using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.LogicaNegocios.Metodos
{
    public class Mantenimientos
    {
        public MantenimientoEmpresaResponse MantenimientoEmpresa(MantenimientoEmpresaRequest request)
        {
            AccesoDatos.Metodos.Mantenimientos mantenimiento = new AccesoDatos.Metodos.Mantenimientos();
            MantenimientoEmpresaResponse response = new MantenimientoEmpresaResponse();
            try
            {
                response = mantenimiento.MantenimientoEmpresa(request);
            }
            catch (Exception ex)
            {
                response.Estado = Constantes.EstadoError;
                response.Mensaje = Constantes.MensajeErrorLogicaNegocios + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty);
                throw;
            }
            return response;
        } 
    }
}
