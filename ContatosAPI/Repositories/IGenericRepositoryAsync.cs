using ContatosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContatosAPI.Repositories {
    // Interface de repositório genérico com métodos assíncronos
    public interface IGenericRepositoryAsync<TEntity>
        where TEntity : class, IEntity {

        bool HasRecords();

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task Create(TEntity entity);

        Task Update(int id, TEntity entity);

        Task Delete(int id);
    }
}