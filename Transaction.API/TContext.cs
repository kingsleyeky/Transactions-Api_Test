using Microsoft.EntityFrameworkCore;

namespace Transaction.API
{
    public class TContext : DbContext
    {
        public TContext(DbContextOptions<TContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

    }
}
