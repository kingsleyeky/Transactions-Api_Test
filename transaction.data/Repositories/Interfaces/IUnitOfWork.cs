﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {        
        Task<int> SaveChangesAsync();
       
    }

}