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
                    PlanetResourceService source = new PlanetResourceService();
                    int IngredientIdInt = int.Parse(IngredientIds[i]);
                    int query2 = ctx.Ingredients
                    .Where(e => e.IngredientId == IngredientIdInt)
                    .First().CraftableId;
                    IngredientIdsFetched.Add($"{query2}");
                }
                List<Craftable> craftables = new List<Craftable>();
                foreach (string craftableid in IngredientIdsFetched)
                {
                    int craftableIdInt = int.Parse(craftableid);
                    var query =
                    ctx
                        .Ingredients
                        .Where(e => e.CraftableId == craftableIdInt)
                        .SelectMany(
                            e => ctx.Craftables
                        );
                    craftables.AddRange(query);
                }

                return craftables.ToArray();
            }
        }

        public IEnumerable<PlanetType> GetRelatedPlanetTypes(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                string[] PlanetIdsString = GetResourceById(id).PlanetResourceId.Split(',');
                List<string> planetidsfetched = new List<string>();
                for (int i = 0; i < PlanetIdsString.Length; i++)
                {
                    PlanetResourceService source = new PlanetResourceService();
                    int planetIdInt = int.Parse(PlanetIdsString[i]);
                    int query2 = ctx.PlanetResources
                    .Where(e => e.PlanetResourceId == planetIdInt)
                    .First().PlanetTypeId;
                    planetidsfetched.Add($"{query2}");
                }
                List<PlanetType> planetTypes = new List<PlanetType>();
                foreach (string planetid in planetidsfetched)
                {
                    int planetIdInt = int.Parse(planetid);
                    var query =
                    ctx
                        .PlanetResources
                        .Where(e => e.PlanetResourceId == planetIdInt)
                        .SelectMany(
                            e => ctx.PlanetTypes
                        );
                    planetTypes.AddRange(query);
                }

                return planetTypes.ToArray();
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
