using NMSWiki.Data;
using NMSWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Services
{
    public class CraftableService
    {
        public bool CreateCraftable(CraftableAdd model)
        {
            var entity =
                new Craftable()
                {
                    Name = model.Name,
                    IngredientId = model.IngredientId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Craftables.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CraftableListItem> GetCraftable()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Craftables
                        .Select(e => new CraftableListItem
                        {
                            Id = e.CraftableId,
                            Name = e.Name,
                            IngredientId = e.IngredientId
                        }
                    );
                return query.ToArray();
            }
        }

        public CraftableDetail GetCraftableById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Craftables
                        .Single(e => e.CraftableId == id);
                    return
                        new CraftableDetail()
                        {
                            Id = entity.CraftableId,
                            Name = entity.Name,
                            IngredientId = entity.IngredientId
                        };
            }
        }

        public bool UpdateCraftable(CraftableEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Craftables
                        .Single(e => e.CraftableId == model.Id);

                entity.Name = model.Name;
                entity.Ingredients = model.Ingredients;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCraftable(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Craftables
                        .Single(e => e.CraftableId == id);

                ctx.Craftables.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
