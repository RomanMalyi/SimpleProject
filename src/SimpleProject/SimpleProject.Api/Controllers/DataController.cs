using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleProject.Data.Models;
using SimpleProject.Data.Repositories;

namespace SimpleProject.Controllers
{
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly SimpleRepository _simpleRepository;

        public DataController(SimpleRepository simpleRepository)
        {
            _simpleRepository = simpleRepository;
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
