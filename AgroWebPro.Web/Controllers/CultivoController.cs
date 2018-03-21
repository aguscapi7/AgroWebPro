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
                if(Request.Cookies["usuario"] != null)
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
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                

            }
            catch (Exception ex)
            {

            }
            return View(cultivoModels);
        }
        
        [HttpPost]
        public ActionResult Mantenimiento(CultivoModels cultivoModels, string btnGuardar, string btnEditar, string btnEliminar)
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


                string mensajeCorrecto = "El cultivo se ha {0} correctamente.";
                string mensajeError = "Ocurrió un error al {0} el cultivo.";

                if (!string.IsNullOrEmpty(btnGuardar))
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
                else if (!string.IsNullOrEmpty(btnEditar))
                {
                    cultivoRequest = new MantenimientoCultivoRequest()
                    {
                        tipoOperacion = Constantes.operacionModificar,
                        idCultivo = Guid.Parse(btnEditar),
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

                familiasRequest = new ConsultarFamiliasRequest();
                familiasResponse = catalogos.ConsultarFamilias(familiasRequest);
                cultivoModels.CopiarFamilias(familiasResponse);

                cultivosEmpresaRequest = new ConsultarCultivosEmpresaRequest();
                cultivosEmpresaRequest.idEmpresa = idEmpresa;

                cultivosEmpresaResponse = empresa.ConsultarCultivosEmpresa(cultivosEmpresaRequest);
                cultivoModels.CopiarCultivosEmpresa(cultivosEmpresaResponse);

            }
            catch (Exception ex)
            {

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

            }
            return Json( new { respuesta = respuesta });
        }

        
    }
}
