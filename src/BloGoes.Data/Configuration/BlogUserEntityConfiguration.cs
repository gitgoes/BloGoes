using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BloGoes.Domain.Entity;

namespace BloGoes.Data.Configuration
{
    public class BlogUserEntityConfiguration : IEntityTypeConfiguration<BlogUserEntity>
    {
        public void Configure(EntityTypeBuilder<BlogUserEntity> builder)
        {
            builder.ToTable("BlogUser");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("BlogUserId");
            builder.Property(p => p.Id).UseIdentityColumn();


            builder.HasMany(p => p.Blogs).WithOne(f => f.BlogUser).HasForeignKey(f => f.BlogUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
