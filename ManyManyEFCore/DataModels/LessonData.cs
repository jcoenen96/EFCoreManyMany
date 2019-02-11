using ManyManyEFCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.DataModels
{
    public class LessonData : Lesson
    {
        public List<StudentLessonData> StudentLessons { get; set; }

        [NotMapped]
        private new List<StudentData> Students { get; set; }
    }
}
