using System;
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
        public static readonly int operacionActualizarPassword = 4;


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
        public static readonly String MenuInicio = "Home-Inicio";
        public static readonly String MenuCultivo = "Cultivo-Mantenimiento";
        public static readonly String MenuTerreno = "Terreno-Mantenimiento";
        public static readonly String MenuReportes = "Reporte-General";
        public static readonly String MenuMantenimientoUsuarios = "Usuario-Mantenimiento";
        public static readonly String MenuTarea = "Tarea-Mantenimiento";
        public static readonly String MenuVenta = "Venta-CrearVenta";
        public static readonly String MenuClienteProveedor = "Venta-MantenimientoClienteProveedor";
        public static readonly String MenuAvanceTarea = "Tarea-AvanceTarea";
        public static readonly String MenuPerfil = "Usuario-Perfil";

        public static readonly String OpcionesRol = "OpcionesRol";
        public static readonly String RolAgricultor = "57bcde54-9b6d-417f-a58a-451ab43431cd";
        
    }
}
