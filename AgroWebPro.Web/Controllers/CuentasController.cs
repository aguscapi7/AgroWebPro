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
    public class CuentasController : BaseController
    {
        public ActionResult Mantenimiento(string a,string m)
        {
            Session[Constantes.MenuActivo] = Constantes.MenuCuentas;
            MovimientoModels movimientoModels = new MovimientoModels();
            ICatalogos catalogos = new Catalogos();
            IEmpresa empresa = new Empresa();
            ICuenta cuenta = new Cuenta();

            ConsultarCategoriasMovimientoRequest categoriasRequest = null;
            ConsultarCategoriasMovimientoResponse categoriasResponse = null;

            ConsultarMovimientosResponse consultarMovimientosResponse = null;
            ConsultarMovimientosRequest consultarMovimientosRequest = null;

            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (ConsultarOpcionPorUsuario(controllerName, actionName))
                {
                    string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                    Guid idEmpresa = Guid.Parse(idEmpresaCookie);

                    if(Session[Constantes.AnnioSeleccionado] != null && Session[Constantes.MesSeleccionado] != null)
                    {
                        movimientoModels.nombreMes = Constantes.Meses[Convert.ToInt32(Session[Constantes.MesSeleccionado]) - 1];
                        movimientoModels.annioSeleccionado = Convert.ToInt32(Session[Constantes.AnnioSeleccionado].ToString());
                        movimientoModels.mesSeleccionado = Convert.ToInt32(Session[Constantes.MesSeleccionado].ToString());
                        movimientoModels.primerDia = new DateTime(movimientoModels.annioSeleccionado, movimientoModels.mesSeleccionado, 1);
                        movimientoModels.ultimoDia = movimientoModels.primerDia.AddMonths(1).AddDays(-1);
                        movimientoModels.fecha = movimientoModels.primerDia.ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        movimientoModels.fecha = DateTime.UtcNow.AddHours(-6).ToString("dd/MM/yyyy");
                        movimientoModels.nombreMes = Constantes.Meses[DateTime.UtcNow.AddHours(-6).Month - 1];
                        movimientoModels.annioSeleccionado = DateTime.UtcNow.AddHours(-6).Year;
                        movimientoModels.mesSeleccionado = DateTime.UtcNow.AddHours(-6).Month;
                        movimientoModels.primerDia = new DateTime(movimientoModels.annioSeleccionado, movimientoModels.mesSeleccionado, 1);
                        movimientoModels.ultimoDia = movimientoModels.primerDia.AddMonths(1).AddDays(-1);

                    }

                    categoriasRequest = new ConsultarCategoriasMovimientoRequest();
                    categoriasResponse = catalogos.ConsultarCategoriasMovimiento(categoriasRequest);
                    movimientoModels.CopiarCategoriasMovimientos(categoriasResponse);

                    consultarMovimientosRequest = new ConsultarMovimientosRequest();
                    consultarMovimientosRequest.mes = movimientoModels.mesSeleccionado;
                    consultarMovimientosRequest.annio = movimientoModels.annioSeleccionado;
                    consultarMovimientosRequest.idEmpresa = idEmpresa;
                    consultarMovimientosRequest.busquedaFechas = false;
                    consultarMovimientosRequest.fechaInicio = DateTime.Now;
                    consultarMovimientosRequest.fechaFin = DateTime.Now;

                    consultarMovimientosResponse = cuenta.ConsultarMovimientos(consultarMovimientosRequest);
                    movimientoModels.CopiarMovimientos(consultarMovimientosResponse);

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
            return View(movimientoModels);
        }

        [HttpPost]
        public ActionResult Mantenimiento(MovimientoModels movimientoModels, string btnBuscar)
        {
            IEmpresa empresa = new Empresa();
            ICuenta cuenta = new Cuenta();
            ICatalogos catalogos = new Catalogos();

            MantenimientoMovimientoRequest tareaRequest = null;
            MantenimientoMovimientoResponse tareaResponse = null;

            ConsultarCategoriasMovimientoRequest categoriasRequest = null;
            ConsultarCategoriasMovimientoResponse categoriasResponse = null;

            ConsultarMovimientosResponse consultarMovimientosResponse = null;
            ConsultarMovimientosRequest consultarMovimientosRequest = null;

            try
            {
                string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                string idUsuarioCookie = Request.Cookies["usuario"]["idUsuario"];
                string idRol = Request.Cookies["usuario"]["idRol"];

                Guid idEmpresa = Guid.Parse(idEmpresaCookie);
                Guid idUsuario = Guid.Parse(idUsuarioCookie);
                int annioSeleccionado = movimientoModels.annioSeleccionado;
                int mesSeleccionado = movimientoModels.mesSeleccionado;

                if (ModelState.IsValid)
                {
                    string mensajeCorrecto = movimientoModels.ingreso ? "El ingreso se ha {0} correctamente." : "El gasto se ha {0} correctamente.";
                    string mensajeError = movimientoModels.ingreso ? "Ocurrió un error al {0} el ingreso." : "Ocurrió un error al {0} el gasto.";

                    if (movimientoModels.idMovimiento == Guid.Empty || movimientoModels.idMovimiento == null)
                    {
                        tareaRequest = new MantenimientoMovimientoRequest()
                        {
                            tipoOperacion = Constantes.operacionCrear,
                            idMovimiento = Guid.NewGuid(),
                            idUsuario = idUsuario,
                            idEmpresa = idEmpresa,
                            annio = movimientoModels.annioSeleccionado,
                            mes = movimientoModels.mesSeleccionado,
                            fecha = Convert.ToDateTime(movimientoModels.fecha),
                            ingreso = movimientoModels.ingreso,
                            monto = movimientoModels.monto,
                            idCategoriaMovimiento = movimientoModels.idCategoriaMovimiento,
                            observaciones = movimientoModels.observaciones
                        };
                        mensajeCorrecto = string.Format(mensajeCorrecto, "guardado");
                        mensajeError = string.Format(mensajeError, "guardar");
                    }
                    else
                    {
                        tareaRequest = new MantenimientoMovimientoRequest()
                        {
                            tipoOperacion = Constantes.operacionModificar,
                            idMovimiento = movimientoModels.idMovimiento,
                            fecha = Convert.ToDateTime(movimientoModels.fecha),
                            monto = movimientoModels.monto,
                            idCategoriaMovimiento = movimientoModels.idCategoriaMovimiento,
                            observaciones = movimientoModels.observaciones
                        };
                        mensajeCorrecto = string.Format(mensajeCorrecto, "editado");
                        mensajeError = string.Format(mensajeError, "editar");
                    }

                    tareaResponse = cuenta.MantenimientoMovimiento(tareaRequest);
                    if (tareaResponse != null && tareaResponse.estado.Equals(Constantes.EstadoCorrecto))
                    {
                        ModelState.Clear();
                        movimientoModels = new MovimientoModels();
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
                    movimientoModels.errorValidacion = true;
                }

                categoriasRequest = new ConsultarCategoriasMovimientoRequest();
                categoriasResponse = catalogos.ConsultarCategoriasMovimiento(categoriasRequest);
                movimientoModels.CopiarCategoriasMovimientos(categoriasResponse);

                movimientoModels.annioSeleccionado = annioSeleccionado;
                movimientoModels.mesSeleccionado = mesSeleccionado;
                movimientoModels.nombreMes = Constantes.Meses[movimientoModels.mesSeleccionado - 1];
                Session[Constantes.AnnioSeleccionado] = movimientoModels.annioSeleccionado;
                Session[Constantes.MesSeleccionado] = movimientoModels.mesSeleccionado;
                movimientoModels.primerDia = new DateTime(movimientoModels.annioSeleccionado, movimientoModels.mesSeleccionado, 1);
                movimientoModels.ultimoDia = movimientoModels.primerDia.AddMonths(1).AddDays(-1);
                movimientoModels.fecha = movimientoModels.primerDia.ToString("dd/MM/yyyy");

                consultarMovimientosRequest = new ConsultarMovimientosRequest();
                consultarMovimientosRequest.mes = movimientoModels.mesSeleccionado;
                consultarMovimientosRequest.annio = movimientoModels.annioSeleccionado;
                consultarMovimientosRequest.idEmpresa = idEmpresa;
                consultarMovimientosRequest.busquedaFechas = false;
                consultarMovimientosRequest.fechaInicio = DateTime.Now;
                consultarMovimientosRequest.fechaFin = DateTime.Now;

                consultarMovimientosResponse = cuenta.ConsultarMovimientos(consultarMovimientosRequest);
                movimientoModels.CopiarMovimientos(consultarMovimientosResponse);


            }
            catch (Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View(movimientoModels);
        }


        public ActionResult CambiarMes(int annio, int mes)
        {
            Session[Constantes.AnnioSeleccionado] = annio;
            Session[Constantes.MesSeleccionado] = mes;
            return Json(new { respuesta = Constantes.EstadoCorrecto });
        }

        public ActionResult Eliminar(Guid idMovimiento)
        {
            ICuenta cuentas = new Cuenta();

            MantenimientoMovimientoResponse movimientoResponse = null;
            MantenimientoMovimientoRequest movimientoRequest = null;

            string respuesta = string.Empty;

            try
            {
                movimientoRequest = new MantenimientoMovimientoRequest()
                {
                    tipoOperacion = Constantes.operacionDesactivar,
                    idMovimiento = idMovimiento,
                    fecha = DateTime.Now
                };

                movimientoResponse = cuentas.MantenimientoMovimiento(movimientoRequest);
                if (movimientoResponse != null && movimientoResponse.estado.Equals(Constantes.EstadoCorrecto))
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