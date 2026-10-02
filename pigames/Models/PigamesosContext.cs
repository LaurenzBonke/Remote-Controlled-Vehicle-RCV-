using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pigames.Models;

public partial class PigamesosContext : DbContext
{
    public PigamesosContext()
    {
    }

    public PigamesosContext(DbContextOptions<PigamesosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<ResultStatus> ResultStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleExtension> VehicleExtensions { get; set; }

    // Die Verbindungsdaten kommen aus der Konfiguration (Program.cs, "ConnectionStrings:DefaultConnection"),
    // nicht aus dem Code. Beim erneuten Scaffolden deshalb "Name=ConnectionStrings:DefaultConnection" verwenden.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasKey(e => e.DisciplineId).HasName("PRIMARY");

            entity.ToTable("Discipline");

            entity.Property(e => e.DisciplineId).HasColumnType("int(11)");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PRIMARY");

            entity.ToTable("Member");

            entity.HasIndex(e => e.TeamId, "TeamId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.MemberId).HasColumnType("int(11)");
            entity.Property(e => e.TeamId).HasColumnType("int(11)");
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.Team).WithMany(p => p.Members)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("Member_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Members)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Member_ibfk_1");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.RaceId).HasName("PRIMARY");

            entity.ToTable("Race");

            entity.HasIndex(e => e.DisciplineId, "DisciplineId");

            entity.Property(e => e.RaceId).HasColumnType("int(11)");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.DisciplineId).HasColumnType("int(11)");
            entity.Property(e => e.Endtime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("time");
            entity.Property(e => e.Starttime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("time");

            entity.HasOne(d => d.Discipline).WithMany(p => p.Races)
                .HasForeignKey(d => d.DisciplineId)
                .HasConstraintName("Race_ibfk_1");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PRIMARY");

            entity.ToTable("Result");

            entity.HasIndex(e => e.RaceId, "RaceId");

            entity.HasIndex(e => e.ResultStatusId, "ResultStatusId");

            entity.HasIndex(e => e.TeamId, "TeamId");

            entity.Property(e => e.ResultId).HasColumnType("int(11)");
            entity.Property(e => e.PenaltySeconds)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Placement)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.RaceId).HasColumnType("int(11)");
            entity.Property(e => e.ResultStatusId).HasColumnType("int(11)");
            entity.Property(e => e.TeamId).HasColumnType("int(11)");
            entity.Property(e => e.TotalTime)
                .HasPrecision(10, 3)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Race).WithMany(p => p.Results)
                .HasForeignKey(d => d.RaceId)
                .HasConstraintName("Result_ibfk_1");

            entity.HasOne(d => d.ResultStatus).WithMany(p => p.Results)
                .HasForeignKey(d => d.ResultStatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("Result_ibfk_3");

            entity.HasOne(d => d.Team).WithMany(p => p.Results)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("Result_ibfk_2");
        });

        modelBuilder.Entity<ResultStatus>(entity =>
        {
            entity.HasKey(e => e.ResultStatusId).HasName("PRIMARY");

            entity.ToTable("ResultStatus");

            entity.Property(e => e.ResultStatusId).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnType("int(11)");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PRIMARY");

            entity.ToTable("Team");

            entity.HasIndex(e => e.TeamName, "TeamName").IsUnique();

            entity.Property(e => e.TeamId).HasColumnType("int(11)");
            entity.Property(e => e.TeamName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValueSql("'''user'''")
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("UserRole_ibfk_2"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("UserRole_ibfk_1"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PRIMARY");
                        j.ToTable("UserRole");
                        j.HasIndex(new[] { "RoleId" }, "RoleId");
                        j.IndexerProperty<int>("UserId").HasColumnType("int(11)");
                        j.IndexerProperty<int>("RoleId").HasColumnType("int(11)");
                    });
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PRIMARY");

            entity.ToTable("Vehicle");

            entity.HasIndex(e => e.TeamId, "TeamId").IsUnique();

            entity.Property(e => e.VehicleId).HasColumnType("int(11)");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.TeamId).HasColumnType("int(11)");

            entity.HasOne(d => d.Team).WithOne(p => p.Vehicle)
                .HasForeignKey<Vehicle>(d => d.TeamId)
                .HasConstraintName("Vehicle_ibfk_1");
        });

        modelBuilder.Entity<VehicleExtension>(entity =>
        {
            entity.HasKey(e => e.ExtensionId).HasName("PRIMARY");

            entity.ToTable("Vehicle_Extension");

            entity.HasIndex(e => e.VehicleId, "VehicleId");

            entity.Property(e => e.ExtensionId).HasColumnType("int(11)");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.VehicleId).HasColumnType("int(11)");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleExtensions)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("Vehicle_Extension_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
