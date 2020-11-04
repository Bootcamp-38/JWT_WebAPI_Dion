using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTWebAPI_Dion.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTWebAPI_Dion.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;
        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<TEntity>> Get() 
        {
            var result = await repository.Get();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(new { data = result });
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var get = await repository.Get(id);
            if(get == null)
            {
                return NotFound();
            }
            return Ok(new { data = get });
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            await repository.Post(entity);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            await repository.Put(entity);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var delete = await repository.Delete(id);
            if (delete == null)
            {
                return NotFound();
            }
            return delete;

        }

    }
}
