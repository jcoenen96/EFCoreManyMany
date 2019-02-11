using ManyManyEFCore.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.Configurations.DataModels
{
    public class StudentLessonDataConfiguration : IEntityTypeConfiguration<StudentLessonData>
    {
        public void Configure(EntityTypeBuilder<StudentLessonData> builder)
        {
            builder.HasKey(sl => new { sl.LessonId, sl.StudentId });

            //lesson
            builder.HasOne(sl => sl.Lesson).WithMany(l => l.StudentLessons).HasForeignKey(sl => sl.LessonId);

            //student
            builder.HasOne(sl => sl.Student).WithMany(s => s.StudentLessons).HasForeignKey(sl => sl.StudentId);
        }
    }
}
