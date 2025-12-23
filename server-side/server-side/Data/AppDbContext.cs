using System.Numerics;
using Microsoft.EntityFrameworkCore;
using server_side.Entities;
using server_side.Models;

namespace server_side.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Shape> Shapes { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Drawing> Drawings { get; set; }
    

    }
}

