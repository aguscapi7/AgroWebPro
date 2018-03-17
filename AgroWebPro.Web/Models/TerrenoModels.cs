using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class TerrenoModels
    {
        public Guid idTerreno { get; set; }
        [Display(Name = "Nombre")]
        public string nombreTerreno { get; set; }
        [Display(Name = "Descripción")]
        public string descripcionTerreno { get; set; }
        public List<CultivoModels> listaCultivosEmpresa { get; set; }
        [Display(Name = "Cultivo")]
        public Guid idCultivo { get; set; }
        public string listaCoordenadas { get; set; }
        public List<TerrenoModels> listaTerrenosEmpresa { get; set; }
        public string nombreCultivo { get; set; }
        public string coordenadas { get; set; }
        public bool actualizarCoordenadas { get; set; }

        public void CopiarCultivosEmpresa(ConsultarCultivosEmpresaResponse cultivosEmpresaResponse)
        {
            try
            {
                listaCultivosEmpresa = new List<CultivoModels>();
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
                        listaCultivosEmpresa.Add(cultivo);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CopiarTerrenosEmpresa(ConsultarTerrenosEmpresaResponse cultivosEmpresaResponse)
        {
            try
            {
                listaTerrenosEmpresa = new List<TerrenoModels>();
                if (cultivosEmpresaResponse != null && cultivosEmpresaResponse.estado.Equals(Constantes.EstadoCorrecto) && cultivosEmpresaResponse.listaTerrenosEmpresa.Count >= 0)
                {
                    TerrenoModels terreno = null;
                    foreach (var terrenoItem in cultivosEmpresaResponse.listaTerrenosEmpresa)
                    {
                        terreno = new TerrenoModels();
                        terreno.idTerreno = terrenoItem.IdTerreno;
                        terreno.nombreTerreno = terrenoItem.NombreTerreno;
                        terreno.descripcionTerreno = terrenoItem.DescripcionTerreno;
                        terreno.listaCoordenadas = terrenoItem.Coordenadas;
                        terreno.nombreCultivo = terrenoItem.NombreCultivo;
                        terreno.coordenadas = terrenoItem.Coordenadas;
                        terreno.idCultivo = terrenoItem.IdCultivo;
                        listaTerrenosEmpresa.Add(terreno);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}