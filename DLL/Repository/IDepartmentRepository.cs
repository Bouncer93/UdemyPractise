
using DLL.DBContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DLL.Repository
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
       
    }

    public class DepartmentRepository : RepositoryBase<Department> , IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext)
        {

        }
       
    }
}
