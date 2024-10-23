using MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos => Set<Todo>();

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("DataSource=app.db");
    }
}
