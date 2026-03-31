using Microsoft.EntityFrameworkCore;
namespace AuthJWT.Services
{
    using AuthJWT.Models;
   
    using System.Collections.Generic;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
