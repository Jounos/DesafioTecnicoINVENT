using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioInventBackend.Model;
using DesafioInventBackend.Model.Context;

namespace DesafioInventBackend.Controller
{
    [Route("api/equipamento-eletronico")]
    [ApiController]
    public class EquipamentoEletronicoController : ControllerBase
    {
        private readonly EquipamentoEletronicoContext _context;

        public EquipamentoEletronicoController(EquipamentoEletronicoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipamentoEletronico>>> GetequipamentoEletronicos()
        {
            return await _context.equipamentoEletronicos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipamentoEletronico>> GetEquipamentoEletronico(int id)
        {
            var equipamentoEletronico = await _context.equipamentoEletronicos.FindAsync(id);

            if (equipamentoEletronico == null)
            {
                return NotFound();
            }

            return equipamentoEletronico;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipamentoEletronico(int id, EquipamentoEletronico equipamentoEletronico)
        {
            if (id != equipamentoEletronico.Id)
            {
                return BadRequest();
            }

            _context.Entry(equipamentoEletronico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipamentoEletronicoExists(id))
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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipamentoEletronico>> PostEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {

            equipamentoEletronico.TemEstoque = equipamentoEletronico.QuantidadeEstoque > 0;

            _context.equipamentoEletronicos.Add(equipamentoEletronico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipamentoEletronico", new { id = equipamentoEletronico.Id }, equipamentoEletronico);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipamentoEletronico(int id)
        {
            var equipamentoEletronico = await _context.equipamentoEletronicos.FindAsync(id);
            if (equipamentoEletronico == null)
            {
                return NotFound();
            }

            _context.equipamentoEletronicos.Remove(equipamentoEletronico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipamentoEletronicoExists(int id)
        {
            return _context.equipamentoEletronicos.Any(e => e.Id == id);
        }
    }
}
