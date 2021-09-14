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
                    Description = model.Desc
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
                                    Id = e.Id,
                                    Name = e.Name,
                                    Desc = e.Description
                                }
                        );

                return query.ToArray();
            }
        }
        public IEnumerable<Craftable> GetRelatedCraftables(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                List<int> ingredientids = GetResourceById(id).IngredientId;
                var query =
                    ctx
                        .Craftables
                        .Where(e => e.IngredientIds == ingredientids)
                        .SelectMany(
                            e => ctx.Craftables
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
                        .Single(e => e.Id == id);
                return
                    new ResourceDetail()
                    {
                        Id = entity.Id,
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
                        .Single(e => e.Id == model.ResourceId);

                entity.Name = model.Name;
                entity.Description = model.Desc;

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
