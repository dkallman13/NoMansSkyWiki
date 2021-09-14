﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Models
{
    public class ResourceDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public List<int> PlanetResourceId { get; set; }
        public List<int> IngredientId { get; set; }
    }
}
