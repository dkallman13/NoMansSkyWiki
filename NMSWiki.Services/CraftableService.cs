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

        public IEnumerable<Resource> GetRelatedResource(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                string[] IngredientIds = GetCraftableById(id).IngredientId.Split(',');
                List<string> IngredientIdsFetched = new List<string>();
                for (int i = 0; i < IngredientIds.Length; i++)
                {
                    int IngredientIdInt = int.Parse(IngredientIds[i]);
                    IngredientService iserve = new IngredientService();
                    Ingredient ingredient = new Ingredient();
                    ingredient.PlanetResourceId = iserve.GetIngredientById(IngredientIdInt).IngredientId;
                    ingredient.ResourceId = iserve.GetIngredientById(IngredientIdInt).ResourceId;
                    ingredient.CraftableId = iserve.GetIngredientById(IngredientIdInt).CraftableId;
                    var query2 = ctx.Ingredients
                        .Join(ctx.Craftables, x => ingredient.PlanetResourceId, e => IngredientIdInt, (x, e) => new CraftableIngredientLookup { craftable = e, ingredient = x }).Where(xe => xe.craftable.IngredientId == xe.ingredient.PlanetResourceId.ToString());
                    IngredientIdsFetched.Add($"{query2.First().ingredient.PlanetResourceId}");
                }
                List<Resource> resource = new List<Resource>();
                foreach (string resourceId in IngredientIdsFetched)
                {
                    var ids3 = ctx.Resources.Select(y => y.IngredientId);
                    foreach (string idset in ids3)
                    {
                        string[] idarray = idset.Split(',');
                        List<IngredientResourceLookup> idsarray = new List<IngredientResourceLookup>();
                        foreach (string individualId in idarray)
                        {
                            if (resourceId == individualId)
                            {
                                var ids = ctx.Resources
                                .Join(ctx.Ingredients, x => individualId, e => e.PlanetResourceId.ToString(), (x, e) => new IngredientResourceLookup { resource = x, ingredient = e }).ToList();
                                var ids2 = ids
                                    .Where(xe => xe.resource.IngredientId
                                    .Select(y => xe.resource.IngredientId.Split(','))
                                    .Any(y => y.Contains(resourceId)))
                                    .ToArray();
                                idsarray.Add(ids2.First());
                            }
                        }
                        foreach (IngredientResourceLookup lookup in idsarray)
                        {
                            resource.Add(new Resource() { ResourceId = lookup.resource.ResourceId, IngredientId = lookup.resource.IngredientId, Name = lookup.resource.Name, PlanetResourceId = lookup.resource.PlanetResourceId, Description = lookup.resource.Description });
                        }
                    }
                }
                return resource;
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
