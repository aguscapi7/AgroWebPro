﻿using AgroWebPro.Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Entidades.Consultas.Salida
{
    [DataContract]
    public class ConsultarTerrenoResponse:RespuestaBase
    {
        public List<PA_ConsultarTerreno_Result> listaTerreno { get; set; }
    }
}
