using NMSWiki.Data;
using NMSWiki.Models;
using NMSWiki.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NMSWiki.WebAPI.Controllers
{
    public class CraftableController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostCraftable(Craftable craftable)
        {
            if (ModelState.IsValid)
            {
                _context.Craftables.Add(craftable);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCraftables()
        {
            List<CraftableListItem> craftables = await _context.Craftables.Select(c => new CraftableListItem
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();

            return Ok(craftables);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCraftableById(int id)
        {
            Craftable craftable = await _context.Craftables.FindAsync(id);
            if (craftable != null)
            {
                var craftableDetail = new CraftableDetail()
                {
                    Id = craftable.Id,
                    Name = craftable.Name,
                    Ingredients = craftable.Ingredients
                };
                return Ok(craftableDetail);
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateCraftable([FromUri] int id, [FromBody] CraftableEdit craftableEdit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Craftable craftable = await _context.Craftables.FindAsync(id);

            if (craftable == null)
            {
                return NotFound();
            }

            craftable.Name = craftableEdit.Name;
            craftable.Ingredients = craftableEdit.Ingredients;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCraftable([FromUri] int id)
        {
            Craftable craftable = await _context.Craftables.FindAsync(id);
            
            if (craftable == null)
            {
                return NotFound();
            }

            _context.Craftables.Remove(craftable);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
