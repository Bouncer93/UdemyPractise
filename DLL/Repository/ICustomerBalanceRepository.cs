﻿
using DLL.DBContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace DLL.Repository
{
   public interface ICustomerBalanceRepository :  IRepositoryBase<CustomerBalance>
    {
        Task MustUpdateBalanceAsync(string email, decimal amount);
    }

    public class CustomerBalanceRepository : RepositoryBase<CustomerBalance>, ICustomerBalanceRepository 
    {
        private readonly ApplicationDbContext _context;
        public CustomerBalanceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task MustUpdateBalanceAsync(string email, decimal amount)
        {
            var customerBalance = await _context.CustomerBalances.FirstOrDefaultAsync(x => x.Email == email);

            var isUpdated = false;
            customerBalance.Balance += amount;
            do
            {
                try
                {
                    if (await _context.SaveChangesAsync()>0 )
                    {
                        isUpdated = true;
                    }

                }
                catch (DbUpdateConcurrencyException ex)
                {
                
                    foreach (var entry in ex.Entries)

                    {
                        if (!(entry.Entity is CustomerBalance)) continue;
                        

                        
                        var databaseentry = entry.GetDatabaseValues();
                        var databasevalues = (CustomerBalance)databaseentry.ToObject();
                        databasevalues.Balance += amount;
                        entry.OriginalValues.SetValues(databaseentry);
                        entry.CurrentValues.SetValues(databasevalues);

                    }
                }

            } while (!isUpdated);
            
        }
    }


}