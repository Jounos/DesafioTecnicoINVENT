using Microsoft.EntityFrameworkCore;

namespace DesafioInventBackend.Model.Context
{
    public class EquipamentoEletronicoContext : DbContext
    {
        public EquipamentoEletronicoContext(DbContextOptions<EquipamentoEletronicoContext> options) : base(options) { }

        public DbSet<EquipamentoEletronico> equipamentoEletronicos { get; set; } = null!;
    }
}
