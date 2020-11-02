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
    public class EmployeesController : ControllerBase
    {
        private readonly IDapper _dapper;
        public EmployeesController(IDapper dapper)
        {
            _dapper = dapper;
        }
        [HttpPost(nameof(Create))]
        public async Task<int> Create(Employee data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("FirstName", data.FirstName, DbType.String);
            dbparams.Add("LastName", data.LastName, DbType.String);
            dbparams.Add("Address", data.Address, DbType.String);
            dbparams.Add("BirthDate", data.BirthDate, DbType.Date);
            dbparams.Add("Phone", data.Phone, DbType.String);
            dbparams.Add("UserId", data.UserId, DbType.Int32);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_InsertEmployee]", dbparams, commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Employee> GetById(int id)
        {
            var result = await Task.FromResult(_dapper.Get<Employee>($"Select * from TB_M_Employee where Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet(nameof(GetAllData))]
        public List<Employee> GetAllData()
        {
            var result = (_dapper.GetAll<Employee>($"Select * from TB_M_Employee", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int id)
        {
            var result = await Task.FromResult(_dapper.Execute($"DELETE FROM TB_M_Employee WHERE Id = {id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(Employee data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Id", data.Id);
            dbparams.Add("FirstName", data.FirstName, DbType.String);
            dbparams.Add("LastName", data.LastName, DbType.String);
            dbparams.Add("Address", data.Address, DbType.String);
            dbparams.Add("BirthDate", data.BirthDate, DbType.Date);
            dbparams.Add("Phone", data.Phone, DbType.String);
            dbparams.Add("UserId", data.UserId, DbType.Int32);

            var updateUser = Task.FromResult(_dapper.Update<int>("[dbo].[SP_UpdateEmployee]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updateUser;
        }
    }
}
