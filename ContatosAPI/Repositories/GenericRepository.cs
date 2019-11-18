using System.Collections.Generic;
using ContatosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContatosAPI.Repositories {

    // Repositório genérico síncrono
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity {
        private readonly DbContext _dbContext;

        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();  // para simplificar referências
        protected IQueryable<TEntity> DbSetAllRecords => DbSet.AsNoTracking();  // para simplificar referências

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">DbContext</param>
        public GenericRepository(DbContext dbContext) {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retorna true se a tabela possui registros.
        /// </summary>
        /// <returns>boolean - true se a tabela possui registros</returns>
        public bool HasRecords() {
            return DbSetAllRecords.Any();
        }

        /// <summary>
        /// Todos os registros da tabela
        /// </summary>
        /// <returns>IQueryable&lt;TEntity&gt;</returns>
        public IQueryable<TEntity> GetAll() {
            return DbSetAllRecords;
        }

        /// <summary>
        /// Localiza registro por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TEntity</returns>
        public TEntity GetById(int id) {
            return DbSetAllRecords.First(e => e.Id == id);
        }

        /// <summary>
        /// Cria um novo registro
        /// </summary>
        /// <param name="entity">Classe do objeto a ser criado</param>
        public void Create(TEntity entity) {
            DbSet.AddAsync(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Atualiza registro
        /// </summary>
        /// <param name="id">id do registro sendo atualizado</param>
        /// <param name="entity">objeto com as atualizações</param>
        public void Update(int id, TEntity entity) {
            DbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Deleta registro
        /// </summary>
        /// <param name="id">id do registro a ser deletado</param>
        public void Delete(int id) {
            var entity = GetById(id);
            DbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
