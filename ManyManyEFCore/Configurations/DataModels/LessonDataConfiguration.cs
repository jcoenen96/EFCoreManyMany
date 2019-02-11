using ManyManyEFCore.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.Configurations.DataModels
{
    public class LessonDataConfiguration : IEntityTypeConfiguration<LessonData>
    {
        public void Configure(EntityTypeBuilder<LessonData> builder)
        {
            //builder.Ignore(l => l.Students);

            builder.Property(l => l.Name).IsRequired();
        }
    }
}
