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
    public class InstituicaoController : ControllerBase
    {
        private readonly BackContext _context;

        public InstituicaoController(BackContext context)
        {
            _context = context;
        }

        // GET: api/Instituicao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instituicao>>> GetInstituicao()
        {
            return await _context.Instituicao.ToListAsync();
        }

        // GET: api/Instituicao/5
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
        [HttpGet("getObras/{id}")]
        public async Task<ActionResult<IEnumerable<Obra>>> GetInstituicaoObras(int id)
        {
            return _context.Obras.FromSqlRaw("select * from Obras where cd_instituicao = {0}", id).ToList();
        }


        // PUT: api/Instituicao/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<Instituicao>> PutInstituicao(int id, Instituicao instituicao_p)
        {
            if (!InstituicaoExists(id))
            {
                return NotFound();
            }
            var instituicao = _context.Instituicao.FirstOrDefault(item => item.id == id);

            if (instituicao_p.nome != null) { instituicao.nome = instituicao_p.nome; };
            if (instituicao_p.entidade != null) { instituicao.entidade = instituicao_p.entidade; };
           

            try
            {
                _context.Instituicao.Update(instituicao);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return NotFound();
            }

            return instituicao;
        }

        // POST: api/Instituicao
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Instituicao>> PostInstituicao(Instituicao Instituicao)
        {
            _context.Instituicao.Add(Instituicao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstituicao", new { id = Instituicao.id }, Instituicao);
        }

        // DELETE: api/Instituicao/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Instituicao>> DeleteInstituicao(int id)
        {
            var Instituicao = await _context.Instituicao.FindAsync(id);
            if (Instituicao == null)
            {
                return NotFound();
            }

            _context.Instituicao.Remove(Instituicao);
            await _context.SaveChangesAsync();

            return Ok("Sucesso! \n"+Instituicao.nome+ ", instituição removida.");
        }

        private bool InstituicaoExists(int id)
        {
            return _context.Instituicao.Any(e => e.id == id);
        }
    }
}
