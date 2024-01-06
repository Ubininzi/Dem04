using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace dem04.EFModel;

public partial class Dem04DbContext : DbContext
{
    public Dem04DbContext()
    {
    }

    public Dem04DbContext(DbContextOptions<Dem04DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Dictionary<string, string> config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("..\\..\\..\\appconfig.json"));
        optionsBuilder.UseSqlServer($"Server={config["ServerName"]};Database={config["DBName"]};Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC27BE664B3C");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("_Name");
            entity.Property(e => e.Number).HasMaxLength(10);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("_Surname");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Equipmen__3214EC2720BEFC7D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SerialNumber).HasMaxLength(100);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("_Type");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Login1).HasName("PK__LOGINS__53DBEC7DD32A7AEE");

            entity.ToTable("LOGINS");

            entity.Property(e => e.Login1)
                .HasMaxLength(50)
                .HasColumnName("_Login");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("_Password");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requests__3214EC27C50CEF35");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateOfAccept).HasColumnType("datetime");
            entity.Property(e => e.DateOfWorkStart).HasColumnType("datetime");
            entity.Property(e => e.RequestDescription).HasMaxLength(400);
            entity.Property(e => e.RequestState).HasMaxLength(15);

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.Client)
                .HasConstraintName("FK__Requests__Client__412EB0B6");

            entity.HasOne(d => d.WorkerNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.Worker)
                .HasConstraintName("FK__Requests__Worker__4222D4EF");

            entity.HasMany(d => d.Equipment).WithMany(p => p.Requests)
                .UsingEntity<Dictionary<string, object>>(
                    "EquipmentForRequest",
                    r => r.HasOne<Equipment>().WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Equipment__Equip__47DBAE45"),
                    l => l.HasOne<Request>().WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Equipment__Reque__46E78A0C"),
                    j =>
                    {
                        j.HasKey("RequestId", "EquipmentId").HasName("PK__Equipmen__B0EC25C392A0E90E");
                        j.ToTable("EquipmentForRequest");
                        j.IndexerProperty<int>("RequestId").HasColumnName("RequestID");
                        j.IndexerProperty<int>("EquipmentId").HasColumnName("EquipmentID");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC27D70482B0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workers__3214EC274160675A");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("_Login");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("_Name");
            entity.Property(e => e.Number).HasMaxLength(10);
            entity.Property(e => e.Role).HasColumnName("_Role");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("_Surname");

            entity.HasOne(d => d.LoginNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.Login)
                .HasConstraintName("FK__Workers___Login__3E52440B");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("FK__Workers___Role__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
