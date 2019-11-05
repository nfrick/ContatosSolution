using ContatosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContatosAPI.Repositories {
    public interface IContatosRepository {
        Task<IEnumerable<ContatoModel>> GetAll();
        Task<ContatoModel> Get(int id);
        Task<ContatoModel> Add(ContatoModel contato);
        Task<ContatoModel> Update(ContatoModel contato);
        void Delete(int id);
    }
}
