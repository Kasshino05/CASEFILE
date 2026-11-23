using Microsoft.EntityFrameworkCore;

namespace CASEFILE.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
