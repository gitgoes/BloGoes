using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BloGoes.Domain.Entity;
using System.Reflection.Metadata;

namespace BloGoes.Data.Configuration
{
    public class BlogEntityConfiguration : IEntityTypeConfiguration<BlogEntity>
    {
        public void Configure(EntityTypeBuilder<BlogEntity> builder)
        {
            builder.ToTable("Blog");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("BlogId");
            builder.Property(p => p.Id).UseIdentityColumn();

        }
    }
}
