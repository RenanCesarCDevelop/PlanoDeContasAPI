using Microsoft.AspNetCore.Mvc;
using PlanoDeContas.Application.Services;
using PlanoDeContas.Domain.Entities;

namespace PlanoDeContas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoDeContaController(PlanoDeContaService service) : ControllerBase
    {
        private readonly PlanoDeContaService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanoDeConta>>> Listar()
        {
            return Ok(await _service.ListarAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanoDeConta>> ObterPorId(int id)
        {
            var conta = await _service.ObterPorIdAsync(id);
            if (conta == null)
                return NotFound("Conta não encontrada.");

            return Ok(conta);
        }

        [HttpGet("proximo-codigo/{paiId}")]
        public async Task<ActionResult<string>> ObterProximoCodigo(int paiId)
        {
            return Ok(await _service.ObterProximoCodigoAsync(paiId));
        }

        [HttpPost]
        public async Task<ActionResult> Criar([FromBody] PlanoDeConta conta)
        {
            try
            {
                await _service.AdicionarAsync(conta);
                return CreatedAtAction(nameof(ObterPorId), new { id = conta.Id }, conta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] PlanoDeConta conta)
        {
            if (id != conta.Id)
                return BadRequest("IDs não coincidem.");

            await _service.AtualizarAsync(conta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            try
            {
                await _service.RemoverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

     
    }
}
