using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNetWebClient_Dion.Models;
using JWTWebAPI_Dion.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using JWTWebAPI_Dion.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using ASPNetWebClient_Dion.Helper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace ASPNetWebClient_Dion.Controllers
{
    public class HomeController : Controller
    {
        const string SessionEmail = "_Email";
        const string SessionPass = "_Age";

        public IActionResult Index()
        {
            HttpContext.Session.SetString(SessionEmail, "Jarvik");
            HttpContext.Session.SetInt32(SessionPass, 345);
            return View();
        }

        [HttpPost]
        public ActionResult Get(UserRoleVM user)
        {
            using (HttpClient client = new HttpClient())
            {
                
                client.BaseAddress = new Uri("https://localhost:44382");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                string data = JsonConvert.SerializeObject(user);
                var contentData = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.PostAsync("/API/Accounts/Login", contentData).Result;
                if (response.IsSuccessStatusCode)
                {
                    char[] trimChars = { '/', '"'};

                    var jwt = response.Content.ReadAsStringAsync().Result.ToString();
                    var handler = new JwtSecurityTokenHandler().ReadJwtToken(jwt.Trim(trimChars)).Claims.FirstOrDefault(x=>x.Type.Equals("RoleName")).Value;

                    //return Json(new { result = "Redirect", url = Url.Action("Index", "Accounts"), data =jwt.ToString()});
                    return Json(response.Content.ReadAsStringAsync().Result.ToString());

                }
                else
                {
                    return Content("GAGAL");
                }
            }
        }

        protected string GetRole(string token)
        {
            string secret = "snfiAInfis1238NAIANDsndia";
            var key = Encoding.UTF8.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            SecurityToken validateToken;
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = "InventoryServicePostmanClient",
                ValidIssuer = "InventoryServiceAccessToken"
            };
            var claims = handler.ValidateToken(token, validations, out validateToken);
            return claims.Identity.Name;
        }

        public IActionResult About()
        {
            ViewBag.RoleName = HttpContext.Session.GetString("Role: ");
            ViewData["Message"] = "ASP.Net Core!!!";

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
    }
}
