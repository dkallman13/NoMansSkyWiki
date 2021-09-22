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
    public class IngredientController : ApiController
    {

        private IngredientService CreateIngredientService()
        {
            var ingredientService = new IngredientService();
            return ingredientService;
        }
        //get all
        /// <summary>
        /// Gets a list of all ingredients and their related craftable IDs and resource IDs, requires no parameters
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            IngredientService ingredientService = CreateIngredientService();
            var ingredients = ingredientService.GetIngredients();
            return Ok(ingredients);
        }
        //post
        /// <summary>
        /// Create an ingredient using a craftable ID and a resource ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Retrieve a single ingredient via its ID and list the related craftable ID and resource ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            IngredientService ingredientService = CreateIngredientService();
            var ingredient = ingredientService.GetIngredientById(id);
            return Ok(ingredient);
        }

        //put
        /// <summary>
        /// Updates the related craftable ID and resource ID of single ingredient via its ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IHttpActionResult Put(IngredientEdit ingredient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateIngredientService();

            if (!service.UpdateIngredient(ingredient))
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Deletes an ingredient from the table via its ID
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateIngredientService();

            if (!service.DeleteIngredient(id))
                return InternalServerError();

            return Ok();
        }

    }
}
