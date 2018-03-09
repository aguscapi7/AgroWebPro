using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using AgroWebPro.LogicaNegocios.Metodos;
using AgroWebPro.Utilitarios;
using AgroWebPro.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Mantenimiento()
        {

            Session[Constantes.MenuActivo] = Constantes.MenuMantenimientoUsuarios;
            UsuarioModels usuarioModels = new UsuarioModels();
            Consultas consultas = new Consultas();

            ConsultarEmpleadosEmpresaRequest empleadosEmpresaRequest = null;
            ConsultarEmpleadosEmpresaResponse empleadosEmpresaResponse = null;

            ConsultarRolesRequest rolesRequest = null;
            ConsultarRolesResponse rolesResponse = null;
            try
            {
                if(Request.Cookies["usuario"] != null)
                {
                    string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                    Guid idEmpresa = Guid.Parse(idEmpresaCookie);

                    empleadosEmpresaRequest = new ConsultarEmpleadosEmpresaRequest();
                    empleadosEmpresaRequest.idEmpresa = idEmpresa;

                    empleadosEmpresaResponse = consultas.ConsultarEmpleadosEmpresa(empleadosEmpresaRequest);
                    usuarioModels.CopiarEmpleadosEmpresa(empleadosEmpresaResponse);

                    rolesRequest = new ConsultarRolesRequest();

                    rolesResponse = consultas.ConsultarRoles(rolesRequest);
                    usuarioModels.CopiarRoles(rolesResponse);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {

            }
            return View(usuarioModels);
        }

        [HttpPost]
        public ActionResult Mantenimiento(UsuarioModels usuarioModels, string btnGuardar, string btnEditar)
        {
            Consultas consultas = new Consultas();
            Mantenimientos mantenimientos = new Mantenimientos();

            MantenimientoUsuarioResponse empleadoResponse = null;
            MantenimientoUsuarioRequest empleadoRequest = null;

            ConsultarEmpleadosEmpresaRequest empleadosEmpresaRequest = null;
            ConsultarEmpleadosEmpresaResponse empleadosEmpresaResponse = null;

            ConsultarRolesRequest rolesRequest = null;
            ConsultarRolesResponse rolesResponse = null;

            string respuesta = string.Empty;

            try
            {

                string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                string idUsuarioCookie = Request.Cookies["usuario"]["idUsuario"];
                string idRol = Request.Cookies["usuario"]["idRol"];

                Guid idEmpresa = Guid.Parse(idEmpresaCookie);
                Guid idUsuario = Guid.Parse(idUsuarioCookie);


                string mensajeCorrecto = "El empleado se ha {0} correctamente.";
                string mensajeError = "Ocurrió un error al {0} el empleado.";

                if (!string.IsNullOrEmpty(btnGuardar))
                {
                    empleadoRequest = new MantenimientoUsuarioRequest()
                    {
                        tipoOperacion = Constantes.operacionCrear,
                        idUsuario = Guid.NewGuid(),
                        nombre = usuarioModels.nombre,
                        apellidos = usuarioModels.apellidos,
                        rol = (Guid)usuarioModels.idRol,
                        correo = usuarioModels.correo,
                        direccion = usuarioModels.direccion,
                        password = "P@ssw0rd123",
                        telefono = usuarioModels.telefono,
                        idEmpresa = idEmpresa,
                        ingresadoPor = idUsuario
                    };
                    mensajeCorrecto = string.Format(mensajeCorrecto, "guardado");
                    mensajeError = string.Format(mensajeError, "guardar");
                }
                else if (!string.IsNullOrEmpty(btnEditar))
                {
                    empleadoRequest = new MantenimientoUsuarioRequest()
                    {
                        tipoOperacion = Constantes.operacionModificar,
                        idUsuario = Guid.Parse(btnEditar),
                        nombre = usuarioModels.nombre,
                        apellidos = usuarioModels.apellidos,
                        rol = (Guid)usuarioModels.idRol,
                        correo = usuarioModels.correo,
                        direccion = usuarioModels.direccion,
                        telefono = usuarioModels.telefono,
                        ingresadoPor = idUsuario,
                        idEmpresa = idEmpresa
                    };
                    mensajeCorrecto = string.Format(mensajeCorrecto, "editado");
                    mensajeError = string.Format(mensajeError, "editar");
                }

                empleadoResponse = mantenimientos.MantenimientoUsuario(empleadoRequest);
                if (empleadoResponse != null && empleadoResponse.estado.Equals(Constantes.EstadoCorrecto))
                {
                    ModelState.Clear();
                    usuarioModels = new UsuarioModels();
                    ViewBag.respuesta = Constantes.EstadoCorrecto;
                    ViewBag.mensaje = mensajeCorrecto;
                }
                else
                {
                    ViewBag.respuesta = Constantes.EstadoError;
                    ViewBag.mensaje = mensajeError;
                }
                

                empleadosEmpresaRequest = new ConsultarEmpleadosEmpresaRequest();
                empleadosEmpresaRequest.idEmpresa = idEmpresa;

                empleadosEmpresaResponse = consultas.ConsultarEmpleadosEmpresa(empleadosEmpresaRequest);
                usuarioModels.CopiarEmpleadosEmpresa(empleadosEmpresaResponse);

                rolesRequest = new ConsultarRolesRequest();

                rolesResponse = consultas.ConsultarRoles(rolesRequest);
                usuarioModels.CopiarRoles(rolesResponse);

            }
            catch (Exception ex)
            {

            }
            return View(usuarioModels);
        }

        public ActionResult Eliminar(Guid idUsuario)
        {
            Mantenimientos mantenimientos = new Mantenimientos();

            MantenimientoUsuarioResponse empleadoResponse = null;
            MantenimientoUsuarioRequest empleadoRequest = null;

            string respuesta = string.Empty;

            try
            {
                empleadoRequest = new MantenimientoUsuarioRequest()
                {
                    tipoOperacion = Constantes.operacionDesactivar,
                    idUsuario = idUsuario
                };

                empleadoResponse = mantenimientos.MantenimientoUsuario(empleadoRequest);
                if (empleadoResponse != null && empleadoResponse.estado.Equals(Constantes.EstadoCorrecto))
                {
                    respuesta = Constantes.EstadoCorrecto;
                }
                else
                {
                    respuesta = Constantes.EstadoError;
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { respuesta = respuesta });
        }


    }
}