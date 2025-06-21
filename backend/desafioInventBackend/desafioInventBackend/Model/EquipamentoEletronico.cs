namespace DesafioInventBackend.Model
{
    public class EquipamentoEletronico
    {
        private long Id { get; set; }
        private string nome { get; set; }
        private int quantidadeEstoque { get; set; }
        private bool temEstoque { get; set; }
        private DateTime dataInclsao { get; set; }
        private DateTime dataExclusao { get; set; }
    }
}
