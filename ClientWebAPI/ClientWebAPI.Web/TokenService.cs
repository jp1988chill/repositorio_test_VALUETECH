using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientWebAPI.Web.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static ClientWebAPI.Api.Core.Enveloped.EnvelopedObject;

namespace Client
{
    public interface ITokenService
    {
        Task<bool> CleanToken();
        Task<string> GetToken();
        Task<string> GetRestAPIPath();
    }

    public class TokenService : ITokenService
    {
        private string _token = null;
        private readonly IOptions<ClientWebAPIConfig> _MantenedorMVCEntitySettings;
        public TokenService(IOptions<ClientWebAPIConfig> MantenedorMVCEntitySettings) => _MantenedorMVCEntitySettings = MantenedorMVCEntitySettings;

        public async Task<string> GetToken()
        {
	        if(_token == null) { 
                _token = await GetNewAccessToken();
            }
            return _token;
        }

        public async Task<bool> CleanToken()
        {
            _token = null;
            return true;
        }

        public async Task<string> GetRestAPIPath()
        {
            return _MantenedorMVCEntitySettings.Value.TokenUrl;
        }

        private async Task<String> GetNewAccessToken()
        {
            /*
	        var Name = _MantenedorMVCEntitySettings.Value.Name;
	        var Password = _MantenedorMVCEntitySettings.Value.Password;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_MantenedorMVCEntitySettings.Value.TokenUrl + "/");
                client.DefaultRequestHeaders.Add("Token", "");
                var verb = "CrearToken";
                try
                {
                    List<User> users = new List<User>();
                    users.Add(new User() { Name = Name, Password = Password, Token = new Guid(), Tokenleasetime = "" });
                    UserTokenServiceRequest request = new UserTokenServiceRequest(users);
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    UserTokenServiceResponse resp = JsonConvert.DeserializeObject<UserTokenServiceResponse>(response.Content.ReadAsStringAsync().Result);
                    
                    if (resp.usersNuevoTokenAsignado.Count > 0)
                    {
                        return resp.usersNuevoTokenAsignado[0].Token.ToString();
                    }
                }
                catch 
                {
                    
                }
                throw new ApplicationException("Unable to retrieve access token from ClientWebAPI");
            }
            */
            return "";
        }
    }
}