using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManyManyEFCore.Data;
using ManyManyEFCore.DataModels;
using ManyManyEFCore.Repository.Pattern;
using ManyManyEFCore.Repository;
using ManyManyEFCore.Models;

namespace ManyManyEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILessonRepository repository;

        public LessonsController(IUnitOfWork unitOfWork, ILessonRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<List<Lesson>> GetLessons()
        {
            return await repository.GetAll();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLesson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lesson = await repository.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return Ok(lesson);
        }

        // PUT: api/Lessons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson([FromRoute] int id, [FromBody] Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lesson.Id)
            {
                return BadRequest();
            }


            repository.Update(lesson);
            await unitOfWork.SaveChangesAsync();
            return Ok(await repository.FindAsync(id));
        }

        // POST: api/Lessons
        [HttpPost]
        public async Task<IActionResult> PostLesson([FromBody] Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await repository.AddAsync(lesson);
            await unitOfWork.SaveChangesAsync();

            return Created(Url.Action(nameof(GetLesson)), lesson);
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson([FromRoute] int id)
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