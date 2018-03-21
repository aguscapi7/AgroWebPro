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
    public interface ISeguridad
    {
        ConsultarOpcionesRolResponse ConsultarOpcionesRol(ConsultarOpcionesRolRequest request);
        ConsultarOpcionUsuarioResponse ConsultarOpcionUsuario(ConsultarOpcionUsuarioRequest request);
    }
}