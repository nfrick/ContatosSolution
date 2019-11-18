using ContatosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContatosAPI.Repositories {
    // Interface de repositório assíncrono para contatos
    public interface IContatosRepositoryAsync : IGenericRepositoryAsync<ContatoModel> {
        Task<IEnumerable<ContatoModel>> GetByPhone(string fone);

        Task<IEnumerable<ContatoModel>> GetSorted();

        Task<IEnumerable<ContatoModel>> AniversariantesDoMes(int mes);
    }
}
