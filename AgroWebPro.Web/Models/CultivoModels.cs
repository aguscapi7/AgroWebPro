using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class CultivoModels
    {
        public Guid idCultivo { get; set; }
        public string nombreCultivo { get; set; }
        public string descripcionCultivo { get; set; }
        public Guid idFamilia { get; set; }
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
                    FamiliaModels familia = new FamiliaModels()
                    {
                        idFamilia = Guid.Empty,
                        nombreFamilia = "Seleccione la familia"
                    };
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
                    foreach (var familiaItem in cultivosEmpresaResult.listaCultivosEmpresa)
                    {
                        cultivo = new CultivoModels();
                        cultivo.idCultivo = familiaItem.IdCultivo;
                        cultivo.nombreCultivo = familiaItem.Nombre;
                        cultivo.idFamilia = familiaItem.IdFamilia;
                        cultivo.nombreFamilia = familiaItem.NombreFamilia;
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