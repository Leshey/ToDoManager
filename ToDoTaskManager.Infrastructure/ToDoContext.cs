using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoTaskManager.Domain.ToDos;

namespace ToDoTaskManager.Infrastructure;

public class ToDoContext : DbContext
{
    public DbSet<ToDo> ToDos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Userid=postgres;Password=testsql;Database=todo_task_manager");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<ToDo>()
            .ToTable("todos");
        
        modelBuilder
            .Entity<ToDo>()
            .HasKey(x => x.Id);

        modelBuilder
            .Entity<ToDo>()
            .Property(x => x.Id)
            .HasColumnName("id");

        modelBuilder
            .Entity<ToDo>()
            .Ignore(x => x.IsDone);

        modelBuilder
            .Entity<ToDo>()
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(250)
            .HasColumnName("name");

        modelBuilder
            .Entity<ToDo>()
            .Property(x => x.DoneTime)
            .IsRequired(false)
            .HasColumnName("done_time");
    }
}
