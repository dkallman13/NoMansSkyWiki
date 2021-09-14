using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Data
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        [ForeignKey(nameof(Craftable))]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Resource))]
        public int ResourceId { get; set; }
    }
}
