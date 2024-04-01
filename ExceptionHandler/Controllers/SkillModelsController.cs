using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Entities;

namespace ExceptionHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillModelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SkillModelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SkillModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillModel>>> GetSkill()
        {
            return await _context.Skill.ToListAsync();
        }

        // GET: api/SkillModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillModel>> GetSkillModel(int id)
        {
            var skillModel = await _context.Skill.FindAsync(id);

            if (skillModel == null)
            {
                return NotFound();
            }

            return skillModel;
        }

        // PUT: api/SkillModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkillModel(int id, SkillModel skillModel)
        {
            if (id != skillModel.SkillId)
            {
                return BadRequest();
            }

            _context.Entry(skillModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillModelExists(id))
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

        // POST: api/SkillModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SkillModel>> PostSkillModel(SkillModel skillModel)
        {
            _context.Skill.Add(skillModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkillModel", new { id = skillModel.SkillId }, skillModel);
        }

        // DELETE: api/SkillModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkillModel(int id)
        {
            var skillModel = await _context.Skill.FindAsync(id);
            if (skillModel == null)
            {
                return NotFound();
            }

            _context.Skill.Remove(skillModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkillModelExists(int id)
        {
            return _context.Skill.Any(e => e.SkillId == id);
        }
    }
}
