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
    public interface IStudentRepository : IRepository<Student, StudentData>
    {
        Task<Student> GetStudentWithLessons(int id);
    }

    public class StudentRepository : Repository<Student, StudentData>, IStudentRepository
    {
        public StudentRepository(DbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper) : base(dbContext, unitOfWork, mapper)
        {   
        }

        public async Task<Student> GetStudentWithLessons(int id)
        {
            var student = await GetDbSet<StudentData>()
                .Include(s => s.StudentLessons)
                .ThenInclude(sl => sl.Lesson)
                .Where(s => s.Id.Equals(id)).SingleOrDefaultAsync();
            return AutoMapper.Map<StudentData, Student>(student);
        }
    }
}
