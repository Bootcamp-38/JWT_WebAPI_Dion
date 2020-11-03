using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JWTWebAPI_Dion.DapperRepository;
using JWTWebAPI_Dion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTWebAPI_Dion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IDapper _dapper;
        public RolesController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(Role data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Name", data.Name, DbType.String);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertRole]", dbparams, commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Role> GetById(int id)
        {
            var result = await Task.FromResult(_dapper.Get<Role>($"Select * from TB_M_Role where Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetAllData))]
        public List<Role> GetAllData()
        {
            var result = (_dapper.GetAll<Role>($"Select * from TB_M_Role", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int id)
        {
            var result = await Task.FromResult(_dapper.Execute($"DELETE FROM TB_M_Role WHERE Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(Role data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id", data.Id);
            dbparams.Add("Name", data.Name, DbType.String);
            var updateUser = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateRole]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateUser;
        }
    }
}
