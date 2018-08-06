using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.LogicaNegocios.Interfaces
{
    public interface ICatalogos
    {
        ConsultarRolesResponse ConsultarRoles(ConsultarRolesRequest request);
        ConsultarFamiliasResponse ConsultarFamilias(ConsultarFamiliasRequest request);
        ConsultarMonedasResponse ConsultarMonedas(ConsultarMonedasRequest request);
        ConsultarUnidadesVentaResponse ConsultarUnidadesVenta(ConsultarUnidadesVentaRequest request);
        ConsultarZonasHorariasResponse ConsultarZonasHorarias(ConsultarZonasHorariasRequest request);
        ConsultarEstadoTareaResponse ConsultarEstadoTarea(ConsultarEstadoTareaRequest request);
        ConsultarTiposTareasResponse ConsultarTiposTareas(ConsultarTiposTareasRequest request);
        ConsultarCategoriasMovimientoResponse ConsultarCategoriasMovimiento(ConsultarCategoriasMovimientoRequest request);
    }
}
