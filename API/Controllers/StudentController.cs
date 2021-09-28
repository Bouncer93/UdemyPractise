
using DLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using BLL.Request;
using LightQuery;

namespace API.Controllers
{
 
    public class StudentController : MainApiController

   
    {

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        [LightQuery(forcePagination: true, defaultPageSize: 10, defaultSort: "studentId desc")]
        public ActionResult GetAllStudent()
        {
            return Ok(  _studentService.GetAllStudentsAsync());
        }
        [HttpGet("{email}")]
        public  async Task<ActionResult> GetAStudent(string email)
        {
            return Ok(await _studentService.GetStudentAsync(email));
        }
        [HttpPut("{email}")]
        public async Task<ActionResult> PutDepartment(string email, Student student)
        {
            return Ok(await _studentService.UpdateAsync(email,student));
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult> DeleteDepartment(string email)
        {
            return Ok(await _studentService.DeletetAsync(email));
        }

        [HttpPost]
        public async Task<ActionResult> PostDepartment(StudentInsertRequestViewModel student)
        {
            return Ok(await _studentService.InsertAsync( student));
        }
    }
    



}
