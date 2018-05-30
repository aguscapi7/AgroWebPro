﻿using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Entidades.Mantenimientos.Entrada;
using AgroWebPro.Entidades.Mantenimientos.Salida;
using AgroWebPro.LogicaNegocios.Interfaces;
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
    public class CultivoController : BaseController
    {
        [HttpGet]
        public ActionResult Mantenimiento()
        {

            Session[Constantes.MenuActivo] = Constantes.MenuCultivo;
            CultivoModels cultivoModels = new CultivoModels();
            ICatalogos catalogos = new Catalogos();
            IEmpresa empresa = new Empresa();

            ConsultarCultivosEmpresaRequest cultivosEmpresaRequest = null;
            ConsultarCultivosEmpresaResponse cultivosEmpresaResponse = null;

            ConsultarFamiliasResponse familiasResponse = null;
            ConsultarFamiliasRequest familiasRequest = null;

            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (ConsultarOpcionPorUsuario(controllerName, actionName))
                {
                    string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                    Guid idEmpresa = Guid.Parse(idEmpresaCookie);

                    cultivosEmpresaRequest = new ConsultarCultivosEmpresaRequest();
                    cultivosEmpresaRequest.idEmpresa = idEmpresa;
                    cultivosEmpresaResponse = empresa.ConsultarCultivosEmpresa(cultivosEmpresaRequest);
                    cultivoModels.CopiarCultivosEmpresa(cultivosEmpresaResponse);

                    familiasRequest = new ConsultarFamiliasRequest();
                    familiasResponse = catalogos.ConsultarFamilias(familiasRequest);
                    cultivoModels.CopiarFamilias(familiasResponse);
                    cultivoModels.listaFamilias.Insert(0,new FamiliaModels { idFamilia = 0, nombreFamilia = "Seleccione" });
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
            return View(cultivoModels);
        }
        
        [HttpPost]
        public ActionResult Mantenimiento(CultivoModels cultivoModels)
        {
            IEmpresa empresa = new Empresa();
            ICatalogos catalogos = new Catalogos();

            MantenimientoCultivoResponse cultivoResponse = null;
            MantenimientoCultivoRequest cultivoRequest = null;

            ConsultarCultivosEmpresaRequest cultivosEmpresaRequest = null;
            ConsultarCultivosEmpresaResponse cultivosEmpresaResponse = null;

            ConsultarFamiliasResponse familiasResponse = null;
            ConsultarFamiliasRequest familiasRequest = null;

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

                    string mensajeCorrecto = "El cultivo se ha {0} correctamente.";
                    string mensajeError = "Ocurrió un error al {0} el cultivo.";

                    if (cultivoModels.idCultivo == null || cultivoModels.idCultivo == Guid.Empty)
                    {
                        cultivoRequest = new MantenimientoCultivoRequest()
                        {
                            tipoOperacion = Constantes.operacionCrear,
                            idCultivo = Guid.NewGuid(),
                            nombreCultivo = cultivoModels.nombreCultivo,
                            descripcionCultivo = cultivoModels.descripcionCultivo,
                            idFamilia = cultivoModels.idFamilia,
                            idEmpresa = idEmpresa,
                            ingresadoPor = idUsuario
                        };
                        mensajeCorrecto = string.Format(mensajeCorrecto, "guardado");
                        mensajeError = string.Format(mensajeError, "guardar");
                    }
                    else
                    {
                        cultivoRequest = new MantenimientoCultivoRequest()
                        {
                            tipoOperacion = Constantes.operacionModificar,
                            idCultivo = (Guid)cultivoModels.idCultivo,
                            nombreCultivo = cultivoModels.nombreCultivo,
                            descripcionCultivo = cultivoModels.descripcionCultivo,
                            idFamilia = cultivoModels.idFamilia,
                            idEmpresa = idEmpresa,
                            ingresadoPor = idUsuario
                        };
                        mensajeCorrecto = string.Format(mensajeCorrecto, "editado");
                        mensajeError = string.Format(mensajeError, "editar");
                    }

                    cultivoResponse = empresa.MantenimientoCultivo(cultivoRequest);
                    if (cultivoResponse != null && cultivoResponse.estado.Equals(Constantes.EstadoCorrecto))
                    {
                        ModelState.Clear();
                        cultivoModels = new CultivoModels();
                        ViewBag.respuesta = Constantes.EstadoCorrecto;
                        ViewBag.mensaje = mensajeCorrecto;
                    }
                    else
                    {
                        ViewBag.respuesta = Constantes.EstadoError;
                        ViewBag.mensaje = mensajeError;
                    }
                }
                else
                {
                    cultivoModels.errorValidacion = true;
                }
                

                familiasRequest = new ConsultarFamiliasRequest();
                familiasResponse = catalogos.ConsultarFamilias(familiasRequest);
                cultivoModels.CopiarFamilias(familiasResponse);
                cultivoModels.listaFamilias.Insert(0, new FamiliaModels { idFamilia = 0, nombreFamilia = "Seleccione" });

                cultivosEmpresaRequest = new ConsultarCultivosEmpresaRequest();
                cultivosEmpresaRequest.idEmpresa = idEmpresa;

                cultivosEmpresaResponse = empresa.ConsultarCultivosEmpresa(cultivosEmpresaRequest);
                cultivoModels.CopiarCultivosEmpresa(cultivosEmpresaResponse);

            }
            catch (Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View(cultivoModels);
        }

        public ActionResult Eliminar(Guid idCultivo)
        {
            IEmpresa empresa = new Empresa();

            MantenimientoCultivoResponse cultivoResponse = null;
            MantenimientoCultivoRequest cultivoRequest = null;
            
            string respuesta = string.Empty;

            try
            {                
                cultivoRequest = new MantenimientoCultivoRequest()
                {
                    tipoOperacion = Constantes.operacionDesactivar,
                    idCultivo = idCultivo
                };                

                cultivoResponse = empresa.MantenimientoCultivo(cultivoRequest);
                if (cultivoResponse != null && cultivoResponse.estado.Equals(Constantes.EstadoCorrecto))
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

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return Json( new { respuesta = respuesta });
        }

        
    }
}
