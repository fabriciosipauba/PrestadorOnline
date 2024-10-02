using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrestadorOnline.Models;
using PrestadorOnline.Data;
using PrestadorOnline.Services;

namespace PrestadorOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestadorController : ControllerBase
    {
        private readonly PrestadoresDbContext _context;
        private readonly IConsultaEspecialidade _consultaEspecialidade;
        private readonly IConsultaServico _consultaServico;

        public PrestadorController(PrestadoresDbContext context,
            IConsultaEspecialidade consultaEspecialidade, IConsultaServico consultaServico)
        {
            _consultaEspecialidade = consultaEspecialidade;
            _context = context;
            _consultaServico = consultaServico;
        }

        // GET: api/prestador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestador>>> GetPrestadores()
        {
            return await _context.Prestadores.ToListAsync();
        }

        // GET: api/prestador/nome/{nome}
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Prestador>>> GetPrestador(string nome)
        {
            var prestadores = await _context.Prestadores
        .Where(p => p.nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
        .ToListAsync();

            if (prestadores == null || !prestadores.Any())
            {
                return NotFound("Nenhum prestador com esse nome encontrado.");
            }

            return Ok(prestadores);
        }

        [HttpGet("especialidade/{especialidade}")]
        public async Task<ActionResult<IEnumerable<Prestador>>> GetPrestadoresEspec(string especialidade)
        {
            var prestadores = await _context.Prestadores
        .Where(p => p.especialidade.Contains(especialidade, StringComparison.OrdinalIgnoreCase))
        .ToListAsync();

            if (prestadores == null || !prestadores.Any())
            {
                return NotFound("Nenhum prestador com essa especialidade encontrado.");
            }

            return Ok(prestadores);
        }

        [HttpGet("servico/{servico}")]
        public async Task<ActionResult<IEnumerable<Prestador>>> GetPrestadoresServ(string servico)
        {
            var prestadores = await _context.Prestadores
        .Where(p => p.servico.Contains(servico, StringComparison.OrdinalIgnoreCase))
        .ToListAsync();

            if (prestadores == null || !prestadores.Any())
            {
                return NotFound("Nenhum prestador que atenda esse serviço encontrado.");
            }

            return Ok(prestadores);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult<Prestador>> PostPrestador(Prestador prestador, [FromServices] PrestadoresDbContext prestadoresDbContext)
        {

            if (prestador == null)
            {
                return BadRequest("O nome do prestador é obrigatório no cadastro.");
            }

            var prestadorExistente = await prestadoresDbContext.Prestadores
        .AnyAsync(p => p.nome == prestador.nome &&
                       p.especialidade == prestador.especialidade &&
                       p.servico == prestador.servico);

            if (prestadorExistente)
            {
                return Conflict($"Prestador {prestador.nome} já cadastrado com esta especialidade e serviço.");
            }

            if (!await _consultaEspecialidade.EspecialidadeExistsAsync(prestador.especialidade))
            {
                return BadRequest($"A especialidade {prestador.especialidade} não existe no registro, informe uma entrada válida para o cadastro.");
            }

            if (!await _consultaServico.ServicoExistsAsync(prestador.servico))
            {
                return BadRequest($"O serviço {prestador.servico} não existe no registro, informe uma entrada válida para o cadastro.");
            }

            _context.Prestadores.Add(prestador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrestador), new { nome = prestador.nome }, prestador);
        }

        [HttpPut("alterarcadastro/{id}")]
        public async Task<ActionResult> AlterarCadastro(int id, Prestador prestador)
        {
            if (id != prestador.prestadorId)
            {
                return Conflict("Você precisa informar um Id de cadastro válido");
            }

            var prestadorCadastrado = await _context.Prestadores.FindAsync(id);

            if (prestadorCadastrado == null)
            {
                return NotFound($"O Id {id} não corresponde a nenhum prestador cadastrado");
            }

            // Atualizar os dados do prestador
            prestadorCadastrado.nome = prestador.nome;
            prestadorCadastrado.email = prestador.email;
            prestadorCadastrado.telefone = prestador.telefone;
            prestadorCadastrado.especialidade = prestador.especialidade;
            prestadorCadastrado.servico = prestador.servico;
            prestadorCadastrado.descricaoServico = prestador.descricaoServico;
            prestadorCadastrado.precoServico = prestador.precoServico;

            if (prestador == null)
            {
                return BadRequest("O nome do prestador é obrigatório no cadastro.");
            }

            if (!await _consultaEspecialidade.EspecialidadeExistsAsync(prestador.especialidade))
            {
                return BadRequest($"A especialidade {prestador.especialidade} não existe no registro, informe uma entrada válida para o cadastro.");
            }

            if (!await _consultaServico.ServicoExistsAsync(prestador.servico))
            {
                return BadRequest($"O serviço {prestador.servico} não existe no registro, informe uma entrada válida para o cadastro.");
            }

            await _context.SaveChangesAsync();

            return Ok($"Cadastro de {prestador.nome} atualizado no registro");

        }

        [HttpDelete("excluircadastro/{id}")]
        public async Task<IActionResult> DeletePrestador(int id)
        {
            var prestador = await _context.Prestadores.FindAsync(id);

            if (prestador == null)
            {
                return NotFound("Informe um ID de prestador válido para exclusão.");
            }

            _context.Prestadores.Remove(prestador);
            await _context.SaveChangesAsync();

            return Ok($"O registro do prestador {prestador.nome} foi apagado com sucesso.");
        }

        private bool PrestadorExists(int id)
        {
            return _context.Prestadores.Any(e => e.prestadorId == id);
        }
    }
}
