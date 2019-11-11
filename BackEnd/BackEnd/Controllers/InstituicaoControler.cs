using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

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
        public async Task<ActionResult<Instituicao>> GetInstituicao(long id)
        {
            var Instituicao = await _context.Instituicao.FindAsync(id);

            if (Instituicao == null)
            {
                return NotFound();
            }

            return Instituicao;
        }

        // PUT: api/Instituicao/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstituicao(string id, Instituicao Instituicao)
        {
            if (id != Instituicao.id)
            {
                return BadRequest();
            }

            _context.Entry(Instituicao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstituicaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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
        public async Task<ActionResult<Instituicao>> DeleteInstituicao(String id)
        {
            var Instituicao = await _context.Instituicao.FindAsync(id);
            if (Instituicao == null)
            {
                return NotFound();
            }

            _context.Instituicao.Remove(Instituicao);
            await _context.SaveChangesAsync();

            return Instituicao;
        }

        private bool InstituicaoExists(string id)
        {
            return _context.Instituicao.Any(e => e.id == id);
        }
    }
}
