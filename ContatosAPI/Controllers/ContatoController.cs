using ContatosAPI.Models;
using ContatosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContatosAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase {
        private readonly IContatosRepository _repo;

        public ContatoController(IContatosRepository repo) {
            _repo = repo;

            //if (_repo.Contatos.Any()) {
            //    return;
            //}

            //_repo.Contatos.Add(new ContatoModel { Nome = "Pessoa", Telefone = "2222-2222", Aniversario = "10/10"});
            //_repo.Contatos.Add(new ContatoModel { Nome = "Outra Pessoa", Telefone = "3333-3333", Aniversario = "11/11" });
            //_repo.SaveChanges();
        }

        [HttpGet]
        public async Task<IEnumerable<ContatoModel>> GetContatos() {
            return await _repo.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoModel>> GetContato(int id) {
            var contato = await _repo.Get(id);

            if (contato == null)
                return NotFound();
            return new ObjectResult(contato);
        }

        [HttpPost]
        public async Task<ActionResult<ContatoModel>> PostContato(ContatoModel contato) {
            contato = await _repo.Add(contato);
            return CreatedAtAction(nameof(GetContato), new { id = contato.Id }, contato);
        }

        [HttpPut("{id}")]
        public IActionResult PutContato(long id, ContatoModel contato) {
            if (id != contato.Id) {
                return BadRequest();
            }
            _repo.Update(contato);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContato(int id) {
            _repo.Delete(id);
            return NoContent();
        }
    }
}
