using NMSWiki.Data;
using NMSWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Services
{
    public class IngredientService
    {
        //post
        /// <summary>
        /// Create an ingredient using a craftable ID and a resource ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateIngredient(IngredientCreate model)
        {
            var entity = new Ingredient()
            {
                CraftableId = model.CraftableId,
                ResourceId=model.ResourceId
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Ingredients.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //get
        /// <summary>
        /// Gets a list of all ingredients and their related craftable IDs and resource IDs, requires no parameters
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IngredientList> GetIngredients()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.
                    Ingredients
                    .Select(
                        e => new IngredientList
                        {
                            IngredientId = e.IngredientId,
                            CraftableId = e.CraftableId,
                            ResourceId = e.ResourceId
                        }
                        );
                return query.ToArray();
            }
        }
        //get by id
        /// <summary>
        /// Retrieve a single ingredient via its ID and list the related craftable ID and resource ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IngredientDetail GetIngredientById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Ingredients
                    .Single(e => e.IngredientId == id);
                return
                    new IngredientDetail
                    {
                        IngredientId = entity.IngredientId,
                        CraftableId = entity.CraftableId,
                        ResourceId = entity.ResourceId
                    };
            }
        }
        //put
        /// <summary>
        /// Updates the related craftable ID and resource ID of single ingredient via its ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateIngredient(IngredientEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ingredients.Single(e => e.IngredientId == model.IngredientId);

                entity.CraftableId = model.CraftableId;
                entity.ResourceId = model.ResourceId;

                return ctx.SaveChanges() == 1;
            }
        }
        //delete
        /// <summary>
        /// Deletes an ingredient from the table via its ID
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        public bool DeleteIngredient(int ingredientId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ingredients.Single(e => e.IngredientId == ingredientId);

                ctx.Ingredients.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
