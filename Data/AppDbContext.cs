using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
  
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

;

            modelBuilder.Entity<UsersModel>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<UsersModel>().HasIndex(p => p.UserName).IsUnique();
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).Seed();
        }
        public virtual DbSet<Entities.UsersModel> Users { get; set; } = default!;
        public virtual DbSet<Entities.JobModel> Jobs { get; set; } = default!;

        public virtual DbSet<Entities.SkillModel> Skill { get; set; } = default!;
        public virtual DbSet<Entities.UserSkillModel> UserSkills { get; set; } = default!;
    }
}
