using System.Data.Entity;

namespace Dinner.Models {
    public class EFDbContext: DbContext {
        public DbSet<User> Users { get; set; }
    }
}