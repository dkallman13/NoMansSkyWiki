using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Data
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(PlanetResource))]
        public List<int> PlanetResourceId { get; set; }
        public virtual List<PlanetResource> PlanetResources { get; set; }

        [ForeignKey(nameof(Ingredient))]
        public List<int> IngredientId { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
    }
}
