using ContatosAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ContatosAPI.Repositories {

    // Interface de repositório genérico com métodos síncronos
    public interface IGenericRepository<TEntity>
        where TEntity : class, IEntity {

        bool HasRecords();

        IQueryable<TEntity> GetAll();

        TEntity GetById(int id);

        void Create(TEntity entity);

        void Update(int id, TEntity entity);

        void Delete(int id);
    }
}