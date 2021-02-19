using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Repositories.Infrastructure;
using Transaction.Data.Repositories.Interfaces;
using Transaction.Models.Core;

namespace Transaction.Data.Repositories.Impl
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(TContext context)
            : base(context)
        {            
        }

    }
}
