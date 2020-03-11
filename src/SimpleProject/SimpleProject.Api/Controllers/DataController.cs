using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleProject.Data.Models;
using SimpleProject.Data.Repositories;

namespace SimpleProject.Controllers
{
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly SimpleRepository _simpleRepository;
        private readonly IMemoryCache _cache;

        public DataController(SimpleRepository simpleRepository,
            IMemoryCache cache)
        {
            _simpleRepository = simpleRepository;
            _cache = cache;
        }

        [HttpGet("entities/{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (_cache.TryGetValue(id, out Entity result)) return Ok(result);

            result = await _simpleRepository.GetById(id);
            if (result == null) return NotFound();
            _cache.Set(id, result);

            return Ok(result);
        }

        [HttpPost("entities")]
        public async Task<IActionResult> Create(Entity entity)
        {
            await _simpleRepository.Create(entity);
            return Ok();
        }

        [HttpDelete("entities/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (_cache.TryGetValue(id, out Entity result))
            {
                await _simpleRepository.Delete(result);
                _cache.Remove(id);
            }
            else
            {
                result = await _simpleRepository.GetById(id);
                if (result != null) await _simpleRepository.Delete(result);
            }

            return Ok();
        }

        [HttpGet("loaderio-9acf12a0712b29568095aaffa117ff2b")]
        public IActionResult GetTest()
        {
            byte[] byteArray = Encoding.UTF8.GetBytes("loaderio-9acf12a0712b29568095aaffa117ff2b");
            MemoryStream stream = new MemoryStream(byteArray);

            return File(stream, "application/octet-stream");
        }
    }
}
