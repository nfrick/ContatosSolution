using ContatosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContatosAPI.Repositories {

    // Repositório assíncrono para Contatos 
    // Apenas para mostrar como a classe pode ser estendida
    public class ContatosRepositoryAsync : GenericRepositoryAsync<ContatoModel>, IContatosRepositoryAsync {

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"><see cref="!:DbContextContext" /></param>
        public ContatosRepositoryAsync(DbContext dbContext)
            : base(dbContext) {
        }

        private IQueryable<ContatoModel> DbSetAllRecordsSorted => DbSetAllRecords.OrderBy(c => c.Nome);

        /// <summary>
        /// Localiza contato pelo telefone
        /// </summary>
        /// <param name="fone">string</param>
        /// <returns>ContatoModel</returns>
        public async Task<IEnumerable<ContatoModel>> GetByPhone(string fone) {
            return await DbSet.Where(c => c.Telefone.Contains(fone)).ToListAsync();
        }

        /// <summary>
        /// Lista de cpntatos em ordem alfabética
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;ContatoModel&gt;&gt;</returns>
        public async Task<IEnumerable<ContatoModel>> GetSorted() {
            return await DbSetAllRecordsSorted.ToListAsync();
        }

        /// <summary>
        /// Lista de aniversariantes do mês
        /// </summary>
        /// <param name="mes">integer</param>
        /// <returns>ContatoModel</returns>
        public async Task<IEnumerable<ContatoModel>> AniversariantesDoMes(int mes) {
            return await AniversariantesDoMes(mes.ToString("00"));
        }

        /// <summary>
        /// Lista de aniversariantes do mês
        /// </summary>
        /// <param name="mes">string</param>
        /// <returns>ContatoModel</returns>
        public async Task<IEnumerable<ContatoModel>> AniversariantesDoMes(string mes) {
            return await DbSetAllRecords
                .Where(c => c.Aniversario.EndsWith(mes))
                .OrderBy(c => c.Aniversario).ThenBy(c => c.Nome)
                .ToListAsync();
        }
    }
}