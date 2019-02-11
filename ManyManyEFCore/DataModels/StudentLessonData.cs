using AutoMapper;
using ManyManyEFCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.DataModels
{
    public class StudentLessonData
    {
        public int StudentId { get; set; }
        public StudentData Student { get; set; }
        public int LessonId { get; set; }
        public LessonData Lesson { get; set; }

        [NotMapped]
        public LessonData LessonMap {
            get
            {
                return this.Lesson;
            }
            set
            {
                this.Lesson = value;
                this.LessonId = value.Id;
            }
        }

        [NotMapped]
        public StudentData StudentMap {
            get
            {
                return this.Student;
            }
            set
            {
                this.Student = value;
                this.StudentId = value.Id;
            }
        }

        [NotMapped]
        public class StudentConverter : ITypeConverter<List<StudentLessonData>, Student>
        {
            public Student Convert(List<StudentLessonData> source, Student destination, ResolutionContext context)
            {
                foreach (var sld in source)
                {
                    destination.Id = sld.StudentMap.Id;
                    destination.Age = sld.StudentMap.Age;
                    destination.Name = sld.StudentMap.Name;
                    destination.Lessons.Add(sld.LessonMap);
                }
                return destination;
            }
        }

        [NotMapped]
        public class LessonConverter : ITypeConverter<List<StudentLessonData>, Lesson>
        {
            public Lesson Convert(List<StudentLessonData> source, Lesson destination, ResolutionContext context)
            {
                foreach (var sld in source)
                {
                    destination.Id = sld.LessonMap.Id;
                    destination.Name = sld.LessonMap.Name;
                    destination.Students.Add(sld.StudentMap);
                }
                return destination;
            }
        }
    }
}
