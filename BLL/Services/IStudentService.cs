using BLL.Request;
using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Exceptions;

namespace BLL.Services
{
   public interface IStudentService
    {
        Task<Student> InsertAsync(StudentInsertRequestViewModel  request);
        Task<Student> DeletetAsync(string email);
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> GetStudentAsync(string email);
        IQueryable<Student> GetAllStudentsAsync();
        Task<bool> IsEmailExist(string email);
        Task<bool>  IsIdExist(int  id);

    }

    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Student> DeletetAsync(string email)
        {

            //var department = await _departmentRepository.FindSingleAsync(x => x.Code == code);

            //if (department == null)
            //{
            //    throw new ApplicationValidationException($"{code} for the department does not exist");
            //}
            //_departmentRepository.Delete(department);
            //if (await _departmentRepository.SaveCompletedAsync())
            //{
            //    return department;
            //}
            //throw new ApplicationValidationException(message: "Problem occured while deleating a  department");

            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);
            if (student==null)
            {
                throw new ApplicationValidationException($"{email} for the student does not exist");
            }
            _unitOfWork.StudentRepository.Delete(student);
            
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return student;
            }
            throw new ApplicationValidationException(message: "Problem occured while deleating the student");

        }

        public IQueryable<Student> GetAllStudentsAsync()
        {
            return  _unitOfWork.StudentRepository.QueryAll();
        }

        public async Task<Student> GetStudentAsync(string email)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);
            if (student == null)
            {
                throw new ApplicationValidationException($"{email} for the student does not exist");
            }
            return student;
        }

        public async Task<Student> InsertAsync(StudentInsertRequestViewModel request)
        {


            var thestudent = new Student()
            {
                Name = request.Name,
                Email = request.Email,
                DepartmentId = request.DepartmentId
            };
           
            await _unitOfWork.StudentRepository.CreateAsync(thestudent);
            if(await _unitOfWork.SaveCompletedAsync())
            {
                return thestudent;
            }
            throw new ApplicationValidationException("Problem occured while inserting Student");

        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            

           

            var aStudent = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);
            if (aStudent==null)
            {
                throw new ApplicationValidationException("Student not found");
            }

            if (!string.IsNullOrWhiteSpace(student.Email))
            {
                var exisitng = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == student.Email);
                if (exisitng != null)
                {
                    throw new ApplicationValidationException("You are updating a student which already exists");
                }

                aStudent.Email = student.Email;

            }
            
            if (!string.IsNullOrWhiteSpace(student.Name))
            {
                var exisitng = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Name == student.Name);
                if (exisitng != null)
                {
                    throw new ApplicationValidationException("You are updating a student name which already exists");
                }

                aStudent.Name = student.Name;

            }

            _unitOfWork.StudentRepository.Update(aStudent);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return aStudent;
            }
            throw new ApplicationValidationException(message: "Problem occured while updating  the Student");

        }

        public async Task<bool> IsEmailExist(string email)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);
            if (student == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsIdExist(int id)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.StudentId == id);
            if (student == null)
            {
                return true;
            }
            return false;

        }

    }
}
