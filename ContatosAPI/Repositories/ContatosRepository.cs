using ContatosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContatosAPI.Repositories {
    public class ContatosRepository : IContatosRepository {
        private readonly ContatosContext _context;

        public ContatosRepository(ContatosContext context) {
            _context = context;
        }
        public async Task<IEnumerable<ContatoModel>> GetAll() {
            return await _context.Contatos.OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<ContatoModel> Get(int id) {
            return await _context.Contatos.FindAsync(id);
        }

        public async Task<ContatoModel> Add(ContatoModel contato) {
            await _context.Contatos.AddAsync(contato);
            _context.SaveChanges();
            return contato;
        }

        public async Task<ContatoModel> Update(ContatoModel contato) {
             _context.Contatos.Update(contato);
            await _context.SaveChangesAsync();
            return contato;
        }

        public void Delete(int id) {
            var contato = _context.Contatos.Find(id);
            if (contato == null) return;
            _context.Contatos.Remove(contato);
            _context.SaveChanges();
        }
    }
}
