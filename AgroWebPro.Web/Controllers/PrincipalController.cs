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
                Session[Constantes.MenuActivo] = Constantes.MenuInicio;


            }
            catch(Exception ex)
            {

            }
            return View();
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
    }
}
