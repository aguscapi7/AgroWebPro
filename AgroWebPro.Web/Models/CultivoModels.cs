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
        public Guid idCultivo { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string nombreCultivo { get; set; }

        [Display(Name = "Descripción")]
        public string descripcionCultivo { get; set; }

        [Display(Name = "Familia")]
        [Required(ErrorMessage = "Debe seleccionar la familia")]
        public Guid? idFamilia { get; set; }

        public string nombreFamilia { get; set; }
        public Guid ingresadoPor { get; set; }
        public bool activo { get; set; }
        public Guid idEmpresa { get; set; }
        public List<FamiliaModels> listaFamilias { get; set; }
        public List<CultivoModels> listaCultivos { get; set; }

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

        public void CopiarCultivosEmpresa(ConsultarCultivosEmpresaResponse cultivosEmpresaResult)
        {
            try
            {
                listaCultivos = new List<CultivoModels>();
                if (cultivosEmpresaResult != null && cultivosEmpresaResult.estado.Equals(Constantes.EstadoCorrecto) && cultivosEmpresaResult.listaCultivosEmpresa.Count >= 0)
                {
                    CultivoModels cultivo = null;
                    foreach (var cultivoItem in cultivosEmpresaResult.listaCultivosEmpresa)
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
    }

    public class FamiliaModels
    {
        public Guid idFamilia { get; set; }
        public string nombreFamilia { get; set; }
    }
}