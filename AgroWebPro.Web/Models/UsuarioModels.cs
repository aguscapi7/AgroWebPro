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
        public Guid? idUsuario { get; set; }
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
        [Compare("password", ErrorMessage = "Las contraseñas deben ser iguales")]
        public string passwordRepetir { get; set; }
        
        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public string fechaNacimiento { get; set; }

        [Display(Name = "Teléfono")]
        public string telefono { get; set; }

        [Display(Name = "Rol de usuario")]
        [Required(ErrorMessage = "Seleccione un rol")]
        public Guid? idRol { get; set; }

        public string nombreEmpresa { get; set; }
        public Guid idZonaHoraria { get; set; }
        public string nombreRol { get; set; }

        public EmpresaModels empresaModels { get; set; }
        public List<UsuarioModels> listaEmpleadosEmpresa { get; set; }
        public List<RolModels> listaRoles { get; set; }
        public bool errorValidacion { get; set; }


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
                        usuario.direccion = usuarioItem.Direccion == null ? "" : usuarioItem.Direccion;
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
                        idRol = null,
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

        public List<OpcionModels> CopiarOpcionesRol(ConsultarOpcionesRolResponse opcionesRolResponse)
        {
            List<OpcionModels> listaOpciones = new List<OpcionModels>();
            try
            {
                if(opcionesRolResponse != null && opcionesRolResponse.estado.Equals(Constantes.EstadoCorrecto) && opcionesRolResponse.listaOpcionesRol.Count > 0)
                {
                    OpcionModels opcion = null;
                    foreach (var item in opcionesRolResponse.listaOpcionesRol)
                    {
                        opcion = new OpcionModels();
                        opcion.idOpcion = item.idOpcion;
                        opcion.controlador = item.controlador;
                        opcion.accion = item.accion;
                        opcion.nombre = item.nombre;
                        opcion.icono = item.icono;
                        listaOpciones.Add(opcion);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return listaOpciones;
        }

        public void CopiarUsuario(ConsultarUsuarioResponse usuarioResponse)
        {
            try
            {
                if(usuarioResponse != null && usuarioResponse.estado.Equals(Constantes.EstadoCorrecto) && usuarioResponse.listaUsuario.Count > 0)
                {
                    var usuario = usuarioResponse.listaUsuario[0];
                    this.idUsuario = usuario.IdUsuario;
                    this.nombre = usuario.Nombre;
                    this.apellidos = usuario.Apellidos;
                    this.correo = usuario.Correo;
                    this.direccion = usuario.Direccion;
                    this.idRol = usuario.IdRol;
                    this.password = Utilitarios.Utilitarios.DesEncriptar(usuario.Password);
                    this.passwordRepetir = Utilitarios.Utilitarios.DesEncriptar(usuario.Password);
                    this.telefono = usuario.Telefono;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }

    public class RolModels
    {
        public Guid? idRol { get; set; }
        public string nombreRol { get; set; }
    }

    public class OpcionModels
    {
        public Guid idOpcion { get; set; }
        public string nombre { get; set; }
        public string controlador { get; set; }
        public string accion { get; set; }
        public string icono { get; set; }
    }
}