using Microsoft.EntityFrameworkCore;
using StaycationHotel.Models;
using System.Collections.Generic;

namespace StaycationHotel.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
