
using DLL.DBContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DLL.Repository
{
    public interface ICourseStudentRepository : IRepositoryBase<CourseStudent>
    {
       
    }

    public class CourseStudentRepository : RepositoryBase<CourseStudent> , ICourseStudentRepository
    {
        public CourseStudentRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext)
        {

        }
       
    }
}
