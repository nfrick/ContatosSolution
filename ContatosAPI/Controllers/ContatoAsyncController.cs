using ContatosAPI.Models;
using ContatosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;

namespace ContatosAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoAsyncController : ControllerBase {
        private readonly IContatosRepositoryAsync _repo;

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="repo"></param>
        public ContatoAsyncController(IContatosRepositoryAsync repo) {
            _repo = repo;
        }

        /// <summary>
        /// Retorna todos os contatos
        /// </summary>
        /// <returns>IEnumerable&lt;ContatoModel&gt;</returns>
        [HttpGet]
        public async Task<IEnumerable> GetAll() {
            return await _repo.GetSorted();
        }

        /// <summary>
        /// Retorna contato por id
        /// </summary>
        /// <param name="id">integer</param>
        /// <returns>ContatoModel</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoModel>> GetById(int id) {
            var contato = await _repo.GetById(id);

            if (contato == null)
                return NotFound();
            return new ObjectResult(contato);
        }

        /// <summary>
        /// Retorna contato por telefone
        /// </summary>
        /// <param name="fone">string</param>
        /// <returns>ContatoModel</returns>
        //  NÃO FUNCIONA
        //[HttpGet, Route("GetByPhone/{fone:string}")]
        //public async Task<IEnumerable> GetByPhone(string fone) {
        //    return await _repo.GetByPhone(fone);
        //}

        /// <summary>
        /// Retorna contatos com aniversário no mês
        /// </summary>
        /// <param name="mes">int</param>
        /// <returns>ContatoModel</returns>
        [HttpGet, Route("GetByMonth/{mes}")]
        public async Task<IEnumerable> GetByMonth(int mes) {
            return await _repo.AniversariantesDoMes(mes);
        }

        /// <summary>
        /// Adiciona contato
        /// </summary>
        /// <param name="contato">ContatoModel - objeto contendo dados do contato</param>
        /// <returns>ContatoModel - contato criado</returns>
        [HttpPost]
        public async Task<ActionResult<ContatoModel>> Add([FromBody]ContatoModel contato) {
            await _repo.Create(contato);
            return CreatedAtAction(nameof(GetById), new { id = contato.Id }, contato);
        }

        /// <summary>
        /// Atualiza contato
        /// </summary>
        /// <param name="id">integer</param>
        /// <param name="contato">ContatoModel</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<ContatoModel>> Update(int id, [FromBody]ContatoModel contato) {
            if (id != contato.Id) {
                return BadRequest();
            }
            await _repo.Update(id, contato);
            return NoContent();
        }

        /// <summary>
        /// Deleta contato
        /// </summary>
        /// <param name="id">integer</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            _repo.Delete(id);
            return NoContent();
        }
    }
}
