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
    public class Accountservice : IAccount
    {
        private readonly TContext _tcontex;

        

        public Accountservice(TContext context)
        {
            _tcontex = context;
        }

        ServiceResponse res = new ServiceResponse();

        public async Task<object> AddAccount(Account account)
        {
            try
            {

                var data = new Account
                {
                    Balance = account.Balance,
                    Name = account.Name,
                    Number = account.Number,
                    PersonID = account.PersonID

                };


                await _tcontex.AddAsync(data);
               
                int result = _tcontex.SaveChanges();
                if (result > 0)
                {

                    res.Data = result;
                    res.Success = true;
                    res.Message = "Account Created";
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

        public async Task<object> GetallAccount()
        {
            try
            {
                var result = await _tcontex.Accounts.ToListAsync();

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
                    res.Message = "List of Account";
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

        public async Task<object> UpdateAccount(Account account, Guid Id)
        {
            try
            {
                var update = await _tcontex.Accounts.Where(p => p.ID == Id).FirstOrDefaultAsync();
                if (update != null)
                {

                    update.Name = account.Name;
                    update.Number = account.Number;
                    int result = _tcontex.SaveChanges();
                    if (result > 0)
                    {
                        res.Success = true;
                        res.Message = "Account Record successfully updated";
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

        public async Task<object> GetAccountById(Guid Id)
        {
            try
            {

                var result = await _tcontex.Accounts.Where(p => p.ID == Id).FirstOrDefaultAsync();
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
                    res.Message = "Account Id Found";
                    res.Success = true;
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<object> GetAccountByPersonId(Guid PersonId)
        {
            try
            {

                //var trac = _tcontex.Accounts.Where(x => x.PersonID == PersonId);

                var result = (from p in _tcontex.People
                                        join a in _tcontex.Accounts on p.ID equals a.PersonID
                                        where p.ID == PersonId
                                        select a).FirstOrDefault();
                                      

                
                if (result == null)
                {
                    res.Data = null;
                    res.Message = "Person's Id not Available";
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
    }
}

