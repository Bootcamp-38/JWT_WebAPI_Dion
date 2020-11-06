using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ASPNetWebClient_Dion.Data;
using ASPNetWebClient_Dion.Helper;
using ASPNetWebClient_Dion.ViewModels;
using JWTWebAPI_Dion.Context;
using JWTWebAPI_Dion.Models;
using JWTWebAPI_Dion.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ASPNetWebClient_Dion.Controllers
{
    public class AccountsController : Controller
    {
        const string SessionEmail = "email";
        public IActionResult Index()
        {

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
                    char[] trimChars = { '/', '"' };

                    var jwt = response.Content.ReadAsStringAsync().Result.ToString();
                    var handler = new JwtSecurityTokenHandler().ReadJwtToken(jwt.Trim(trimChars)).Claims.FirstOrDefault(x => x.Type.Equals("RoleName")).Value;

                    HttpContext.Session.SetString("Role: ", handler);

                    return Json(new { result = "Redirect", url = Url.Action("About", "Home")});
                    //return Json(response.Content.ReadAsStringAsync().Result.ToString());

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
    }
}
