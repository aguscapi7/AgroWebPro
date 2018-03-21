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
    public class TareaController : BaseController
    {
        public ActionResult Mantenimiento()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuTarea;
            TareaModels tareaModels = new TareaModels();
            ICatalogos catalogos = new Catalogos();
            IEmpresa empresa = new Empresa();
            ITarea tarea = new Tarea();

            ConsultarTiposTareasRequest tiposTareasRequest = null;
            ConsultarTiposTareasResponse tiposTareasResponse = null;

            ConsultarEmpleadosEmpresaRequest empleadosEmpresaRequest = null;
            ConsultarEmpleadosEmpresaResponse empleadosEmpresaResponse = null;

            ConsultarTerrenosEmpresaRequest terrenosEmpresaRequest = null;
            ConsultarTerrenosEmpresaResponse terrenosEmpresaResponse = null;

            ConsultarTareasEmpresaRequest tareasEmpresaRequest = null;
            ConsultarTareasEmpresaResponse tareasEmpresaResponse = null;
            
            try
            {
                if (Request.Cookies["usuario"] != null)
                {
                    string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                    Guid idEmpresa = Guid.Parse(idEmpresaCookie);

                    tiposTareasRequest = new ConsultarTiposTareasRequest();
                    tiposTareasResponse = catalogos.ConsultarTiposTareas(tiposTareasRequest);
                    tareaModels.CopiarTiposTareas(tiposTareasResponse);

                    empleadosEmpresaRequest = new ConsultarEmpleadosEmpresaRequest();
                    empleadosEmpresaRequest.idEmpresa = idEmpresa;
                    empleadosEmpresaResponse = empresa.ConsultarEmpleadosEmpresa(empleadosEmpresaRequest);
                    tareaModels.CopiarEmpleadosEmpresa(empleadosEmpresaResponse);

                    terrenosEmpresaRequest = new ConsultarTerrenosEmpresaRequest();
                    terrenosEmpresaRequest.idEmpresa = idEmpresa;
                    terrenosEmpresaResponse = empresa.ConsultarTerrenosEmpresa(terrenosEmpresaRequest);
                    tareaModels.CopiarTerrenosEmpresa(terrenosEmpresaResponse);

                    tareasEmpresaRequest = new ConsultarTareasEmpresaRequest();
                    tareasEmpresaRequest.idEmpresa = idEmpresa;
                    tareasEmpresaResponse = tarea.ConsultarTareasEmpresa(tareasEmpresaRequest);
                    tareaModels.CopiarTareasEmpresa(tareasEmpresaResponse);

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch (Exception ex)
            {

            }
            return View(tareaModels);
        }

        [HttpPost]
        public ActionResult Mantenimiento(TareaModels tareaModels, string btnGuardar, string btnEditar)
        {
            IEmpresa empresa = new Empresa();
            ITarea tarea = new Tarea();
            ICatalogos catalogos = new Catalogos();

            MantenimientoTareaRequest tareaRequest = null;
            MantenimientoTareaResponse tareaResponse = null;

            ConsultarTiposTareasRequest tiposTareasRequest = null;
            ConsultarTiposTareasResponse tiposTareasResponse = null;

            ConsultarEmpleadosEmpresaRequest empleadosEmpresaRequest = null;
            ConsultarEmpleadosEmpresaResponse empleadosEmpresaResponse = null;

            ConsultarTerrenosEmpresaRequest terrenosEmpresaRequest = null;
            ConsultarTerrenosEmpresaResponse terrenosEmpresaResponse = null;

            try
            {
                string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                string idUsuarioCookie = Request.Cookies["usuario"]["idUsuario"];
                string idRol = Request.Cookies["usuario"]["idRol"];

                Guid idEmpresa = Guid.Parse(idEmpresaCookie);
                Guid idUsuario = Guid.Parse(idUsuarioCookie);


                string mensajeCorrecto = "La tarea se ha {0} correctamente.";
                string mensajeError = "Ocurrió un error al {0} la tarea.";

                if (!string.IsNullOrEmpty(btnGuardar))
                {
                    tareaRequest = new MantenimientoTareaRequest()
                    {
                        tipoOperacion = Constantes.operacionCrear,
                        idTarea = Guid.NewGuid(),
                        asignadaPor = idUsuario,
                        idUsuario = tareaModels.idUsuarioTarea,
                        idTerreno = tareaModels.idTerreno,
                        idTipoTarea = tareaModels.idTipoTarea,
                        resumen = tareaModels.resumen,
                        observaciones = tareaModels.observaciones,
                        fechaInicio = tareaModels.fechaInicio,
                        fechaFinalizacion = tareaModels.fechaFinalizacion,
                        horasEstimadas = tareaModels.horasEstimadas
                        
                    };
                    mensajeCorrecto = string.Format(mensajeCorrecto, "guardado");
                    mensajeError = string.Format(mensajeError, "guardar");
                }
                else if (!string.IsNullOrEmpty(btnEditar))
                {
                    tareaRequest = new MantenimientoTareaRequest()
                    {
                        tipoOperacion = Constantes.operacionModificar,
                        idTarea = Guid.Parse(btnEditar),
                        idTerreno = tareaModels.idTerreno,
                        idTipoTarea = tareaModels.idTipoTarea,
                        resumen = tareaModels.resumen,
                        observaciones = tareaModels.observaciones,
                        fechaInicio = tareaModels.fechaInicio,
                        fechaFinalizacion = tareaModels.fechaFinalizacion
                    };
                    mensajeCorrecto = string.Format(mensajeCorrecto, "editado");
                    mensajeError = string.Format(mensajeError, "editar");
                }

                tareaResponse = tarea.MantenimientoTarea(tareaRequest);
                if (tareaResponse != null && tareaResponse.estado.Equals(Constantes.EstadoCorrecto))
                {
                    ModelState.Clear();
                    tareaModels = new TareaModels();
                    ViewBag.respuesta = Constantes.EstadoCorrecto;
                    ViewBag.mensaje = mensajeCorrecto;
                }
                else
                {
                    ViewBag.respuesta = Constantes.EstadoError;
                    ViewBag.mensaje = mensajeError;
                }

                tiposTareasRequest = new ConsultarTiposTareasRequest();
                tiposTareasResponse = catalogos.ConsultarTiposTareas(tiposTareasRequest);
                tareaModels.CopiarTiposTareas(tiposTareasResponse);

                empleadosEmpresaRequest = new ConsultarEmpleadosEmpresaRequest();
                empleadosEmpresaRequest.idEmpresa = idEmpresa;
                empleadosEmpresaResponse = empresa.ConsultarEmpleadosEmpresa(empleadosEmpresaRequest);
                tareaModels.CopiarEmpleadosEmpresa(empleadosEmpresaResponse);

                terrenosEmpresaRequest = new ConsultarTerrenosEmpresaRequest();
                terrenosEmpresaRequest.idEmpresa = idEmpresa;
                terrenosEmpresaResponse = empresa.ConsultarTerrenosEmpresa(terrenosEmpresaRequest);
                tareaModels.CopiarTerrenosEmpresa(terrenosEmpresaResponse);
            }
            catch(Exception ex)
            {

            }
            return View(tareaModels);
        }

        public ActionResult Eliminar(Guid idTarea)
        {
            ITarea tarea = new Tarea();

            MantenimientoTareaResponse tareaResponse = null;
            MantenimientoTareaRequest tareaRequest = null;

            string respuesta = string.Empty;

            try
            {
                tareaRequest = new MantenimientoTareaRequest()
                {
                    tipoOperacion = Constantes.operacionDesactivar,
                    idTarea = idTarea
                };

                tareaResponse = tarea.MantenimientoTarea(tareaRequest);
                if (tareaResponse != null && tareaResponse.estado.Equals(Constantes.EstadoCorrecto))
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

        public ActionResult AvanceTarea()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuAvanceTarea;
            return View();
        }

    }
}
