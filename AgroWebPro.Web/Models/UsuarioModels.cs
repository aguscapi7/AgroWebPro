using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class UsuarioModels
    {
        public Guid idUsuario { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public string passwordRepetir { get; set; }
        public string direccion { get; set; }
        public string nombreEmpresa { get; set; }
        public Guid idZonaHoraria { get; set; }
        public EmpresaModels empresaModels { get; set; }
    }
}