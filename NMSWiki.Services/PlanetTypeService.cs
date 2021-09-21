using NMSWiki.Data;
using NMSWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Services
{
    public class PlanetTypeService
    {

        //post
        public bool CreatePlanetType(PlanetTypeCreate model)
        {
            var entity = new PlanetType()
            {
                Name = model.Name,
                PlanetResourceId = model.PlanetResourceId
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.PlanetTypes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //get
        public IEnumerable<PlanetTypeList> GetPlanetTypes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.
                    PlanetTypes
                    .Select(
                        e => new PlanetTypeList
                        {
                            PlanetTypeId = e.PlanetTypeId,
                            Name = e.Name,
                            PlanetResourceId = e.PlanetResourceId
                        }
                        );
                return query.ToArray();
            }
        }
        //get by id
        public PlanetTypeDetail GetPlanetTypeById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .PlanetTypes
                    .Single(e => e.PlanetTypeId == id);
                return
                    new PlanetTypeDetail
                    {
                        PlanetTypeId = entity.PlanetTypeId,
                        Name = entity.Name,
                        PlanetResourceId = entity.PlanetResourceId
                    };
            }
        }
        public IEnumerable<Resource> GetRelatedResource(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                string[] PlanetResourceIds = GetPlanetTypeById(id).PlanetResourceId.Split(',');
                List<string> planetResourceIdsFetched = new List<string>();
                for (int i = 0; i < PlanetResourceIds.Length; i++)
                {
                    int PlanetResourceIdInt = int.Parse(PlanetResourceIds[i]);
                    PlanetResourceService Service = new PlanetResourceService();
                    PlanetResource Planetresource = new PlanetResource();
                    Planetresource.PlanetResourceId = Service.GetPlanetResourceById(PlanetResourceIdInt).PlanetResourceId;
                    Planetresource.ResourceId = Service.GetPlanetResourceById(PlanetResourceIdInt).ResourceId;
                    Planetresource.PlanetTypeId = Service.GetPlanetResourceById(PlanetResourceIdInt).PlanetTypeId;
                    var query2 = ctx.PlanetResources
                        .Join(ctx.PlanetTypes, x => Planetresource.PlanetResourceId, e => PlanetResourceIdInt, (x, e) => new PlanetTypesPlanetResourceLookup { planetType = e, planetResource = x }).Where(xe => xe.planetType.PlanetResourceId == xe.planetResource.PlanetResourceId.ToString());
                    planetResourceIdsFetched.Add($"{query2.First().planetResource.PlanetResourceId}");
                }
                List<Resource> resource = new List<Resource>();
                foreach (string resourceId in planetResourceIdsFetched)
                {
                    var ids3 = ctx.Resources.Select(y => y.PlanetResourceId);
                    foreach (string idset in ids3)
                    {
                        string[] idarray = idset.Split(',');
                        List<PlanetResourceLookup> idsarray = new List<PlanetResourceLookup>();
                        foreach (string individualId in idarray)
                        {
                            if (resourceId == individualId)
                            {
                                var ids = ctx.Resources
                                .Join(ctx.PlanetResources, x => individualId, e => e.PlanetResourceId.ToString(), (x, e) => new PlanetResourceLookup { resource = x, planetResource = e }).ToList();
                                var ids2 = ids
                                    .Where(xe => xe.resource.PlanetResourceId
                                    .Select(y => xe.resource.PlanetResourceId.Split(','))
                                    .Any(y => y.Contains(resourceId)))
                                    .ToArray();
                                idsarray.Add(ids2.First());
                            }
                        }
                        foreach (PlanetResourceLookup lookup in idsarray)
                        {
                            resource.Add(new Resource() { ResourceId = lookup.resource.ResourceId, IngredientId = lookup.resource.IngredientId, Name = lookup.resource.Name, PlanetResourceId = lookup.resource.PlanetResourceId, Description = lookup.resource.Description });
                        }
                    }
                }
                return resource;
            }
        }
        //put
        public bool UpdatePlanetType(PlanetTypeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.PlanetTypes.Single(e => e.PlanetTypeId == model.PlanetTypeId);

                entity.Name = model.Name;
                entity.PlanetResourceId = model.PlanetResourceId;

                return ctx.SaveChanges() == 1;
            }
        }
        //delete
        public bool DeletePlanetType(int planetTypeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.PlanetTypes.Single(e => e.PlanetTypeId == planetTypeId);

                ctx.PlanetTypes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
