using PlanoDeContas.Domain.Enums;

namespace PlanoDeContas.Domain.Entities
{
    public class PlanoDeConta
    {
        public int Id { get; set; }  // Chave primária
        public string Codigo { get; set; } = string.Empty; // Ex: "1.2.3"
        public string Descricao { get; set; } = string.Empty;
        public bool AceitaLancamentos { get; set; } // Se for false, pode ter filhos
        public int? PaiId { get; set; } // ID da conta pai (nula se for raiz)
        public TipoContaEnum Tipo { get; set; }
        //Relacionamento com a conta pai
        public PlanoDeConta? Pai { get; set; }

       // Relacionamento com as contas filhas
        public ICollection<PlanoDeConta> Filhos { get; set; } = [];
    }
}
