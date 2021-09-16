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
        private CraftableService CreateCraftableService()
        {
            var craftableService = new CraftableService();
            return craftableService;
        }
        [HttpPost]
        public IHttpActionResult PostCraftable(CraftableAdd craftable)
        {
            if (ModelState.IsValid)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                CraftableService cservice = CreateCraftableService();
                if (!cservice.CreateCraftable(craftable))
                    return InternalServerError();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IHttpActionResult GetCraftables()
        {
            CraftableService cservice = CreateCraftableService();
            var craftables = cservice.GetCraftable();
            return Ok(craftables);
        }

        [HttpGet]
        public IHttpActionResult GetCraftableById(int id)
        {

            CraftableService cservice = CreateCraftableService();
            var craftable = cservice.GetCraftableById(id);
            if (craftable != null)
            {
                return Ok(craftable);
            }
            return NotFound();
        }

        [HttpPut]
        public IHttpActionResult UpdateCraftable(CraftableEdit craftableEdit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCraftableService();
            if (!service.UpdateCraftable(craftableEdit))
                return InternalServerError();


            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCraftableService();

            if (!service.DeleteCraftable(id))
                return InternalServerError();

            return Ok();
        }
    }
}
