using AgroWebPro.Entidades.Consultas.Entrada;
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
using System.Web.Routing;

namespace AgroWebPro.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registro()
        {
            UsuarioModels usuarioModels = new UsuarioModels();
            
            return View(usuarioModels);
        }

        [HttpPost]
        public ActionResult Registro(UsuarioModels modelo, string btnGuardar)
        {
            Mantenimientos mantenimientos = new Mantenimientos();
            MantenimientoUsuarioResponse usuarioResponse = null ;
            MantenimientoUsuarioRequest usuarioRequest = null;

            try
            {
                if(modelo != null)
                {
                    if (modelo.password.Equals(modelo.passwordRepetir))
                    {
                        Guid idUsuario = Guid.NewGuid();
                        usuarioRequest = new MantenimientoUsuarioRequest
                        {
                            tipoOperacion = Constantes.operacionCrear,
                            nombre = modelo.nombre,
                            apellidos = modelo.apellidos,
                            correo = modelo.correo,
                            password = modelo.password,
                            rol = Guid.Parse(Constantes.RolPropietario),
                            idUsuario = idUsuario,
                            ingresadoPor = null
                        };

                        usuarioResponse = mantenimientos.MantenimientoUsuario(usuarioRequest);

                        if(usuarioResponse != null && usuarioResponse.estado.Equals(Constantes.EstadoCorrecto))
                        {
                            HttpCookie cookie = new HttpCookie("usuario");
                            cookie.Values["nombre"] = usuarioRequest.nombre;
                            cookie.Values["apellidos"] = usuarioRequest.apellidos;
                            cookie.Values["idUsuario"] = usuarioRequest.idUsuario.ToString();
                            cookie.Values["rol"] = usuarioRequest.rol.ToString();
                            cookie.Values["password"] = usuarioRequest.password;
                            cookie.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(cookie);

                            return RedirectToAction("Inicio", "Principal");
                        }
                        else if (usuarioResponse != null && usuarioResponse.estado.Equals(Constantes.EstadoErrorCustom))
                        {
                            ViewBag.MensajeError = usuarioResponse.mensaje;
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

            }
            return View(modelo);
        }
    }
}