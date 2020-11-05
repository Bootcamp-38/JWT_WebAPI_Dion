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
                    return Json(response.Content.ReadAsStringAsync().Result.ToString());
                }
                else
                {
                    return Content("GAGAL");
                }
            }
        }

        public IActionResult About()
        {
            ViewBag.Email = HttpContext.Session.GetString(SessionEmail);
            ViewBag.Pass = HttpContext.Session.GetInt32(SessionPass);
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
