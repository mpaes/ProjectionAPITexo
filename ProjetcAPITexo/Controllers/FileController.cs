using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetcAPITexo.Business;
using System;
using System.Threading.Tasks;

namespace ProjetcAPITexo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _csvService;

        public FileController(IFileBusiness csvService)
        {
            _csvService = csvService;
        }

        [HttpPost("Readfilecsv")]
        public IActionResult Readfilecsv([FromForm] IFormFileCollection file)
        {
            try
            {
                var employees = _csvService.ReadCSV<MovieList>(file[0].OpenReadStream());
                if (employees == null) return BadRequest();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
