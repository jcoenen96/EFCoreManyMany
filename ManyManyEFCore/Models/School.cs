﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Lesson> Lessons { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}
