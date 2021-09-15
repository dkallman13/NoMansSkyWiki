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
        [HttpGet]
        public IHttpActionResult Get()
        {
            ResourceService resService = CreateResourceService();
            var  resources= resService.GetResources();
            return Ok(resources);
        }
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            ResourceService resService = CreateResourceService();
            var resources = resService.GetResourceById(id);
            return Ok(resources);
        }
        [HttpGet]
        [Route("api/Resource/GetCraftable/{ResId:int}")]
        public IHttpActionResult GetCraftable(int ResId)
        {
            ResourceService resService = CreateResourceService();
            var craftables = resService.GetRelatedCraftables(ResId);
            return Ok(craftables);
        }
        [HttpGet]
        [Route("api/Resource/GetPlanetTypes/{ResId:int}")]
        public IHttpActionResult GetPlanetTypes(int ResId)
        {
            ResourceService resService = CreateResourceService();
            var craftables = resService.GetRelatedPlanetTypes(ResId);
            return Ok(craftables);
        }
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
