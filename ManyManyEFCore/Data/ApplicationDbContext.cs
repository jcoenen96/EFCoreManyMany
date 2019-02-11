using ManyManyEFCore.Configurations;
using ManyManyEFCore.Configurations.DataModels;
using ManyManyEFCore.DataModels;
using ManyManyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.Data
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        //default models
        //public DbSet<School> Schools { get; set; }
        //public DbSet<Student> Students { get; set; }
        //public DbSet<Lesson> Lessons { get; set; }

        //special database models for many to many
        public DbSet<SchoolData> Schools { get; set; }
        public DbSet<StudentData> Students { get; set; }
        public DbSet<LessonData> Lessons { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //default models
            //modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            //modelBuilder.ApplyConfiguration(new LessonConfiguration());
            //modelBuilder.ApplyConfiguration(new StudentConfiguration());

            //special database models for many to many
            modelBuilder.ApplyConfiguration(new SchoolDataConfiguration());
            modelBuilder.ApplyConfiguration(new LessonDataConfiguration());
            modelBuilder.ApplyConfiguration(new StudentDataConfiguration());
            modelBuilder.ApplyConfiguration(new StudentLessonDataConfiguration());

            //relations
            modelBuilder.Entity<SchoolData>().HasMany(sd => sd.Lessons).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SchoolData>().HasMany(sd => sd.Students).WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentData>().HasMany(sd => sd.StudentLessons).WithOne().OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(sd => sd.LessonId);
            modelBuilder.Entity<LessonData>().HasMany(ld => ld.StudentLessons).WithOne().OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(sd => sd.LessonId);

            modelBuilder.Entity<StudentLessonData>().HasOne(sld => sld.Student).WithMany().OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(sd => sd.StudentId);
            modelBuilder.Entity<StudentLessonData>().HasOne(sld => sld.Lesson).WithMany().OnDelete(DeleteBehavior.ClientSetNull).HasForeignKey(sd => sd.LessonId);

            
            //seeding
            var lesson1 = new LessonData() { Id = 1, Name = "Wiskunde" };
            var lesson2 = new LessonData() { Id = 1, Name = "Nederlands" };

            var student1 = new StudentData { Id = 1, Name = "Joris", Age = 22, Lessons = new List<Lesson>() { lesson1, lesson2 } };
            var student2 = new StudentData { Id = 2, Name = "Ruben", Age = 24, Lessons = new List<Lesson>() { lesson2 } };
            var student3 = new StudentData { Id = 3, Name = "Nick", Age = 22, Lessons = new List<Lesson>() { lesson1 } };

            var school = new SchoolData() { Id = 1, Name = "Fontys", Lessons = new List<LessonData>() { lesson1, lesson2 }, Students = new List<StudentData>() { student1, student2, student3 } };

            modelBuilder.Entity<SchoolData>().HasData(school);

            base.OnModelCreating(modelBuilder);
        }
    }
}
