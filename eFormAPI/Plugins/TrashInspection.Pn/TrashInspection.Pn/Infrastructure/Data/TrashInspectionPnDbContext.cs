﻿using TrashInspection.Pn.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TrashInspection.Pn.Infrastructure.Data
{
    public class TrashInspectionPnDbContext : DbContext
    {

        public TrashInspectionPnDbContext() { }

        public TrashInspectionPnDbContext(DbContextOptions<TrashInspectionPnDbContext> options) : base(options)
        {

        }

        public DbSet<Installation> Installations { get; set; }
        public DbSet<InstallationSite> InstallationSites { get; set; }
        public DbSet<Entities.TrashInspection> TrashInspections { get; set; }
        public DbSet<TrashInspectionPnSetting> TrashInspectionPnSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex(x => x.Created_By_User_Id);
            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex(x => x.Eak_Code);
            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex();
            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex();
            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex();
            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex();
            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex();
            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex();
            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex();
            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex();
            modelBuilder.Entity<Entities.TrashInspection>()
                .HasIndex();
        }
    }
}
