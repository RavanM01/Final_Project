using JobBoard.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<AppUserJob> UserJobs { get; set; }
        public DbSet<JobSkills> JobSkills { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);

            builder.Entity<AppUserJob>()
       .HasKey(uj => uj.Id);

            builder.Entity<AppUserJob>()
                .HasOne(uj => uj.User)
                .WithMany(u => u.UserJobs)
                .HasForeignKey(uj => uj.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppUserJob>()
                .HasOne(uj => uj.Job)
                .WithMany(j => j.AppUserJobs)
                .HasForeignKey(uj => uj.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUserJob>()
                .HasOne(uj => uj.CreatedByUser)
                .WithMany()
                .HasForeignKey(uj => uj.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<JobSkills>()
    .HasKey(js => new { js.JobId, js.SkillId });

            builder.Entity<JobSkills>()
                .HasOne(js => js.Job)
                .WithMany(j => j.JobSkills)
                .HasForeignKey(js => js.JobId);

            builder.Entity<JobSkills>()
                .HasOne(js => js.Skill)
                .WithMany(s => s.JobSkills)
                .HasForeignKey(js => js.SkillId);
        }
    }
}