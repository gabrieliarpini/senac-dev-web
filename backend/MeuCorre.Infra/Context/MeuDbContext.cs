using MeuCorre.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuCorre.Infra.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(
            DbContextOptions<MeuDbContext> opcoes) : base(opcoes)
        {
            //Desabilita o rastreamento de alterações para melhorar a
            //performance em consultas somente leitura.
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        //Define a ligação entre a classe c# com a tabela do DB.
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
