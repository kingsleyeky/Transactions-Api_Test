using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.API;
using Transaction.Entity;

namespace Transaction.Service.Implementation
{
    public class Personservice : IPerson
    {
        private readonly TContext _tcontex;

        public Personservice(TContext context)
        {
            _tcontex = context;
        }


        ServiceResponse res = new ServiceResponse();

       
        public async Task<object> GetPersonById (Guid Id)
        {
            try
            {

                var result = await _tcontex.People.Where(p => p.ID == Id).FirstOrDefaultAsync();
                if (result == null)
                {
                    res.Data = null;
                    res.Message = "Id not Available";
                    res.Success = false;
                    return res;
                }
                else
                {
                    res.Data = result;
                    res.Message = "Person Id Found";
                    res.Success = true;
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public Task GetPersonEmail(string Email)
        {
            throw new NotImplementedException();
        }

        public Task GetPersonName(string Name)
        {
            throw new NotImplementedException();
        }

        public async Task<object> AddPerson(Person person)
        {
            try
            {

                var data = new Person
                {
                   
                  EmailAddress = person.EmailAddress,
                  DateCreated = DateTime.Now,
                  Firstname = person.Firstname,
                  PhoneNumber = person.PhoneNumber,
                  Surname = person.Surname

                };

                await _tcontex.AddAsync(data);

                int result = _tcontex.SaveChanges();
                if (result > 0)
                {

                    res.Data = result;
                    res.Success = true;
                    res.Message = "Person Created";
                    return res;
                }
                else
                {

                    res.Data = result;
                    res.Success = false;
                    res.Message = "Db Error";
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<object> GetallPerson()
        {
            try
            {
                var result = await _tcontex.People.ToListAsync();
               
                if (result == null)
                {
                    res.Data = null;
                    res.Message = "List not Available";
                    res.Success = false; ;
                    return res;
                }
                else
                {
                    res.Data = result;
                    res.Message = "List of Persons";
                    res.Success = true; ;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message + ":" + ex.StackTrace;
                res.Data = null;
                return res;
            }
        }

        public async Task<object> UpdatePerson(Person person, Guid Id)
        {
            try
            {
                var update = await _tcontex.People.Where(p => p.ID == Id).FirstOrDefaultAsync();
                if (update != null)
                {

                    update.EmailAddress = person.EmailAddress;
                    update.Firstname = person.Firstname;
                    update.PhoneNumber = person.PhoneNumber;
                    update.Surname = person.Surname;

                    int result =  _tcontex.SaveChanges();
                    if (result > 0)
                    {
                        res.Success = true;
                        res.Message = "Person Record successfully updated";
                        res.Data = update;
                        return res;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Db Error";
                        return res;
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "Person id does not exist";
                    res.Data = null;
                    return res;
                }
            }
            catch (Exception ex)
            {

              
                res.Success = false;
                res.Message = ex.Message + ":" + ex.StackTrace;
                res.Data = null;
                return res;

            }
        }
    }
}
