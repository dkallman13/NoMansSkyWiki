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

        /// <summary>
        /// Create a craftable using a name and assigned ingredient IDs
        /// </summary>
        /// <param name="craftable"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets a list of all craftables, requires no parameters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCraftables()
        {
            CraftableService cservice = CreateCraftableService();
            var craftables = cservice.GetCraftable();
            return Ok(craftables);
        }

        /// <summary>
        /// Retrieves resources related to the craftable, requires a craftable ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Craftable/GetResource/{id:int}")]
        public IHttpActionResult GetRelatedResource(int id)
        {
            CraftableService cservice = CreateCraftableService();
            var resources = cservice.GetRelatedResource(id);
            if (resources != null)
                return Ok(resources);
            return NotFound();
        }

        /// <summary>
        /// Lists a single craftable with an ID, its name, and required ingredients. Requires a craftable ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Change the name and ingredients of a craftable
        /// </summary>
        /// <param name="craftableEdit"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes a craftable from the database using its assigned ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
