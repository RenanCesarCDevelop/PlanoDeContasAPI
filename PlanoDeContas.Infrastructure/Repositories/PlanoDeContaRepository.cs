using Microsoft.EntityFrameworkCore;
using PlanoDeContas.Domain.Entities;
using PlanoDeContas.Domain.Interfaces;
using PlanoDeContas.Infrastructure.Data;

namespace PlanoDeContas.Infrastructure.Repositories
{
    public class PlanoDeContaRepository : IPlanoDeContaRepository
    {
        private readonly AppDbContext _context;

        public PlanoDeContaRepository(AppDbContext context) => _context = context;

        public async Task<PlanoDeConta?> ObterPorIdAsync(int id)
        {
            return await _context.PlanoDeContas
                .Include(p => p.Filhos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PlanoDeConta?> ObterPorCodigoAsync(string codigo)
        {
            return await _context.PlanoDeContas.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public async Task<IEnumerable<PlanoDeConta>> ListarAsync()
        {
            return await _context.PlanoDeContas.ToListAsync();
        }

        public async Task AdicionarAsync(PlanoDeConta conta)
        {
            _context.PlanoDeContas.Add(conta);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(PlanoDeConta conta)
        {
            _context.PlanoDeContas.Update(conta);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(PlanoDeConta conta)
        {
            _context.PlanoDeContas.Remove(conta);
            await _context.SaveChangesAsync();
        }

        public async Task<string> ObterProximoCodigoAsync(int paiId)
        {
            var filhos = await _context.PlanoDeContas
                .Where(p => p.PaiId == paiId)
                .OrderByDescending(p => p.Codigo)
                .ToListAsync();

            if (!filhos.Any())
                return "1"; // Primeiro código filho

            var ultimoCodigo = filhos.First().Codigo;
            var partes = ultimoCodigo.Split('.');
            var proximoNumero = int.Parse(partes.Last()) + 1;

            return string.Join(".", partes.Take(partes.Length - 1)) + "." + proximoNumero;
        }

        public async Task<IEnumerable<PlanoDeConta>> ListarPorPaiIdAsync(int paiId)
        {
            return await _context.PlanoDeContas
                .Where(p => p.PaiId == paiId)
                .ToListAsync();
        }

    }
}
