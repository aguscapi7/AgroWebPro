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
    public class ConsultarTestResponse:RespuestaBase
    {
        public List<PA_ConsultarTest_Result> listaNombres { get; set; }
    }
}