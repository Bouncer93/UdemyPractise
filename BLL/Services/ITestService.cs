using Bogus;
using DLL.DBContext;
using DLL.Models;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ITestService
    {
        Task  SeedData();
        Task SeedData2();
    }
    public class TestService : ITestService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public TestService(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public  async Task SeedData()
        {
            
            var fakeStudentDummy = new Faker<Student>()
        .RuleFor(s => s.Name, (f,s) => f.Name.FirstName())
        .RuleFor(s => s.Email, (f,s) => f.Internet.Email(s.Name));

            var fakeDepartmentDummy = new Faker<Department>()
                 .RuleFor(d => d.Name, (f, d) => f.Name.FirstName())
                 .RuleFor(d => d.Code, (f, d) => f.Lorem.Text())
                  .RuleFor(d => d.Students, f =>fakeStudentDummy.Generate(50).ToList() );


            var departmentListWithStudent = fakeDepartmentDummy.Generate(100).ToList();

           await _context.AddRangeAsync(departmentListWithStudent);

            await _context.SaveChangesAsync();


        }

        public async Task SeedData2()
        {
                          


            //var fakeCoursesDummy = new Faker<Course>()
            //     .RuleFor(d => d.Name, (f, d) => f.Name.FirstName())
            //     .RuleFor(d => d.Code, (f, d) => f.Lorem.Text())
            //      .RuleFor(d => d.Credit, (f, d) => f.Random.Number(1, 10));




            //var CourseListDummy = fakeCoursesDummy.Generate(50).ToList();

            //await _context.AddRangeAsync(CourseListDummy);

            //await _context.SaveChangesAsync();

            var courseIds = await _context.Courses.Select(c => c.CourseId).ToListAsync();
            var studentIds = await _context.Students.Select(c => c.StudentId).ToListAsync();
            int  count = 0;
           
            foreach (var courseId in courseIds)
            {
                var students = studentIds.Skip(count).Take(5).ToList();
                var courseStudents = new List<CourseStudent>();
                foreach (var aStudent in students)
                {
                    courseStudents.Add(new CourseStudent() { CourseId = courseId, StudentId = aStudent });
                }
               

                await _context.CourseStudents.AddRangeAsync(courseStudents);
                await _context.SaveChangesAsync();
                count += 5;
            }




        }
    }
}
