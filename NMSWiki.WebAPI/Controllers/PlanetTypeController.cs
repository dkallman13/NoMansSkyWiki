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
        /// <summary>
        /// Gets all planet types with their Ids, names and Ids of resources found in each planet.
        /// </summary>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            PlanetTypeService planetTypeService = CreatePlanetTypeService();
            var planetTypes = planetTypeService.GetPlanetTypes();
            return Ok(planetTypes);
        }
        /// <summary>
        /// Gets Resources related to a particular planet.
        /// </summary>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
        [HttpGet]
        [Route("api/PlanetType/GetResource/{ResId:int}")]
        public IHttpActionResult GetCraftable(int ResId)
        {
            PlanetTypeService ptService = CreatePlanetTypeService();
            var resources = ptService.GetRelatedResource(ResId);
            return Ok(resources);
        }
        //post
        /// <summary>
        /// Posts a planet type and its parameters contain a planettyepId,name and ResourceId.
        /// </summary>
        ///     /// <remarks>
        /// Sample request:
        ///
        ///     POST /PlanetType/post
        ///     {
        ///        "PlanetTypeId":1,
        ///        "Name":"Lush",
        ///        "ResourceId":1
        ///     }
        /// </remarks>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
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
        /// <summary>
        /// Gets a planetType and it contains planettyepId,name and ResourceId by Id.
        /// </summary>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            PlanetTypeService planetTypeService = CreatePlanetTypeService();
            var planetType = planetTypeService.GetPlanetTypeById(id);
            return Ok(planetType);
        }

        //put
        /// <summary>
        /// It updates a planet type.
        /// </summary>
        ///     /// <remarks>
        /// Sample request:
        ///
        ///     PUT /PlanetType/put
        ///     {
        ///        "PlanetTypeId":1,
        ///        "Name":"Lush",
        ///        "ResourceId":1
        ///     }
        /// </remarks>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
        public IHttpActionResult Put(PlanetTypeEdit planetType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlanetTypeService();

            if (!service.UpdatePlanetType(planetType))
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// It deletes a a planet type.
        /// </summary>
        /// <response code="200">Successful call</response>
        /// <response code="500">Internal error</response>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreatePlanetTypeService();

            if (!service.DeletePlanetType(id))
                return InternalServerError();

            return Ok();
        }
    }
}
