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
    public class TerrenoController : Controller
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
                if (Request.Cookies["usuario"] != null)
                {
                    string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                    Guid idEmpresa = Guid.Parse(idEmpresaCookie);

                    cultivosEmpresaRequest = new ConsultarCultivosEmpresaRequest();
                    cultivosEmpresaRequest.idEmpresa = idEmpresa;
                    cultivosEmpresaResponse = empresa.ConsultarCultivosEmpresa(cultivosEmpresaRequest);
                    terrenoModels.CopiarCultivosEmpresa(cultivosEmpresaResponse);

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

            }
            return View(terrenoModels);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Mantenimiento(TerrenoModels terrenoModels, string btnGuardar, string btnEditar)
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

                if (!string.IsNullOrEmpty(btnGuardar))
                {
                    terrenoRequest = new MantenimientoTerrenoRequest()
                    {
                        tipoOperacion = Constantes.operacionCrear,
                        idTerreno = Guid.NewGuid(),
                        nombre = terrenoModels.nombreTerreno,
                        descripcion = terrenoModels.descripcionTerreno,
                        idCultivo = terrenoModels.idCultivo,
                        actualizarCoordenadas = true,
                        coordenadas = terrenoModels.listaCoordenadas,
                        idEmpresa = idEmpresa,
                        ingresadoPor = idUsuario
                    };
                    mensajeCorrecto = string.Format(mensajeCorrecto, "guardado");
                    mensajeError = string.Format(mensajeError, "guardar");
                }
                else if (!string.IsNullOrEmpty(btnEditar))
                {
                    terrenoRequest = new MantenimientoTerrenoRequest()
                    {
                        tipoOperacion = Constantes.operacionModificar,
                        idTerreno = Guid.Parse(btnEditar),
                        nombre = terrenoModels.nombreTerreno,
                        descripcion = terrenoModels.descripcionTerreno,
                        idCultivo = terrenoModels.idCultivo,
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


                cultivosEmpresaRequest = new ConsultarCultivosEmpresaRequest();
                cultivosEmpresaRequest.idEmpresa = idEmpresa;
                cultivosEmpresaResponse = empresa.ConsultarCultivosEmpresa(cultivosEmpresaRequest);
                terrenoModels.CopiarCultivosEmpresa(cultivosEmpresaResponse);

                terrenosEmpresaRequest = new ConsultarTerrenosEmpresaRequest();
                terrenosEmpresaRequest.idEmpresa = idEmpresa;
                terrenosEmpresaResponse = empresa.ConsultarTerrenosEmpresa(terrenosEmpresaRequest);
                terrenoModels.CopiarTerrenosEmpresa(terrenosEmpresaResponse);
            }
            catch(Exception ex)
            {

            }
            return View(terrenoModels);
        }
    }
}