using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleProject.Data.Models;

namespace SimpleProject.Gateway.Controllers
{
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ConnectionSettings _connectionSettings;

        public GatewayController(IHttpClientFactory clientFactory,
            ConnectionSettings connectionSettings)
        {
            _clientFactory = clientFactory;
            _connectionSettings = connectionSettings;
        }

        [HttpGet("entities/{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _clientFactory
                .CreateClient()
                .GetAsync(_connectionSettings.SimpleApiUrl + $"entities/{id}");

            return await GetResult(response);
        }

        [HttpPost("entities")]
        public async Task<IActionResult> Create(Entity entity)
        {
            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_connectionSettings.SimpleApiUrl + "entities", content);

            return await GetResult(response);
        }

        [HttpDelete("entities/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _clientFactory
                .CreateClient()
                .DeleteAsync(_connectionSettings.SimpleApiUrl + $"entities/{id}");

            return await GetResult(response);
        }

        private async Task<IActionResult> GetResult(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) return StatusCode((int) response.StatusCode);

            var resultContent = await response.Content.ReadAsStringAsync();
            return StatusCode((int) response.StatusCode, resultContent);
        }
    }
}
