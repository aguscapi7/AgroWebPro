using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.AccesoDatos.Interfaces
{
    public interface IEmpresa
    {
        
        MantenimientoEmpresaResponse MantenimientoEmpresa(MantenimientoEmpresaRequest request);
        MantenimientoCultivoResponse MantenimientoCultivo(MantenimientoCultivoRequest request);
        MantenimientoTerrenoResponse MantenimientoTerreno(MantenimientoTerrenoRequest request);

        ConsultarEmpresaResponse ConsultarEmpresa(ConsultarEmpresaRequest request);
        ConsultarTerrenoResponse ConsultarTerreno(ConsultarTerrenoRequest request);
        ConsultarCultivosEmpresaResponse ConsultarCultivosEmpresa(ConsultarCultivosEmpresaRequest request); ConsultarEmpleadosEmpresaResponse ConsultarEmpleadosEmpresa(ConsultarEmpleadosEmpresaRequest request);
        ConsultarClientesProveedoresEmpresaResponse ConsultarClientesProveedoresEmpresa(ConsultarClientesProveedoresEmpresaRequest request);
        ConsultarTerrenosEmpresaResponse ConsultarTerrenosEmpresa(ConsultarTerrenosEmpresaRequest request);

    }
}
