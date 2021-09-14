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
                            CraftableId = e.CraftableId,
                            ResourceId = e.ResourceId
                        }
                        );
                return query.ToArray();
            }
        }
        //get by id
        public IngredientDetail GetIngredientById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Ingredients
                    .Single(e => e.Id == id);
                return
                    new IngredientDetail
                    {
                        CraftableId = entity.CraftableId,
                        ResourceId = entity.ResourceId
                    };
            }
        }
        //put
        public bool UpdateIngredient(IngredientEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ingredients.Single(e => e.Id == model.IngredientId);

                entity.CraftableId = model.CraftableId;
                entity.ResourceId = model.ResourceId;

                return ctx.SaveChanges() == 1;
            }
        }
        //delete
        public bool DeleteIngredient(int ingredientId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Ingredients.Single(e => e.Id == ingredientId);

                ctx.Ingredients.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
