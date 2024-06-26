using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Review1q1.BusinessObjects;

public partial class Bonus2Context : DbContext
{
    public Bonus2Context()
    {
    }

    public Bonus2Context(DbContextOptions<Bonus2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =Desktop-by-me\\SQLEXPRESS; database =Bonus2; uid=sa;pwd=123;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF121AB19A8");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(6);
            entity.Property(e => e.Idnumber)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("IDNumber");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Title).HasName("PK__Rooms__2CB664DD15FEE655");

            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Comment).HasColumnType("ntext");
            entity.Property(e => e.Description).HasColumnType("ntext");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Services__3214EC074D4804BE");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.FeeType).HasMaxLength(50);
            entity.Property(e => e.RoomTitle)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.EmployeeNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.Employee)
                .HasConstraintName("FK__Services__Employ__3C69FB99");

            entity.HasOne(d => d.RoomTitleNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.RoomTitle)
                .HasConstraintName("FK__Services__RoomTi__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
