using System;
using System.Collections.Generic;
using ClientWebAPI.Web.Models;
using Newtonsoft.Json;

namespace Client
{
    public class UserTokenServiceRequest
    {
        public UserTokenServiceRequest(List<Region> regiones)
        {
            this.Regiones = regiones;
        }
        public List<Region> Regiones { get; set; }
    }

    public class UserTokenServiceResponse
    {
        public int httpCode { get; set; }
        public string httpMessage { get; set; }
        public string moreInformation { get; set; }
        public string userFriendlyError { get; set; }
        public List<Region> regionesNuevoTokenAsignado { get; set; }
    }

    ///////////////////////////////////////////////////////////////////////////
    public class CardTokenServiceRequest
    {
        public CardTokenServiceRequest(List<Comuna> comunaNuevoTokenAsignado)
        {
            this.Comunas = comunaNuevoTokenAsignado;
        }
        public List<Comuna> Comunas { get; set; }
    }

    public class CardTokenServiceResponse
    {
        public int httpCode { get; set; }
        public string httpMessage { get; set; }
        public string moreInformation { get; set; }
        public string comunaFriendlyError { get; set; }
        public List<Comuna> comunaNuevoTokenAsignado { get; set; }
    }
}