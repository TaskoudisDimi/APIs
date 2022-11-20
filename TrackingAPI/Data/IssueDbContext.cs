using Microsoft.EntityFrameworkCore;
using TrackingAPI.Models;

namespace TrackingAPI.Data
{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext(DbContextOptions<IssueDbContext> options) : base(options)
        {

        }
        public DbSet<Issue> Issues { get; set; }

    }
}