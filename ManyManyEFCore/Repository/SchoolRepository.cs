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
    public interface ISchoolRepository : IRepository<School, SchoolData>
    {
    }

    public class SchoolRepository : Repository<School, SchoolData>, ISchoolRepository
    {
        public SchoolRepository(DbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper) : base(dbContext, unitOfWork, mapper)
        {
        }
    }
}
