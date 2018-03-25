using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.AccesoDatos.Interfaces
{
    public interface IReportes
    {
        ConsultarReporteVentasResponse ConsultarReporteVentas(ConsultarReporteVentasRequest request);
        ConsultarReporteTareasResponse ConsultarReporteTareas(ConsultarReporteTareasRequest request);
    }
}
