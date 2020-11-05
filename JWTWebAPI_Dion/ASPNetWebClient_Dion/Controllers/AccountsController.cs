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

        
    }
}
