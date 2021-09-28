using BLL.Request;
using DLL.Models;
using DLL.Repository;
using DLL.ResponseViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities.Exceptions;
using Utilities.Models;

namespace BLL.Services
{
    public interface ICourseStudentService
    {
        Task<ApiSuccessResponse> Insert(CourseAssignRequestViewModel request);
        Task<StudentCourseViewModel> GetSpecificStudentCourseListAsync(int studentId);

    }

    public class CourseStudentService : ICourseStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseStudentService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public  async Task<StudentCourseViewModel> GetSpecificStudentCourseListAsync(int studentId)
        {
            return await _unitOfWork.StudentRepository.GetSpecificStudentCourseListAsync(studentId);
        }

        public async Task<ApiSuccessResponse> Insert(CourseAssignRequestViewModel request)
        {

            var isStudentAlreadyEnroll = await _unitOfWork.CourseStudentRepository.FindSingleAsync(x => x.CourseId == request.CourseId && x.StudentId == request.StudentId);

            if (isStudentAlreadyEnroll != null)
            {
                throw new ApplicationValidationException(message: " this student is already enrolled in a course");
            }

            var courseStudent = new CourseStudent()
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId
            };

            await _unitOfWork.CourseStudentRepository.CreateAsync(courseStudent);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return new ApiSuccessResponse()
                {
                    StatusCode = 200,
                    Message = "The student has successfully enrolled into a course"
                };
            }
            throw new ApplicationValidationException(message: "problem occured while inserting");
        }


    }
}

         



