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
    public class SantiagoController : Controller
    {
        // GET: Santiago
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            TestModels testModels = new TestModels();
            IUsuario usuario = new Usuario();
            ConsultarTestResponse testResponse = new ConsultarTestResponse();
            ConsultarTestRequest testRequest = null;
            try
            {
                testResponse = usuario.ConsultarTest(testRequest);
                testModels.CopiarTest(testResponse);
            }
            catch(Exception ex)
            {

            }
            return View(testModels);
        }

        [HttpPost]
        public ActionResult Test(TestModels testModels)
        {
            IUsuario usuario = new Usuario();
            ConsultarTestResponse testResponse = null;
            ConsultarTestRequest testRequest = new ConsultarTestRequest();

            MantenimientoTestResponse mantenimientoTestResponse = null;
            MantenimientoTestRequest mantenimientoTesRequest = new MantenimientoTestRequest();
                            if (ModelState.IsValid)
                {
                    if(testModels.nombre.Count() < 200)
                    {
                        string mensajeCorrecto = "El usuario se ha agregado correctamente";
                        string mensajeError = "Ocurrio un error al agregar el usuario";

                        mantenimientoTesRequest.nombre = testModels.nombre;
                        mantenimientoTesRequest.tipoOperacion = Constantes.operacionCrear;
                        mantenimientoTestResponse = usuario.MantenimientoTest(mantenimientoTesRequest);
                        if (mantenimientoTestResponse != null && mantenimientoTestResponse.estado.Equals(Constantes.EstadoCorrecto))
                        {
                            ModelState.Clear();
                            testModels = new TestModels();
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
                        testModels = null;
                    }
                    
                }

                testResponse = usuario.ConsultarTest(testRequest);
                testModels.CopiarTest(testResponse);
            
            return View(testModels);
        }

        public ActionResult Eliminar(int id)
        {
            IUsuario usuario = new Usuario();

            MantenimientoTestResponse testResponse = null;
            MantenimientoTestRequest testRequest = null;

            string respuesta = string.Empty;

            try
            {
                testRequest = new MantenimientoTestRequest()
                {
                    tipoOperacion = Constantes.operacionDesactivar,
                    id = id
                };

                testResponse = usuario.MantenimientoTest(testRequest);
                if (testResponse != null && testResponse.estado.Equals(Constantes.EstadoCorrecto))
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
    }
}