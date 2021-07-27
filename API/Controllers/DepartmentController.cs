
using BLL.Request;
using BLL.Services;
using DLL.Models;
using DLL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{  
    public class DepartmentController : MainApiController 
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
       [HttpGet]
       public async Task< IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAll());
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> GetADepartment(string code)
        {
            return Ok( await _departmentService.Read(code));
        }
        [HttpPut("{code}")]
        public async Task<IActionResult> PutDepartment(string code,Department department)
        {
            return Ok(await _departmentService.Update(code,department));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteDepartment(string code)
        {
            return Ok( await _departmentService.Delete(code));
        }

        [HttpPost]
        public async Task< IActionResult> PostDepartment(DepartmentInsertRequestViewModel request)
        {
            return Ok(await _departmentService.Insert(request));
        }





    }  

}
