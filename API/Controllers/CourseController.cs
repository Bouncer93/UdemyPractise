using BLL.Request;
using BLL.Services;
using DLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    
    public class CourseController : MainApiController
    {
        private readonly ICourseService _courseService;

        public CourseController( ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _courseService.GetAll());
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> GetADepartment(string code)
        {
            return Ok(await _courseService.Read(code));
        }
        [HttpPut("{code}")]
        public async Task<IActionResult> PutDepartment(string code, Course course)
        {
            var result = await _courseService.Update(code, course);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteDepartment(string code)
        {
            var result = await _courseService.Delete(code);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostDepartment([FromForm]CourseInsertRequestViewModel request)
        {
            return Ok(await _courseService.Insert(request));
        }

    }
}
