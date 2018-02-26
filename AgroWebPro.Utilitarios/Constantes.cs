﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Utilitarios
{
    public static class Constantes
    {
        //Tipos de operacion
        public static readonly int operacionCrear = 1;
        public static readonly int operacionModificar = 2;
        public static readonly int operacionDesactivar = 3;


        public static readonly String EstadoCorrecto = "00";
        public static readonly String EstadoErrorCustom = "98";
        public static readonly String EstadoError = "99";
        public static readonly String MensajeErrorAccesoDatos = "Error Acceso Datos: ";
        public static readonly String MensajeErrorLogicaNegocios = "Error Logica Negocios: ";
        public static readonly String MensajeErrorWeb = "Error WEB: ";

        //Definicion de los roles
        public static readonly String RolPropietario = "cba49700-ffe6-49cf-a368-5dcdc388c739";

        //Definicion de las opciones de menu
        public static readonly String MenuActivo = "menu-activo";
        public static readonly String MenuInicio = "inicio";
        public static readonly String MenuCultivo = "cultivo";
        public static readonly String MenuTerreno = "terreno";
        public static readonly String MenuReportes = "reportes";
        public static readonly String MenuMantenimientoUsuarios = "mantenimiento-usuarios";
    }
}
