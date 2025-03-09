using PlanoDeContas.Domain.Entities;
using PlanoDeContas.Domain.Enums;
using PlanoDeContas.Domain.Interfaces;

namespace PlanoDeContas.Application.Services
{
    public class PlanoDeContaService(IPlanoDeContaRepository repository)
    {
        private readonly IPlanoDeContaRepository _repository = repository;

        public async Task<IEnumerable<PlanoDeConta>> ListarAsync()
        {
            return await _repository.ListarAsync();
        }

        public async Task<PlanoDeConta?> ObterPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(PlanoDeConta conta)
        {
            // Regra: Verificar se o código já existe
            var existente = await _repository.ObterPorCodigoAsync(conta.Codigo);
            if (existente != null)
                throw new Exception("Já existe uma conta com esse código.");

            // Regra: Se aceitar lançamentos, não pode ter filhos
            if (conta.AceitaLancamentos && conta.PaiId.HasValue)
            {
                var pai = await _repository.ObterPorIdAsync(conta.PaiId.Value);
                if (pai == null)
                    throw new Exception("O PaiId informado não existe no banco de dados.");
                if (pai.AceitaLancamentos == true)
                    throw new Exception("Conta que aceita lançamentos não pode ter filhos.");
            }

            // Regra: As contas devem ser do mesmo tipo do pai
            if (conta.PaiId.HasValue)
            {
                var pai = await _repository.ObterPorIdAsync(conta.PaiId.Value);
                if (pai == null)
                    throw new Exception("O PaiId informado não existe no banco de dados.");
                if (pai.Tipo != conta.Tipo)
                    throw new Exception($"A conta deve ser do mesmo tipo do seu pai. O tipo esperado é {pai.Tipo}.");
            }

            await _repository.AdicionarAsync(conta);
        }


        public async Task AtualizarAsync(PlanoDeConta conta)
        {
            await _repository.AtualizarAsync(conta);
        }

        public async Task RemoverAsync(int id)
        {
            var conta = await _repository.ObterPorIdAsync(id);
            if (conta == null)
                throw new Exception("Conta não encontrada.");

            await _repository.RemoverAsync(conta);
        }

        public async Task<string> ObterProximoCodigoAsync(int paiId)
        {
            var filhos = await _repository.ListarPorPaiIdAsync(paiId);

            if (!filhos.Any())
                return "1"; // Primeiro código filho

            var ultimoCodigo = filhos.OrderByDescending(p => p.Codigo).First().Codigo;
            var partes = ultimoCodigo.Split('.');
            var ultimoNumero = int.Parse(partes[^1]); // Obtém o último número corretamente
            var proximoNumero = ultimoNumero + 1;

            if (proximoNumero > 999)
            {
                // Encontrar o novo pai
                var pai = await _repository.ObterPorIdAsync(paiId);
                if (pai == null || pai.PaiId == null)
                    throw new Exception("Não é possível sugerir um novo código.");

                return await ObterProximoCodigoAsync(pai.PaiId.Value);
            }

            return string.Join(".", partes.Take(partes.Length - 1)) + "." + proximoNumero;
        }
    }
}
