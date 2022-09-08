using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<SubTechnology> SubTechnologies { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(e =>
            {
                e.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                e.Property(p => p.Id).HasColumnName("Id");
                e.Property(p => p.Name).HasColumnName("Name");
                e.HasMany(p => p.SubTechnologies);
            });

            modelBuilder.Entity<SubTechnology>(e =>
            {
                e.ToTable("SubTechnologies").HasKey(k => k.Id);
                e.Property(s => s.Id).HasColumnName("Id");
                e.Property(s => s.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                e.Property(s => s.Name).HasColumnName("Name");
                e.HasOne(s => s.ProgrammingLanguage);
            });
        }

    }
}
