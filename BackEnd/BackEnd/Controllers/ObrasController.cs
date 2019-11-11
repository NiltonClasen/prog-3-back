﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Controllers
{
    [Route("RestAPIPesquisa/[controller]")]
    [ApiController]
    public class ObrasController : ControllerBase
    {
        private readonly BackContext _context;

        public ObrasController(BackContext context)
        {
            _context = context;
        }

        // GET: RestAPIPesquisa/obras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Obra>>> GetObras()
        {
            return await _context.Obras.ToListAsync();
        }

        // GET: RestAPIPesquisa/obras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Obra>> GetObra(long id_p)
        {
            int id = Convert.ToInt32(id_p);
            var obra = await _context.Obras.FindAsync(id);

            if (obra == null)
            {
                return NotFound();
            }

            return obra;
        }

        // PUT: RestAPIPesquisa/obras/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObra(string id_p, Obra obra)
        {
            int id = Convert.ToInt32(id_p);
            if (id != obra.id)
            {
                return BadRequest();
            }

            _context.Entry(obra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObraExists(id))
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

        // POST: RestAPIPesquisa/obras
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Obra>> PostObra(Obra obra)
        {
            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObra", new { id = obra.id }, obra);
        }

        // DELETE: RestAPIPesquisa/obras/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Obra>> DeleteObra(String id_p)
        {
            int id = Convert.ToInt32(id_p);
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }

            _context.Obras.Remove(obra);
            await _context.SaveChangesAsync();

            return obra;
        }

        private bool ObraExists(int id)
        {
            return _context.Obras.Any(e => e.id == id);
        }
    }
}
