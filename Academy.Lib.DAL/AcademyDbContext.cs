using Academy.Lib.Models;
using Common.Lib.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Lib.DAL
{
    public class AcademyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }




        public AcademyDbContext(DbContextOptions<AcademyDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Entity>()
                .Ignore(x => x.CurrentValidation);
        }
    }

}
