using Microsoft.EntityFrameworkCore;

namespace ContatosAPI.Models {
    public class ContatosContext : DbContext {
        public ContatosContext(DbContextOptions<ContatosContext> options)
            : base(options) {
        }

        public DbSet<ContatoModel> Contatos { get; set; }
    }
}
