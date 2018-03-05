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
            catch(Exception ex)
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

            ConsultarCultivosEmpresaRequest cultivosEmpresaRequest = null;
            ConsultarCultivosEmpresaResponse cultivosEmpresaResponse = null;

            ConsultarFamiliasResponse familiasResponse = null;
            ConsultarFamiliasRequest familiasRequest = null;

            MantenimientoCultivoResponse cultivoResponse = null;
            MantenimientoCultivoRequest cultivoRequest = null;

            try
            {
                string idEmpresaCookie = Request.Cookies["usuario"]["idEmpresa"];
                string idUsuarioCookie = Request.Cookies["usuario"]["idUsuario"];
                Guid idEmpresa = Guid.Parse(idEmpresaCookie);
                Guid idUsuario = Guid.Parse(idUsuarioCookie);
                

                cultivoRequest = new MantenimientoCultivoRequest()
                {
                    tipoOperacion = Constantes.operacionCrear,
                    idCultivo = Guid.NewGuid(),
                    nombreCultivo = cultivoModels.nombreCultivo,
                    descripcionCultivo = cultivoModels.descripcionCultivo,
                    idFamilia = cultivoModels.idFamilia,
                    idEmpresa = idEmpresa,
                    ingresadoPor = idUsuario
                };

                cultivoResponse = mantenimientos.MantenimientoCultivo(cultivoRequest);

                cultivosEmpresaRequest = new ConsultarCultivosEmpresaRequest();
                cultivosEmpresaRequest.idEmpresa = idEmpresa;

                cultivosEmpresaResponse = consultas.ConsultarCultivosEmpresa(cultivosEmpresaRequest);
                cultivoModels.CopiarCultivosEmpresa(cultivosEmpresaResponse);

            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}
