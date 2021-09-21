using NMSWiki.Data;
using NMSWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Services
{
    public class ResourceService
    {
        public bool CreateResource(ResourceCreate model)
        {
            var entity =
                new Resource()
                {
                    Name = model.Name,
                    Description = model.Desc,
                    PlanetResourceId = model.PlanetResourceId,
                    IngredientId = model.IngredientId
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Resources.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ResourceDetail> GetResources()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Resources
                        .Select(
                            e =>
                                new ResourceDetail()
                                {
                                    Id = e.ResourceId,
                                    Name = e.Name,
                                    Desc = e.Description,
                                    PlanetResourceId = e.PlanetResourceId,
                                    IngredientId = e.IngredientId
                                }
                        );

                return query.ToArray();
            }
        }
        public IEnumerable<Craftable> GetRelatedCraftables(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                string[] IngredientIds = GetResourceById(id).IngredientId.Split(',');
                List<string> IngredientIdsFetched = new List<string>();
                for (int i = 0; i < IngredientIds.Length; i++)
                {
                    int IngredientIdInt = int.Parse(IngredientIds[i]);
                    IngredientService iserve = new IngredientService();
                    Ingredient ingredient = new Ingredient();
                    ingredient.IngredientId = iserve.GetIngredientById(IngredientIdInt).IngredientId;
                    ingredient.ResourceId = iserve.GetIngredientById(IngredientIdInt).ResourceId;
                    ingredient.CraftableId = iserve.GetIngredientById(IngredientIdInt).CraftableId;
                    var query2 = ctx.Ingredients
                        .Join(ctx.Resources, x => ingredient.IngredientId, e => IngredientIdInt, (x, e) => new IngredientResourceLookup { resource = e, ingredient = x }).Where(xe => xe.resource.IngredientId == xe.ingredient.IngredientId.ToString());
                    IngredientIdsFetched.Add($"{query2.First().ingredient.IngredientId}");
                }
                List<Craftable> craftable = new List<Craftable>();
                foreach (string resourceId in IngredientIdsFetched)
                {
                    var ids3 = ctx.Resources.Select(y => y.IngredientId);
                    foreach (string idset in ids3)
                    {
                        string[] idarray = idset.Split(',');
                        List<CraftableIngredientLookup> idsarray = new List<CraftableIngredientLookup>();
                        foreach (string individualId in idarray)
                        {
                            if (resourceId == individualId)
                            {
                                var ids = ctx.Craftables
                                .Join(ctx.Ingredients, x => individualId, e => e.IngredientId.ToString(), (x, e) => new CraftableIngredientLookup { craftable = x, ingredient = e }).ToList();
                                var ids2 = ids
                                    .Where(xe => xe.craftable.IngredientId
                                    .Select(y => xe.craftable.IngredientId.Split(','))
                                    .Any(y => y.Contains(resourceId)))
                                    .ToArray();
                                idsarray.Add(ids2.First());
                            }
                        }
                        foreach (CraftableIngredientLookup lookup in idsarray)
                        {
                            craftable.Add(new Craftable() { CraftableId = lookup.craftable.CraftableId, IngredientId = lookup.craftable.IngredientId, Name = lookup.craftable.Name });
                        }
                    }
                }
                return craftable;
            }
        }

        public IEnumerable<PlanetType> GetRelatedPlanetTypes(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                string[] PlanetResourceIds = GetResourceById(id).PlanetResourceId.Split(',');
                List<string> PlanetResourceIdsFetched = new List<string>();
                for (int i = 0; i < PlanetResourceIds.Length; i++)
                {
                    int PrIdInt = int.Parse(PlanetResourceIds[i]);
                    PlanetResourceService service = new PlanetResourceService();
                    PlanetResource pr = new PlanetResource();
                    pr.PlanetResourceId = service.GetPlanetResourceById(PrIdInt).PlanetResourceId;
                    pr.ResourceId = service.GetPlanetResourceById(PrIdInt).ResourceId;
                    pr.PlanetTypeId = service.GetPlanetResourceById(PrIdInt).PlanetTypeId;
                    var query2 = ctx.PlanetResources
                        .Join(ctx.Resources, x => pr.PlanetResourceId, e => PrIdInt, (x, e) => new PlanetResourceLookup { resource = e, planetResource = x }).Where(xe => xe.resource.PlanetResourceId == PlanetResourceIds[i]);
                    PlanetResourceIdsFetched.Add($"{query2.First().planetResource.PlanetResourceId}");
                }
                List<PlanetType> craftable = new List<PlanetType>();
                foreach (string resourceId in PlanetResourceIdsFetched)
                {
                    var ids3 = ctx.PlanetTypes.Select(y => y.PlanetResourceId);
                    foreach (string idset in ids3)
                    {
                        string[] idarray = idset.Split(',');
                        List<PlanetTypesPlanetResourceLookup> idsarray = new List<PlanetTypesPlanetResourceLookup>();
                        foreach (string individualId in idarray)
                        {
                            if (resourceId == individualId)
                            {
                                var ids = ctx.PlanetTypes
                                .Join(ctx.PlanetResources, x => individualId, e => e.PlanetResourceId.ToString(), (x, e) => new PlanetTypesPlanetResourceLookup {  planetResource = e, planetType = x }).ToList();
                                var ids2 = ids
                                    .Where(xe => xe.planetResource.PlanetResourceId.ToString()
                                    .Select(y => xe.planetType.PlanetResourceId.Split(','))
                                    .Any(y => y.Contains(resourceId)))
                                    .ToArray();
                                idsarray.Add(ids2.First());
                            }
                        }
                        foreach (PlanetTypesPlanetResourceLookup lookup in idsarray)
                        {
                            craftable.Add(new PlanetType() { PlanetResourceId = lookup.planetType.PlanetResourceId, PlanetTypeId = lookup.planetType.PlanetTypeId, Name = lookup.planetType.Name });
                        }
                    }
                }
                return craftable;
            }
        }
        public ResourceDetail GetResourceById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Resources
                        .Single(e => e.ResourceId == id);
                return
                    new ResourceDetail()
                    {
                        Id = entity.ResourceId,
                        Name = entity.Name,
                        Desc = entity.Description,
                        IngredientId = entity.IngredientId,
                        PlanetResourceId = entity.PlanetResourceId
                    };
            }
        }
        public bool UpdateResource(ResourceEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Resources
                        .Single(e => e.ResourceId == model.ResourceId);

                entity.Name = model.Name;
                entity.Description = model.Desc;
                entity.PlanetResourceId = model.PlanetResourceId;
                entity.IngredientId = model.IngredientId;

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
