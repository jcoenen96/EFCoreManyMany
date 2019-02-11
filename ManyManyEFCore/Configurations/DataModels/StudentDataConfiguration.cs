using ManyManyEFCore.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.Configurations.DataModels
{
    public class StudentDataConfiguration : IEntityTypeConfiguration<StudentData>
    {
        public void Configure(EntityTypeBuilder<StudentData> builder)
        {
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Age).IsRequired();
        }
    }
}
