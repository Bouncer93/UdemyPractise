using DLL.DBContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public interface IStudentService
    {
        Task<Student> InsertAsync(Student student);
        Task<Student> DeletetAsync(string email);
        Task<Student> UpdateAsync(string email,Student student);
        Task<Student> GetStudentAsync(string email);
        Task<List<Student>> GetAllStudentsAsync();
        
    }


    public class StudentRepository : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Student> DeletetAsync(string email)
        {
            var student =await _context.Students.FirstOrDefaultAsync(s => s.Email == email);
            _context.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentAsync(string email)
        {
            var student =await _context.Students.FirstOrDefaultAsync(s => s.Email == email);
            return student;
        }

        public async Task<Student> InsertAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;

        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var st = await _context.Students.FirstOrDefaultAsync(s => s.Email == email);
            st.Name = student.Name;
            _context.Students.Update(st);
            await _context.SaveChangesAsync();
            return st;
        }
    }

}
