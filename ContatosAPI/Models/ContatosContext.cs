using Microsoft.EntityFrameworkCore;

namespace ContatosAPI.Models {
    public class ContatosContext : DbContext {
        public ContatosContext(DbContextOptions options)
            : base(options) {
        }

        public DbSet<ContatoModel> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder mb) {
            // https://docs.microsoft.com/pt-br/ef/ef6/modeling/code-first/conventions/custom

            // Associa a classe ContatoModel à tabela Contatos
            mb.Entity<ContatoModel>().ToTable("Contatos");
        }
    }
}
