using System.Collections.Generic;
using ContatosAPI.Models;
using System.Threading.Tasks;

namespace ContatosAPI.Repositories {

    // Interface de repositório síncrono para contatos
    public interface IContatosRepository : IGenericRepository<ContatoModel> {
        IEnumerable<ContatoModel> GetByPhone(string fone);

        IEnumerable<ContatoModel> GetSorted();

        IEnumerable<ContatoModel> AniversariantesDoMes(int mes);
    }
}
