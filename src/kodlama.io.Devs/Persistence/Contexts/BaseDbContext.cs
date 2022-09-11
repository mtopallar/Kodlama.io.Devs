using Core.Security.Entities;
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
        public DbSet<UserWebAddress> UserWebAddresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

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

            modelBuilder.Entity<UserWebAddress>(e =>
            {
                e.ToTable("UserWebAddresses").HasKey(k => k.Id);
                e.Property(u => u.Id).HasColumnName("Id");
                e.Property(u => u.UserId).HasColumnName("UserId");
                e.Property(u => u.GithubAddress).HasColumnName("GithubAdress");
                e.HasOne(u => u.User);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users").HasKey(k => k.Id);
                e.Property(u => u.Id).HasColumnName("Id");
                e.Property(u => u.FirstName).HasColumnName("FirstName");
                e.Property(u => u.LastName).HasColumnName("LastName");
                e.Property(u => u.Email).HasColumnName("Email");
                e.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
                e.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                e.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");
                e.HasMany(u => u.UserOperationClaims);
                e.HasMany(u => u.RefreshTokens);
            });

            modelBuilder.Entity<OperationClaim>(e =>
            {
                e.ToTable("OperationClaims").HasKey(k => k.Id);
                e.Property(o => o.Id).HasColumnName("Id");
                e.Property(o => o.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(e =>
            {
                e.ToTable("UserOperationClaims").HasKey(k => k.Id);
                e.Property(u => u.Id).HasColumnName("Id");
                e.Property(u => u.UserId).HasColumnName("UserId");
                e.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
                e.HasOne(u => u.User);
                e.HasOne(u => u.OperationClaim);
            });

            modelBuilder.Entity<RefreshToken>(e =>
            {
                e.ToTable("RefreshTokens").HasKey(k => k.Id);
                e.Property(r => r.Id).HasColumnName("Id");
                e.Property(r => r.UserId).HasColumnName("UserId");
                e.Property(r => r.Token).HasColumnName("Token");
                e.Property(r => r.Expires).HasColumnName("Expires");
                e.Property(r => r.Created).HasColumnName("Created");
                e.Property(r => r.CreatedByIp).HasColumnName("CreatedByIp");
                e.Property(r => r.Revoked).HasColumnName("Revoked");
                e.Property(r => r.RevokedByIp).HasColumnName("RevokedByIp");
                e.Property(r => r.ReplacedByToken).HasColumnName("ReplacedByToken");
                e.Property(r => r.ReasonRevoked).HasColumnName("ReasonRevoked");
                e.HasOne(r => r.User);

            });
        }

    }
}
