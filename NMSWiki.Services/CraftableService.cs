﻿using NMSWiki.Data;
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
                    Ingredients = model.Ingredients
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
                            Id = e.Id,
                            Name = e.Name,
                            Ingredients = e.Ingredients
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
                        .Single(e => e.Id == id);
                    return
                        new CraftableDetail()
                        {
                            Id = entity.Id,
                            Name = entity.Name,
                            Ingredients = entity.Ingredients
                        };
            }
        }

        public bool UpdateCraftable(CraftableEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Craftables
                        .Single(e => e.Id == model.Id);

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
                        .Single(e => e.Id == id);

                ctx.Craftables.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}