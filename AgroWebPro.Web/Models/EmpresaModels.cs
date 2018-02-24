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
        public List<ZonaHoraria> listaZonasHorarias { get; set; }

        public void CopiarListaZonasHorarias(ConsultarZonasHorariasResponse zonasHorariasResponse)
        {
            listaZonasHorarias = new List<ZonaHoraria>();
            if(zonasHorariasResponse != null && zonasHorariasResponse.Estado.Equals(Constantes.EstadoCorrecto) && zonasHorariasResponse.listaZonasHorarias.Count() > 0)
            {
                ZonaHoraria zona = null;
                foreach (var item in zonasHorariasResponse.listaZonasHorarias)
                {
                    zona = new ZonaHoraria();
                    zona.zonaHoraria = item.ZonaHoraria;
                    zona.idZonaHoraria = item.IdZonaHoraria;
                    listaZonasHorarias.Add(zona);
                }
                
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