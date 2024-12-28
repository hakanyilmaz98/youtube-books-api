using System.Reflection;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class BooksContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public BooksContext(DbContextOptions<BooksContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}