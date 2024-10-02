using PrestadorOnline.Data;
using PrestadorOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace PrestadorOnline.Services
{
    public class ConsultaServico : IConsultaServico
    {
        private readonly ServicosDbContext _context;
        public ConsultaServico(ServicosDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ServicoExistsAsync(string nome)
        {
            return await _context.Servico
                .AnyAsync(e => e.nome == nome);
        }
    }
}
