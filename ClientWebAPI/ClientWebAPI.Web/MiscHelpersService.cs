using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ClientWebAPI.Web.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Client
{
    public interface IMiscHelpersService
    {
        Task<HttpResponseMessage> DeleteAsJsonAsync<TValue>(HttpClient httpClient, string requestUri, TValue value);
    }

    public class MiscHelpersService : IMiscHelpersService
    {
        public MiscHelpersService()
        {
        }
        public async Task<HttpResponseMessage> DeleteAsJsonAsync<TValue>(HttpClient httpClient, string requestUri, TValue value)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(value),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri, UriKind.Relative)
            };
            return await httpClient.SendAsync(request);
        }
    }
}