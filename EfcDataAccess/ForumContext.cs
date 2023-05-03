using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcDataAccess;

public class ForumContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Forum> Forums { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/FOrum.db");
    }
}