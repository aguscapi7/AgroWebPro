using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using AgroWebPro.LogicaNegocios.Interfaces;
using AgroWebPro.LogicaNegocios.Metodos;
using AgroWebPro.Utilitarios;
using AgroWebPro.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class UsuarioController : BaseController
    {
        public ActionResult Mantenimiento()
        {

            IEmpresa empresa = new Empresa();
            ICatalogos catalogos = new Catalogos();

            Session[Constantes.MenuActivo] = Constantes.MenuMantenimientoUsuarios;
            UsuarioModels usuarioModels = new UsuarioModels();
            ConsultarEmpleadosEmpresaRequest empleadosEmpresaRequest = null;
            ConsultarEmpleadosEmpresaResponse empleadosEmpresaResponse = null;

            ConsultarRolesRequest rolesRequest = null;
            ConsultarRolesResponse rolesResponse = null;
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (ConsultarOpcionPorUsuario(controllerName, actionName))
                {
                    string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                    Guid idEmpresa = Guid.Parse(idEmpresaCookie);

                    empleadosEmpresaRequest = new ConsultarEmpleadosEmpresaRequest();
                    empleadosEmpresaRequest.idEmpresa = idEmpresa;

                    empleadosEmpresaResponse = empresa.ConsultarEmpleadosEmpresa(empleadosEmpresaRequest);
                    usuarioModels.CopiarEmpleadosEmpresa(empleadosEmpresaResponse);

                    rolesRequest = new ConsultarRolesRequest();

                    rolesResponse = catalogos.ConsultarRoles(rolesRequest);
                    usuarioModels.CopiarRoles(rolesResponse);

                    usuarioModels.password = "password";
                    usuarioModels.passwordRepetir = "password";
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View(usuarioModels);
        }

        [HttpPost]
        public ActionResult Mantenimiento(UsuarioModels usuarioModels)
        {
            IUsuario usuario = new Usuario();
            IEmpresa empresa = new Empresa();
            ICatalogos catalogos = new Catalogos();

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

                if (ModelState.IsValid)
                {
                    string mensajeCorrecto = "El empleado se ha {0} correctamente.";
                    string mensajeError = "Ocurrió un error al {0} el empleado.";

                    if (usuarioModels.idUsuario == null || usuarioModels.idUsuario == Guid.Empty)
                    {
                        string[] partesClave = Guid.NewGuid().ToString().Split('-');
                        string claveTemporal = partesClave[0] + partesClave[1];
                        empleadoRequest = new MantenimientoUsuarioRequest()
                        {
                            tipoOperacion = Constantes.operacionCrear,
                            idUsuario = Guid.NewGuid(),
                            nombre = usuarioModels.nombre,
                            apellidos = usuarioModels.apellidos,
                            rol = (Guid)usuarioModels.idRol,
                            correo = usuarioModels.correo,
                            direccion = usuarioModels.direccion,
                            password = Utilitarios.Utilitarios.Encriptar(claveTemporal),
                            telefono = usuarioModels.telefono,
                            idEmpresa = idEmpresa,
                            ingresadoPor = idUsuario
                        };
                        mensajeCorrecto = string.Format(mensajeCorrecto, "guardado");
                        mensajeError = string.Format(mensajeError, "guardar");
                    }
                    else
                    {
                        empleadoRequest = new MantenimientoUsuarioRequest()
                        {
                            tipoOperacion = Constantes.operacionModificar,
                            idUsuario = (Guid)usuarioModels.idUsuario,
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

                    empleadoResponse = usuario.MantenimientoUsuario(empleadoRequest);
                    if (empleadoResponse != null && empleadoResponse.estado.Equals(Constantes.EstadoCorrecto))
                    {
                        if (usuarioModels.idUsuario == Guid.Empty || usuarioModels.idUsuario == null)
                        {
                            string correoSalida = ConfigurationManager.AppSettings["DireccionCorreo"].ToString();
                            string claveCorreoSalida = ConfigurationManager.AppSettings["ClaveCorreo"].ToString();
                            string cuerpo = " <br/>{0}, se ha creado una cuenta en AgroWebPro.<br/><label><strong>Usuario: {1}</strong></label><br/><label><strong>Contraseña: {2}</strong></label></br>Ingresar <a href=\"localhost//AgroWebPro.Web//\">www.agrowebpro.com</a> ";
                            Utilitarios.Utilitarios.EnvioCorreo(empleadoRequest.correo, "Creación cuenta AgroWebPro", string.Format(cuerpo,empleadoRequest.nombre,empleadoRequest.correo,Utilitarios.Utilitarios.DesEncriptar(empleadoRequest.password)), correoSalida, claveCorreoSalida);

                        }
                        ModelState.Clear();
                        usuarioModels = new UsuarioModels();
                        ViewBag.respuesta = Constantes.EstadoCorrecto;
                        ViewBag.mensaje = mensajeCorrecto;
                    }
                    else if(empleadoResponse != null && empleadoResponse.estado.Equals(Constantes.EstadoErrorCustom))
                    {
                        ViewBag.respuesta = Constantes.EstadoError;
                        ViewBag.mensaje = empleadoResponse.mensaje;
                        usuarioModels.errorValidacion = true;
                    }
                    else
                    {
                        ViewBag.respuesta = Constantes.EstadoError;
                        ViewBag.mensaje = mensajeError;
                        usuarioModels.errorValidacion = true;
                    }

                }
                else
                {
                    usuarioModels.errorValidacion = true;
                }                

                empleadosEmpresaRequest = new ConsultarEmpleadosEmpresaRequest();
                empleadosEmpresaRequest.idEmpresa = idEmpresa;

                empleadosEmpresaResponse = empresa.ConsultarEmpleadosEmpresa(empleadosEmpresaRequest);
                usuarioModels.CopiarEmpleadosEmpresa(empleadosEmpresaResponse);

                rolesRequest = new ConsultarRolesRequest();

                rolesResponse = catalogos.ConsultarRoles(rolesRequest);
                usuarioModels.CopiarRoles(rolesResponse);

                usuarioModels.password = "password";
                usuarioModels.passwordRepetir = "password";

            }
            catch (Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View(usuarioModels);
        }

        public ActionResult Eliminar(Guid idUsuario)
        {
            IUsuario usuario = new Usuario();

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

                empleadoResponse = usuario.MantenimientoUsuario(empleadoRequest);
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

        public ActionResult Perfil()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuPerfil;
            UsuarioModels usuarioModels = new UsuarioModels();

            IUsuario usuario = new Usuario();

            ConsultarUsuarioResponse usuarioResponse = null;
            ConsultarUsuarioRequest usuarioRequest = null;
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (ConsultarOpcionPorUsuario(controllerName, actionName))
                {

                    Guid idUsuario = Guid.Parse(Request.Cookies["usuario"]["idUsuario"].ToString());

                    usuarioRequest = new ConsultarUsuarioRequest();
                    usuarioRequest.idUsuario = idUsuario;
                    usuarioResponse = usuario.ConsultarUsuario(usuarioRequest);

                    usuarioModels.CopiarUsuario(usuarioResponse);

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch(Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View(usuarioModels);
        }

        [HttpPost]
        public ActionResult Perfil(UsuarioModels usuarioModels)
        {
            MantenimientoUsuarioResponse empleadoResponse = null;
            MantenimientoUsuarioRequest empleadoRequest = null;

            IUsuario usuario = new Usuario();

            try
            {
                if (ModelState.IsValid)
                {
                    empleadoRequest = new MantenimientoUsuarioRequest()
                    {
                        tipoOperacion = Constantes.operacionModificar,
                        idUsuario = (Guid)usuarioModels.idUsuario,
                        nombre = usuarioModels.nombre,
                        apellidos = usuarioModels.apellidos,
                        rol = (Guid)usuarioModels.idRol,
                        correo = usuarioModels.correo,
                        direccion = usuarioModels.direccion,
                        telefono = usuarioModels.telefono,
                        password = Utilitarios.Utilitarios.Encriptar(usuarioModels.password)
                    };
                    empleadoResponse = usuario.MantenimientoUsuario(empleadoRequest);
                    if (empleadoResponse != null && empleadoResponse.estado.Equals(Constantes.EstadoCorrecto))
                    {
                        ModelState.Clear();
                        ViewBag.respuesta = Constantes.EstadoCorrecto;
                        ViewBag.mensaje = "La información se ha actualizado correctamente";
                    }
                    else
                    {
                        ViewBag.respuesta = Constantes.EstadoError;
                        ViewBag.mensaje = "Ocurrió un error al actualizar la información";
                    }
                }
                else
                {

                }
            }
            catch(Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View(usuarioModels);
        }
    }
}