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
    public class EmpresaController : BaseController
    {
        public ActionResult Perfil()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuPerfilEmpresa;
            EmpresaModels empresaModels = new EmpresaModels();

            IEmpresa empresa = new Empresa();
            ICatalogos catalogos = new Catalogos();

            ConsultarEmpresaResponse empresaResponse = null;
            ConsultarEmpresaRequest empresaRequest = null;

            ConsultarZonasHorariasRequest zonasHorariasRequest = null;
            ConsultarZonasHorariasResponse zonasHorariasResponse = null;
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (ConsultarOpcionPorUsuario(controllerName, actionName))
                {

                    Guid idUsuario = Guid.Parse(Request.Cookies["usuario"]["idEmpresa"].ToString());

                    empresaRequest = new ConsultarEmpresaRequest();
                    empresaRequest.idEmpresa = idUsuario;
                    empresaResponse = empresa.ConsultarEmpresa(empresaRequest);

                    empresaModels.CopiarEmpresa(empresaResponse);

                    zonasHorariasRequest = new ConsultarZonasHorariasRequest();
                    zonasHorariasResponse = catalogos.ConsultarZonasHorarias(zonasHorariasRequest);

                    empresaModels.CopiarListaZonasHorarias(zonasHorariasResponse);

                }
                else
                {
                    return RedirectToAction("Error", "Home");
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

        [HttpPost]
        public ActionResult Perfil(EmpresaModels empresaModels)
        {
            MantenimientoEmpresaResponse empresaResponse = null;
            MantenimientoEmpresaRequest empresaRequest = null;

            IEmpresa empresa = new Empresa();
            ICatalogos catalogos = new Catalogos();

            ConsultarZonasHorariasRequest zonasHorariasRequest = null;
            ConsultarZonasHorariasResponse zonasHorariasResponse = null;

            try
            {
                if (ModelState.IsValid)
                {
                    empresaRequest = new MantenimientoEmpresaRequest()
                    {
                        tipoOperacion = Constantes.operacionModificar,
                        idEmpresa = (Guid)empresaModels.idEmpresa,
                        nombreEmpresa = empresaModels.nombreEmpresa,
                        cedulaJuridica = empresaModels.cedulaJuridica,
                        latitud = empresaModels.latitud,
                        longitud = empresaModels.longitud,
                        direccion = empresaModels.direccion,
                        telefono = empresaModels.telefono,
                        descripcionEmpresa = empresaModels.descripcionEmpresa,
                        idZonaHoraria = (Guid)empresaModels.idZonaHoraria
                        
                    };
                    empresaResponse = empresa.MantenimientoEmpresa(empresaRequest);
                    if (empresaResponse != null && empresaResponse.estado.Equals(Constantes.EstadoCorrecto))
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
                    zonasHorariasRequest = new ConsultarZonasHorariasRequest();
                    zonasHorariasResponse = catalogos.ConsultarZonasHorarias(zonasHorariasRequest);

                    empresaModels.CopiarListaZonasHorarias(zonasHorariasResponse);

                }
                else
                {

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

        public ActionResult Terrenos()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuTerreno;
            return View();
        }
    }
}