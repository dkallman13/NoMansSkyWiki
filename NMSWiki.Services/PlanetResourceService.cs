using NMSWiki.Data;
using NMSWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Services
{
    public class PlanetResourceService
    {
        //post
        public bool CreatePlanetResource(PlanetResourceCreate model)
        {
            var entity =
                new PlanetResource()
                {
                    PlanetTypeId = model.PlanetTypeId,
                    ResourceId = model.ResourceId
                };
            using(var ctx=new ApplicationDbContext())
            {
                ctx.PlanetResources.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //get all
        public IEnumerable<PlanetResourceList> GetPlanetResources()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query = ctx.PlanetResources.Select(e => new PlanetResourceList
                {
                    PlanetResourceId = e.PlanetResourceId,
                    PlanetTypeId = e.PlanetTypeId,
                    ResourceId = e.ResourceId
                }
                );
                return query.ToArray();
            }
        }
        //get by id
        public PlanetResourceDetail GetPlanetResourceById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .PlanetResources
                    .Single(e => e.PlanetResourceId == id);
                return
                    new PlanetResourceDetail
                    {
                        PlanetResourceId=entity.PlanetResourceId,
                        PlanetTypeId = entity.PlanetTypeId,
                        ResourceId = entity.ResourceId
                    };
            }
        }
        //put
        public bool UpdatePlanetResource(PlanetResourceEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.PlanetResources.Single(e => e.PlanetResourceId == model.PlanetResourceId);

                entity.PlanetTypeId = model.PlanetTypeId;
                entity.ResourceId = model.ResourceId;

                return ctx.SaveChanges() == 1;
            }
        }
        //delete
        public bool DeletePlanetResource(int planetResourceId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.PlanetResources.Single(e => e.PlanetResourceId == planetResourceId);

                ctx.PlanetResources.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
