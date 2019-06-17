using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FullEquip.Infrastructure.DataAccess.Identity;
using Microsoft.EntityFrameworkCore;
using FullEquip.Core.Entities;
using System;
using Microsoft.AspNetCore.Identity;

namespace FullEquip.Infrastructure.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<OnlineCourse> OnlineCourses { get; set; }
        public DbSet<ClassRoomCourse> ClassRoomCourses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CourseStudent>()
                .HasKey(x => new { x.CourseId, x.StudentId });

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                UserName = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin1234"),
                SecurityStamp = string.Empty
            });
        }
    }
}
