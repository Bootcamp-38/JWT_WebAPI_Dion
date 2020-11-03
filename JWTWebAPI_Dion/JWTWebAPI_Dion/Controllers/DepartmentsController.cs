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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDapper _dapper;
        public DepartmentsController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(Department data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Name", data.Name, DbType.String);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertDepartment]", dbparams, commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Department> GetById(int id)
        {
            var result = await Task.FromResult(_dapper.Get<Department>($"Select * from TB_M_Department where Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetAllData))]
        public List<Department> GetAllData()
        {
            var result = (_dapper.GetAll<Department>($"Select * from TB_M_Department", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int id)
        {
            var result = await Task.FromResult(_dapper.Execute($"DELETE FROM TB_M_Department WHERE Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(Department data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id", data.Id);
            dbparams.Add("Name", data.Name, DbType.String);
            var updateUser = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateDepartment]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateUser;
        }
    }
}
