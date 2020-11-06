using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using JWTWebAPI_Dion.DapperRepository;
using JWTWebAPI_Dion.Models;
using JWTWebAPI_Dion.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JWTWebAPI_Dion.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IDapper _dapper;
        public AccountsController(IConfiguration config, IDapper dapper)
        {
            _configuration = config;
            _dapper = dapper;
        }

        [HttpPost(nameof(Login))]
        public async Task<string> Login(UserRoleVM userRoleVM)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@UserEmail", userRoleVM.UserEmail, DbType.String);
            dbparams.Add("@UserPassword", userRoleVM.UserPassword, DbType.String);
            var result = await Task.FromResult(_dapper.Get<UserRoleVM>("[dbo].[SP_LoginUser]", 
                dbparams, commandType: CommandType.StoredProcedure));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("RoleName", result.RoleName),
                    new Claim("UserEmail", result.UserEmail)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
            //return result;
        }

        [HttpPost(nameof(Register))]
        public async Task<int> Register(User data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("@Email", data.Email, DbType.String);
            dbparams.Add("@Password", data.Password, DbType.String);
            //dbparams.Add("IsUpdatePassword", data.IsUpdatePassword);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertUser]", dbparams, commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpPatch(nameof(ChangePassword))]
        public Task<int> ChangePassword(User data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id", data.Id);
            //dbparams.Add("Email", data.Email, DbType.String);
            dbparams.Add("Password", data.Password, DbType.String);

            var updateUser = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UserChangePassword]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateUser;
        }
    }
}
