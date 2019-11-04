using ContatosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContatosAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase {
        private readonly ContatosContext _context;

        public ContatoController(ContatosContext context) {
            _context = context;

            if (_context.Contatos.Any()) {
                return;
            }

            _context.Contatos.Add(new ContatoModel { Nome = "Pessoa", Telefone = "2222-2222", Aniversario = "10/10"});
            _context.Contatos.Add(new ContatoModel { Nome = "Outra Pessoa", Telefone = "3333-3333", Aniversario = "11/11" });
            _context.SaveChanges();
        }

        // GET: api/ContatoModel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoModel>>> GetContatos() {
            return await _context.Contatos.OrderBy(c=>c.Nome).ToListAsync();
        }

        // GET: api/ContatoModel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoModel>> GetContato(long id) {
            var contato = await _context.Contatos.FindAsync(id);

            if (contato == null) {
                return NotFound();
            }

            return contato;
        }

        [HttpPost]
        public async Task<ActionResult<ContatoModel>> PostContato(ContatoModel contato) {
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContato), new { id = contato.Id }, contato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContato(long id, ContatoModel contatoModel) {
            if (id != contatoModel.Id) {
                return BadRequest();
            }

            _context.Entry(contatoModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(long id) {
            var contato = await _context.Contatos.FindAsync(id);

            if (contato == null) {
                return NotFound();
            }

            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
