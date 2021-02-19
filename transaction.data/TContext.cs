using Microsoft.EntityFrameworkCore;
using Transaction.Models.Core;

namespace Transaction.Data
{
    public class TContext : DbContext
    {
        public TContext(DbContextOptions<TContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Models.Core.Transaction> Transactions { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

    }
}
