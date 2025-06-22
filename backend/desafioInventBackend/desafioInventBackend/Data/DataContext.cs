using DesafioInventBackend.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace DesafioInventBackend.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<EquipamentoEletronico> equipamentoEletronico { get; set; } = null!;
    }
}
