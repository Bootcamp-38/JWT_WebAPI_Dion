using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JWTWebAPI_Dion.Base;
using JWTWebAPI_Dion.DapperRepository;
using JWTWebAPI_Dion.Models;
using JWTWebAPI_Dion.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTWebAPI_Dion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : BaseController<Application, ApplicationRepository>
    {
        public ApplicationsController(ApplicationRepository application) : base(application)
        {

        }
    }
}
