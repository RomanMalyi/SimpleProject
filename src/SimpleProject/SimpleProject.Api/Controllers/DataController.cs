using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleProject.Data.Models;
using SimpleProject.Data.Repositories;

namespace SimpleProject.Controllers
{
    public class DataController : ControllerBase
    {
        private readonly SimpleRepository _simpleRepository;

        public DataController(SimpleRepository simpleRepository)
        {
            _simpleRepository = simpleRepository;
        }

        [HttpGet("loaderio-9acf12a0712b29568095aaffa117ff2b")]
        public IActionResult GetTest()
        {
            byte[] byteArray = Encoding.UTF8.GetBytes("loaderio-9acf12a0712b29568095aaffa117ff2b");
            MemoryStream stream = new MemoryStream(byteArray);

            return File(stream, "application/octet-stream");
        }


        [HttpGet("entities/{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _simpleRepository.GetById(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("entities")]
        public async Task<IActionResult> Get([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            return Ok(await _simpleRepository.Get(skip, take));
        }

        [HttpPost("entities")]
        public async Task<IActionResult> Create(Entity entity)
        {
            await _simpleRepository.Create(entity);
            return Ok();
        }
    }
}
