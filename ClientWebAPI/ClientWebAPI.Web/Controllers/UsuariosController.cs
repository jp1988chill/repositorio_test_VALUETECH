using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientWebAPI.Web.Models;
using System.Reflection;
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Policy;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using Client;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace ClientWebAPI.Web
{
    public class UsuariosController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IMiscHelpersService _miscHelpersService;

        public string RestAPIPath = String.Empty;
        public UsuariosController(ITokenService tokenService, IMiscHelpersService miscHelpersService)
        {
            _tokenService = tokenService;
            _miscHelpersService = miscHelpersService;
            RestAPIPath = (_tokenService.GetRestAPIPath().Result);
        }

        /////////////////////////Métodos ASP .NET Core 3.x: Implementación Rest API Microservicios Inicio /////////////////////////

        
        [HttpPost, Produces("application/json")]
        public async Task<List<Region>> ListAllRegiones()
        {
            List<Region> list = new List<Region>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RestAPIPath + "/");
                var APIKeyToken = await _tokenService.GetToken();
                client.DefaultRequestHeaders.Add("Token", APIKeyToken);
                var verb = "ObtenerRegiones/";
                try
                {
                    HttpResponseMessage response = client.GetAsync(verb).Result;
                    response.EnsureSuccessStatusCode();
                    UserTokenServiceResponse resp = JsonConvert.DeserializeObject<UserTokenServiceResponse>(response.Content.ReadAsStringAsync().Result);
                    if (resp.regionesNuevoTokenAsignado.Count > 0)
                    {
                        list.AddRange(resp.regionesNuevoTokenAsignado.Select(item => new Region()
                        {
                            Idregion = Convert.ToInt32(item.Idregion),
                            region = item.region
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return list;
        }


        [HttpPost, Produces("application/json")]
        public async Task<List<Comuna>> ListAllComunas()
        {
            List<Comuna> list = new List<Comuna>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RestAPIPath + "/");
                var APIKeyToken = await _tokenService.GetToken();
                client.DefaultRequestHeaders.Add("Token", APIKeyToken);
                var verb = "ObtenerComunas";
                try
                {
                    HttpResponseMessage response = client.PostAsync(verb, new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    CardTokenServiceResponse resp = JsonConvert.DeserializeObject<CardTokenServiceResponse>(response.Content.ReadAsStringAsync().Result);
                    
                    if ((resp.comunaNuevoTokenAsignado != null) && (resp.comunaNuevoTokenAsignado.Count > 0))
                    {
                        list.AddRange(resp.comunaNuevoTokenAsignado.Select(item => new Comuna()
                        {
                            Idcomuna = item.Idcomuna,
                            Idregion = item.Idregion,
                            comuna = item.comuna,
                            informacionadicional = item.informacionadicional
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return list;
        }

        [HttpPost, Produces("application/json")]
        public async Task<List<Comuna>> GetCard(string inId)
        {
            List<Comuna> list = new List<Comuna>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RestAPIPath + "/");
                var APIKeyToken = await _tokenService.GetToken();
                client.DefaultRequestHeaders.Add("Token", APIKeyToken);
                var verb = "ObtenerComuna/" + inId;
                try
                {
                    HttpResponseMessage response = client.GetAsync(verb).Result;
                    response.EnsureSuccessStatusCode();
                    CardTokenServiceResponse resp = JsonConvert.DeserializeObject<CardTokenServiceResponse>(response.Content.ReadAsStringAsync().Result);

                    if (resp.comunaNuevoTokenAsignado.Count > 0)
                    {
                        list.AddRange(resp.comunaNuevoTokenAsignado.Select(item => new Comuna()
                        {
                            Idcomuna = item.Idcomuna,
                            Idregion = item.Idregion,
                            comuna = item.comuna,
                            informacionadicional = item.informacionadicional
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return list;
        }

        [HttpPost, Produces("application/json")]
        public async Task<List<Comuna>> GenerateComuna(List<Comuna> cards)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RestAPIPath + "/");
                var APIKeyToken = await _tokenService.GetToken();
                client.DefaultRequestHeaders.Add("Token", APIKeyToken);
                var verb = "CrearComuna";
                try
                {
                    CardTokenServiceRequest request = new CardTokenServiceRequest(cards);
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    CardTokenServiceResponse resp = JsonConvert.DeserializeObject<CardTokenServiceResponse>(response.Content.ReadAsStringAsync().Result);
                    return resp.comunaNuevoTokenAsignado;
                }
                catch /*(Exception ex)*/
                {
                    return null;
                }
            }
        }


        [HttpPost, Produces("application/json")]
        public async Task<List<Region>> GenerateRegion(List<Region> user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RestAPIPath + "/");
                var APIKeyToken = await _tokenService.GetToken();
                client.DefaultRequestHeaders.Add("Token", APIKeyToken);
                var verb = "CrearRegion";
                try
                {
                    UserTokenServiceRequest request = new UserTokenServiceRequest(user);
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    UserTokenServiceResponse resp = JsonConvert.DeserializeObject<UserTokenServiceResponse>(response.Content.ReadAsStringAsync().Result);
                    return resp.regionesNuevoTokenAsignado;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        [HttpPost, Produces("application/json")]
        public async Task<CardTokenServiceResponse> UpdateCard(string inIdUsuarios, Comuna card)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RestAPIPath + "/");
                var APIKeyToken = await _tokenService.GetToken();
                client.DefaultRequestHeaders.Add("Token", APIKeyToken);
                var verb = "ActualizarComuna";
                try
                {
                    List<Comuna> list = new List<Comuna>();
                    list.Add(card);
                    CardTokenServiceRequest request = new CardTokenServiceRequest(list);
                    HttpResponseMessage response = client.PutAsync(verb, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")).Result;
                    response.EnsureSuccessStatusCode();
                    CardTokenServiceResponse resp = JsonConvert.DeserializeObject<CardTokenServiceResponse>(response.Content.ReadAsStringAsync().Result);
                    return resp;
                }
                catch /*(Exception ex)*/
                {
                    return null;
                }
            }
        }

        [HttpPost, Produces("application/json")]
        public async Task<UserTokenServiceResponse> DeleteUsers(List<Region> users)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(RestAPIPath + "/");
                    var APIKeyToken = await _tokenService.GetToken();
                    client.DefaultRequestHeaders.Add("Token", APIKeyToken);
                    UserTokenServiceRequest request = new UserTokenServiceRequest(users);
                    var result = _miscHelpersService.DeleteAsJsonAsync(client, "EliminarUsuario", request).Result;
                    result.EnsureSuccessStatusCode();
                    UserTokenServiceResponse resp = JsonConvert.DeserializeObject<UserTokenServiceResponse>(result.Content.ReadAsStringAsync().Result);
                    return resp;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        [HttpPost, Produces("application/json")]
        public async Task<CardTokenServiceResponse> DeleteCards(List<Comuna> cards)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(RestAPIPath + "/");
                    var APIKeyToken = await _tokenService.GetToken();
                    client.DefaultRequestHeaders.Add("Token", APIKeyToken);
                    CardTokenServiceRequest request = new CardTokenServiceRequest(cards);
                    var result = _miscHelpersService.DeleteAsJsonAsync(client, "EliminarTarjeta", request).Result;
                    result.EnsureSuccessStatusCode();
                    CardTokenServiceResponse resp = JsonConvert.DeserializeObject<CardTokenServiceResponse>(result.Content.ReadAsStringAsync().Result);
                    return resp;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        [HttpPost, Produces("application/json")]
        public async Task<CardTokenServiceResponse> DeleteCard(string inId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(RestAPIPath + "/");
                    var APIKeyToken = await _tokenService.GetToken();
                    client.DefaultRequestHeaders.Add("Token", APIKeyToken);
                    List<Comuna> cards = GetCard(inId).Result;
                    CardTokenServiceRequest request = new CardTokenServiceRequest(cards);
                    var result = _miscHelpersService.DeleteAsJsonAsync(client, "EliminarComuna", request).Result;
                    result.EnsureSuccessStatusCode();
                    CardTokenServiceResponse resp = JsonConvert.DeserializeObject<CardTokenServiceResponse>(result.Content.ReadAsStringAsync().Result);
                    return resp;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /////////////////////////Métodos ASP .NET Core 3.x: Implementación Rest API Microservicios Fin /////////////////////////

        // GET: Usuarios
        public async Task<IActionResult> List()
        {
            List<Comuna> list = await ListAllComunas();
            return View(list);
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(string nombreRegion){
            List<ComunasPorRegion> ret = new List<ComunasPorRegion>();
            List<Comuna> lst = (await ListAllComunas());
            List<Region> lstReg = (await ListAllRegiones());

            //If no data exists, initialize database with default values.
            if ((lst.Count <= 0) /*&& (lstReg.Count <= 0)*/ )
            {
                lstReg.Add(new Region() { region = "Región de Arica y Parinacota" });
                lstReg.Add(new Region() { region = "Región de Tarapacá" });
                lstReg.Add(new Region() { region = "Región de Antofagasta" });
                lstReg.Add(new Region() { region = "Región de Atacama" });
                lstReg.Add(new Region() { region = "Región de Coquimbo" });
                lstReg.Add(new Region() { region = "Región de Valparaíso" });
                lstReg.Add(new Region() { region = "Región Metropolitana" });
                lstReg.Add(new Region() { region = "Región del Libertador General Bernardo O’Higgins" });
                lstReg.Add(new Region() { region = "Región del Maule" });
                lstReg.Add(new Region() { region = "Región de Ñuble" });
                lstReg.Add(new Region() { region = "Región del Biobío" });
                lstReg.Add(new Region() { region = "Región de La Araucanía" });
                lstReg.Add(new Region() { region = "Región de Los Ríos" });
                lstReg.Add(new Region() { region = "Región de Los Lagos" });
                lstReg.Add(new Region() { region = "Región de Aysén" });
                lstReg.Add(new Region() { region = "Región de Magallanes y de la Antártica Chilena: Punta Arenas" });

                var regionesGeneradas = await GenerateRegion(lstReg);

                if (regionesGeneradas == null)
                {
                    regionesGeneradas = lstReg;
                }

                lst.Add(new Comuna() { Idcomuna = 0, Idregion = regionesGeneradas.FirstOrDefault().Idregion, comuna = "Puyehue", informacionadicional = "< Info >< Superficie > 4799.4 </ Superficie >< Poblacion Densidad = '51.6' > 247552 </ Poblacion ></ Info > " });
                lst.Add(new Comuna() { Idcomuna = 0, Idregion = regionesGeneradas.FirstOrDefault().Idregion, comuna = "Rancagua", informacionadicional = "< Info >< Superficie > 4799.4 </ Superficie >< Poblacion Densidad = '51.6' > 247552 </ Poblacion ></ Info > " });

                var comunasGeneradas = await GenerateComuna(lst);

                //Generar lista de Comunas para visualizar
                foreach (Region reg in regionesGeneradas)
                {
                    foreach (Comuna comu in comunasGeneradas.Where(item => Convert.ToInt32(item.Idregion) == reg.Idregion))
                    {
                        if (comu.Idcomuna != 0)
                        {
                            ret.Add(new ComunasPorRegion() { Region = reg, Comuna = comu });
                        }
                    }
                }

            }
            else
            {
                if ((nombreRegion != null) && !nombreRegion.Contains("*"))
                {
                    try
                    {
                        foreach (Region reg in lstReg.Where(item => item.region.ToUpper().Contains(nombreRegion.ToUpper())))
                        {
                            foreach (Comuna comu in lst.Where(item => Convert.ToInt32(item.Idregion) == reg.Idregion))
                            {
                                ret.Add(new ComunasPorRegion() { Region = reg, Comuna = comu });
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    foreach (Region reg in lstReg)
                    {
                        foreach (Comuna comu in lst.Where(item => Convert.ToInt32(item.Idregion) == reg.Idregion))
                        {
                            ret.Add(new ComunasPorRegion() { Region = reg, Comuna = comu });
                        }
                    }
                }
            }
            return View(ret);
        }

        // GET: idComuna
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<ComunasPorRegion> ret = new List<ComunasPorRegion>();
            List<Comuna> lst = (await ListAllComunas());
            List<Region> lstReg = (await ListAllRegiones());

            if (ret == null)
            {
                return NotFound();
            }

            foreach (Comuna comu in lst.Where(item => item.Idcomuna == Convert.ToInt32(id)))
            {
                foreach (Region reg in lstReg.Where(item => item.Idregion == comu.Idregion))
                {
                    ret.Add(new ComunasPorRegion() { Region = reg, Comuna = comu });
                }
            }

            return View(ret.FirstOrDefault());
        }

        public IActionResult CreateUser()
        {
            var user = new Region() { Idregion = 0 ,region = "" };
            return View(user);
        }

        public IActionResult CreateCard()
        {
            List<Region> users = ListAllRegiones().Result;
            var card = new Comuna() { Idcomuna = 0, Idregion = 0, comuna = "", informacionadicional = "" };
            return View(card);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("Idregion,region")] ClientWebAPI.Web.Models.Region usuariosInst)
        {
            List<Region> lst = new List<Region>();
            lst.Add(usuariosInst);
            var usuariosset = await GenerateRegion(lst);
            return View(usuariosset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCard([Bind("Idcomuna,Idregion,comuna,informacionadicional")] ClientWebAPI.Web.Models.Comuna cardInst)
        {
            List<Comuna> lst = new List<Comuna>();
            cardInst.Idcomuna = 0;
            lst.Add(cardInst);
            List<Comuna> usuariosset = await GenerateComuna(lst);
            return View(usuariosset[0]);
        }

        // GET: idComuna
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<ComunasPorRegion> ret = new List<ComunasPorRegion>();
            List<Comuna> lst = (await ListAllComunas());
            List<Region> lstReg = (await ListAllRegiones());

            if (ret == null)
            {
                return NotFound();
            }

            foreach (Comuna comu in lst.Where(item => item.Idcomuna == Convert.ToInt32(id)))
            {
                foreach (Region reg in lstReg.Where(item => item.Idregion == comu.Idregion))
                {
                    ret.Add(new ComunasPorRegion() { Region = reg, Comuna = comu });
                }
            }

            return View(ret.FirstOrDefault());
        }

        // POST: idComuna
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Region,Comuna")] ComunasPorRegion comunasPorRegion)
        {
            if (null == comunasPorRegion.Comuna)
            {
                return NotFound();
            }

            var comu = (await ListAllComunas()).Where(item => item.Idcomuna == Convert.ToInt32(id)).FirstOrDefault();
            if (comu != null){
                comunasPorRegion.Comuna.Idregion = comu.Idregion;
            }

            var usuariosset = await UpdateCard(id.ToString(), comunasPorRegion.Comuna);
            if (usuariosset == null)
            {
                return NotFound();
            }
            if (usuariosset.httpCode != 200)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: idComuna
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosset = await GetCard(id.ToString());
            if (usuariosset == null)
            {
                return NotFound();
            }

            return View(usuariosset.FirstOrDefault());
        }

        // POST: idComuna
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await DeleteCard(id.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}


