using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MVC.Client.Models;

namespace MVC.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAuthenticationService _authSvc;

        public HomeController(IAuthenticationService authSvc)
        {
            _authSvc = authSvc;
        }

        public async Task<IActionResult> Index()
        {
            await WriteIdentityInformation();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task WriteIdentityInformation() {
            var identityToken = await _authSvc.GetTokenAsync(HttpContext, OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity token: {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim Value: {claim.Value}");
            }
        }
    }
}
