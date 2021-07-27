using DLL.DBContext;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;


namespace DLL
{
    public static class DLLDepdancy
    {
        public static void AllDepadancies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStudentService, StudentRepository>();
        }
    }
}
