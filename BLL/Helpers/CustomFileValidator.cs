using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;

using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public class CustomFileValidator: PropertyValidator
    {
        private readonly IFileValidate _fileValidate;
        public CustomFileValidator(IFileValidate fileValidate)
        {
            _fileValidate = fileValidate;
        }

      

      

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var fileToValidate = context.PropertyValue as IFormFile;

            var (valid, errorMessage) = _fileValidate.ValidateFile(fileToValidate);
            if (valid) return true;
            context.MessageFormatter.AppendArgument("ErrorMessage", errorMessage);
            return false;
        }

        //protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        //{
        //    var fileToValidate = context.PropertyValue as IFormFile;

        //    var (valid, errorMessage) = _fileValidate.ValidateFile(fileToValidate);
        //    if (valid) return true;
        //    context.MessageFormatter.AppendArgument("ErrorMessage", errorMessage);
        //    return false;



        //}
    }
}
