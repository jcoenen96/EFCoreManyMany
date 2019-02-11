using ManyManyEFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.DataModels
{
    public class SchoolData : School
    {
        public new List<LessonData> Lessons { get; set; }
        public new List<StudentData> Students { get; set; }
    }
}
