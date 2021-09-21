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
                    ingredient.PlanetResourceId = iserve.GetIngredientById(IngredientIdInt).IngredientId;
                    ingredient.ResourceId = iserve.GetIngredientById(IngredientIdInt).ResourceId;
                    ingredient.CraftableId = iserve.GetIngredientById(IngredientIdInt).CraftableId;
                    var query2 = ctx.Ingredients
                        .Join(ctx.Resources, x => ingredient.PlanetResourceId, e =>IngredientIdInt, (x, e) => new IngredientResourceLookup { resource = e, ingredient = x }).Where(xe => xe.resource.IngredientId ==xe.ingredient.PlanetResourceId.ToString());
                    IngredientIdsFetched.Add($"{query2.First().ingredient.PlanetResourceId}");
                }
                List<Craftable> craftables = new List<Craftable>();
                foreach (string craftableid in IngredientIdsFetched)
                {
                    int craftableIdInt = int.Parse(craftableid);
                    var query =
                    ctx
                        .Craftables
                        .Join(ctx.Ingredients, x =>x.IngredientId, e => craftableid, (x, e) => new CraftableIngredientLookup { craftable = x, ingredient = e }).Where(xe => xe.craftable.IngredientId == xe.ingredient.PlanetResourceId.ToString());
                    craftables.Add(new Craftable() { CraftableId =  query.First().craftable.CraftableId, IngredientId = query.First().craftable.IngredientId, Name = query.First().craftable.Name });
                }

                return craftables;
            }
        }

        public IEnumerable<PlanetType> GetRelatedPlanetTypes(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                string[] planetResourceIds = GetResourceById(id).PlanetResourceId.Split(',');
                List<string> PlanetResourceIdsFetched = new List<string>();
                for (int i = 0; i < planetResourceIds.Length; i++)
                {
                    PlanetResourceService source = new PlanetResourceService();
                    int PlanetResourceIdInt = int.Parse(planetResourceIds[i]);
                    PlanetResource resource = new PlanetResource();
                    resource.PlanetResourceId = source.GetPlanetResourceById(PlanetResourceIdInt).PlanetResourceId;
                    resource.ResourceId = source.GetPlanetResourceById(PlanetResourceIdInt).ResourceId;
                    resource.PlanetTypeId = source.GetPlanetResourceById(PlanetResourceIdInt).PlanetTypeId;
                    var query2 = ctx.PlanetResources
                        .Join(ctx.Resources, x => resource.PlanetResourceId, e => PlanetResourceIdInt, (x, e) => new PlanetResourceLookup { resource = e, planetResource = x }).Where(xe => xe.resource.PlanetResourceId == xe.planetResource.PlanetResourceId.ToString());
                    PlanetResourceIdsFetched.Add($"{query2.First().planetResource.PlanetResourceId}");
                }
                List<PlanetType> planetTypes = new List<PlanetType>();
                foreach (string PlanetId in PlanetResourceIdsFetched)
                {
                    int craftableIdInt = int.Parse(PlanetId);
                    var query =
                    ctx
                        .PlanetTypes
                        .Join(ctx.PlanetResources, x => x.PlanetResourceId, e => PlanetId, (x, e) => new PlanetTypesPlanetResourceLookup { planetType = x, planetResource = e })
                        .Where(xe => xe.planetType.PlanetResourceId == xe.planetResource.PlanetResourceId.ToString());
                    planetTypes.Add(new PlanetType() { PlanetTypeId = query.First().planetType.PlanetTypeId, PlanetResourceId = query.First().planetType.PlanetResourceId, Name = query.First().planetType.Name });
                }
                return planetTypes;
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
