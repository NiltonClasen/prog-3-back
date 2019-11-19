using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [Route("RestAPIPesquisa/instituicao")]
    [ApiController]
    [Authorize]
    public class InstituicaoController : ControllerBase
    {
        private readonly BackContext _context;

        public InstituicaoController(BackContext context)
        {
            _context = context;
        }

        // GET: api/Instituicao
        ///<summary>
        ///deleta a obra de acordo com o ip
        /// </summary>
        /// <returns>A obra foi deletada</returns>
        /// <response code="200">A obra foi deletada.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instituicao>>> GetInstituicao()
        {
            return await _context.Instituicao.ToListAsync();
        }

        // GET: api/Instituicao/5
        ///<summary>
        ///Lista a Instituicao de acordo com o id.
        /// </summary>
        /// <returns>A Instituicao</returns>
        /// <response code="200">Lista a Instituicao de acordo com o id.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Instituicao>> GetInstituicao(int id)
        {
            var Instituicao = await _context.Instituicao.FindAsync(id);

            if (Instituicao == null)
            {
                return NotFound();
            }

            return Instituicao;
        }

        // GET: api/Instituicao
        ///<summary>
        ///Lista as obras de acordo com o id da instituição passada.
        /// </summary>
        /// <returns>A(s) obras(s)</returns>
        /// <response code="200">Lista a(s) obra(s) de acordo com o id da instituição.</response>
        [HttpGet("getObras/{id}")]
        public async Task<ActionResult<IEnumerable<Obra>>> GetInstituicaoObras(int id)
        {
            return _context.Obras.FromSqlRaw("select * from Obras where cd_instituicao = {0}", id).ToList();
        }


        // PUT: api/Instituicao/5
        ///<summary>
        ///Atualiza o registro de obra de acordo com as informações passadas
        /// </summary>
        /// <returns>A Instituicao atualizada</returns>
        /// <response code="200">A Instituicao atualizada.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<Instituicao>> PutInstituicao(int id, Instituicao instituicao_p)
        {
            var aux = 0;
            if (!InstituicaoExists(id))
            {
                return NotFound();
            }
            var instituicao = _context.Instituicao.FirstOrDefault(item => item.id == id);

            if (instituicao_p.nome != null) { instituicao.nome = instituicao_p.nome; aux++; };
            if (instituicao_p.entidade != null) { instituicao.entidade = instituicao_p.entidade; aux++; };


            try
            {
                _context.Instituicao.Update(instituicao);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return NotFound();
            }
            if (aux > 0)
            {
                return NotFound("Parâmetros passados de forma incorreta.");
            }
            else
            {
                return instituicao;

            }
        }

        // POST: api/Instituicao
        ///<summary>
        ///Salva o registro no banco de dados
        /// </summary>
        /// <returns>A Instituicao foi salva</returns>
        /// <response code="200">A Instituicao foi salva.</response>
        [HttpPost]
        public async Task<ActionResult<Instituicao>> PostInstituicao(Instituicao Instituicao)
        {
            _context.Instituicao.Add(Instituicao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstituicao", new { id = Instituicao.id }, Instituicao);
        }

        // DELETE: api/Instituicao/5
        ///<summary>
        ///deleta a Instituição de acordo com o ip
        /// </summary>
        /// <returns>A Instituição foi deletada</returns>
        /// <response code="200">A Instituição foi deletada.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Instituicao>> DeleteInstituicao(int id)
        {
            var Instituicao = await _context.Instituicao.FindAsync(id);
            if (Instituicao == null)
            {
                return NotFound();
            }

            try
            {
                _context.Instituicao.Remove(Instituicao);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return NotFound("Não é possível excluir uma instituição que possui obras associadas");
            }


            return Ok("Sucesso! \n" + Instituicao.nome + ", instituição removida.");
        }

        private bool InstituicaoExists(int id)
        {
            return _context.Instituicao.Any(e => e.id == id);
        }
    }
}
