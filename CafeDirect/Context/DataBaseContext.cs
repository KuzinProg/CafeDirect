using System;
using System.Collections.Generic;
using CafeDirect.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeDirect.Context;

public partial class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=pulka1601;database=cafedirectdb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Contract)
                .HasMaxLength(256)
                .HasColumnName("contract");
            entity.Property(e => e.FirstName)
                .HasMaxLength(64)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(64)
                .HasColumnName("last_name");
            entity.Property(e => e.Login)
                .HasMaxLength(64)
                .HasColumnName("login");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(64)
                .HasColumnName("middle_name");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");
            entity.Property(e => e.Photo)
                .HasMaxLength(256)
                .HasColumnName("photo");
            entity.Property(e => e.Role)
                .HasMaxLength(32)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(32)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PRIMARY");

            entity.ToTable("menu");

            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.Waiter, "order_employee_employee_id_fk");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ClientsCount).HasColumnName("clients_count");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Place).HasColumnName("place");
            entity.Property(e => e.Status)
                .HasMaxLength(64)
                .HasColumnName("status");
            entity.Property(e => e.Waiter).HasColumnName("waiter");

            entity.HasOne(d => d.WaiterNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Waiter)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_employee_employee_id_fk");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PRIMARY");

            entity.ToTable("order_item");

            entity.HasIndex(e => e.MenuItem, "order_item_menu_menu_id_fk");

            entity.HasIndex(e => e.Order, "order_item_order_order_id_fk");

            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.MenuItem).HasColumnName("menu_item");
            entity.Property(e => e.Order).HasColumnName("order");

            entity.HasOne(d => d.MenuItemNavigation).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.MenuItem)
                .HasConstraintName("order_item_menu_menu_id_fk");

            entity.HasOne(d => d.OrderNavigation).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_item_order_order_id_fk");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PRIMARY");

            entity.ToTable("schedule");

            entity.HasIndex(e => e.Employee, "schedule_employee_employee_id_fk");

            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Employee).HasColumnName("employee");
            entity.Property(e => e.WorkMode)
                .HasMaxLength(32)
                .HasColumnName("work_mode");

            entity.HasOne(d => d.EmployeeNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.Employee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("schedule_employee_employee_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
