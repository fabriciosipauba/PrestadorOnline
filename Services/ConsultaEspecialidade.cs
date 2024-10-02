using PrestadorOnline.Data;
using PrestadorOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace PrestadorOnline.Services
{
    public class ConsultaEspecialidade : IConsultaEspecialidade
    {
        private readonly EspecialidadesDbContext _context;
        public ConsultaEspecialidade(EspecialidadesDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EspecialidadeExistsAsync(string nome)
        {
            return await _context.Especialidades
                .AnyAsync(e => e.nome == nome);
        }
    }
}
