using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManyManyEFCore.Data;
using ManyManyEFCore.DataModels;
using ManyManyEFCore.Repository;
using ManyManyEFCore.Models;
using ManyManyEFCore.Repository.Pattern;

namespace ManyManyEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISchoolRepository repository;

        public SchoolsController(IUnitOfWork unitOfWork, ISchoolRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<List<School>> GetSchools()
        {
            return await repository.GetAll();
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchool([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var school = await repository.FindAsync(id); 

            if (school == null)
            {
                return NoContent();
            }

            return Ok(school);
        }

        // PUT: api/Schools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool([FromRoute] int id, [FromBody] School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Update(school);
            await unitOfWork.SaveChangesAsync();
            return Ok(await repository.FindAsync(id));
        }

        // POST: api/Schools
        [HttpPost]
        public async Task<IActionResult> PostSchool([FromBody] School school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await repository.AddAsync(school);
            await unitOfWork.SaveChangesAsync();
            return Created(Url.Action(nameof(GetSchool)), school);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await repository.RemoveAsync(id);
            await unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}