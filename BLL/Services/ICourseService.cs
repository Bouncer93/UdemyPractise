using BLL.Request;
using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities.Exceptions;

namespace BLL.Services
{
   public interface ICourseService
    {
        Task<Course> Insert(CourseInsertRequestViewModel request);
        Task<List<Course>> GetAll();
        Task<Course> Read(string code);
        Task<Course> Update(string code, Course course);
        Task<Course> Delete(string code);

        Task<bool> IsNameExists(string name);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsIdExist(int id);
    }

    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
        }

        public async Task<Course> Insert(CourseInsertRequestViewModel request)
        {
            var course = new Course();
            course.Code = request.Code;
            course.Name = request.Name;
            course.Credit = request.Credit;
           
             await _unitOfWork.CourseRepository.CreateAsync(course);
            if(await _unitOfWork.SaveCompletedAsync())
            {
                return course;
            }
            throw new ApplicationValidationException(message: "Problem occured while inserting course");
        }

        public async Task<Course> Delete(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x=>x.Code==code);

            if (course == null)
            {
                throw new ApplicationValidationException($"{code} for the course does not exist");
            }
            _unitOfWork.CourseRepository.Delete(course);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return course;
            }
            throw new ApplicationValidationException(message: "Problem occured while deleating a  course");

        }

        public async Task<List<Course>> GetAll()
        {
            return await _unitOfWork.CourseRepository.GetList();

        }

        public async Task<Course> Update(string code, Course course)
        {

           

            var Acourse = await _unitOfWork.CourseRepository.FindSingleAsync(x=>x.Code==code);
            if (Acourse == null)
            {
                throw new ApplicationValidationException(message: "Course  not found");
            }

           

            if(!string.IsNullOrWhiteSpace(course.Code))
            {
                var exisitng = await _unitOfWork.CourseRepository.FindSingleAsync(x=>x.Code== course.Code);
                if(exisitng!=null)
                {
                    throw new ApplicationValidationException("You are updating a course which already exists");
                }

                Acourse.Code = course.Code;

            }



            if (!string.IsNullOrWhiteSpace(course.Name))
            {
                var exisitng = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Name == course.Name);
                if (exisitng != null)
                {
                    throw new ApplicationValidationException("You are updating a course which already exists");
                }

                Acourse.Name = course.Name;

            }

            _unitOfWork.CourseRepository.Update(course);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return course;
            }
            throw new ApplicationValidationException(message: "Problem occured while updating a  course");

        }

        public async Task<Course> Read(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x=>x.Code==code);
            if (course == null)
            {
                throw new ApplicationValidationException("The course could not be found");
            }
            else
                return course;

        }

        public async Task<bool> IsNameExists(string name)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x=>x.Name==name);
            if (course == null)
            {
                return true;
            }
            return false;
        }

        public  async Task<bool> IsCodeExists(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);
            if (course == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsIdExist(int id)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.CourseId == id);
            if (course == null)
            {
                return true;
            }
            return false;

        }
    }
}
