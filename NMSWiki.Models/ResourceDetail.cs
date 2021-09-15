using System;
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
        public string PlanetResourceId { get; set; }
        public string IngredientId { get; set; }
    }
}
