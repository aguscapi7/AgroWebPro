﻿using AgroWebPro.Entidades.Consultas.Entrada;
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
    public interface ITarea
    {
        MantenimientoAvanceTareaUsuarioResponse MantenimientoAvanceTareaUsuario(MantenimientoAvanceTareaUsuarioRequest request);
        MantenimientoTareaResponse MantenimientoTarea(MantenimientoTareaRequest request);
        ConsultarTareasEmpresaResponse ConsultarTareasEmpresa(ConsultarTareasEmpresaRequest request);
        ConsultarTareasUsuarioResponse ConsultarTareasUsuario(ConsultarTareasUsuarioRequest request);
        CrearRecoleccionCultivoResponse CrearRecoleccionCultivo(CrearRecoleccionCultivoRequest request);
    }
}
