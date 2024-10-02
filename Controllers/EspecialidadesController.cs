using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrestadorOnline.Models;
using PrestadorOnline.Data;

namespace PrestadorOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly EspecialidadesDbContext _context;
        public EspecialidadesController(EspecialidadesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especialidades>>> GetEspecialidades()
        {
            return await _context.Especialidades.ToListAsync();
        }

        [HttpGet("busca/{nome}")]
        public async Task<ActionResult<IEnumerable<Especialidades>>> GetPrestadoresEspec(string nome)
        {
            var buscaEspecialidade = await _context.Especialidades
        .Where(p => p.nome.Contains(nome))
        .ToListAsync();

            if (buscaEspecialidade == null || !buscaEspecialidade.Any())
            {
                return NotFound("Nenhuma especialidade encontrada.");
            }

            return Ok(buscaEspecialidade);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult<Especialidades>> PostEspecialidade(Especialidades especialidade)
        {
            var especialidadeExistente = await _context.Especialidades
        .FirstOrDefaultAsync(e => e.nome == especialidade.nome);

            if (especialidadeExistente != null)
            {
                return Conflict(new { message = "A especialidade já está cadastrada." });
            }

            _context.Especialidades.Add(especialidade);
            await _context.SaveChangesAsync();
                               
            return CreatedAtAction("GetEspecialidades", new { id = especialidade.especialidadeId }, especialidade);
                        
        }
        
        [HttpPut("alterar/{id}")]
        public async Task<IActionResult> PutEspecialidade(int id, Especialidades especialidade)
        {
            if (id != especialidade.especialidadeId)
            {
                return Conflict("Você precisa informar um Id de cadastro válido");
            }

            _context.Entry(especialidade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!await EspecialidadeExists(id))
                {
                    return NotFound($"O Id {id} não corresponde a nenhuma especialidade cadastrada");
                }

                else
                {
                    throw;
                }
            }

            return Ok($"O registro do id {id} foi alterado com sucesso para {especialidade.nome}");
        }

        private async Task<bool> EspecialidadeExists(int id)
        {
            return await _context.Especialidades.AnyAsync(e => e.especialidadeId == id);
        }

        [HttpDelete("excluircadastro/{id}")]
        public async Task<IActionResult> DeleteEspecialidade(int id, [FromServices] PrestadoresDbContext prestadoresDbContext)
        {
            try
            {
                var apagarEspecialidade = await _context.Especialidades.FindAsync(id);

                if (apagarEspecialidade == null)
                {
                    return NotFound("Informe um ID de especialidade válida para exclusão.");
                }
                                
                var hasDependencies = await prestadoresDbContext.Prestadores.AnyAsync(
                    p => p.especialidade == apagarEspecialidade.nome);

                if (hasDependencies)
                {
                    return Conflict($"A especialidade '{apagarEspecialidade.nome}' está vinculada a um ou mais prestadores e não pode ser excluída.");
                }

                _context.Especialidades.Remove(apagarEspecialidade);
                await _context.SaveChangesAsync();

                return Ok($"O registro do id {apagarEspecialidade.especialidadeId}: {apagarEspecialidade.nome} foi apagado com sucesso.");
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