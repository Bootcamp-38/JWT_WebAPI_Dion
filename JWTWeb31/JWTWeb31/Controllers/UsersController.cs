using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JWTWeb31.DapperConfig;
using JWTWeb31.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTWeb31.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDapper _dapper;
        public UsersController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(User data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Email", data.Email, DbType.String);
            dbparams.Add("Password", data.Password, DbType.String);
            dbparams.Add("IsUpdatePassword", data.IsUpdatePassword, DbType.String);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertUser]", dbparams, commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<User> GetById(int id)
        {
            var result = await Task.FromResult(_dapper.Get<User>($"Select * from TB_M_User where Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetAllData))]
        public List<User> GetAllData()
        {
            var result = (_dapper.GetAll<User>($"Select * from TB_M_User", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int id)
        {
            var result = await Task.FromResult(_dapper.Execute($"DELETE FROM TB_M_User WHERE Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(User data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id", data.Id);
            dbparams.Add("Email", data.Email, DbType.String);
            dbparams.Add("Password", data.Password, DbType.String);
            dbparams.Add("IsUpdatePassword", data.IsUpdatePassword, DbType.String);

            var updateUser = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateUser]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateUser;
        }
    }
}
