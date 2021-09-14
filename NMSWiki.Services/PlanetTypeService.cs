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
                Name = model.Name
            };
            using (var ctx=new ApplicationDbContext())
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
                            Name = e.Name
                        }
                        );
                return query.ToArray();
            }
        }
        //get by id
        public PlanetTypeDetail GetPlanetTypeById(int id)
        {
            using (var ctx=new ApplicationDbContext())
            {
                var entity = ctx
                    .PlanetTypes
                    .Single(e => e.PlanetTypeId == id);
                return
                    new PlanetTypeDetail
                    {
                        PlanetTypeId = entity.PlanetTypeId,
                        Name = entity.Name
                    };
            }
        }
        //put
        public bool UpdatePlanetType(PlanetTypeEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx.PlanetTypes.Single(e => e.PlanetTypeId == model.PlanetTypeId);

                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }
        //delete
        public bool DeletePlanetType(int planetTypeId)
        {
            using (var ctx=new ApplicationDbContext())
            {
                var entity = ctx.PlanetTypes.Single(e => e.PlanetTypeId == planetTypeId);

                ctx.PlanetTypes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
