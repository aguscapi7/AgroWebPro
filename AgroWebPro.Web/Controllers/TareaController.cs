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
                    tareaModels.listaTiposTareas.Insert(0, new TipoTarea { idTipoTarea = 0, nombreTarea = "Seleccione" });

                    empleadosEmpresaRequest = new ConsultarEmpleadosEmpresaRequest();
                    empleadosEmpresaRequest.idEmpresa = idEmpresa;
                    empleadosEmpresaResponse = empresa.ConsultarEmpleadosEmpresa(empleadosEmpresaRequest);
                    tareaModels.CopiarEmpleadosEmpresa(empleadosEmpresaResponse);
                    tareaModels.listaEmpleadosEmpresa.Insert(0, new UsuarioModels { idUsuario = null, nombre = "Seleccione" });

                    terrenosEmpresaRequest = new ConsultarTerrenosEmpresaRequest();
                    terrenosEmpresaRequest.idEmpresa = idEmpresa;
                    terrenosEmpresaResponse = empresa.ConsultarTerrenosEmpresa(terrenosEmpresaRequest);
                    tareaModels.CopiarTerrenosEmpresa(terrenosEmpresaResponse);
                    tareaModels.listaTerrenosEmpresa.Insert(0, new TerrenoModels { idTerreno = null, nombreTerreno = "Seleccione" });

                    tareasEmpresaRequest = new ConsultarTareasEmpresaRequest();
                    tareasEmpresaRequest.idEmpresa = idEmpresa;
                    tareasEmpresaResponse = tarea.ConsultarTareasEmpresa(tareasEmpresaRequest);
                    tareaModels.CopiarTareasEmpresa(tareasEmpresaResponse);

                    tareaModels.fechaInicio = DateTime.Now.ToString("dd/MM/yyyy");
                    tareaModels.fechaFinalizacion = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");

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
            return View(tareaModels);
        }

        [HttpPost]
        public ActionResult Mantenimiento(TareaModels tareaModels, string btnBuscar)
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

            ConsultarTareasEmpresaRequest tareasEmpresaRequest = null;
            ConsultarTareasEmpresaResponse tareasEmpresaResponse = null;

            try
            {
                string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                string idUsuarioCookie = Request.Cookies["usuario"]["idUsuario"];
                string idRol = Request.Cookies["usuario"]["idRol"];

                Guid idEmpresa = Guid.Parse(idEmpresaCookie);
                Guid idUsuario = Guid.Parse(idUsuarioCookie);

                if (ModelState.IsValid)
                {
                    string mensajeCorrecto = "La tarea se ha {0} correctamente.";
                    string mensajeError = "Ocurrió un error al {0} la tarea.";

                    if (tareaModels.idTarea == Guid.Empty || tareaModels.idTarea == null)
                    {
                        tareaRequest = new MantenimientoTareaRequest()
                        {
                            tipoOperacion = Constantes.operacionCrear,
                            idTarea = Guid.NewGuid(),
                            asignadaPor = idUsuario,
                            idUsuario = (Guid)tareaModels.idUsuarioTarea,
                            idTerreno = (Guid)tareaModels.idTerreno,
                            idTipoTarea = tareaModels.idTipoTarea,
                            resumen = tareaModels.resumen,
                            observaciones = tareaModels.observaciones,
                            fechaInicio = DateTime.Parse(tareaModels.fechaInicio),
                            fechaFinalizacion = DateTime.Parse(tareaModels.fechaFinalizacion),
                            horasEstimadas = tareaModels.horasEstimadas

                        };
                        mensajeCorrecto = string.Format(mensajeCorrecto, "guardado");
                        mensajeError = string.Format(mensajeError, "guardar");
                    }
                    else
                    {
                        tareaRequest = new MantenimientoTareaRequest()
                        {
                            tipoOperacion = Constantes.operacionModificar,
                            idTarea = tareaModels.idTarea,
                            idUsuario = (Guid)tareaModels.idUsuarioTarea,
                            idTerreno = (Guid)tareaModels.idTerreno,
                            idTipoTarea = tareaModels.idTipoTarea,
                            resumen = tareaModels.resumen,
                            observaciones = tareaModels.observaciones,
                            fechaInicio = DateTime.Parse(tareaModels.fechaInicio),
                            fechaFinalizacion = DateTime.Parse(tareaModels.fechaFinalizacion),
                            horasEstimadas = tareaModels.horasEstimadas
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
                }
                else
                {
                    tareaModels.errorValidacion = true;
                }
                

                tiposTareasRequest = new ConsultarTiposTareasRequest();
                tiposTareasResponse = catalogos.ConsultarTiposTareas(tiposTareasRequest);
                tareaModels.CopiarTiposTareas(tiposTareasResponse);
                tareaModels.listaTiposTareas.Insert(0, new TipoTarea { idTipoTarea = 0, nombreTarea = "Seleccione" });

                empleadosEmpresaRequest = new ConsultarEmpleadosEmpresaRequest();
                empleadosEmpresaRequest.idEmpresa = idEmpresa;
                empleadosEmpresaResponse = empresa.ConsultarEmpleadosEmpresa(empleadosEmpresaRequest);
                tareaModels.CopiarEmpleadosEmpresa(empleadosEmpresaResponse);
                tareaModels.listaEmpleadosEmpresa.Insert(0, new UsuarioModels { idUsuario = null, nombre = "Seleccione" });

                terrenosEmpresaRequest = new ConsultarTerrenosEmpresaRequest();
                terrenosEmpresaRequest.idEmpresa = idEmpresa;
                terrenosEmpresaResponse = empresa.ConsultarTerrenosEmpresa(terrenosEmpresaRequest);
                tareaModels.CopiarTerrenosEmpresa(terrenosEmpresaResponse);
                tareaModels.listaTerrenosEmpresa.Insert(0, new TerrenoModels { idTerreno = null, nombreTerreno = "Seleccione" });

                tareasEmpresaRequest = new ConsultarTareasEmpresaRequest();
                tareasEmpresaRequest.idEmpresa = idEmpresa;
                tareasEmpresaResponse = tarea.ConsultarTareasEmpresa(tareasEmpresaRequest);
                tareaModels.CopiarTareasEmpresa(tareasEmpresaResponse);

                tareaModels.fechaInicio = DateTime.Now.ToString("dd/MM/yyyy");
                tareaModels.fechaFinalizacion = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            }
            catch(Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
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

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return Json(new { respuesta = respuesta });
        }

        public ActionResult AvanceTarea()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuAvanceTarea;
            AvanceTareaModels avanceTareaModels = new AvanceTareaModels();
            ITarea tarea = new Tarea();
            ICatalogos catalogos = new Catalogos();

            ConsultarEstadoTareaRequest estadoTareaRequest = null;
            ConsultarEstadoTareaResponse estadoTareaResponse = null;

            ConsultarTareasUsuarioRequest tareasUsuarioRequest = null;
            ConsultarTareasUsuarioResponse tareasUsuarioResponse = null;

            try
            {
                if (Request.Cookies["usuario"] != null)
                {
                    Guid idUsuario = Guid.Parse(Request.Cookies["usuario"]["idUsuario"]);
                    estadoTareaRequest = new ConsultarEstadoTareaRequest();
                    estadoTareaResponse = catalogos.ConsultarEstadoTarea(estadoTareaRequest);
                    avanceTareaModels.CopiarEstadoTarea(estadoTareaResponse);

                    tareasUsuarioRequest = new ConsultarTareasUsuarioRequest();
                    tareasUsuarioRequest.idUsuario = idUsuario;
                    tareasUsuarioResponse = tarea.ConsultarTareasUsuario(tareasUsuarioRequest);
                    avanceTareaModels.CopiarTareasUsuario(tareasUsuarioResponse);
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
            return View(avanceTareaModels);
        }

        [HttpPost]
        public ActionResult AvanceTarea(AvanceTareaModels avanceTareaModels)
        {
            Session[Constantes.MenuActivo] = Constantes.MenuAvanceTarea;
            ITarea tarea = new Tarea();
            ICatalogos catalogos = new Catalogos();

            ConsultarEstadoTareaRequest estadoTareaRequest = null;
            ConsultarEstadoTareaResponse estadoTareaResponse = null;

            ConsultarTareasUsuarioRequest tareasUsuarioRequest = null;
            ConsultarTareasUsuarioResponse tareasUsuarioResponse = null;

            MantenimientoAvanceTareaUsuarioRequest avanceResquest = null;
            MantenimientoAvanceTareaUsuarioResponse avanceResponse = null;

            CrearRecoleccionCultivoRequest recoleccionCultivoRequest = null;
            CrearRecoleccionCultivoResponse recoleccionCultivoResponse = null;

            try
            {
                Guid idUsuario = Guid.Parse(Request.Cookies["usuario"]["idUsuario"]);
                if (ModelState.IsValid)
                {
                    string mensajeCorrecto = "El avance de la tarea se ha registrado correctamente.";
                    string mensajeError = "Ocurrió un error al registrar el avance de la tarea.";


                    avanceResquest = new MantenimientoAvanceTareaUsuarioRequest()
                    {
                        tipoOperacion = Constantes.operacionCrear,
                        idTarea = (Guid)avanceTareaModels.idTarea,
                        horasEstimadas = avanceTareaModels.horas,
                        idEstadoTarea = avanceTareaModels.idEstadoTarea,
                        observaciones = avanceTareaModels.observaciones

                    };

                    avanceResponse = tarea.MantenimientoAvanceTareaUsuario(avanceResquest);
                    if(avanceResponse != null && avanceResponse.estado.Equals(Constantes.EstadoCorrecto))
                    {
                        if (avanceTareaModels.tieneRecoleccion)
                        {
                            recoleccionCultivoRequest = new CrearRecoleccionCultivoRequest()
                            {
                                causaRechazo = avanceTareaModels.causaRechazo,
                                idTarea = (Guid)avanceTareaModels.idTarea,
                                idUsuarioRecolecta = idUsuario,
                                kilogramosPrimera = avanceTareaModels.kilogramosPrimera,
                                kilogramosSegunda = avanceTareaModels.kilogramosSegunda,
                                kilogramosRechazo = avanceTareaModels.kilogramosRechazo,
                                idUsuarioSupervisor = ""
                            };
                            recoleccionCultivoResponse = tarea.CrearRecoleccionCultivo(recoleccionCultivoRequest);
                            if(recoleccionCultivoResponse != null && recoleccionCultivoResponse.estado.Equals(Constantes.EstadoCorrecto))
                            {
                                ViewBag.respuesta = Constantes.EstadoCorrecto;
                                ViewBag.mensaje = mensajeCorrecto;
                                ModelState.Clear();
                                avanceTareaModels = new AvanceTareaModels();
                                Session["idTareaSeleccionada"] = null;
                            }
                            else
                            {
                                ViewBag.respuesta = Constantes.EstadoError;
                                ViewBag.mensaje = mensajeError;
                            }
                        }
                        else
                        {
                            ViewBag.respuesta = Constantes.EstadoCorrecto;
                            ViewBag.mensaje = mensajeCorrecto;
                            ModelState.Clear();
                            avanceTareaModels = new AvanceTareaModels();
                            Session["idTareaSeleccionada"] = null;
                        }
                    }
                    else
                    {
                        ViewBag.respuesta = Constantes.EstadoError;
                        ViewBag.mensaje = mensajeError;
                    }
                }

                estadoTareaRequest = new ConsultarEstadoTareaRequest();
                estadoTareaResponse = catalogos.ConsultarEstadoTarea(estadoTareaRequest);
                avanceTareaModels.CopiarEstadoTarea(estadoTareaResponse);

                tareasUsuarioRequest = new ConsultarTareasUsuarioRequest();
                tareasUsuarioRequest.idUsuario = idUsuario;
                tareasUsuarioResponse = tarea.ConsultarTareasUsuario(tareasUsuarioRequest);
                avanceTareaModels.CopiarTareasUsuario(tareasUsuarioResponse);

            }
            catch (Exception ex)
            {
                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View(avanceTareaModels);
        }

        public ActionResult RedireccionarAvanceTarea(Guid idTarea)
        {
            Session["idTareaSeleccionada"] = idTarea;
            return RedirectToAction("AvanceTarea");
        }
    }
}
