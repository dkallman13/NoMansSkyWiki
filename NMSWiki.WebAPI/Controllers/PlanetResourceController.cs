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
        /// <summary>
        /// Gets all PlanetResources each of which contains an Id, PlanetTypeId and ResourceId.
        /// </summary>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            PlanetResourceService planetResourceService = CreatePlanetResourceService();
            var planetTypes = planetResourceService.GetPlanetResources();
            return Ok(planetTypes);
        }
        //post
        /// <summary>
        /// Posts a PlanetResource by Id, and it includes PlanetResourceID, PlanetTypeId and ResourceId.
        /// </summary>
        ///         /// Sample request:
        ///
        ///     POST /PlanetResource/post
        ///     {
        ///        "PlanetResourceId":1,
        ///        "PlanetTypeId":1,
        ///        "ResourceId":1
        ///     }
        /// </remarks>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
        [HttpPost]
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
        /// <summary>
        /// Gets a PlanetResource which contains an Id, PlanetTypeId and ResourceId by id.
        /// </summary>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            PlanetResourceService planetResourceService = CreatePlanetResourceService();
            var planetResource = planetResourceService.GetPlanetResourceById(id);
            return Ok(planetResource);
        }

        //put
        /// <summary>
        /// Updates body parameters of PlanetResource by ID.
        /// </summary>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put(PlanetResourceEdit planetResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlanetResourceService();

            if (!service.UpdatePlanetResource(planetResource))
                return InternalServerError();

            return Ok();
        }
        /// <summary>
        /// Deletes PlanetResource by Id.
        /// </summary>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreatePlanetResourceService();

            if (!service.DeletePlanetResource(id))
                return InternalServerError();

            return Ok();
        }
    }
}
