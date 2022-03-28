using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public class SMDbContext : DbContext
    {
        private string _connectionString;
        private string _migrationAssemblyName;

        public SMDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StudentRegistration>()
                 .HasKey(sm => sm.Id);

            builder.Entity<StudentRegistration>()
                .HasOne(sr => sr.Course)
                .WithMany(x => x.StudentRegistrations)
                .HasForeignKey(sr => sr.CourseId);

            builder.Entity<StudentRegistration>()
                .HasOne(sr => sr.Student)
                .WithMany(x => x.StudentRegistrations)
                .HasForeignKey(sr => sr.StudentId);

            base.OnModelCreating(builder);
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentRegistration> StudentRegistrations { get; set; }
      
    }
}
