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
    public class VentaController : BaseController
    {
        public ActionResult CrearVenta()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuVenta;
            return View();
        }

        [HttpPost]
        public ActionResult CrearVenta(VentaModels ventaModels)
        {
            return View(ventaModels);
        }

        public ActionResult MantenimientoClienteProveedor()
        {
            IVenta empresa = new Venta();
            ICatalogos catalogos = new Catalogos();

            Session[Constantes.MenuActivo] = Constantes.MenuClienteProveedor;
            ClienteProveedorModels usuarioModels = new ClienteProveedorModels();
            ConsultarClientesProveedoresEmpresaRequest empleadosEmpresaRequest = null;
            ConsultarClientesProveedoresEmpresaResponse empleadosEmpresaResponse = null;
            
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (ConsultarOpcionPorUsuario(controllerName, actionName))
                {
                    string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                    Guid idEmpresa = Guid.Parse(idEmpresaCookie);

                    empleadosEmpresaRequest = new ConsultarClientesProveedoresEmpresaRequest();
                    empleadosEmpresaRequest.idEmpresa = idEmpresa;

                    empleadosEmpresaResponse = empresa.ConsultarClientesProveedoresEmpresa(empleadosEmpresaRequest);
                    usuarioModels.CopiarClientesProveedores(empleadosEmpresaResponse);
                    
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
            return View(usuarioModels);
        }

        [HttpPost]
        public ActionResult MantenimientoClienteProveedor(ClienteProveedorModels clienteProveedorModels)
        {
            IVenta ventas = new Venta();
            IEmpresa empresa = new Empresa();

            MantenimientoClienteProveedorResponse manteClienteProveedorResponse = null;
            MantenimientoClienteProveedorRequest manteClienteProveedorRequest = null;

            ConsultarClientesProveedoresEmpresaRequest clientesProveedoresEmpresaRequest = null;
            ConsultarClientesProveedoresEmpresaResponse clientesProveedoresEmpresaResponse = null;
            
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
                    string mensajeCorrecto = "El " + (clienteProveedorModels.esCliente ? "cliente":"proveedor") + " se ha {0} correctamente.";
                    string mensajeError = "Ocurrió un error al {0} el " + (clienteProveedorModels.esCliente ? "cliente" : "proveedor") + ".";

                    if (clienteProveedorModels.idClienteProveedor == null || clienteProveedorModels.idClienteProveedor == Guid.Empty)
                    {
                        manteClienteProveedorRequest = new MantenimientoClienteProveedorRequest()
                        {
                            tipoOperacion = Constantes.operacionCrear,
                            idClienteProveedor = Guid.NewGuid(),
                            nombre = clienteProveedorModels.nombre,
                            apellidos = clienteProveedorModels.apellidos,
                            correo = clienteProveedorModels.correo,
                            direccion = clienteProveedorModels.direccion,
                            telefono = clienteProveedorModels.telefono,
                            idEmpresa = idEmpresa,
                            ingresadoPor = idUsuario,
                            esCliente = clienteProveedorModels.esCliente
                        };
                        mensajeCorrecto = string.Format(mensajeCorrecto, "guardado");
                        mensajeError = string.Format(mensajeError, "guardar");
                    }
                    else
                    {
                        manteClienteProveedorRequest = new MantenimientoClienteProveedorRequest()
                        {
                            tipoOperacion = Constantes.operacionModificar,
                            idClienteProveedor = (Guid)clienteProveedorModels.idClienteProveedor,
                            nombre = clienteProveedorModels.nombre,
                            apellidos = clienteProveedorModels.apellidos,
                            correo = clienteProveedorModels.correo,
                            direccion = clienteProveedorModels.direccion,
                            telefono = clienteProveedorModels.telefono,
                            ingresadoPor = idUsuario,
                            idEmpresa = idEmpresa,
                            esCliente = clienteProveedorModels.esCliente
                        };
                        mensajeCorrecto = string.Format(mensajeCorrecto, "editado");
                        mensajeError = string.Format(mensajeError, "editar");
                    }

                    manteClienteProveedorResponse = ventas.MantenimientoClienteProveedor(manteClienteProveedorRequest);
                    if (manteClienteProveedorResponse != null && manteClienteProveedorResponse.estado.Equals(Constantes.EstadoCorrecto))
                    {
                        ModelState.Clear();
                        clienteProveedorModels = new ClienteProveedorModels();
                        ViewBag.respuesta = Constantes.EstadoCorrecto;
                        ViewBag.mensaje = mensajeCorrecto;
                    }
                    else if (manteClienteProveedorResponse != null && manteClienteProveedorResponse.estado.Equals(Constantes.EstadoErrorCustom))
                    {
                        ViewBag.respuesta = Constantes.EstadoError;
                        ViewBag.mensaje = manteClienteProveedorResponse.mensaje;
                        clienteProveedorModels.errorValidacion = true;
                    }
                    else
                    {
                        ViewBag.respuesta = Constantes.EstadoError;
                        ViewBag.mensaje = mensajeError;
                        clienteProveedorModels.errorValidacion = true;
                    }

                }
                else
                {
                    clienteProveedorModels.errorValidacion = true;
                }

                clientesProveedoresEmpresaRequest = new ConsultarClientesProveedoresEmpresaRequest();
                clientesProveedoresEmpresaRequest.idEmpresa = idEmpresa;

                clientesProveedoresEmpresaResponse = ventas.ConsultarClientesProveedoresEmpresa(clientesProveedoresEmpresaRequest);
                clienteProveedorModels.CopiarClientesProveedores(clientesProveedoresEmpresaResponse);
                

            }
            catch (Exception ex)
            {

                AgroWebPro.Utilitarios.Utilitarios.BitacoraErrores(ex.Message + ((ex.InnerException != null) ? Environment.NewLine + ex.InnerException.Message : string.Empty),
                                                    "Error WEB: ",
                                                    this.GetType().Name,
                                                    System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return View(clienteProveedorModels);
        }
    }
}