using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ClientWebAPI.Web.Models
{
    public partial class Region
    {
        public int Idregion { get; set; }    
        public string region { get; set; }    
    }

    public partial class RegionResponse
    {
        public string Idregion { get; set; }
        public string region { get; set; }
        public string Tokenleasetime { get; set; } //allows the token to last up to 10 minutes

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
