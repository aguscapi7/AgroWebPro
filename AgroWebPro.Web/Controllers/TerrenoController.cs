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
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class TerrenoController : BaseController
    {
        
        public ActionResult Mantenimiento()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuTerreno;
            IEmpresa empresa = new Empresa();

            TerrenoModels terrenoModels = new TerrenoModels();

            ConsultarCultivosEmpresaRequest cultivosEmpresaRequest = null;
            ConsultarCultivosEmpresaResponse cultivosEmpresaResponse = null;

            ConsultarTerrenosEmpresaRequest terrenosEmpresaRequest = null;
            ConsultarTerrenosEmpresaResponse terrenosEmpresaResponse = null;
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
                    terrenoModels.CopiarCultivosEmpresa(cultivosEmpresaResponse);
                    terrenoModels.listaCultivosEmpresa.Insert(0, new CultivoModels { idCultivo = null, nombreCultivo = "Seleccione" });

                    terrenosEmpresaRequest = new ConsultarTerrenosEmpresaRequest();
                    terrenosEmpresaRequest.idEmpresa = idEmpresa;
                    terrenosEmpresaResponse = empresa.ConsultarTerrenosEmpresa(terrenosEmpresaRequest);
                    terrenoModels.CopiarTerrenosEmpresa(terrenosEmpresaResponse);
                    
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
            return View(terrenoModels);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Mantenimiento(TerrenoModels terrenoModels)
        {
            IEmpresa empresa = new Empresa();
            ICatalogos catalogos = new Catalogos();

            MantenimientoTerrenoRequest terrenoRequest = null;
            MantenimientoTerrenoResponse terrenoResponse = null;

            ConsultarCultivosEmpresaRequest cultivosEmpresaRequest = null;
            ConsultarCultivosEmpresaResponse cultivosEmpresaResponse = null;

            ConsultarTerrenosEmpresaRequest terrenosEmpresaRequest = null;
            ConsultarTerrenosEmpresaResponse terrenosEmpresaResponse = null;
            try
            {
                string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                string idUsuarioCookie = Request.Cookies["usuario"]["idUsuario"];
                string idRol = Request.Cookies["usuario"]["idRol"];

                Guid idEmpresa = Guid.Parse(idEmpresaCookie);
                Guid idUsuario = Guid.Parse(idUsuarioCookie);


                string mensajeCorrecto = "El terreno se ha {0} correctamente.";
                string mensajeError = "Ocurrió un error al {0} el terreno.";

                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(terrenoModels.listaCoordenadas))
                    {
                        if (terrenoModels.idTerreno == null || terrenoModels.idTerreno == Guid.Empty)
                        {
                            terrenoRequest = new MantenimientoTerrenoRequest()
                            {
                                tipoOperacion = Constantes.operacionCrear,
                                idTerreno = Guid.NewGuid(),
                                nombre = terrenoModels.nombreTerreno,
                                descripcion = terrenoModels.descripcionTerreno,
                                idCultivo = (Guid)terrenoModels.idCultivo,
                                actualizarCoordenadas = true,
                                coordenadas = terrenoModels.listaCoordenadas,
                                idEmpresa = idEmpresa,
                                ingresadoPor = idUsuario
                            };
                            mensajeCorrecto = string.Format(mensajeCorrecto, "guardado");
                            mensajeError = string.Format(mensajeError, "guardar");
                        }
                        else
                        {
                            terrenoRequest = new MantenimientoTerrenoRequest()
                            {
                                tipoOperacion = Constantes.operacionModificar,
                                idTerreno = (Guid)terrenoModels.idTerreno,
                                nombre = terrenoModels.nombreTerreno,
                                descripcion = terrenoModels.descripcionTerreno,
                                idCultivo = (Guid)terrenoModels.idCultivo,
                                actualizarCoordenadas = terrenoModels.actualizarCoordenadas,
                                coordenadas = terrenoModels.listaCoordenadas,
                                idEmpresa = idEmpresa,
                                ingresadoPor = idUsuario
                            };
                            mensajeCorrecto = string.Format(mensajeCorrecto, "editado");
                            mensajeError = string.Format(mensajeError, "editar");
                        }

                        terrenoResponse = empresa.MantenimientoTerreno(terrenoRequest);
                        if (terrenoResponse != null && terrenoResponse.estado.Equals(Constantes.EstadoCorrecto))
                        {
                            ModelState.Clear();
                            terrenoModels = new TerrenoModels();
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
                        ViewBag.respuesta = Constantes.EstadoError;
                        ViewBag.mensaje = "Debe ingresar un terreno en el mapa para continuar.";
                        terrenoModels.errorValidacion = true;
                    }
                }
                else
                {
                    terrenoModels.errorValidacion = true;
                }


                cultivosEmpresaRequest = new ConsultarCultivosEmpresaRequest();
                cultivosEmpresaRequest.idEmpresa = idEmpresa;
                cultivosEmpresaResponse = empresa.ConsultarCultivosEmpresa(cultivosEmpresaRequest);
                terrenoModels.CopiarCultivosEmpresa(cultivosEmpresaResponse);
                terrenoModels.listaCultivosEmpresa.Insert(0, new CultivoModels { idCultivo = null, nombreCultivo = "Seleccione" });

                terrenosEmpresaRequest = new ConsultarTerrenosEmpresaRequest();
                terrenosEmpresaRequest.idEmpresa = idEmpresa;
                terrenosEmpresaResponse = empresa.ConsultarTerrenosEmpresa(terrenosEmpresaRequest);
                terrenoModels.CopiarTerrenosEmpresa(terrenosEmpresaResponse);
            }
            catch(Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View(terrenoModels);
        }

        public ActionResult Eliminar(Guid idTerreno)
        {
            IEmpresa empresa = new Empresa();

            MantenimientoTerrenoResponse terrenoResponse = null;
            MantenimientoTerrenoRequest terrenoRequest = null;

            string respuesta = string.Empty;

            try
            {
                terrenoRequest = new MantenimientoTerrenoRequest()
                {
                    tipoOperacion = Constantes.operacionDesactivar,
                    idTerreno = idTerreno
                };

                terrenoResponse = empresa.MantenimientoTerreno(terrenoRequest);
                if (terrenoResponse != null && terrenoResponse.estado.Equals(Constantes.EstadoCorrecto))
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
            return Json(new { respuesta = respuesta });
        }
    }
}