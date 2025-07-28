using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ClientWebAPI.Web.Models
{
    public partial class Comuna
    {
        public int Idcomuna { get; set; } 
        public int Idregion { get; set; }    
        public string comuna { get; set; } 
        public string informacionadicional { get; set; }
    }

    public partial class ComunaResponse
    {
        public string Idcomuna { get; set; }
        public string Idregion { get; set; }
        public string comuna { get; set; }
        public string informacionadicional { get; set; }
        public List<Comuna> Comuna { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public partial class ComunaBody
    {
        public List<Comuna> Comunas { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public partial class ComunasPorRegion
    {
        public Region Region { get; set; }

        public Comuna Comuna { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
