using Data.Entity;
using Data.Entity.TimeOffs;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class VacationManagerContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<PaidTimeOff> PaidTimeOffs { get; set; }
        public virtual DbSet<SickTimeOff> SickTimeOffs { get; set; }
        public virtual DbSet<UnpaidTimeOff> UnpaidTimeOffs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13; Initial Catalog=CodeFirstDB;");
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                 .HasMany(t => t.Developers).WithOne(u => u.Team);
               
        }
    }
}
