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
    public class PrincipalController : Controller
    {
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

        public ActionResult Mapa()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuTerreno;
            return View();
        }

        public ActionResult Cultivo()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuCultivo;
            return View();
        }

        [HttpGet]
        public ActionResult MantenimientoCultivo()
        {

            Session[Constantes.MenuActivo] = Constantes.MenuCultivo;
            CultivoModels cultivoModels = new CultivoModels();
            Consultas consultas = new Consultas();

            ConsultarCultivosEmpresaRequest cultivosEmpresaRequest = null;
            ConsultarCultivosEmpresaResponse cultivosEmpresaResponse = null;

            ConsultarFamiliasResponse familiasResponse = null;
            ConsultarFamiliasRequest familiasRequest = null;


            try
            {
                string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                Guid idEmpresa = Guid.Parse(idEmpresaCookie);

                cultivosEmpresaRequest = new ConsultarCultivosEmpresaRequest();
                cultivosEmpresaRequest.idEmpresa = idEmpresa;

                cultivosEmpresaResponse = consultas.ConsultarCultivosEmpresa(cultivosEmpresaRequest);
                cultivoModels.CopiarCultivosEmpresa(cultivosEmpresaResponse);

                familiasRequest = new ConsultarFamiliasRequest();
                familiasResponse = consultas.ConsultarFamilias(familiasRequest);
                cultivoModels.CopiarFamilias(familiasResponse);

            }
            catch (Exception ex)
            {

            }
            return View(cultivoModels);
        }
        
        [HttpPost]
        public ActionResult MantenimientoCultivo(CultivoModels cultivoModels, string btnGuardar, string btnEditar, string btnEliminar)
        {
            Consultas consultas = new Consultas();
            Mantenimientos mantenimientos = new Mantenimientos();

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
                        idFamilia = (Guid)cultivoModels.idFamilia,
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
                        idFamilia = (Guid)cultivoModels.idFamilia,
                        idEmpresa = idEmpresa,
                        ingresadoPor = idUsuario
                    };
                    mensajeCorrecto = string.Format(mensajeCorrecto, "editado");
                    mensajeError = string.Format(mensajeError, "editar");
                }
                else if (!string.IsNullOrEmpty(btnEliminar))
                {
                    cultivoRequest = new MantenimientoCultivoRequest()
                    {
                        tipoOperacion = Constantes.operacionDesactivar,
                        idCultivo = cultivoModels.idCultivo
                    };
                }

                cultivoResponse = mantenimientos.MantenimientoCultivo(cultivoRequest);
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
                familiasResponse = consultas.ConsultarFamilias(familiasRequest);
                cultivoModels.CopiarFamilias(familiasResponse);

                cultivosEmpresaRequest = new ConsultarCultivosEmpresaRequest();
                cultivosEmpresaRequest.idEmpresa = idEmpresa;

                cultivosEmpresaResponse = consultas.ConsultarCultivosEmpresa(cultivosEmpresaRequest);
                cultivoModels.CopiarCultivosEmpresa(cultivosEmpresaResponse);

            }
            catch (Exception ex)
            {

            }
            return View(cultivoModels);
        }

        public ActionResult EliminarCultivo(Guid idCultivo)
        {
            Mantenimientos mantenimientos = new Mantenimientos();

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

                cultivoResponse = mantenimientos.MantenimientoCultivo(cultivoRequest);
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
