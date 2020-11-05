using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ASPNetWebClient_Dion.Controllers
{
    public class AccountsController : Controller
    {
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
                    return Json(response.Content.ReadAsStringAsync().Result.ToString());
                }
                else
                {
                    return Content("GAGAL");
                }
            }
        }
    }
}
