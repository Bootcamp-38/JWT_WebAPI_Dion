using JWTWebAPI_Dion.Context;
using JWTWebAPI_Dion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebAPI_Dion.Repositories.Data
{
    public class ApplicationRepository : GeneralRepository<Application, MyContext>
    {
        public ApplicationRepository(MyContext myContext) : base(myContext) 
        { 
            
        }
    }
}
