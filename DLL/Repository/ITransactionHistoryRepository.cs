using DLL.DBContext;
using DLL.Models;
using DLL.ResponseViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DLL.Repository
{
    public interface ITransactionHistoryRepository : IRepositoryBase<TransactionHistory>
    {
    }

    public class TransactionHistoryRepository : RepositoryBase<TransactionHistory> , ITransactionHistoryRepository
    {
        public TransactionHistoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
