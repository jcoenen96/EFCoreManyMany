using ManyManyEFCore.Models;
using ManyManyEFCore.Repository;
using ManyManyEFCore.Repository.Pattern;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStudentRepository repository;

        public StudentsController(IUnitOfWork unitOfWork, IStudentRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<List<Student>> GetStudents()
        {
            return await repository.GetAll();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await repository.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }


            repository.Update(student);
            await unitOfWork.SaveChangesAsync();
            return Ok(await repository.FindAsync(id));
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await repository.AddAsync(student);
            await unitOfWork.SaveChangesAsync();

            return Created(Url.Action(nameof(GetStudent)), student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
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
