using NMSWiki.Models;
using NMSWiki.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NMSWiki.WebAPI.Controllers
{
    [Authorize]
    public class PlanetTypeController : ApiController
    {
        private PlanetTypeService CreatePlanetTypeService()
        {
            var planetTypeService = new PlanetTypeService();
            return planetTypeService;
        }
        //get all
        public IHttpActionResult Get()
        {
            PlanetTypeService planetTypeService = CreatePlanetTypeService();
            var planetTypes = planetTypeService.GetPlanetTypes();
            return Ok(planetTypes);
        }
        [HttpGet]
        [Route("api/PlanetType/GetResource/{ResId:int}")]
        public IHttpActionResult GetCraftable(int ResId)
        {
            PlanetTypeService ptService = CreatePlanetTypeService();
            var resources = ptService.GetRelatedResource(ResId);
            return Ok(resources);
        }
        //post
        public IHttpActionResult Post(PlanetTypeCreate planetType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreatePlanetTypeService();

            if (!service.CreatePlanetType(planetType))
                return InternalServerError();

            return Ok();
        }

        //get by id
        public IHttpActionResult Get(int id)
        {
            PlanetTypeService planetTypeService = CreatePlanetTypeService();
            var planetType = planetTypeService.GetPlanetTypeById(id);
            return Ok(planetType);
        }

        //put
        public IHttpActionResult Put(PlanetTypeEdit planetType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlanetTypeService();

            if (!service.UpdatePlanetType(planetType))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreatePlanetTypeService();

            if (!service.DeletePlanetType(id))
                return InternalServerError();

            return Ok();
        }
    }
}
