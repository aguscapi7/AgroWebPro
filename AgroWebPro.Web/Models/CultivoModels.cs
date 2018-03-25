using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class CultivoModels
    {
        public Guid? idCultivo { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El largo máximo es 50")]
        public string nombreCultivo { get; set; }

        [Display(Name = "Descripción")]
        public string descripcionCultivo { get; set; }

        [Display(Name = "Familia")]
        [Required(ErrorMessage = "Debe seleccionar la familia")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar la familia")]
        public int idFamilia { get; set; }

        [Display(Name = "Moneda")]
        public int idMoneda { get; set; }

        [Display(Name = "Precio")]
        public decimal precio { get; set; }

        [Display(Name = "Unidad de venta")]
        public int idUnidadVenta { get; set; }

        public string nombreFamilia { get; set; }
        public Guid ingresadoPor { get; set; }
        public bool activo { get; set; }
        public Guid idEmpresa { get; set; }
        public string simboloMoneda { get; set; }
        public string nombreUnidadVenta { get; set; }
        public List<FamiliaModels> listaFamilias { get; set; }
        public List<CultivoModels> listaCultivos { get; set; }
        public List<UnidadVentaModels> listaUnidadVenta { get; set; }
        public List<MonedaModels> listaMonedas { get; set; }
        public bool errorValidacion { get; set; }

        public void CopiarFamilias(ConsultarFamiliasResponse familiasResponse)
        {
            try
            {
                listaFamilias = new List<FamiliaModels>();
                if(familiasResponse != null && familiasResponse.estado.Equals(Constantes.EstadoCorrecto) && familiasResponse.listaFamilias.Count >= 0)
                {
                    FamiliaModels familia = null;
                    foreach (var familiaItem in familiasResponse.listaFamilias)
                    {
                        familia = new FamiliaModels();
                        familia.idFamilia = familiaItem.IdFamilia;
                        familia.nombreFamilia = familiaItem.NombreFamilia;
                        listaFamilias.Add(familia);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void CopiarCultivosEmpresa(ConsultarCultivosEmpresaResponse cultivosEmpresaResponse)
        {
            try
            {
                listaCultivos = new List<CultivoModels>();
                if (cultivosEmpresaResponse != null && cultivosEmpresaResponse.estado.Equals(Constantes.EstadoCorrecto) && cultivosEmpresaResponse.listaCultivosEmpresa.Count >= 0)
                {
                    CultivoModels cultivo = null;
                    foreach (var cultivoItem in cultivosEmpresaResponse.listaCultivosEmpresa)
                    {
                        cultivo = new CultivoModels();
                        cultivo.idCultivo = cultivoItem.IdCultivo;
                        cultivo.nombreCultivo = cultivoItem.Nombre;
                        cultivo.idFamilia = cultivoItem.IdFamilia;
                        cultivo.nombreFamilia = cultivoItem.NombreFamilia;
                        cultivo.descripcionCultivo = cultivoItem.Descripcion;
                        listaCultivos.Add(cultivo);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CopiarMonedas(ConsultarMonedasResponse monedasResponse)
        {
            try
            {
                listaMonedas = new List<MonedaModels>();
                if (monedasResponse != null && monedasResponse.estado.Equals(Constantes.EstadoCorrecto) && monedasResponse.listaMonedas.Count >= 0)
                {
                    MonedaModels moneda = null;
                    foreach (var monedaItem in monedasResponse.listaMonedas)
                    {
                        moneda = new MonedaModels();
                        moneda.simboloMoneda = monedaItem.SimboloMoneda;
                        moneda.idMoneda = monedaItem.IdMoneda;
                        moneda.codigoMoneda = monedaItem.CodigoMoneda;
                        moneda.nombreMoneda = monedaItem.NombreMoneda;
                        listaMonedas.Add(moneda);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CopiarUnidadesVenta(ConsultarUnidadesVentaResponse unidadesVentaResponse)
        {
            try
            {
                listaUnidadVenta = new List<UnidadVentaModels>();
                if (unidadesVentaResponse != null && unidadesVentaResponse.estado.Equals(Constantes.EstadoCorrecto) && unidadesVentaResponse.listaUnidadesVenta.Count >= 0)
                {
                    UnidadVentaModels unidadVenta = null;
                    foreach (var unidadVentaItem in unidadesVentaResponse.listaUnidadesVenta)
                    {
                        unidadVenta = new UnidadVentaModels();
                        unidadVenta.idUnidadVenta = unidadVentaItem.IdUnidadVenta;
                        unidadVenta.nombreUnidadVenta = unidadVentaItem.NombreUnidad;
                        unidadVenta.simboloUnidadVenta = unidadVentaItem.SimboloUnidad;
                        listaUnidadVenta.Add(unidadVenta);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class FamiliaModels
    {
        public int idFamilia { get; set; }
        public string nombreFamilia { get; set; }
    }

    public class UnidadVentaModels
    {
        public int idUnidadVenta { get; set; }
        public string nombreUnidadVenta { get; set; }
        public string simboloUnidadVenta { get; set; }
    }

    public class MonedaModels
    {
        public int idMoneda { get; set; }
        public string nombreMoneda { get; set; }
        public string simboloMoneda { get; set; }
        public string codigoMoneda { get; set; }
    }
}