using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCClient.Models;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> PrivacyAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
             var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5000");

            HttpClient httpClient = new HttpClient();
            //DiscoveryDocumentResponse discoveryDocument = await httpClient.GetDiscoveryDocumentAsync();



            string disco2 = "https://localhost:5000/.well-known/openid-configuration";
       
             
            //var disco = await client.GetDiscoveryDocumentAsync();
            
            UserInfoRequest request = new UserInfoRequest();
            request.Address = disco.UserInfoEndpoint;
            request.Token = accessToken;
            

            var response = await client.GetUserInfoAsync(request);
             

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
