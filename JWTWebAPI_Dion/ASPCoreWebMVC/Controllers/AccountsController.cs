using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWebMVC.Controllers
{
    public class AccountsController : Controller
    {
        readonly HttpClient client = new HttpClient { 
            BaseAddress = new Uri("https://localhost:44382/API/")
        };
        public IActionResult Index()
        {
            return View();
        }
    }
}
