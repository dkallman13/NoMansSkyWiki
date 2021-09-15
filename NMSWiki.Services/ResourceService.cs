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

                string[] PlanetIdsString = GetResourceById(id).PlanetResourceId.Split(',');

                var query =
                    ctx
                        .Craftables
                        .Where(e => e.IngredientId.Split(',') == PlanetIdsString)
                        .SelectMany(
                            e => ctx.Craftables
                        );

                return query.ToArray();
            }
        }

        public IEnumerable<PlanetType> GetRelatedPlanetTypes(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                string[] PlanetIdsString = GetResourceById(id).PlanetResourceId.Split(',');
                int[] PlanetIds = { };
                foreach (string planettypeid in PlanetIdsString)
                {
                    PlanetIds.Append(Int32.Parse(planettypeid));
                }
                var query =
                    ctx
                        .PlanetTypes
                        .Where(e => e.PlanetResourceId == PlanetIds)
                        .SelectMany(
                            e => ctx.PlanetTypes
                        );

                return query.ToArray();
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
                        IngredientId  = entity.IngredientId,
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
