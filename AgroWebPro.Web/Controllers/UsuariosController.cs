using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
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
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Mantenimiento()
        {

            Session[Constantes.MenuActivo] = Constantes.MenuMantenimientoUsuarios;
            UsuarioModels usuarioModels = new UsuarioModels();
            Consultas consultas = new Consultas();

            ConsultarEmpleadosEmpresaRequest empleadosEmpresaRequest = null;
            ConsultarEmpleadosEmpresaResponse empleadosEmpresaResponse = null;

            ConsultarRolesRequest rolesRequest = null;
            ConsultarRolesResponse rolesResponse = null;
            try
            {
                string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                Guid idEmpresa = Guid.Parse(idEmpresaCookie);

                empleadosEmpresaRequest = new ConsultarEmpleadosEmpresaRequest();
                empleadosEmpresaRequest.idEmpresa = idEmpresa;

                empleadosEmpresaResponse = consultas.ConsultarEmpleadosEmpresa(empleadosEmpresaRequest);
                usuarioModels.CopiarEmpleadosEmpresa(empleadosEmpresaResponse);

                rolesRequest = new ConsultarRolesRequest();

                rolesResponse = consultas.ConsultarRoles(rolesRequest);
                usuarioModels.CopiarRoles(rolesResponse);


            }
            catch(Exception ex)
            {

            }
            return View(usuarioModels);
        }
    }
}