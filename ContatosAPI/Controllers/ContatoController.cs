﻿using ContatosAPI.Models;
using ContatosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections;
using System.Collections.Generic;

namespace ContatosAPI.Controllers {

    // Controller síncrono para Contatos
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase {
        private readonly IContatosRepository _repo;

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="repo"></param>
        public ContatoController(IContatosRepository repo) {
            _repo = repo;
        }

        /// <summary>
        /// Retorna todos os contatos
        /// </summary>
        /// <returns>IEnumerable&lt;ContatoModel&gt;</returns>
        [HttpGet]
        public IEnumerable GetAll() {
            return _repo.GetSorted();
        }

        /// <summary>
        /// Retorna contato por id
        /// </summary>
        /// <param name="id">integer</param>
        /// <returns>ContatoModel</returns>
        [HttpGet("{id}")]
        public ActionResult<ContatoModel> GetById(int id) {
            var contato = _repo.GetById(id);

            if (contato == null) {
                return NotFound();
            }

            return new ObjectResult(contato);
        }

        /// <summary>
        /// Retorna contato por telefone
        /// </summary>
        /// <param name="fone">string</param>
        /// <returns>ContatoModel</returns>
        //  NÃO FUNCIONA
        //[HttpGet, Route("GetByPhone/{fone:string}")]
        //public IEnumerable<ContatoModel> GetByPhone(string fone) {
        //    return _repo.GetByPhone(fone);
        //}

        /// <summary>
        /// Retorna contatos com aniversário no mês
        /// </summary>
        /// <param name="mes">int</param>
        /// <returns>ContatoModel</returns>
        [HttpGet, Route("GetByMonth/{mes}")]
        public IEnumerable<ContatoModel> GetByMonth(int mes) {
            var contatos = _repo.AniversariantesDoMes(mes);
                return contatos;
        }

        /// <summary>
        /// Adiciona contato
        /// </summary>
        /// <param name="contato">ContatoModel - objeto contendo dados do contato</param>
        /// <returns>ContatoModel - contato criado</returns>
        [HttpPost]
        public ActionResult<ContatoModel> Add([FromBody]ContatoModel contato) {
            _repo.Create(contato);
            return CreatedAtAction(nameof(GetById), new { id = contato.Id }, contato);
        }

        /// <summary>
        /// Atualiza contato
        /// </summary>
        /// <param name="id">integer</param>
        /// <param name="contato">ContatoModel</param>
        [HttpPut("{id}")]
        public ActionResult<ContatoModel> Update(int id, [FromBody]ContatoModel contato) {
            if (id != contato.Id) {
                return BadRequest();
            }
            _repo.Update(id, contato);
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
