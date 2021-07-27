using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public interface IStudentService
    {
        Task<Student> InsertAsync(Student student);
        Task<Student> DeletetAsync(string email);
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> GetStudentAsync(string email);
        Task<List<Student>> GetAllStudentsAsync();
    }

    public class StudentService : IStudentService
    {
        private readonly DLL.Repository.IStudentService _studentRepository;

        public StudentService(DLL.Repository.IStudentService studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Student> DeletetAsync(string email)
        {
           
         return   await _studentRepository.DeletetAsync(email);
           
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public async Task<Student> GetStudentAsync(string email)
        {
            return await _studentRepository.GetStudentAsync(email);
        }

        public async Task<Student> InsertAsync(Student student)
        {
            return await _studentRepository.InsertAsync(student);

        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            return await _studentRepository.UpdateAsync(email,student);
        }

    }
}
