using AgroWebPro.Entidades.Consultas.Entrada;
using AgroWebPro.Entidades.Consultas.Salida;
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
    public class ReporteController : BaseController
    {
        public ActionResult Resumen()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuReportes;
            return View();
        }
        
        public ActionResult Ventas()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuReportes;
            return View();
        }

        public ActionResult General()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuReportes;
            return View();
        }

        public ActionResult Cosechas()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuReportes;
            return View();
        }

        public ActionResult Tareas()
        {
            Session[Constantes.MenuActivo] = Constantes.MenuReportes;
            return View();
        }

        public ActionResult ResumenTareas(int tiempo, Guid idTerreno)
        {
            string respuesta = Constantes.EstadoError;
            IReportes reportes = new Reportes();

            ConsultarReporteTareasResponse reporteTareasResponse = null;
            ConsultarReporteTareasRequest reporteTareasRequest = null;
            try
            {
                if (Request.Cookies["usuario"] != null)
                {
                    Guid idEmpresa = Guid.Parse(Request.Cookies["usuario"]["idEmpresa"].ToString());
                    reporteTareasRequest = new ConsultarReporteTareasRequest();
                    reporteTareasRequest.idEmpresa = idEmpresa;
                    DateTime fechaInicio = DateTime.Now;
                    DateTime fechaFinalizacion = DateTime.Now;
                    //Por dia
                    if (tiempo == 1)
                    {
                        fechaInicio = DateTime.Now;
                        fechaFinalizacion = DateTime.Now;
                    }
                    //Por semana
                    else if(tiempo == 2)
                    {
                        fechaInicio = PrimerDiaSemana();
                        fechaFinalizacion = UltimoDiaSemana(fechaInicio);
                    }
                    //Por mes
                    else if(tiempo == 3)
                    {
                        fechaInicio = PrimerDiaMes();
                        fechaFinalizacion = UltimoDiaMes(fechaInicio);
                    }
                    //Por año
                    else if(tiempo == 4)
                    {
                        fechaInicio = PrimerDiaAnnio();
                        fechaFinalizacion = UltimoDiaAnnio();
                    }
                    reporteTareasRequest.fechaInicio = fechaInicio;
                    reporteTareasRequest.fechaFinalizacion = fechaFinalizacion;
                    reporteTareasRequest.idTerreno = idTerreno;
                    reporteTareasResponse = reportes.ConsultarReporteTareas(reporteTareasRequest);

                    if(reporteTareasResponse != null && reporteTareasResponse.estado.Equals(Constantes.EstadoCorrecto))
                    {
                        var agrupada = reporteTareasResponse.listaReporteTareas.GroupBy(x => x.idEstadoTarea);
                        return Json(new { respuesta = reporteTareasResponse.estado, agrupada = agrupada });
                    }


                }
                else
                {
                    return RedirectToAction("Inicio", "Home");
                }
            }
            catch(Exception ex)
            {

            }
            return Json(new { respuesta = respuesta });
        }

        public static DateTime PrimerDiaSemana()
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            int delta = DayOfWeek.Monday - DateTime.Now.DayOfWeek;
            DateTime monday = DateTime.Now.AddDays(delta == 1 ? -6 : delta);
            return monday;
        }

        public static DateTime UltimoDiaSemana(DateTime fecha)
        {
            return fecha.AddDays(6);
        }

        public static DateTime PrimerDiaMes()
        {
            DateTime fecha = DateTime.Now;
            var primerDiaMes = new DateTime(fecha.Year, fecha.Month, 1);
            return primerDiaMes;
        }

        public static DateTime UltimoDiaMes(DateTime fecha)
        {
            var ultimoDiaMes = fecha.AddMonths(1).AddDays(-1);
            return ultimoDiaMes;
        }

        public static DateTime PrimerDiaAnnio()
        {
            DateTime fecha = DateTime.Now;
            var primerDiaAnnio = new DateTime(fecha.Year, 1, 1);
            return primerDiaAnnio;
        }

        public static DateTime UltimoDiaAnnio()
        {
            DateTime fecha = DateTime.Now;
            var ultimoDiaAnnio = new DateTime(fecha.Year, 12, 31);
            return ultimoDiaAnnio;
        }
    }
}