using PlanoDeContas.Domain.Entities;

namespace PlanoDeContas.Domain.Interfaces
{
    public interface IPlanoDeContaRepository
    {
        Task<PlanoDeConta?> ObterPorIdAsync(int id);
        Task<PlanoDeConta?> ObterPorCodigoAsync(string codigo);
        Task<IEnumerable<PlanoDeConta>> ListarAsync();
        Task AdicionarAsync(PlanoDeConta conta);
        Task AtualizarAsync(PlanoDeConta conta);
        Task RemoverAsync(PlanoDeConta conta);
        Task<string> ObterProximoCodigoAsync(int paiId);
        Task<IEnumerable<PlanoDeConta>> ListarPorPaiIdAsync(int paiId);

    }
}
