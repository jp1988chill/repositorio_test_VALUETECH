using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prueba.Domain
{
    public class Region
    {
        public Region(int idregion, string region)
        {
            this.Idregion = idregion;
            this.region = region;
        }

        [Key]
        public int Idregion { get; set; } //PK: Autoinc

        public string region { get; set; } // 1 Region : N Comunas
    }

    public class RegionResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string regionFriendlyError { get; set; }
        public List<Region> regionesNuevoTokenAsignado { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class RegionBody
    {
        public List<Region> Regiones { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}