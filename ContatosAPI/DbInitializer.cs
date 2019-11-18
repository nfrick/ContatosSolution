using ContatosAPI.Models;
using System.Linq;

namespace ContatosAPI {
    public class DbInitializer {
        /// <summary>
        /// Inicializa o banco de dados: verifica se foi criado e adiciona registros
        /// </summary>
        /// <param name="context">database context</param>
        public static void Initialize(ContatosContext context) {
            context.Database.EnsureCreated();

            if (context.Contatos.Any()) {
                return;   // DB has been seeded
            }

            context.Contatos.Add(new ContatoModel { Nome = "Pessoa", Telefone = "2222-2222", Aniversario = "10/10" });
            context.Contatos.Add(new ContatoModel { Nome = "Outra Pessoa", Telefone = "3333-3333", Aniversario = "11/11" });
            context.SaveChanges();
        }

    }
}
