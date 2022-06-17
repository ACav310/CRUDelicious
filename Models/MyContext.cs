#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace CRUDelicious.Models;
// the MyContext class representing a session with our MySQL database, allowing us to query for or save data
public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<Dish> Dishes { get; set; } 
}