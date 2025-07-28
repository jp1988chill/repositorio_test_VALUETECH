using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Prueba.Domain
{
    public class Comuna
    {
        public Comuna(int idcomuna, int idregion, string comuna, string informacionadicional)
        {
            this.Idcomuna = idcomuna;
            this.Idregion = idregion;
            this.comuna = comuna;
            this.informacionadicional = informacionadicional;
        }

        [Key]
        public int Idcomuna { get; set; } //ID objeto interno .NET
        public int Idregion { get; set; } 
        public string comuna { get; set; }

        [XmlAttribute("InformacionAdicional")]
        public string informacionadicional { get; set; }
    }

    public class ComunaResponse
    {
        public int HttpCode { get; set; }
        public string HttpMessage { get; set; }
        public string MoreInformation { get; set; }
        public string comunaFriendlyError { get; set; }
        public List<Comuna> comunaNuevoTokenAsignado { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ComunaBody
    {
        public List<Comuna> Comunas { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}