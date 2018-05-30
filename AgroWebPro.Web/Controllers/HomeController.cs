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
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace AgroWebPro.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (ConsultarOpcionPorUsuario(controllerName, actionName))
                {
                    string idUsuario = Request.Cookies["usuario"]["idUsuario"];
                    if (!string.IsNullOrEmpty(idUsuario))
                    {
                        return RedirectToAction("Inicio");
                    }
                }
                
            }
            catch(Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View();
        }

       
        [HttpPost]
        public ActionResult Login(UsuarioModels usuarioModels)
        {
            IUsuario usuario = new Usuario();
            ISeguridad seguridad = new Seguridad();
            ConsultarUsuarioLoginResponse consultarUsuarioResponse = null;
            ConsultarUsuarioLoginRequest consultarUsuarioRequest = null;

            ConsultarOpcionesRolResponse opcionesRolResponse = null;
            ConsultarOpcionesRolRequest opcionesRolRequest = null;
            try
            {
                consultarUsuarioRequest = new ConsultarUsuarioLoginRequest()
                {
                    correo = usuarioModels.correo,
                    password = Utilitarios.Utilitarios.Encriptar(usuarioModels.password)
                };

                consultarUsuarioResponse = usuario.ConsultarUsuarioLogin(consultarUsuarioRequest);

                if(consultarUsuarioResponse != null && consultarUsuarioResponse.estado.Equals(Constantes.EstadoCorrecto) && consultarUsuarioResponse.listaUsuarioLogin.Count > 0)
                {
                    var consultaUsuario = consultarUsuarioResponse.listaUsuarioLogin[0];
                    HttpCookie cookie = new HttpCookie("usuario");
                    cookie.Values["nombre"] = consultaUsuario.Nombre;
                    cookie.Values["apellidos"] = consultaUsuario.Apellidos;
                    cookie.Values["idUsuario"] = consultaUsuario.IdUsuario.ToString();
                    cookie.Values["rol"] = consultaUsuario.IdRol.ToString();
                    cookie.Values["password"] = Utilitarios.Utilitarios.Encriptar(consultaUsuario.Password);
                    cookie.Values["idEmpresa"] = consultaUsuario.IdEmpresa.ToString();
                    cookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(cookie);

                    opcionesRolRequest = new ConsultarOpcionesRolRequest();
                    opcionesRolRequest.idRol = consultaUsuario.IdRol;
                    opcionesRolResponse = seguridad.ConsultarOpcionesRol(opcionesRolRequest);
                    if(opcionesRolResponse != null && opcionesRolResponse.estado.Equals(Constantes.EstadoCorrecto) && opcionesRolResponse.listaOpcionesRol.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        cookie.Values["opciones-menu"] = serializer.Serialize(usuarioModels.CopiarOpcionesRol(opcionesRolResponse));
                        Session[Constantes.OpcionesRol] = usuarioModels.CopiarOpcionesRol(opcionesRolResponse);
                    }

                    return RedirectToAction("Inicio", "Home");
                }
            }
            catch(Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            Session["errorCredenciales"] = true;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Registro()
        {
            UsuarioModels usuarioModels = new UsuarioModels();
            ICatalogos catalogos = new Catalogos();
            ConsultarZonasHorariasRequest zonasHorariasRequest = null;

            try
            {
                zonasHorariasRequest = new ConsultarZonasHorariasRequest();
                usuarioModels.empresaModels = new EmpresaModels();
                usuarioModels.empresaModels.CopiarListaZonasHorarias(catalogos.ConsultarZonasHorarias(zonasHorariasRequest));
                usuarioModels.idZonaHoraria = Guid.Parse("8897f249-893f-4435-b6e6-e2885b859c8e");
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
        public ActionResult Registro(UsuarioModels modelo, string btnGuardar)
        {
            IUsuario usuario = new Usuario();
            IEmpresa empresa = new Empresa();
            ICatalogos catalogos = new Catalogos();
            ISeguridad seguridad = new Seguridad();

            UsuarioModels usuarioModels = new UsuarioModels();

            MantenimientoUsuarioResponse usuarioResponse = null ;
            MantenimientoUsuarioRequest usuarioRequest = null;

            MantenimientoEmpresaResponse empresaResponse = null;
            MantenimientoEmpresaRequest empresaRequest = null;

            ConsultarOpcionesRolResponse opcionesRolResponse = null;
            ConsultarOpcionesRolRequest opcionesRolRequest = null;

            ConsultarZonasHorariasRequest zonasHorariasRequest = null;

            try
            {
                zonasHorariasRequest = new ConsultarZonasHorariasRequest();
                modelo.empresaModels = new EmpresaModels();
                modelo.empresaModels.CopiarListaZonasHorarias(catalogos.ConsultarZonasHorarias(zonasHorariasRequest));

                if (modelo != null)
                {
                    if (modelo.password.Equals(modelo.passwordRepetir))
                    {
                        Guid idUsuario = Guid.NewGuid();
                        Guid idEmpresa = Guid.NewGuid();

                        usuarioRequest = new MantenimientoUsuarioRequest
                        {
                            tipoOperacion = Constantes.operacionCrear,
                            nombre = modelo.nombre,
                            apellidos = modelo.apellidos,
                            correo = modelo.correo,
                            password = Utilitarios.Utilitarios.Encriptar(modelo.password),
                            rol = Guid.Parse(Constantes.RolPropietario),
                            idUsuario = idUsuario,
                            ingresadoPor = null,
                            idEmpresa = idEmpresa
                        };

                        empresaRequest = new MantenimientoEmpresaRequest()
                        {
                            idEmpresa = idEmpresa,
                            nombreEmpresa = modelo.nombreEmpresa,
                            idZonaHoraria = modelo.idZonaHoraria,
                            tipoOperacion = Constantes.operacionCrear
                        };

                        empresaResponse = empresa.MantenimientoEmpresa(empresaRequest);

                        if(empresaResponse != null && empresaResponse.estado.Equals(Constantes.EstadoCorrecto))
                        {
                            usuarioResponse = usuario.MantenimientoUsuario(usuarioRequest);

                            if (usuarioResponse != null && empresaResponse != null && usuarioResponse.estado.Equals(Constantes.EstadoCorrecto) && empresaResponse.estado.Equals(Constantes.EstadoCorrecto))
                            {
                                HttpCookie cookie = new HttpCookie("usuario");
                                cookie.Values["nombre"] = usuarioRequest.nombre;
                                cookie.Values["apellidos"] = usuarioRequest.apellidos;
                                cookie.Values["idUsuario"] = usuarioRequest.idUsuario.ToString();
                                cookie.Values["rol"] = usuarioRequest.rol.ToString();
                                cookie.Values["password"] = Utilitarios.Utilitarios.Encriptar(usuarioRequest.password);
                                cookie.Values["idEmpresa"] = usuarioRequest.idEmpresa.ToString();
                                cookie.Expires = DateTime.Now.AddDays(1);
                                Response.Cookies.Add(cookie);

                                opcionesRolRequest = new ConsultarOpcionesRolRequest();
                                opcionesRolRequest.idRol = usuarioRequest.rol;
                                opcionesRolResponse = seguridad.ConsultarOpcionesRol(opcionesRolRequest);
                                if (opcionesRolResponse != null && opcionesRolResponse.estado.Equals(Constantes.EstadoCorrecto) && opcionesRolResponse.listaOpcionesRol.Count > 0)
                                {
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    cookie.Values["opciones-menu"] = serializer.Serialize(usuarioModels.CopiarOpcionesRol(opcionesRolResponse));
                                }

                                return RedirectToAction("Inicio");
                            }
                            else if (usuarioResponse != null && usuarioResponse.estado.Equals(Constantes.EstadoErrorCustom))
                            {
                                ViewBag.MensajeError = usuarioResponse.mensaje;
                            }
                        }
                        
                    }
                    else
                    {
                        ViewBag.MensajeError = "Las contraseñas no coinciden";
                    }
                }
            }
            catch(Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View(modelo);
        }

        public ActionResult Inicio()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuInicio;
            EmpresaModels empresaModels = new EmpresaModels();
            IEmpresa empresa = new Empresa();
            ITarea tarea = new Tarea();

            ConsultarTerrenosEmpresaRequest terrenosEmpresaRequest = null;
            ConsultarTerrenosEmpresaResponse terrenosEmpresaResponse = null;

            ConsultarTareasUsuarioRequest tareasUsuarioRequest = null;
            ConsultarTareasUsuarioResponse tareasUsuarioResponse = null;
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (ConsultarOpcionPorUsuario(controllerName, actionName))
                {
                    string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                    string idUsuarioCookie = Request.Cookies["usuario"]["idUsuario"];
                    Guid idEmpresa = Guid.Parse(idEmpresaCookie);
                    Guid idUsuario = Guid.Parse(idUsuarioCookie);

                    empresaModels.usuario = new UsuarioModels() { nombre = Request.Cookies["usuario"]["nombre"] };

                    terrenosEmpresaRequest = new ConsultarTerrenosEmpresaRequest();
                    terrenosEmpresaRequest.idEmpresa = idEmpresa;
                    terrenosEmpresaResponse = empresa.ConsultarTerrenosEmpresa(terrenosEmpresaRequest);
                    empresaModels.CopiarTerrenosEmpresa(terrenosEmpresaResponse);
                    empresaModels.listaTerrenosEmpresa.Insert(0, new TerrenoModels {nombreTerreno = "Todos", idTerreno = Guid.Empty });


                    tareasUsuarioRequest = new ConsultarTareasUsuarioRequest();
                    tareasUsuarioRequest.idUsuario = idUsuario;
                    tareasUsuarioResponse = tarea.ConsultarTareasUsuario(tareasUsuarioRequest);
                    empresaModels.CopiarTareasUsuario(tareasUsuarioResponse);

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
            return View(empresaModels);
        }

        public ActionResult CerrarSesion()
        {
            try
            {
                string[] myCookies = Request.Cookies.AllKeys;
                foreach (string nombreCookie in myCookies)
                {
                    Request.Cookies.Remove(nombreCookie);

                    var aCookie = new HttpCookie(nombreCookie) { Expires = DateTime.Now.AddDays(-1) };
                    Response.Cookies.Add(aCookie);
                }
            }
            catch(Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult OlvidoContrasenna(string correo)
        {
            ConsultarUsuarioLoginRequest usuarioLoginRequest = null;
            ConsultarUsuarioLoginResponse usuarioLoginResponse = null;

            MantenimientoUsuarioResponse mantenimientoUsuarioResponse = null;
            MantenimientoUsuarioRequest mantenimientoUsuarioRequest = null;
            IUsuario usuario = new Usuario();

            string resultado = Constantes.EstadoError;
            string mensaje = string.Empty;
            try
            {
                usuarioLoginRequest = new ConsultarUsuarioLoginRequest();
                usuarioLoginRequest.correo = correo;
                usuarioLoginRequest.olvido = true;

                usuarioLoginResponse = usuario.ConsultarUsuarioLogin(usuarioLoginRequest);

                if(usuarioLoginResponse != null && usuarioLoginResponse.estado.Equals(Constantes.EstadoCorrecto) && usuarioLoginResponse.listaUsuarioLogin.Count() > 0)
                {
                    var usuarioLogin = usuarioLoginResponse.listaUsuarioLogin[0];
                    string[] partesClave = Guid.NewGuid().ToString().Split('-');
                    string claveTemporal = partesClave[0] + partesClave[1];

                    mantenimientoUsuarioRequest = new MantenimientoUsuarioRequest();
                    mantenimientoUsuarioRequest.idUsuario = usuarioLogin.IdUsuario;
                    mantenimientoUsuarioRequest.password = Utilitarios.Utilitarios.Encriptar(claveTemporal);
                    mantenimientoUsuarioRequest.tipoOperacion = Constantes.operacionActualizarPassword;

                    mantenimientoUsuarioResponse = usuario.MantenimientoUsuario(mantenimientoUsuarioRequest);

                    string correoSalida = ConfigurationManager.AppSettings["DireccionCorreo"].ToString();
                    string claveCorreoSalida = ConfigurationManager.AppSettings["ClaveCorreo"].ToString();
                    string cuerpo = "{0}, se ha restablecido su cuenta en AgroWebPro.<br/><label><strong>Usuario: {1}</strong></label><br/><label><strong>Contraseña temporal: {2}</strong></label></br>Ingresar <a href=\"localhost/AgroWebPro.Web/\">www.agrowebpro.com</a> ";
                    Utilitarios.Utilitarios.EnvioCorreo(correo, "Restablecer cuenta AgroWebPro", string.Format(cuerpo, usuarioLogin.Nombre, usuarioLogin.Correo, claveTemporal), correoSalida, claveCorreoSalida);
                    
                    mensaje = "Se envió un correo a {0} con los detalles para restablecer la contraseña";
                    mensaje = string.Format(mensaje, correo);
                    resultado = Constantes.EstadoCorrecto;
                }
                else
                {
                    mensaje = "El correo no se encuentra registrado en el sistema";
                }

            }
            catch(Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return Json(new { resultado = resultado, mensaje = mensaje });
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}