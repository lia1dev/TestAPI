using Microsoft.EntityFrameworkCore;
using TestAPI.Entities;
using TestAPI.Entities.Journals;

namespace TestAPI.Data
{
    public class TreeDbContext : DbContext
    {
        public TreeDbContext(DbContextOptions<TreeDbContext> options) : base(options) { }

        public DbSet<Tree> Trees { get; set; }
        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<Journal> Journals { get; set; }
    }
}