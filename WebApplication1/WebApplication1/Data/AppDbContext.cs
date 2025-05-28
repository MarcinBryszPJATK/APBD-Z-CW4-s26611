using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}