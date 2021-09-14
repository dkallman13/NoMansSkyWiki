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
    public class PlanetResourceController : ApiController
    {
        private PlanetResourceService CreatePlanetResourceService()
        {
            var planetResourceService = new PlanetResourceService();
            return planetResourceService;
        }
        //get all
        public IHttpActionResult Get()
        {
            PlanetResourceService planetResourceService = CreatePlanetResourceService();
            var planetTypes = planetResourceService.GetPlanetResources();
            return Ok(planetTypes);
        }
        //post
        public IHttpActionResult Post(PlanetResourceCreate planetResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreatePlanetResourceService();

            if (!service.CreatePlanetResource(planetResource))
                return InternalServerError();

            return Ok();
        }

        //get by id
        public IHttpActionResult Get(int id)
        {
            PlanetResourceService planetResourceService = CreatePlanetResourceService();
            var planetResource = planetResourceService.GetPlanetResourceById(id);
            return Ok(planetResource);
        }

        //put
        public IHttpActionResult Put(PlanetResourceEdit planetResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlanetResourceService();

            if (!service.UpdatePlanetResource(planetResource))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreatePlanetResourceService();

            if (!service.DeletePlanetResource(id))
                return InternalServerError();

            return Ok();
        }
    }
}
