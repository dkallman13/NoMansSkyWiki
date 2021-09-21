using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Models
{
    public class ResourceCreate
    {
        [Required]
        public string Name { get; set; }
        public string Desc { get; set; }
        public string PlanetResourceId { get; set; }
        public string IngredientId { get; set; }
    }
}
