using Microsoft.EntityFrameworkCore;
using SequentialNumbers.Models;

namespace SequentialNumbers.Database
{
    public class SqDbContext : DbContext
    {
        public DbSet<SequenceEntry> SequenceEntries { get; set; }

        public SqDbContext()
        {
            
        }
        
        public SqDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}