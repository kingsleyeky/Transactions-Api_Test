using Microsoft.EntityFrameworkCore;
using Transaction.Entity;

namespace Transaction.API
{
    public class TContext : DbContext
    {
        public TContext(DbContextOptions<TContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

    }
}
