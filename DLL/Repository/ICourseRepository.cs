
using DLL.DBContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DLL.Repository
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
       
    }

    public class CourseRepository : RepositoryBase<Course> , ICourseRepository
    {
        public CourseRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext)
        {

        }
       
    }
}
