using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Module1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            return Ok(await _studentService.GetAllStudentsAsync());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostStudentAsync([FromBody] Student student)
        {
            if (student is null)
            {
                return BadRequest();
            }

            var result = await _studentService.CreateStudentAsync(student);

            if(result.Nick != string.Empty)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}