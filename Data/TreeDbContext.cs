using Microsoft.EntityFrameworkCore;
using TestAPI.Entities;

namespace TestAPI.Data
{
    public class TreeDbContext : DbContext
    {
        public TreeDbContext(DbContextOptions<TreeDbContext> options) : base(options) { }

        public DbSet<Tree> Trees { get; set; }
        public DbSet<TreeNode> TreeNodes { get; set; }
    }
}