using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Models;

public class ApplicationDbContext : DbContext
{
    static ApplicationDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
}