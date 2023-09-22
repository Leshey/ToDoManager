using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoTaskManager.Domain.ToDos;

namespace ToDoTaskManager.Infrastructure;

public class ToDoTaskManagerContext : DbContext
{
    private readonly IConfiguration _dbConfig;

    public ToDoTaskManagerContext(IConfiguration configuration) 
    {
        _dbConfig = configuration;
    }

    public DbSet<ToDo> ToDos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(_dbConfig.GetConnectionString("Postgres"));
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
