using AgroWebPro.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroWebPro.Web.Controllers
{
    public class BaseController : Controller
    {
        public UsuarioModels ConsultarOpcionPorUsuario(string controlador, string accion, Guid idUsuario)
        {
            UsuarioModels usuario = new UsuarioModels();
            try
            {
                
            }
            catch(Exception ex)
            {

            }
            return usuario;
        }
        

    }
}