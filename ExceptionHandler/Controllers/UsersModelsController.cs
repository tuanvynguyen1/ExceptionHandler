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
    public class UsersModelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersModelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UsersModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersModel>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UsersModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersModel>> GetUsersModel(int id)
        {
            var usersModel = await _context.Users.FindAsync(id);

            if (usersModel == null)
            {
                return NotFound();
            }

            return usersModel;
        }

        // PUT: api/UsersModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersModel(int id, UsersModel usersModel)
        {
            if (id != usersModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(usersModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersModelExists(id))
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

        // POST: api/UsersModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsersModel>> PostUsersModel(UsersModel usersModel)
        {
            _context.Users.Add(usersModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsersModel", new { id = usersModel.Id }, usersModel);
        }

        // DELETE: api/UsersModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersModel(int id)
        {
            var usersModel = await _context.Users.FindAsync(id);
            if (usersModel == null)
            {
                return NotFound();
            }

            _context.Users.Remove(usersModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersModelExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
