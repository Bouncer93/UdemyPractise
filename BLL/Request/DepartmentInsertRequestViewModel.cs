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
    public class DepartmentInsertRequestViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public class DepatmentValidator : AbstractValidator<DepartmentInsertRequestViewModel>
        {
            private readonly IServiceProvider _serviceProvider;

            public DepatmentValidator(IServiceProvider serviceProvider)
            {
                //RuleFor(x => x).NotNull();
                //RuleFor(x => x.Name).Length(0, 10);
                //RuleFor(x => x.Email).EmailAddress();
                //RuleFor(x => x.Age).InclusiveBetween(18, 60);
                _serviceProvider = serviceProvider;
                RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(4).MaximumLength(25).MustAsync(NameExists).WithMessage("Name already exists");
                RuleFor(x => x.Code).NotNull().NotEmpty().MinimumLength(3).MaximumLength(10).MustAsync(CodeExists).WithMessage("Code already exists");
              
            }

            private async Task<bool> CodeExists(string code, CancellationToken arg2)
            {
                if(string.IsNullOrEmpty(code))
                {
                    return   true;
                }

                var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();

                return await requiredService.IsCodeExists(code);
              
            }

            private async Task<bool> NameExists(string name, CancellationToken arg2)
            {
                if (string.IsNullOrEmpty(name))
                {
                    return  true;
                }

                var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();

                return await requiredService.IsNameExists(name);


            }
        }
    }
}
