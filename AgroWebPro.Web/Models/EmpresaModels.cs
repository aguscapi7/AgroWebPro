using AgroWebPro.Entidades.Consultas.Salida;
using AgroWebPro.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroWebPro.Web.Models
{
    public class EmpresaModels
    {
        public Guid idEmpresa { get; set; }
        public string nombreEmpres { get; set; }
        public string descripcionEmpresa { get; set; }
        public string telefono { get; set; }
        public string cedulaJuridica { get; set; }
        public string direccion { get; set; }
        public Guid idZonaHoraria { get; set; }

        public UsuarioModels usuario { get; set; }

        public List<ZonaHoraria> listaZonasHorarias { get; set; }
        public List<TerrenoModels> listaTerrenosEmpresa { get; set; }
        public string coordenadasTerrenos { get; set; }
        public Guid idTerreno { get; set; }

        public void CopiarListaZonasHorarias(ConsultarZonasHorariasResponse zonasHorariasResponse)
        {
            listaZonasHorarias = new List<ZonaHoraria>();
            if(zonasHorariasResponse != null && zonasHorariasResponse.estado.Equals(Constantes.EstadoCorrecto) && zonasHorariasResponse.listaZonasHorarias.Count() > 0)
            {
                ZonaHoraria zona = new ZonaHoraria()
                {
                    idZonaHoraria = Guid.Empty,
                    zonaHoraria = "Seleccione zona horaria"
                };
                listaZonasHorarias.Add(zona);
                foreach (var item in zonasHorariasResponse.listaZonasHorarias)
                {
                    zona = new ZonaHoraria();
                    zona.zonaHoraria = item.ZonaHoraria;
                    zona.idZonaHoraria = item.IdZonaHoraria;
                    listaZonasHorarias.Add(zona);
                }
                
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
                    int contador = 0;
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
                        coordenadasTerrenos += terrenoItem.Coordenadas + "|";
                        contador++;
                        listaTerrenosEmpresa.Add(terreno);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class ZonaHoraria
    {
        public Guid idZonaHoraria { get; set; }
        public string zonaHoraria { get; set; }
        public string abreviatura { get; set; }
        public string coordenadas { get; set; }
        public decimal diferenciaUtcViejo { get; set; }
        public decimal diferenciaVeranoUtcViejo { get; set; }
        public decimal diferenciaUtc { get; set; }
        public decimal diferenciaVeranoUtc { get; set; }
    }
}