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
    
    public class ResourceController : ApiController
    {
        private ResourceService CreateResourceService()
        {
            var resourceService = new ResourceService();
            return resourceService;
        }

        /// <summary>
        /// gets all the resources
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get()
        {
            ResourceService resService = CreateResourceService();
            var  resources= resService.GetResources();
            return Ok(resources);
        }
        /// <summary>
        /// gets a single resource when you input the id
        /// </summary>
        /// <param name="id">
        /// this is the ID 
        /// </param>
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            ResourceService resService = CreateResourceService();
            var resources = resService.GetResourceById(id);
            return Ok(resources);
        }
        /// <summary>
        /// gets all the craftables that can be made with the resource, specified by the id
        /// </summary>
        /// <param name="ResId">the id of the resource that you are getting all the linked recipies</param>
        [HttpGet]
        [Route("api/Resource/GetCraftable/{ResId:int}")]
        public IHttpActionResult GetCraftable(int ResId)
        {
            ResourceService resService = CreateResourceService();
            var craftables = resService.GetRelatedCraftables(ResId);
            return Ok(craftables);
        }
        /// <summary>
        /// the same as getcraftable but for planettypes
        /// </summary>
        /// <param name="ResId">the id of the resource that you are getting all the linked planets where they are found</param>
        [HttpGet]
        [Route("api/Resource/GetPlanetTypes/{ResId:int}")]
        public IHttpActionResult GetPlanetTypes(int ResId)
        {
            ResourceService resService = CreateResourceService();
            var craftables = resService.GetRelatedPlanetTypes(ResId);
            return Ok(craftables);
        }
        /// <summary>
        /// adds a resource to the database
        /// </summary>
        /// <param name="resource">the object of what you are adding</param>
        [HttpPost]
        public IHttpActionResult Post(ResourceCreate resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateResourceService();
            if (!service.CreateResource(resource))
                return InternalServerError();

            return Ok();
        }
        /// <summary>
        /// updates a Resource
        /// </summary>
        /// <param name="resource">the object of what you are updating</param>
        [HttpPut]
        public IHttpActionResult Put(ResourceEdit resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateResourceService();
            if (!service.UpdateResource(resource))
                return InternalServerError();

            return Ok();
        }
    }
}
