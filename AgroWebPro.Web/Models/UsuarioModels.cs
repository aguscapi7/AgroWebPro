using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class UsuarioModels
    {
        public Guid idUsuario { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string nombre { get; set; }

        [Display(Name = "Apellidos")]
        public string apellidos { get; set; }
        
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El correo es requerido")]
        public string correo { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string password { get; set; }

        [Display(Name = "Repetir contraseña")]
        public string passwordRepetir { get; set; }
        
        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public string fechaNacimiento { get; set; }

        [Display(Name = "Teléfono")]
        public string telefono { get; set; }

        [Display(Name = "Rol de usuario")]
        public Guid idRol { get; set; }

        public string nombreEmpresa { get; set; }
        public Guid idZonaHoraria { get; set; }
        public string nombreRol { get; set; }

        public EmpresaModels empresaModels { get; set; }
        public List<UsuarioModels> listaEmpleadosEmpresa { get; set; }
        public List<RolModels> listaRoles { get; set; }


        public void CopiarEmpleadosEmpresa(ConsultarEmpleadosEmpresaResponse empleadosEmpresaResponse)
        {
            try
            {
                listaEmpleadosEmpresa = new List<UsuarioModels>();
                if (empleadosEmpresaResponse != null && empleadosEmpresaResponse.estado.Equals(Constantes.EstadoCorrecto) && empleadosEmpresaResponse.listaEmpleadosEmpresa.Count >= 0)
                {
                    UsuarioModels usuario = null;
                    foreach (var usuarioItem in empleadosEmpresaResponse.listaEmpleadosEmpresa)
                    {
                        usuario = new UsuarioModels();
                        usuario.idUsuario = usuarioItem.IdUsuario;
                        usuario.nombre = usuarioItem.Nombre;
                        usuario.apellidos = usuarioItem.Apellidos;
                        usuario.correo = usuarioItem.Correo;
                        usuario.idRol = usuarioItem.IdRol;
                        usuario.nombreRol = usuarioItem.NombreRol;
                        usuario.telefono = usuarioItem.Telefono;
                        usuario.direccion = usuarioItem.Direccion;
                        listaEmpleadosEmpresa.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CopiarRoles(ConsultarRolesResponse rolesResponse)
        {
            try
            {
                listaRoles = new List<RolModels>();
                if (rolesResponse != null && rolesResponse.estado.Equals(Constantes.EstadoCorrecto) && rolesResponse.listaRoles.Count >= 0)
                {
                    RolModels rol = new RolModels()
                    {
                        idRol = Guid.Empty,
                        nombreRol = "Seleccione el rol"
                    };
                    listaRoles.Add(rol);
                    foreach (var usuarioItem in rolesResponse.listaRoles)
                    {
                        rol = new RolModels();
                        rol.idRol = usuarioItem.IdRol;
                        rol.nombreRol = usuarioItem.NombreRol;
                        listaRoles.Add(rol);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class RolModels
    {
        public Guid idRol { get; set; }
        public string nombreRol { get; set; }
    }
}