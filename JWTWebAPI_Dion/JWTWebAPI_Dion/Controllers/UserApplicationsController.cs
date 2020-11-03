using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JWTWebAPI_Dion.DapperRepository;
using JWTWebAPI_Dion.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTWebAPI_Dion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApplicationsController : ControllerBase
    {
        private readonly IDapper _dapper;
        public UserApplicationsController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(UserApplication data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("UserId", data.UserId, DbType.Int32);
            dbparams.Add("ApplicationId", data.ApplicationId, DbType.Int32);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertUserApplication]", dbparams, commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<UserApplication> GetById(int id)
        {
            var result = await Task.FromResult(_dapper.Get<UserApplication>($"Select * from TB_T_UserApplication where Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetAllData))]
        public List<UserApplication> GetAllData()
        {
            var result = (_dapper.GetAll<UserApplication>($"Select * from TB_T_UserApplication", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int id)
        {
            var result = await Task.FromResult(_dapper.Execute($"DELETE FROM TB_T_UserApplication WHERE Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(UserApplication data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id", data.Id);
            dbparams.Add("UserId", data.UserId, DbType.Int32);
            dbparams.Add("ApplicationId", data.ApplicationId, DbType.Int32);

            var updateUser = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateUserApplication]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateUser;
        }
    }
}
