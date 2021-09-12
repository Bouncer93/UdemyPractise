using BLL.Request;
using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using static BLL.Request.DepartmentInsertRequestViewModel;

namespace BLL
{
    public static class BLLDepandacy
    {
        public static void AllDepadancies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ICourseService, CourseService>();

            AllFluentValidationDependancy(services);
        }

        private static void AllFluentValidationDependancy(IServiceCollection services)
        {
            services.AddTransient<IValidator<DepartmentInsertRequestViewModel>, DepatmentValidator>();
            services.AddTransient<IValidator<StudentInsertRequestViewModel>, StudentInsertRequestViewModelValidator>();
            services.AddTransient<IValidator<CourseInsertRequestViewModel>, CourseInsertRequestViewModelValidator>();
        }
    }
}
