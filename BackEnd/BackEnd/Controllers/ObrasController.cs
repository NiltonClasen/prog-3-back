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
    [Route("RestAPIPesquisa/[controller]")]
    [ApiController]
    [Authorize]
    public class ObrasController : ControllerBase
    {
        private readonly BackContext _context;

        public ObrasController(BackContext context)
        {
            _context = context;
        }

        /// GET: RestAPIPesquisa/obras
        ///<summary>
        ///Lista todas as obras.
        /// </summary>
        /// <returns>As obras</returns>
        /// <response code="200">Lista todas as obras cadastradas</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Obra>>> GetObras()
        {
            return await _context.Obras.ToListAsync();
        }

        // GET: RestAPIPesquisa/obras/%id%
        ///<summary>
        ///Lista a obra de acordo com o id.
        /// </summary>
        /// <returns>A obra</returns>
        /// <response code="200">Lista a obra de acordo com o id.</response>
        [HttpGet("/RestAPIPesquisa/obras/{id}")]

        public async Task<ActionResult<Obra>> GetObra(int id)
        {
            var obra = await _context.Obras.FindAsync(id);

            if (obra == null)
            {
                return NotFound();
            }

            return obra;
        }

        // PUT: RestAPIPesquisa/obras/5
        ///<summary>
        ///Atualiza o registro de obra de acordo com as informações passadas
        /// </summary>
        /// <returns>A obra atualizada</returns>
        /// <response code="200">A obra atualizada.</response>
        [HttpPut("{id}")]

        public async Task<ActionResult<Obra>> PutObra(int id, Obra obra_p)
        {
            var aux = 0;
            if (!ObraExists(id))
            {
                return NotFound();
            }
            var obra = _context.Obras.FirstOrDefault(item => item.id == id);

            if (obra_p.autor != null) { obra.autor = obra_p.autor;  aux++; };
            if (obra_p.titulo != null) { obra.titulo = obra_p.titulo; aux++; };
            if (obra_p.ano != null) { obra.ano = obra_p.ano; aux++; };
            if (obra_p.edicao != null) { obra.edicao = obra_p.edicao; aux++; };
            if (obra_p.local != null) { obra.local = obra_p.local; aux++; };
            if (obra_p.editora != null) { obra.editora = obra_p.editora; aux++; };
            if (obra_p.paginas != null) { obra.paginas = obra_p.paginas; aux++; };
            if (obra_p.isbn != null) { obra.isbn = obra_p.isbn; aux++; };
            if (obra_p.issn != null) { obra.issn = obra_p.issn; aux++; };
            if (obra_p.cd_instituicao != 0) { obra.cd_instituicao = obra_p.cd_instituicao; aux++; };

            try
            {
                _context.Obras.Update(obra);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return NotFound();
            }
            if(aux > 0)
            {
                return NotFound("Parâmetros passados de forma incorreta.");
            }
            else
            {
            return obra;

            }

        }

        // POST: RestAPIPesquisa/obras
        ///<summary>
        ///Salva o registro no banco de dados
        /// </summary>
        /// <returns>A obra foi salva</returns>
        /// <response code="200">A obra foi salva.</response>
        [HttpPost]

        public async Task<ActionResult<Obra>> PostObra(Obra obra)
        {
            if (obra.cd_instituicao == 0) { obra.cd_instituicao = 1; };
            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObra", new { id = obra.id }, obra);
        }

        // DELETE: RestAPIPesquisa/obras/5
        ///<summary>
        ///deleta a obra de acordo com o ip
        /// </summary>
        /// <returns>A obra foi deletada</returns>
        /// <response code="200">A obra foi deletada.</response>
        [HttpDelete("{id}")]

        public async Task<ActionResult<Obra>> DeleteObra(int id)
        {
            var obra = _context.Obras.Find(id);
            if (obra == null)
            {
                return NotFound("Nenhuma obra com o id " + id + " foi encontrada");
            }

            _context.Obras.Remove(obra);
            _context.SaveChangesAsync();

            return Ok("Sucess \nObra removida " + obra.titulo);
        }

        // DELETE: RestAPIPesquisa/obras/5
        ///<summary>
        ///deleta a obra de acordo com o argumento passado como parâmetro
        /// </summary>
        /// <returns>A(s) obra(s) foi/foram deletada(s)</returns>
        /// <response code="200">A(s) obra(s) foi/foram deletada(s)</response>
        [HttpDelete("/RestAPIPesquisa/obras")]

        public async Task<ActionResult<Obra>> DeleteObra([FromBody] Obra obra_p)
        {

            if (!_context.Obras.Any(e => e.titulo == obra_p.titulo))
            {
                return NotFound("nenhuma obra com o nome " + obra_p.titulo + " foi encontrada");
            }
            var aux = _context.Obras.Where(teste => teste.titulo == obra_p.titulo);

            foreach (var obra_del in aux)
            {
                _context.Obras.Remove(obra_del);
            }

            _context.SaveChanges();

            return Ok("obra(s) com o titulo " + obra_p.titulo + " removida(s)");
        }

        private bool ObraExists(int id)
        {
            return _context.Obras.Any(e => e.id == id);
        }
    }
}
