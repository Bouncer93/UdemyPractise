using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Request
{
   public class CourseAssignRequestViewModel
    {
        public int CourseId { get; set; }
        public int StudentId{ get; set; }

    }

    public class CourseAssignRequestViewModelValidator : AbstractValidator<CourseAssignRequestViewModel>
    {

        private readonly IServiceProvider _serviceProvider;
        public CourseAssignRequestViewModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x => x.CourseId).NotEmpty().NotNull().MustAsync(CourseIdExists).WithMessage("Course does not exist");
             RuleFor(x => x.StudentId).NotEmpty().NotNull().MustAsync(StudentIdExist).WithMessage("Student does not exist");
        }

        private async Task<bool> CourseIdExists(int id, CancellationToken arg2)
        {
          
            var requiredService = _serviceProvider.GetRequiredService<ICourseService>();

            return !await requiredService.IsIdExist(id);

        }

        private async Task<bool> StudentIdExist(int id, CancellationToken arg2)
        {

            var requiredService = _serviceProvider.GetRequiredService<IStudentService>();

            return ! await requiredService.IsIdExist(id);

        }


    }
}
