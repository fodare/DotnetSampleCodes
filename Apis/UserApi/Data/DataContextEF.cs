using System;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
namespace APIBasics.Data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;
        public DataContextEF(IConfiguration configuration)
        {
            _config = configuration;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserJobInfo> UserJobInfos { get; set; }
        public virtual DbSet<UserSalary> UserSalarys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<User>()
            .ToTable("Users", "TutorialAppSchema")
            .HasKey(userId => userId.UserId);

            modelBuilder.Entity<UserJobInfo>()
            .HasKey(userId => userId.UserId);

            modelBuilder.Entity<UserSalary>()
            .HasKey(userId => userId.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("devDbConnectionString"),
            optionsBuilder => optionsBuilder.EnableRetryOnFailure());
        }
    }
}