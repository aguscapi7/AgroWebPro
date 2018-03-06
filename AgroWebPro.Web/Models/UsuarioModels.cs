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
        public Guid idRol { get; set; }
        public string nombreRol { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string telefono { get; set; }

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
                    listaEmpleadosEmpresa.Add(usuario);
                    foreach (var usuarioItem in empleadosEmpresaResponse.listaEmpleadosEmpresa)
                    {
                        usuario = new UsuarioModels();
                        usuario.idUsuario = usuarioItem.IdUsuario;
                        usuario.nombre = usuarioItem.Nombre;
                        usuario.apellidos = usuarioItem.Apellidos;
                        usuario.correo = usuarioItem.Correo;
                        usuario.idRol = usuarioItem.IdRol;
                        usuario.nombreRol = usuarioItem.NombreRol;
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