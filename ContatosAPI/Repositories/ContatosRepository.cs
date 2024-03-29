﻿using System.Collections.Generic;
using ContatosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContatosAPI.Repositories {

    // Repositório síncrono para Contatos 
    // Apenas para mostrar como a classe pode ser estendida
    public class ContatosRepository : GenericRepository<ContatoModel>, IContatosRepository {

        private IQueryable<ContatoModel> DbSetAllRecordsSorted => DbSetAllRecords.OrderBy(c => c.Nome);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"><see cref="ContatosContext"/></param>
        public ContatosRepository(ContatosContext dbContext)
            : base(dbContext) {
        }

        /// <summary>
        /// Localiza contato pelo telefone
        /// </summary>
        /// <param name="fone">string</param>
        /// <returns>ContatoModel</returns>
        public IEnumerable<ContatoModel> GetByPhone(string fone) {
            return DbSet.Where(c => c.Telefone.Contains(fone));
        }

        /// <summary>
        /// Lista de cpntatos em ordem alfabética
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;ContatoModel&gt;&gt;</returns>
        public IEnumerable<ContatoModel> GetSorted() {
            return DbSetAllRecordsSorted.ToList();
        }

        /// <summary>
        /// Lista de aniversariantes do mês
        /// </summary>
        /// <param name="mes">integer</param>
        /// <returns>ContatoModel</returns>
        public IEnumerable<ContatoModel> AniversariantesDoMes(int mes) {
            return AniversariantesDoMes(mes.ToString("00"));
        }

        /// <summary>
        /// Lista de aniversariantes do mês
        /// </summary>
        /// <param name="mes">string</param>
        /// <returns>ContatoModel</returns>
        public IEnumerable<ContatoModel> AniversariantesDoMes(string mes) {
            return DbSetAllRecords
                .Where(c => c.Aniversario.EndsWith(mes))
                .OrderBy(c => c.Aniversario).ThenBy(c => c.Nome)
                .ToList();
        }
    }
}