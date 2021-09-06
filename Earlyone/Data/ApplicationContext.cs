using Earlyone.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Earlyone.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Auditorium> Auditoria { get; set; }
        public DbSet<Journal> Journals { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
             Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Auditorium>()
               .HasKey(p => new { p.StudentId, p.TeacherId });

            modelBuilder.Entity<Auditorium>()
                .HasOne(p => p.Student)
                .WithMany(p => p.Auditoria)
                .HasForeignKey(pw => pw.StudentId);

            modelBuilder.Entity<Auditorium>()
                .HasOne(pw => pw.Teacher)
                .WithMany(w => w.Auditoria)
                .HasForeignKey(pw => pw.TeacherId);
        }


        public DbSet<Earlyone.Models.Journal> Journal { get; set; }



    }
}
