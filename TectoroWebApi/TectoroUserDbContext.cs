using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;

namespace TectoroWebApi
{
    public partial class TectoroUserDbContext : DbContext
    {
        public TectoroUserDbContext()
        {
        }

        public TectoroUserDbContext(DbContextOptions<TectoroUserDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TectoroUsersDB"].ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Users", "Domain");

                entity.Property(e => e.Alias).HasMaxLength(1000);

                entity.Property(e => e.Email).HasMaxLength(1000);

                entity.Property(e => e.FirstName).HasMaxLength(1000);

                entity.Property(e => e.LastName).HasMaxLength(1000);

                entity.Property(e => e.Position).HasMaxLength(200);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(200);
            });
        }
    }
}
