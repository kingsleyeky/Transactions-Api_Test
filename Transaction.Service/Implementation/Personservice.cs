using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.API;

namespace Transaction.Service.Implementation
{
    public class Personservice : IPerson
    {
        private readonly TContext _tcontex;

        public Personservice(TContext context)
        {
            _tcontex = context;
        }

        public Task GetPerson(int Id)
        {
            throw new NotImplementedException();
        }

        public Task GetPersonEmail(string Email)
        {
            throw new NotImplementedException();
        }

        public Task GetPersonName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
