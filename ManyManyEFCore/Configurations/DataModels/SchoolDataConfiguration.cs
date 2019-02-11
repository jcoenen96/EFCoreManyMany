using ManyManyEFCore.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.Configurations.DataModels
{
    public class SchoolDataConfiguration : IEntityTypeConfiguration<SchoolData>
    {
        public void Configure(EntityTypeBuilder<SchoolData> builder)
        {
            builder.Ignore(s => s.Students);
            builder.Ignore(s => s.Lessons);

            builder.Property(s => s.Name).IsRequired();
        }
    }
}
