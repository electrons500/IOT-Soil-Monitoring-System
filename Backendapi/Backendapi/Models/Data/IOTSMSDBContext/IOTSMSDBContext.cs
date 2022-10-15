using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Backendapi.Models.Data.IOTSMSDBContext
{
    public partial class IOTSMSDBContext : IdentityDbContext<ApplicationUser>
    {
        public IOTSMSDBContext()
        {
        }

        public IOTSMSDBContext(DbContextOptions<IOTSMSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Arduino> Arduino { get; set; }
        public virtual DbSet<Farm> Farm { get; set; }
        public virtual DbSet<FarmMapLocation> FarmMapLocation { get; set; }
        public virtual DbSet<Farmer> Farmer { get; set; }
        public virtual DbSet<FarmerImage> FarmerImage { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<SoilCategory> SoilCategory { get; set; }
        public virtual DbSet<SoilData> SoilData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Conn");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            //copy this code here
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Id)
                   .IsClustered(false);
                entity.ToTable(name: "Users");
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.HasKey(e => e.Id)
                   .IsClustered(false);
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.UserId })
                   .IsClustered(false);
                entity.ToTable("UserRoles");
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.HasKey(e => e.Id)
                   .IsClustered(false);
                entity.ToTable("UserClaims");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => e.LoginProvider)
                   .IsClustered(false);
                entity.ToTable("UserLogins");

            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.HasKey(e => e.Id)
                   .IsClustered(false);
                entity.ToTable("RoleClaims");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => e.LoginProvider)
                   .IsClustered(false);
                entity.ToTable("UserTokens");
            });

            //ends here


            modelBuilder.Entity<Arduino>(entity =>
            {
                entity.HasKey(e => new { e.ArduinoId, e.SerialNumber })
                    .IsClustered(false);

                entity.Property(e => e.ArduinoId).HasMaxLength(100);

                entity.Property(e => e.SerialNumber).HasMaxLength(100);

                entity.Property(e => e.Bn)
                    .IsRequired()
                    .HasColumnName("BN")
                    .HasMaxLength(100);

                entity.Property(e => e.DateOfActivation).HasMaxLength(100);

                entity.Property(e => e.DeploymentDate)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastPowerOnDate).HasMaxLength(100);

                entity.Property(e => e.LastPowerOnTime).HasMaxLength(100);

                entity.Property(e => e.Pid)
                    .IsRequired()
                    .HasColumnName("PID")
                    .HasMaxLength(10);

                entity.Property(e => e.Vid)
                    .IsRequired()
                    .HasColumnName("VID")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Farm>(entity =>
            {
                entity.HasKey(e => e.FarmId)
                    .IsClustered(false);

                entity.Property(e => e.FarmId).HasMaxLength(250);

                entity.Property(e => e.ArduinoId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FarmName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FarmerId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Farmer)
                    .WithMany(p => p.Farm)
                    .HasForeignKey(d => d.FarmerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Farmer_Farm_");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Farm)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Region_Farm_");

                entity.HasOne(d => d.SoilCategory)
                    .WithMany(p => p.Farm)
                    .HasForeignKey(d => d.SoilCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoilCategory_Farm_");

                entity.HasOne(d => d.Arduino)
                    .WithMany(p => p.Farm)
                    .HasForeignKey(d => new { d.ArduinoId, d.SerialNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Arduino_Farm_");
            });

            modelBuilder.Entity<FarmMapLocation>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_MaplocationId")
                    .IsClustered(false);

                entity.Property(e => e.ArduinoId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Descriptions).HasMaxLength(100);

                entity.Property(e => e.FarmId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Latitude).HasMaxLength(200);

                entity.Property(e => e.Longitude).HasMaxLength(200);

                entity.Property(e => e.MaplocationId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.FarmMapLocation)
                    .HasForeignKey(d => d.FarmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefFarm11");

                entity.HasOne(d => d.Arduino)
                    .WithMany(p => p.FarmMapLocation)
                    .HasForeignKey(d => new { d.ArduinoId, d.SerialNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefArduino12");
            });

            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.HasKey(e => e.FarmerId)
                    .IsClustered(false);

                entity.Property(e => e.FarmerId).HasMaxLength(250);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(250);

                entity.HasOne(d => d.FarmerImage)
                    .WithMany(p => p.Farmer)
                    .HasForeignKey(d => d.FarmerImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FarmerImage_Farmer_");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Farmer)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gender_Farmer_");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Farmer)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Region_Farmer_");
            });

            modelBuilder.Entity<FarmerImage>(entity =>
            {
                entity.HasKey(e => e.FarmerImageId)
                    .IsClustered(false);

                entity.Property(e => e.FarmerPhoto).IsRequired();
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.GenderId)
                    .IsClustered(false);

                entity.Property(e => e.GenderName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .IsClustered(false);

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SoilCategory>(entity =>
            {
                entity.HasKey(e => e.SoilCategoryId)
                    .IsClustered(false);

                entity.Property(e => e.SoilName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SoilData>(entity =>
            {
                entity.HasKey(e => e.SoilDataId)
                    .IsClustered(false);

                entity.Property(e => e.ArduinoId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Humidity)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nitrogen)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phosphorus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Potassium)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SoilMoisture)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SoilTemperature)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Temperature)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Arduino)
                    .WithMany(p => p.SoilData)
                    .HasForeignKey(d => new { d.ArduinoId, d.SerialNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Arduino_SoilData_");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
