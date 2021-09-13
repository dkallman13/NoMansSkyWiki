using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Data
{
    public class Ingredients
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        [ForeignKey(nameof(Craftables))]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Resources))]
        public int ResourceId { get; set; }
    }
}
