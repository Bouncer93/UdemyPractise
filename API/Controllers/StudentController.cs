
using DLL.Models;
using DLL.Repository;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

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
        public async Task< ActionResult> GetAllStudent()
        {
            return Ok( await _studentService.GetAllStudentsAsync());
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
        public async Task<ActionResult> PostDepartment(Student student)
        {
            return Ok(await _studentService.InsertAsync( student));
        }
    }
    



}
