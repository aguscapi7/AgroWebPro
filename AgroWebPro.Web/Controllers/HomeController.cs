﻿using AgroWebPro.Entidades.Consultas.Entrada;
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
using System.Web.Routing;

namespace AgroWebPro.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if(Request.Cookies["usuario"] != null)
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

            }
            return View();
        }

       
        [HttpPost]
        public ActionResult Login(UsuarioModels usuarioModels)
        {
            Consultas consultas = new Consultas();
            ConsultarUsuarioLoginResponse consultarUsuarioResponse = null;
            ConsultarUsuarioLoginRequest consultarUsuarioRequest = null;
            try
            {
                consultarUsuarioRequest = new ConsultarUsuarioLoginRequest()
                {
                    correo = usuarioModels.correo,
                    password = usuarioModels.password
                };

                consultarUsuarioResponse = consultas.ConsultarUsuarioLogin(consultarUsuarioRequest);

                if(consultarUsuarioResponse != null && consultarUsuarioResponse.estado.Equals(Constantes.EstadoCorrecto) && consultarUsuarioResponse.listaUsuarioLogin.Count > 0)
                {
                    var usuario = consultarUsuarioResponse.listaUsuarioLogin[0];
                    HttpCookie cookie = new HttpCookie("usuario");
                    cookie.Values["nombre"] = usuario.Nombre;
                    cookie.Values["apellidos"] = usuario.Apellidos;
                    cookie.Values["idUsuario"] = usuario.IdUsuario.ToString();
                    cookie.Values["rol"] = usuario.IdRol.ToString();
                    cookie.Values["password"] = usuario.Password;
                    cookie.Values["idEmpresa"] = usuario.IdEmpresa.ToString();
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Inicio", "Home");
                }
            }
            catch(Exception ex)
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult Registro()
        {
            UsuarioModels usuarioModels = new UsuarioModels();
            Consultas consultas = new Consultas();
            ConsultarZonasHorariasRequest zonasHorariasRequest = null;

            try
            {
                zonasHorariasRequest = new ConsultarZonasHorariasRequest();
                consultas = new Consultas();
                usuarioModels.empresaModels = new EmpresaModels();
                usuarioModels.empresaModels.CopiarListaZonasHorarias(consultas.ConsultarZonasHorarias(zonasHorariasRequest));
            }
            catch(Exception ex)
            {

            }
            
            return View(usuarioModels);
        }

        [HttpPost]
        public ActionResult Registro(UsuarioModels modelo, string btnGuardar)
        {
            Mantenimientos mantenimientos = new Mantenimientos();

            MantenimientoUsuarioResponse usuarioResponse = null ;
            MantenimientoUsuarioRequest usuarioRequest = null;

            MantenimientoEmpresaResponse empresaResponse = null;
            MantenimientoEmpresaRequest empresaRequest = null;


            Consultas consultas = new Consultas();
            ConsultarZonasHorariasRequest zonasHorariasRequest = null;

            try
            {
                zonasHorariasRequest = new ConsultarZonasHorariasRequest();
                consultas = new Consultas();
                modelo.empresaModels = new EmpresaModels();
                modelo.empresaModels.CopiarListaZonasHorarias(consultas.ConsultarZonasHorarias(zonasHorariasRequest));

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
                            password = modelo.password,
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

                        empresaResponse = mantenimientos.MantenimientoEmpresa(empresaRequest);

                        if(empresaResponse != null && empresaResponse.estado.Equals(Constantes.EstadoCorrecto))
                        {
                            usuarioResponse = mantenimientos.MantenimientoUsuario(usuarioRequest);

                            if (usuarioResponse != null && empresaResponse != null && usuarioResponse.estado.Equals(Constantes.EstadoCorrecto) && empresaResponse.estado.Equals(Constantes.EstadoCorrecto))
                            {
                                HttpCookie cookie = new HttpCookie("usuario");
                                cookie.Values["nombre"] = usuarioRequest.nombre;
                                cookie.Values["apellidos"] = usuarioRequest.apellidos;
                                cookie.Values["idUsuario"] = usuarioRequest.idUsuario.ToString();
                                cookie.Values["rol"] = usuarioRequest.rol.ToString();
                                cookie.Values["password"] = usuarioRequest.password;
                                cookie.Values["idEmpresa"] = usuarioRequest.idEmpresa.ToString();
                                cookie.Expires = DateTime.Now.AddDays(1);
                                Response.Cookies.Add(cookie);

                                return RedirectToAction("Inicio", "Principal");
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

            }
            return View(modelo);
        }

        public ActionResult Inicio()
        {
            EmpresaModels empresaModels = new EmpresaModels();
            try
            {
                empresaModels.usuario = new UsuarioModels() { nombre = Request.Cookies["usuario"]["nombre"] };
                Session[Constantes.MenuActivo] = Constantes.MenuInicio;


            }
            catch (Exception ex)
            {

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

            }
            return RedirectToAction("Index", "Home");
        }
    }
}