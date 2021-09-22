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
