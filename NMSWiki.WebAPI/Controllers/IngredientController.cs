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
    public class IngredientController : ApiController
    {
        [Authorize]
        public class PlanetResourceController : ApiController
        {
            private IngredientService CreateIngredientService()
            {
                var ingredientService = new IngredientService();
                return ingredientService;
            }
            //get all
            public IHttpActionResult Get()
            {
                IngredientService ingredientService = CreateIngredientService();
                var ingredients = ingredientService.GetIngredients();
                return Ok(ingredients);
            }
            //post
            public IHttpActionResult Post(IngredientCreate ingredient)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var service = CreateIngredientService();

                if (!service.CreateIngredient(ingredient))
                    return InternalServerError();

                return Ok();
            }

            //get by id
            public IHttpActionResult Get(int id)
            {
                IngredientService ingredientService = CreateIngredientService();
                var ingredient = ingredientService.GetIngredientById(id);
                return Ok(ingredient);
            }

            //put
            public IHttpActionResult Put(IngredientEdit ingredient)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var service = CreateIngredientService();

                if (!service.UpdateIngredient(ingredient))
                    return InternalServerError();

                return Ok();
            }

            public IHttpActionResult Delete(int id)
            {
                var service = CreateIngredientService();

                if (!service.DeleteIngredient(id))
                    return InternalServerError();

                return Ok();
            }
        }
    }
}
