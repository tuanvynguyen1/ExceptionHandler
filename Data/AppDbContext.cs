using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        public virtual DbSet<Entities.RoleModel> Roles { get; set; } = default!;
        public virtual DbSet<Entities.UserRoleModel> UserRoles { get; set; } = default!;

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).CreatedAt = now;
                }
                ((BaseModel)entity.Entity).UpdatedAt = now;
            }
        }
    }
}
