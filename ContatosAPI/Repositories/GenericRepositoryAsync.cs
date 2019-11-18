using System.Collections.Generic;
using ContatosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContatosAPI.Repositories {

    // Repositório genérico assíncrono
    public class GenericRepositoryAsync<TEntity> : IGenericRepositoryAsync<TEntity>
        where TEntity : class, IEntity {
        private readonly DbContext _dbContext;
        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>(); // para simplificar referências

        protected IQueryable<TEntity> DbSetAllRecords => DbSet.AsNoTracking(); // para simplificar referências

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">DbContext</param>
        public GenericRepositoryAsync(DbContext dbContext) => _dbContext = dbContext;

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
        /// <returns>Task&lt;IEnumerable&lt;TEntity&gt;&gt;</returns>
        public async Task<IEnumerable<TEntity>> GetAll() {
            return await DbSetAllRecords.ToListAsync();
        }

        /// <summary>
        /// Localiza registro por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TEntity</returns>
        public async Task<TEntity> GetById(int id) {
            return await DbSetAllRecords.FirstAsync(e => e.Id == id);
        }

        /// <summary>
        /// Cria um novo registro
        /// </summary>
        /// <param name="entity">Classe do objeto a ser criado</param>
        public async Task Create(TEntity entity) {
            await DbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza registro
        /// </summary>
        /// <param name="id">id do registro sendo atualizado</param>
        /// <param name="entity">objeto com as atualizações</param>
        public async Task Update(int id, TEntity entity) {
            DbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Deleta registro
        /// </summary>
        /// <param name="id">id do registro a ser deletado</param>
        public async Task Delete(int id) {
            var entity = await GetById(id);
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
