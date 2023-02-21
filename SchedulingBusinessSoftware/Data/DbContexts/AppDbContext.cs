using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchedulingBusinessSoftware.Entities;
using SchedulingBusinessSoftware.Interfaces;
using SchedulingBusinessSoftware.Models;
using System.Collections.Generic;

namespace SchedulingBusinessSoftware.Data.DbContexts
{
    public class AppDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Appointment> Appoitment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.HasKey(u => u.Id);

                b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

                b.ToTable("Account");

                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);

                b.HasMany<ApplicationUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
                b.HasMany<ApplicationUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
                b.HasMany<ApplicationUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
                b.HasMany<ApplicationUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<ApplicationUserClaim>(b =>
            {
                b.HasKey(uc => uc.Id);

                b.ToTable("AccountClaim");
            });

            builder.Entity<ApplicationUserLogin>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);

                b.ToTable("AccountLogin");
            });

            builder.Entity<ApplicationUserToken>(b =>
            {
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                b.Property(t => t.LoginProvider).HasMaxLength(255);
                b.Property(t => t.Name).HasMaxLength(255);

                b.ToTable("AccountToken");
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.HasKey(r => r.Id);

                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                b.ToTable("Role");

                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                b.HasMany<ApplicationUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
                b.HasMany<ApplicationRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            builder.Entity<ApplicationRoleClaim>(b =>
            {
                b.HasKey(rc => rc.Id);

                b.ToTable("RoleClaim");
            });

            builder.Entity<ApplicationUserRole>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });

                b.ToTable("AccountRole");
            });

            builder.Entity<Appointment>(a =>
            {
                a.HasKey(a => a.Id);
                a.ToTable("Appointment");
                a.Property(x => x.Id).ValueGeneratedOnAdd();
                //a.HasData
                //(
                //new Appointment
                //{
                //    Title = "Appointment1",
                //    Description = "Description Appointment",
                //    CreatedDate = new DateTime(),
                //    UpdatedDate = new DateTime(),
                //    ScheduledAt = new DateTime()
                //},
                //      new Appointment
                //      {
                //          Title = "Appointment2",
                //          Description = "Description Appointment2",
                //          CreatedDate = new DateTime(2023, 8, 2),
                //          UpdatedDate = new DateTime(2023, 8, 2),
                //          ScheduledAt = new DateTime(2023, 3, 3)
                //      }
                //    );
            }
            );
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
