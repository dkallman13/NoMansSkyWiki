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
            var planetTypeService = new PlanetTypeService(userId);
            return planetTypeService;
        }
        //get all
        public IHttpActionResult Get()
        {
            PlanetTypeService planetTypeService = CreatePlanetTypeService();
            var planetTypes = planetTypeService.GetPlanetTypes();
            return Ok(planetTypes);
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
    }
}
