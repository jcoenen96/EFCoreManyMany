using AutoMapper;
using ManyManyEFCore.DataModels;
using ManyManyEFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.MapperProfiles
{
    public class DbProfile : Profile
    {
        public DbProfile()
        {
            CreateMap<School, SchoolData>().ReverseMap();

            CreateMap<List<StudentLessonData>, Student>()
                .ConvertUsing<StudentLessonData.StudentConverter>();

            CreateMap<List<StudentLessonData>, Lesson>()
                .ConvertUsing<StudentLessonData.LessonConverter>();

            CreateMap<StudentData, Student>().ForMember(sd => sd.Lessons, s => s.MapFrom(src => src.StudentLessons.Select(sl => sl.StudentMap).ToList())).ReverseMap();
            CreateMap<LessonData, Lesson>().ForMember(ld => ld.Students, l => l.MapFrom(src => src.StudentLessons.Select(sl => sl.LessonMap).ToList())).ReverseMap();
        }
    }
}
