using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentnames = new string[] {"Aakash","Abhi","Devu","Kiran","Karun" };
            return Ok(studentnames);
        }
    }
}
