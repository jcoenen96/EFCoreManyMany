using AutoMapper;
using ManyManyEFCore.DataModels;
using ManyManyEFCore.Models;
using ManyManyEFCore.Repository.Pattern;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.Repository
{
    public interface ILessonRepository : IRepository<Lesson, LessonData>
    {

    }
    public class LessonRepository : Repository<Lesson, LessonData>, ILessonRepository
    {
        public LessonRepository(DbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper) : base(dbContext, unitOfWork, mapper)
        {
        }

        public async Task<Lesson> GetLessonWithStudents(int id)
        {
            var lesson = await GetDbSet<LessonData>()
                .Include(s => s.StudentLessons)
                .ThenInclude(sl => sl.Student)
                .Where(s => s.Id.Equals(id)).SingleOrDefaultAsync();
            return AutoMapper.Map<LessonData, Lesson>(lesson);
        }
    }
}
