using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrestadorOnline.Models;
using PrestadorOnline.Data;

namespace PrestadorOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly ServicosDbContext _context;
        public ServicoController(ServicosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servico>>> GetServicos()
        {
            return await _context.Servico.ToListAsync();
        }

        [HttpGet("busca/{nome}")]
        public async Task<ActionResult<IEnumerable<Servico>>> GetServico(string nome)
        {
            var buscaServico = await _context.Servico
        .Where(p => p.nome.Contains(nome))
        .ToListAsync();

            if (buscaServico == null || !buscaServico.Any())
            {
                return NotFound("Nenhum serviço encontrado.");
            }

            return Ok(buscaServico);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult<Especialidades>> PostServico(Servico servico,
            [FromServices] EspecialidadesDbContext especialidadesDbContext)
        {
            var servicoExistente = await _context.Servico
        .FirstOrDefaultAsync(e => e.nome == servico.nome);

            if (servicoExistente != null)
            {
                return Conflict(new { message = "O serviço já está cadastrado." });
            }

            var especialidadeExistente = await especialidadesDbContext.Especialidades
        .FirstOrDefaultAsync(e => e.nome == servico.nome);

            if (especialidadeExistente != null)
            {
                return Conflict(new { message = "O serviço informado já existe como uma especialidade." });
            }

            _context.Servico.Add(servico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServicos", new { id = servico.servicoId }, servico);
        }

        [HttpPut("alterar/{id}")]
        public async Task<IActionResult> PutServico(int id, Servico servico)
        {
            if (id != servico.servicoId)
            {
                return Conflict("Você precisa informar um Id de cadastro válido");
            }

            _context.Entry(servico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {

                if (!await ServicoExists(id))
                {
                    return NotFound($"O Id {id} não corresponde a nenhum serviço cadastrada");
                }
                else
                {
                    throw;
                }
            }

            return Ok($"O registro do id {id} foi alterado com sucesso para {servico.nome}");
        }

        private async Task<bool> ServicoExists(int id)
        {
            return await _context.Servico.AnyAsync(e => e.servicoId == id);
        }

        [HttpDelete("excluircadastro/{id}")]
        public async Task<IActionResult> DeleteServico(int id, [FromServices] PrestadoresDbContext prestadoresDbContext)
        {
            try
            {
                var apagarServico = await _context.Servico.FindAsync(id);

                if (apagarServico == null)
                {
                    return NotFound("Informe um ID de serviço válido para exclusão.");
                }

                // Verificar dependência no banco de dados de prestadores
                var hasDependencies = await prestadoresDbContext.Prestadores.AnyAsync(p => p.servico == apagarServico.nome);
                
                if (hasDependencies)
                {
                    return Conflict($"A especialidade '{apagarServico.nome}' está vinculada a um ou mais prestadores e não pode ser excluída.");
                }

                _context.Servico.Remove(apagarServico);
                await _context.SaveChangesAsync();

                return Ok($"O registro do id {apagarServico.servicoId}: {apagarServico.nome} foi apagado com sucesso.");
            }

            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar excluir a especialidade. Tente novamente mais tarde.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}