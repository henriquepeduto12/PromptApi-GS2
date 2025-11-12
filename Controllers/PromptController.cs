using Microsoft.AspNetCore.Mvc;
using System.Net;
using PromptApi.Models;
using PromptApi.Services;

namespace PromptApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromptController : ControllerBase
    {
        private readonly IPromptService _service;
        private readonly ILogger<PromptController> _logger;

        public PromptController(IPromptService service, ILogger<PromptController> logger)
        {
            _service = service; _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? tag = null)
        {
            try {
                var list = await _service.GetAllAsync(tag);
                return Ok(list);
            } catch (Exception ex) {
                _logger.LogError(ex, "Erro ao listar prompts");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "Erro interno" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try {
                var item = await _service.GetByIdAsync(id);
                return item is null ? NotFound(new { message = "Não encontrado" }) : Ok(item);
            } catch (Exception ex) {
                _logger.LogError(ex, "Erro ao obter prompt {Id}", id);
                return StatusCode(500, new { message = "Erro interno" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Prompt input)
        {
            try {
                var created = await _service.CreateAsync(input);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            } catch (ArgumentException aex) {
                _logger.LogWarning(aex, "Validação falhou ao criar prompt");
                return BadRequest(new { message = aex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "Erro ao criar prompt");
                return StatusCode(500, new { message = "Erro interno" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Prompt input)
        {
            try {
                await _service.UpdateAsync(id, input);
                return NoContent();
            } catch (KeyNotFoundException) {
                return NotFound(new { message = "Não encontrado" });
            } catch (ArgumentException aex) {
                return BadRequest(new { message = aex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "Erro ao atualizar prompt {Id}", id);
                return StatusCode(500, new { message = "Erro interno" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try {
                await _service.DeleteAsync(id);
                return NoContent();
            } catch (ArgumentException aex) {
                return BadRequest(new { message = aex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "Erro ao excluir prompt {Id}", id);
                return StatusCode(500, new { message = "Erro interno" });
            }
        }
    }
}
