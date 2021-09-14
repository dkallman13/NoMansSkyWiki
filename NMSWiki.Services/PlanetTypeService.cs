﻿using NMSWiki.Data;
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
        private readonly int _userId;
        public PlanetTypeService(int userId)
        {
            _userId = userId;
        }
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
                    PlanetTypes.Where(e => e.PlanetTypeId == _userId)
                    .Select(
                        e => new PlanetTypeList
                        {
                            PlanetId = e.PlanetId,
                            Name = e.Name
                        }
                        );
                return query.ToArray();
            }
        }

    }
}
