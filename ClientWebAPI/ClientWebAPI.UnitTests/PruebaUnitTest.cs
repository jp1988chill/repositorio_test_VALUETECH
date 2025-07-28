using Client;
using ClientWebAPI.Web;
using ClientWebAPI.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Prueba.UnitTests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


/// <summary>
/// ///////////////////////////////// Prueba Unitaria que realiza prueba en tiempo real de un Cliente consumiendo los servicios
/// </summary>
namespace ClientWebAPI.UnitTests
{
    

    [TestClass]
    public class ClientWebAPIUnitTest
    {
        /////////////////////////Métodos ASP .NET Core 3.x: Implementación Rest API Microservicios Inicio /////////////////////////
        private readonly Client.ITokenService _tokenService;
        private readonly IMiscHelpersService _miscHelpersService;
        private readonly UsuariosController _usuariosController;
        private readonly IConfiguration Configuration;
        public ClientWebAPIUnitTest()
        {
            var services = new ServiceCollection();

            //Add IOption dependency
            services.AddOptions();

            //Build a new Configuration required by Client Rest API Controller
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            //Rest API Security implements and rely on Token Handler Management
            services.AddSingleton<ITokenService, TokenService>();
            //Access to MiscHelpers
            services.AddSingleton<IMiscHelpersService, MiscHelpersService>();

            services.Configure<ClientWebAPIConfig>(Configuration.GetSection("ClientWebAPI"));
            _tokenService = services.BuildServiceProvider().GetService<ITokenService>();
            _miscHelpersService = services.BuildServiceProvider().GetService<IMiscHelpersService>();
            _usuariosController = new UsuariosController(_tokenService, _miscHelpersService);
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Casos de Prueba #1:

            //Eliminar/Crear Usuarios, generar Token y crear una Tarjeta
            var listUsers = _usuariosController.ListAllRegiones().Result;
            var listCards = _usuariosController.ListAllComunas().Result;
            if(listUsers.Count > 0) {
                var res = _usuariosController.DeleteUsers(listUsers).Result;
            }
            if (listCards.Count > 0)
            {
                var res = _usuariosController.DeleteCards(listCards).Result;
            }

            _tokenService.CleanToken(); //Clean and force new Token generation
                                        //because we deleted the old user associated to it

            listUsers = _usuariosController.ListAllRegiones().Result;
            listCards = _usuariosController.ListAllComunas().Result;
            Assert.AreEqual(listUsers.Count + listCards.Count, 1); //Only the current new user per Token is allowed

            
        }
    }
}
