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
        public DbSet<Interviewer> Interviewer { get; set; }

        public DbSet<ApplicationUsersAndAppointments> VM_ApplicationUsers_And_Appointments { get; set; }
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

            builder.Entity<Interviewer>(b =>
            {
                b.HasKey(a => a.Id);
                b.ToTable("Interviewer");

                b.HasMany<Appointment>().WithOne().HasForeignKey(ap => ap.Id).IsRequired();

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
                a.HasOne<ApplicationUser>().WithMany().HasForeignKey(ax => ax.Candidate).IsRequired();

                //a.Property(t => t.AppointmentType) 
                //.HasConversion(
                //    t => t.ToString(),
                //    t => (SchedulingBusinessSoftware.Entities.Appointment.Type)Enum.Parse(typeof(Type), t));

            });

            builder.Entity<ApplicationUsersAndAppointments>(aa =>
            {
                aa.HasNoKey();
                aa.ToView("VM_APPLICATIONUSERS_AND_APPOINTMENTS");
            });
        }

       
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
