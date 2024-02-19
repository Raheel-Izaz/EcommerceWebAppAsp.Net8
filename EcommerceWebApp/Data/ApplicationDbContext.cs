using EcommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }    
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
