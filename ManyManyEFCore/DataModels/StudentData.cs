using ManyManyEFCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.DataModels
{
    public class StudentData : Student
    {
        public List<StudentLessonData> StudentLessons { get; set; }

        [NotMapped]
        private new List<LessonData> Lessons { get; set; }
    }
}
